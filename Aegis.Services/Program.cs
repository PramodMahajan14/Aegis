using System.Text;
using Aegis.DataAccess.Data;
using Aegis.Model.Auth;
using Aegis.Services.Helper;
using Aegis.Services.Middleware;
using Aegis.Services.Services;
using Aegis.Services.Services.Interfaces;
using Aegis.Utility.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region Add Services

// Controllers
builder.Services.AddControllers();




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



// await using (var scope = app.Services.CreateAsyncScope())
// {
//     var seeder = scope.ServiceProvider
//         .GetRequiredService<IDatabaseSeeder>();

//     await seeder.SeedAsync();
// }



builder.Services.AddHttpContextAccessor();

# region Looger

builder.Host.UseSerilog((context, config) =>
{
   config.ReadFrom.Configuration(context.Configuration); 
});

#endregion

#region JWT Configuration

// Bind JwtSettings from appsettings.json
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

// Read JwtSettings once for JWT Authentication
var jwtSettings = builder.Configuration
    .GetSection("Jwt")
    .Get<JwtSettings>()
    ?? throw new InvalidOperationException("Jwt settings are missing.");

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
builder.Services.AddAuthorization();

#endregion

#region ===================Dependency Injection

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<RefreshTokenService>();
builder.Services.AddScoped<IEmployee, EmployeeService>();
builder.Services.AddScoped<ILoggingService, LoggingService>();
builder.Services.AddScoped<UserHelper>();

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

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion