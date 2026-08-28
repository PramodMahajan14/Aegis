
using Aegis.Model.Master;
using Aegis.Utility.Common;
using Microsoft.EntityFrameworkCore;

namespace Aegis.DataAccess.DataSeeder
{
    public static class OrganizationTypeSeeder
    {
        public static void Seed(ModelBuilder builder)
        {

            builder.Entity<OrganizationType>().HasData(
               new OrganizationType
               {
                   Id = OrganizationTypeMaser.Direct,
                   Name = "Direct Business",
                   Description = "An organization that manages its own products or services, customers, prospects, and sales activities."
               },

               new OrganizationType
               {
                   Id = OrganizationTypeMaser.Agency,
                   Name = "Sales Agency",
                   Description = "An organization that manages sales and marketing activities on behalf of other businesses."
               }
            );

        }
    }
}