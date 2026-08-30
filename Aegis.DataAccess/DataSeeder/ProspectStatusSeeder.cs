using Aegis.Model.Master;
using Aegis.Utility.Common;
using Microsoft.EntityFrameworkCore;

namespace Aegis.DataAccess.DataSeeder
{
    public static class ProspectStatusSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<ProspectStatus>().HasData(
                new ProspectStatus
                {
                    Id = ProspectsStatusMaster.NEW,
                    Name = "New",
                    Code = "NEW",
                    Description = "Salesperson is actively working on it",
                    DisplayOrder = 1,
                    IsActive = true
                },
                  new ProspectStatus
                  {
                      Id = ProspectsStatusMaster.ACTIVE,
                      Name = "Active",
                      Code = "ACTIVE",
                      Description = "Salesperson is actively working on it",
                      DisplayOrder = 2,
                      IsActive = true
                  },
                  new ProspectStatus
                  {
                      Id = ProspectsStatusMaster.FOLLOW_UP,
                      Name = "Follow Up",
                      Code = "FOLLOW_UP",
                      Description = "Waiting for a response / next interaction",
                      DisplayOrder = 3,
                      IsActive = true
                  },
                  new ProspectStatus
                  {
                      Id = ProspectsStatusMaster.QUALIFICATION,
                      Name = "Qualification",
                      Code = "QUALIFICATION",
                      Description = "Salesperson is checking whether it is a genuine opportunity",
                      DisplayOrder = 4,
                      IsActive = true
                  },
                  new ProspectStatus
                  {
                      Id = ProspectsStatusMaster.QUALIFIED,
                      Name = "Qualified",
                      Code = "QUALIFIED",
                      Description = "Project is confirmed as worth pursuing",
                      DisplayOrder = 5,
                      IsActive = true
                  },
                  new ProspectStatus
                  {
                      Id = ProspectsStatusMaster.DORMANT,
                      Name = "Dormant",
                      Code = "DORMANT",
                      Description = "No current activity, but not rejected",
                      DisplayOrder = 6,
                      IsActive = true
                  },
                  new ProspectStatus
                  {
                      Id = ProspectsStatusMaster.DISQUALIFIED,
                      Name = "Disqualified",
                      Code = "DISQUALIFIED",
                      Description = "Not a suitable business prospect",
                      DisplayOrder = 7,
                      IsActive = true
                  },
                  new ProspectStatus
                  {
                      Id = ProspectsStatusMaster.CONVERTED,
                      Name = "Converted",
                      Code = "CONVERTED",
                      Description = "Prospect has been converted into an Opportunity",
                      DisplayOrder = 8,
                      IsActive = true
                  }
            );
        }
    }
}