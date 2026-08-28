


using System.Globalization;
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
    public class ConfigSystemUser(IServiceProvider serviceProvider, ILogger<ConfigSystemUser> logger) : IHostedService
    {
        public readonly IServiceProvider _service = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        public readonly ILogger<ConfigSystemUser> _logger = logger ?? throw new ArgumentNullException(nameof(logger));




        public async Task StartAsync(CancellationToken cancellation)
        {
            _logger.LogInformation("Starting database seeding process...");
            using var scope = _service.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var organization = serviceProvider.GetRequiredService<IOptions<SystemConfig>>().Value;

            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellation);


            try
            {

                var configured = await dbContext.Organizations.AnyAsync(x => x.Id == organization.OrganizationId && x.IsSystemTenant && x.IsActive);
                if (!configured)
                {
                    return;
                }

                var user = await SeedSuperAdminUserAsync(userManager, organization);

                if (user == null)
                {
                    await transaction.RollbackAsync();
                    return;
                }


                // 2. Seed the Job Role
                var jobRole = await GetOrCreateAsync(
                    dbContext.JobRoles,
                    j => j.Name == SystemConfigInstance.JobRole && j.OrganizationId == SystemConfigInstance.OrganizationId,
                    () => new JobRole
                    {
                        Name = SystemConfigInstance.JobRole,
                        Description = "Administrator with full system access.",
                        OrganizationId = SystemConfigInstance.OrganizationId
                    });

                var applicationRole = await GetOrCreateAsync(dbContext.ApplicationRoles, a => a.Name == SystemConfigInstance.AppRole && a.OrganizationId == SystemConfigInstance.OrganizationId,
                () => new ApplicationRole
                {
                    Name = SystemConfigInstance.AppRole,
                    Description = "Administrator with full system access.",
                    IsSystem = true,
                    OrganizationId = SystemConfigInstance.OrganizationId

                });
                // 4. Seed the Employee record linked to the user and job role
                var employee = await GetOrCreateAsync(
                    dbContext.Employees,
                    e => e.Email == SystemConfigInstance.Email && e.OrganizationId == SystemConfigInstance.OrganizationId,
                    () => new Employee
                    {
                        UserId = user.Id,
                        FirstName = "Admin",
                        LastName = "User",
                        Email = SystemConfigInstance.Email,
                        OrganizationId = SystemConfigInstance.OrganizationId,
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
                        OrganizationId = SystemConfigInstance.OrganizationId,
                        IsEnabled = true,

                        //  Force explicit nulls so EF Core passes standard NULL to MySQL
                        AssignedById = employee.Id, 
                        UnassignedById = null,
                        AssignedAt = DateTime.UtcNow
                    });

                // 6. seed Permission to Application

                await SeedAllPermissionsToApplicationRole(dbContext, applicationRole.Id);
                // All entities are now tracked by the DbContext.
                // A single SaveChanges call is more efficient.
                await dbContext.SaveChangesAsync(cancellation);

                // If all operations were successful, commit the transaction.
                await transaction.CommitAsync(cancellation);
                _logger.LogInformation("Super Admin user created successfully.");
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

            var assignedPermissons = await context.ApplicationRolePermissons.Where(x => x.ApplicationRoleId == ApplicationRoleId).Select(a => a.FeaturePermissionId).ToListAsync();

            var missingPermission = allPermissons.Except(assignedPermissons).ToList();

            if (missingPermission.Any())
            {
                var newMapp = missingPermission.Select(Permission => new ApplicationRolePermisson
                {
                    ApplicationRoleId = ApplicationRoleId,
                    FeaturePermissionId = Permission
                });

                await context.ApplicationRolePermissons.AddRangeAsync(newMapp);
                _logger.LogInformation("Added {count} new permissons to Supper ApplicationRole", missingPermission.Count());

            }
            else
            {
                _logger.LogInformation("Super Application Role already has all permisson");

            }


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