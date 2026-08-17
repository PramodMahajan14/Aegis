namespace Aegis.Utility.Common
{
    public static class AppConstants
    {
        public const string SuperAdminRole = "SUPER_ADMIN";

        public static readonly Guid TenantId =
            Guid.Parse("c84c9fae-750c-4327-b3fd-338517be8161");
    }

    public static class ModuleMaster
    {
        public static readonly Guid Leads = Guid.Parse("6b4c01ba-8aa4-47ed-8ea2-1dc7fa5cd638");
        public static readonly Guid Opportunities = Guid.Parse("a009cd33-faf4-4e8a-bde4-fc2562c2198e");
        public static readonly Guid Contacts = Guid.Parse("1f65bb9f-4f28-4530-99f1-e20cbaf9b344");
        public static readonly Guid Pipeline = Guid.Parse("c50e290a-672b-430a-8dad-58fbb574415a");
        public static readonly Guid Activities = Guid.Parse("8e4ecd5b-f2e4-4d73-baf5-910508e4ee5a");
        public static readonly Guid Tasks = Guid.Parse("78d7bee8-a5d0-4689-91f7-2b350f22696a");
        public static readonly Guid Meetings = Guid.Parse("bf21b75b-7ea6-4ca9-b69b-120dc9eba6fc");
        public static readonly Guid SiteVisits = Guid.Parse("0a539ca7-0233-49f9-b969-c841f7d3c43b");
        public static readonly Guid Requirements = Guid.Parse("97a3673c-5760-448a-b248-e3688ad193fb");
        public static readonly Guid Documents = Guid.Parse("c6167d9f-0c7f-4271-a325-cbbcb42c0298");

        public static readonly Guid Calendar = Guid.Parse("2624c7ab-baec-4b9f-8002-55c627c532a5");
        public static readonly Guid Campaigns = Guid.Parse("93d57136-92fc-4a0c-9f36-9fd9f85e23b5");

        public static readonly Guid Reports = Guid.Parse("82d77160-b060-480d-9d22-244a3b011b8b");

        public static readonly Guid Notifications = Guid.Parse("fe25290f-1402-4401-8933-c9aad35dfaec");


    }
}

