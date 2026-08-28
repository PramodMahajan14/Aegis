using Aegis.DataAccess.DataSeeder;
using Aegis.Model.Auth;
using Aegis.Model.EmployeeModels;
using Aegis.Model.Master;
using Aegis.Model.OrganizationModel;
using Aegis.Utility.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aegis.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }


    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
   
   #region Organization 
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<EmployeeOrganization> EmployeeOrganizations {get;set;}

    #endregion

    #region Master
    public DbSet<Module> Modules { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<FeaturePermission> FeaturePermissions { get; set; }
    public DbSet<ApplicationRole> ApplicationRoles { get; set; }
    public DbSet<JobRole> JobRoles { get; set; }
    public DbSet<ApplicationRolePermisson> ApplicationRolePermissons { get; set; }
    public DbSet<OrganizationType> OrganizationTypes {get;set;}

    #endregion
   
    #region Employee
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeAppRoleMap> EmployeeAppRoleMaps {get;set;}

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        ModuleSeeder.Seed(modelBuilder);
        FeatureSeeder.Seed(modelBuilder);
        PermissionSeeder.Seed(modelBuilder);
        OrganizationTypeSeeder.Seed(modelBuilder);
        
        modelBuilder.Entity<Organization>().HasData(
            new Organization
            {
                Id = SystemConfigInstance.OrganizationId,
                Name = SystemConfigInstance.Name,
                Email = SystemConfigInstance.Email,
                ContactPerson = SystemConfigInstance.ContactPerson,
                ContactNumber = SystemConfigInstance.ContactPerson,
                DomainName = SystemConfigInstance.DomainName,
                OnboardingDate = DateTime.UtcNow,
                IsSystemTenant = true,
                OrganizationTypeId = OrganizationTypeMaser.Direct
                
            }
        );

       



    }


}

// docker run -d --name mysql-db -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root123 -e MYSQL_DATABASE=AegisDb -v mysql_data:/var/lib/mysql mysql:latest