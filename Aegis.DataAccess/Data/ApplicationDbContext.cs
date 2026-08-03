using Aegis.Model.Auth;
using Aegis.Model.Employee;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aegis.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }


    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    public DbSet<Employee> Employees {get;set;} 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

    }


}

// docker run -d --name mysql-db -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root123 -e MYSQL_DATABASE=AegisDb -v mysql_data:/var/lib/mysql mysql:latest