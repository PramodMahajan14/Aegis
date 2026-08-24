


using System.Linq.Expressions;
using Aegis.DataAccess.Data;
using Aegis.Model.Auth;
using Aegis.Model.Employee;
using Aegis.Model.Master;
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

        public async Task StartAsync(CancellationToken cancellation)
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

                var configured = await dbContext.Tenants.AnyAsync(x => x.Id == tenant.TenantId && x.IsSystemTenant && x.IsActive);
                if (!configured)
                {
                    return;
                }

                var user = await SeedSuperAdminUserAsync(userManager, tenant);

                if (user == null)
                {
                    await transaction.RollbackAsync();
                    return;
                }


                // 2. Seed the Job Role
                var jobRole = await GetOrCreateAsync(
                    dbContext.JobRoles,
                    j => j.Name == SystemConfigInstance.JobRole && j.TenantId == SystemConfigInstance.TenantId,
                    () => new JobRole
                    {
                        Name = SystemConfigInstance.JobRole,
                        Description = "Administrator with full system access.",
                        TenantId = SystemConfigInstance.TenantId
                    });

                var applicationRole = await GetOrCreateAsync(dbContext.ApplicationRoles, a => a.Name == SystemConfigInstance.AppRole && a.TenantId == SystemConfigInstance.TenantId,
                () => new ApplicationRole
                {
                    Name = SystemConfigInstance.AppRole,
                    Description = "Administrator with full system access.",
                    IsSystem = true,
                    TenantId = SystemConfigInstance.TenantId

                });
                // 4. Seed the Employee record linked to the user and job role
                var employee = await GetOrCreateAsync(
                    dbContext.Employees,
                    e => e.Email == SystemConfigInstance.Email && e.TenantId == SystemConfigInstance.TenantId,
                    () => new Employee
                    {
                        UserId = user.Id,
                        FirstName = "Admin",
                        LastName = "User",
                        Email = SystemConfigInstance.Email,
                        TenantId = SystemConfigInstance.TenantId,
                        ContactNumber = SystemConfigInstance.ContactNumber,
                        JobRoleId = jobRole.Id,
                        IsSystem = true,
                        JoiningDate = DateTime.UtcNow,
                        DateOfBirth = new DateTime(1970, 1, 1)
                        // Set other non-nullable fields to sensible defaults
                    });

                // 5. Seed the Employee-AppRole Mapping
                await GetOrCreateAsync(
                    dbContext.EmployeeAppRoleMaps,
                    erm => erm.EmployeeId == employee.Id && erm.AppRoleId == applicationRole.Id,
                    () => new EmployeeAppRoleMap
                    {
                        EmployeeId = employee.Id,
                        AppRoleId = applicationRole.Id,
                        TenantId = SystemConfigInstance.TenantId,
                        IsEnabled = true
                    });





            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during database seeding. Rolling back changes.");
                await transaction.RollbackAsync(cancellation);
                throw;
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

        public async Task SeedAllPermissionsToApplicationRole(ApplicationDbContext context, Guid ApplicationRoleId)
        {
            var allPermissons = await context.FeaturePermissions.Select(x => x.Id).ToListAsync();

            var AssignedPermissons = await context.ApplicationRolePermissons.Where(x => x.ApplicationRoleId == ApplicationRoleId).Select(a => a.FeaturePermissionId).ToListAsync();
        }
        /// <summary>
        /// A generic helper to find an entity by a predicate or create, add, and return it if not found.
        /// This promotes code reuse and makes the main logic cleaner.
        /// </summary>
        private async Task<T> GetOrCreateAsync<T>(DbSet<T> dbSet, Expression<Func<T, bool>> predicate, Func<T> factory) where T : class
        {
            var entity = await dbSet.FirstOrDefaultAsync(predicate);
            if (entity == null)
            {
                entity = factory();
                await dbSet.AddAsync(entity);
                _logger.LogInformation("Creating new entity of type {EntityType}.", typeof(T).Name);
            }
            return entity;
        }

        public Task StopAsync(CancellationToken cancellation) => Task.CompletedTask;

    }
}