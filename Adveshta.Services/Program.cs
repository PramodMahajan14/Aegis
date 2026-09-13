using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Adveshta.DataAccess.Data;
using Adveshta.Helpers.Prospect;
using Adveshta.Model.Auth;
using Adveshta.Services.Behaviors;
using Adveshta.Services.Helper;
using Adveshta.Services.Mapper;
using Adveshta.Services.Middleware;
using Adveshta.Services.Services;
using Adveshta.Services.Services.Interfaces;
using Adveshta.Utility.Common;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region Logger

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

#endregion

#region CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCorsPolicy", policy =>
    {

        var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
        policy.WithOrigins(allowedOrigins) // Your exact frontend origin
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Required if your login endpoint sets cookies
    });
});


#endregion

builder.Services.AddHostedService<ConfigSystemUser>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    // Plug in the ValidationBehavior so every command is validated before its handler runs
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddAutoMapper((cfg) =>
{
    cfg.AddProfile<ApplicationMapper>();
});

// Register all FluentValidation validators from this assembly automatically
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

#region Add Services

// Controllers
builder.Services.AddControllers().AddJsonOptions((option) =>
{
    option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    option.JsonSerializerOptions.Converters.Add(new DateTimeUtcJsonConverter());
});




// Database
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
});

// ASP.NET Identity
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

#endregion







builder.Services.AddHttpContextAccessor();



// (MediatR already registered above with behaviors — no duplicate needed)

#region JWT Configuration

// Bind JwtSettings from appsettings.json
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

// Read JwtSettings once for JWT Authentication
var jwtSettings = builder.Configuration
    .GetSection("Jwt")
    .Get<JwtSettings>()
    ?? throw new InvalidOperationException("Jwt settings are missing.");

if (jwtSettings != null && jwtSettings.Key != null)
{
    builder.Services.AddSingleton(jwtSettings);

    // Authentication
    builder.Services
        .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
            options.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse();

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var response = ApiResponse<object>.ErrorResponse(
                        "Unauthorized",
                        "Access token is invalid or expired.",
                        StatusCodes.Status401Unauthorized);

                    await context.Response.WriteAsJsonAsync(response);
                }
            };
        });

    // Authorization
}
builder.Services.AddAuthorization();

#endregion

#region Services

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<RefreshTokenService>();
builder.Services.AddScoped<IEmployee, EmployeeService>();
// builder.Services.AddScoped<ILoggingService, LoggingService>();
builder.Services.AddScoped<UserHelper>();
builder.Services.AddScoped<EmployeeHelper>();


builder.Services.AddScoped<ProspectStatusHelper>();

builder.Services.AddSingleton<ILoggingService, LoggingService>();
#endregion

#region ===================== Swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

var app = builder.Build();

#region ==================== Middleware

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// CRITICAL: Must be exactly here (after routing, before auth)
app.UseCors("DevCorsPolicy");
app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();




#endregion


#region Helper
public class DateTimeUtcJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.GetDateTime().ToUniversalTime();

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
}
#endregion