using Aegis.Model.Master;
using Aegis.Utility.Common;
using Microsoft.EntityFrameworkCore;

namespace Aegis.DataAccess.DataSeeder
{
    public static class ModuleSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>().HasData(

                new Module
                {
                    Id = ModuleMaster.Leads,
                    Key = "leads",
                    Name = "Leads",
                    Description = "Manage potential customers and business opportunities."
                },

                new Module
                {
                    Id = ModuleMaster.Opportunities,
                    Key = "opportunities",
                    Name = "Opportunities",
                    Description = "Manage qualified business opportunities through their lifecycle."
                },

                new Module
                {
                    Id = ModuleMaster.Contacts,
                    Key = "contacts",
                    Name = "Contacts",
                    Description = "Manage people and customer relationships."
                },

                new Module
                {
                    Id = ModuleMaster.Pipeline,
                    Key = "pipeline",
                    Name = "Pipeline",
                    Description = "Manage business pipelines, stages and opportunity progression."
                },

                new Module
                {
                    Id = ModuleMaster.Activities,
                    Key = "activities",
                    Name = "Activities",
                    Description = "Track interactions and business activities."
                },

                new Module
                {
                    Id = ModuleMaster.Tasks,
                    Key = "tasks",
                    Name = "Tasks",
                    Description = "Manage assigned work and follow-up activities."
                },

                new Module
                {
                    Id = ModuleMaster.Meetings,
                    Key = "meetings",
                    Name = "Meetings",
                    Description = "Schedule and manage business meetings."
                },

                new Module
                {
                    Id = ModuleMaster.SiteVisits,
                    Key = "site_visits",
                    Name = "Site Visits",
                    Description = "Manage field visits and customer visits."
                },

                new Module
                {
                    Id = ModuleMaster.Requirements,
                    Key = "requirements",
                    Name = "Requirements",
                    Description = "Capture and manage business and customer requirements."
                },

                new Module
                {
                    Id = ModuleMaster.Documents,
                    Key = "documents",
                    Name = "Documents",
                    Description = "Manage documents associated with CRM records."
                },

                new Module
                {
                    Id = ModuleMaster.Calendar,
                    Key = "calendar",
                    Name = "Calendar",
                    Description = "Manage CRM schedules and events."
                },

                new Module
                {
                    Id = ModuleMaster.Campaigns,
                    Key = "campaigns",
                    Name = "Campaigns",
                    Description = "Manage campaigns and lead acquisition activities."
                },

                new Module
                {
                    Id = ModuleMaster.Reports,
                    Key = "reports",
                    Name = "Reports",
                    Description = "Analyze CRM data and business performance."
                },

                new Module
                {
                    Id = ModuleMaster.Notifications,
                    Key = "notifications",
                    Name = "Notifications",
                    Description = "Manage CRM notifications and reminders."
                }
            );
        }
    }
}