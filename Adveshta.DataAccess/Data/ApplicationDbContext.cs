using Adveshta.DataAccess.DataSeeder;
using Adveshta.Model.Auth;
using Adveshta.Model.EmployeeModels;
using Adveshta.Model.Master;
using Adveshta.Model.OrganizationModel;
using Adveshta.Model.ProspectModel;
using Adveshta.Utility.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    #region Organization 
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<EmployeeOrganization> EmployeeOrganizations { get; set; }

    #endregion

    #region Master
    public DbSet<Module> Modules { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<FeaturePermission> FeaturePermissions { get; set; }
    public DbSet<ApplicationRole> ApplicationRoles { get; set; }
    public DbSet<JobRole> JobRoles { get; set; }
    public DbSet<ApplicationRolePermisson> ApplicationRolePermissons { get; set; }
    public DbSet<OrganizationType> OrganizationTypes { get; set; }

    public DbSet<ProjectStage> ProjectStages { get; set; }
    public DbSet<ProspectStatus> ProspectsStatus { get; set; }

    #endregion

    #region Employee
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeAppRoleMap> EmployeeAppRoleMaps { get; set; }
    #endregion


    #region Prospect
    public DbSet<Prospect> Prospects { get; set; }

    public DbSet<ProspectSource> ProspectSources { get; set; }
    public DbSet<ProspectTemperature> ProspectTemperatures { get; set; }

    public DbSet<ProspectTimeLine> ProspectTimeLines {get;set;}
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);


        #region  System Config 
        ModuleSeeder.Seed(modelBuilder);
        FeatureSeeder.Seed(modelBuilder);
        PermissionSeeder.Seed(modelBuilder);
        OrganizationTypeSeeder.Seed(modelBuilder);

        // Prospect
        ProspectSourceSeeder.Seed(modelBuilder);
        ProspectTemperatureSeeder.Seed(modelBuilder);

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
        #endregion

        #region System Core Master 

        ProspectStatusSeeder.Seed(modelBuilder);

        #endregion


        // convention to all decimal properties

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(decimal) ||
                    property.ClrType == typeof(decimal?))
                {
                    property.SetPrecision(18);
                    property.SetScale(2);
                }
            }
        }


    }


}

// docker run -d --name mysql-db -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root123 -e MYSQL_DATABASE=AdveshtaDb -v mysql_data:/var/lib/mysql mysql:latest