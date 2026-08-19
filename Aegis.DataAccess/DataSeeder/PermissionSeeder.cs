using Aegis.Model.Master;
using Microsoft.EntityFrameworkCore;

namespace Aegis.DataAccess.DataSeeder
{
    public static class PermissionSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeaturePermission>().HasData(
                new FeaturePermission {}
            );
        }
    }
}