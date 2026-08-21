


using Aegis.DataAccess.Data;
using Aegis.Model.Auth;
using Aegis.Services.Services.Interfaces;
using Aegis.Utility.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;

namespace Aegis.Services.Services
{
    public class ConfigSystemUser : ISystemConfig
    {

        public readonly ApplicationUser _application;
        public readonly IServiceProvider _service;
        public readonly ILogger<ConfigSystemUser> _logger;
        public ConfigSystemUser(IServiceProvider serviceProvider, ILogger<ConfigSystemUser> logger)
        {
            _logger = logger;
            _service = serviceProvider;
        }

        public async Task StartSync(CancellationToken cancellation)
        {
            _logger.LogInformation("Starting database seeding process...");
            using var scope = _service.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var tenant = serviceProvider.GetRequiredService<IOptions<SystemConfig>>().Value;

            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellation);


            try
            {

              var configured = await dbContext.Tenants.AnyAsync(x=>x.Id == tenant.TenantId && x.IsSystemTenant && x.IsActive);
                if (!configured)
                {
                    return;
                }

                var user = await SeedSuperAdminUserAsync(userManager,tenant);

                if(user == null)
                {
                   await transaction.RollbackAsync();
                   return;
                }





            }
            catch (Exception ex)
            {

            }



        }


        private async Task<ApplicationUser> SeedSuperAdminUserAsync(UserManager<ApplicationUser> userManager, SystemConfig config)
        {
            _logger.LogInformation("Seeding Super Admin user: {Email}", config.Email);
            var user = await userManager.FindByEmailAsync(config.Email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = config.Email,
                    Email = config.Email,
                    EmailConfirmed = true,
                    IsRootUser = true
                };
                var result = await userManager.CreateAsync(user, config.Password);

                if (!result.Succeeded)
                {
                    // If user creation fails, it's a critical error.
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to create super admin user. Errors: {Errors}", errors);
                    throw new InvalidOperationException($"Failed to create super admin user: {errors}");
                }
                _logger.LogInformation("Super Admin user created successfully.");
            }
            else
            {
                _logger.LogInformation("Super Admin user already exists.");
            }
            return user;
        }
          


      public  Task StopAsync(CancellationToken cancellation)=> Task.CompletedTask;
        
    }
}