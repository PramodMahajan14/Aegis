using Aegis.Model.Master;
using Aegis.Utility.Common;
using Microsoft.EntityFrameworkCore;

namespace Aegis.DataAccess.DataSeeder
{
    public static class PermissionSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeaturePermission>().HasData(
                #region Client
                 new FeaturePermission
                 {

                    Id = ClientPermissions.CREATE,
                    FeatureId = FeatureMaster.ClientManagement,
                    Name = "Client Create",
                    Description = "Create Client",
                    Key = "client.create",
                    IsEnabled = true,
                    DefaultValue = true
                },
                 new FeaturePermission
                 {

                     Id = ClientPermissions.UPDATE,
                     FeatureId = FeatureMaster.ClientManagement,
                     Name = "Client Update",
                     Description = "Update Client",
                     Key = "client.update",
                     IsEnabled = true,
                     DefaultValue = true
                 },
                 new FeaturePermission
                 {

                     Id = ClientPermissions.DELETE,
                     FeatureId = FeatureMaster.ClientManagement,
                     Name = "Client Delete",
                     Description = "Delete Client",
                     Key = "client.update",
                     IsEnabled = true,
                     DefaultValue = true
                 },
                 new FeaturePermission
                 {

                     Id = ClientPermissions.VIEW,
                     FeatureId = FeatureMaster.ClientManagement,
                     Name = "Client View",
                     Description = "View Client",
                     Key = "client.view",
                     IsEnabled = true,
                     DefaultValue = true
                 }
                #endregion
            );
        }
    }
}