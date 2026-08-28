namespace Aegis.Utility.Common
{
    public class SystemConfig
    {

        public string CONFIG_SECTION_NAME = "SuperAdminAccount";
        public string Email { get; set; } = "codedev90@gmail.com";
        public string Password { get; set; } = "User@123";
        public string JobRole = "Admin";
        public string SuperAdminRole = "SUPER_ADMIN";
        public Guid OrganizationId { get; set; } = Guid.Parse("c84c9fae-750c-4327-b3fd-338517be8161");
    }

    public static class SystemConfigInstance
    {

        public const string CONFIG_SECTION_NAME = "SuperAdminAccount";
        public const string Email = "codedev90@gmail.com";
        public const string Password = "User@123";
        public const string JobRole = "Admin";
        public const string AppRole = "SUPER_ADMIN";
        public const string ContactPerson = "Pramod Mahajan";
        public const string ContactNumber = "9022471779";
        public const string DomainName = "codedev.in";
        public const string Name = "Code Dev";
        public static Guid OrganizationId => Guid.Parse("c84c9fae-750c-4327-b3fd-338517be8161");
    }

    public static class OrganizationTypeMaser
    {
        public static readonly Guid Direct = Guid.Parse("1f22266a-9a9d-4768-a9b8-c328dc9bdd7b");
        public static readonly Guid Agency = Guid.Parse("50da9d49-1386-4268-b45f-b7e8a103fd78");
    }

    public static class ModuleMaster
    {
        public static readonly Guid Clients = Guid.Parse("ecddab43-7ba2-4b17-b5f3-5f3639b5a1bd");
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

        // public static readonly Guid Calendar = Guid.Parse("2624c7ab-baec-4b9f-8002-55c627c532a5");
        public static readonly Guid Campaigns = Guid.Parse("93d57136-92fc-4a0c-9f36-9fd9f85e23b5");

        public static readonly Guid Reports = Guid.Parse("82d77160-b060-480d-9d22-244a3b011b8b");

        public static readonly Guid Notifications = Guid.Parse("fe25290f-1402-4401-8933-c9aad35dfaec");


    }

    public static class FeatureMaster

    {



        #region Companies

        public static readonly Guid ClientManagement =
            Guid.Parse("604fb10f-8b94-45d4-a2fc-914a4e4fe021");

        public static readonly Guid ClientRelationships =
            Guid.Parse("eea8aad9-986e-4372-89ee-9027c0c6ded5");

        public static readonly Guid ClientActivities =
            Guid.Parse("3a5f1730-e80a-4502-80d3-9cb07d30891b");


        #endregion
        #region Leads

        public static readonly Guid LeadManagement =
            Guid.Parse("9ce21b3c-baf9-4ebe-9219-ff08cae22f1b");

        public static readonly Guid LeadCapture =
            Guid.Parse("6327f4df-4a2e-4268-962a-481c1ab9b93e");

        public static readonly Guid LeadSources =
            Guid.Parse("b14e3341-795c-416a-9d65-d25f4de688b0");

        public static readonly Guid LeadQualification =
            Guid.Parse("a767bedb-1089-4a1a-b5d2-03205885262a");

        public static readonly Guid LeadScoring =
            Guid.Parse("e9f2a460-0cac-4153-83b3-f0ec1a9c4c01");

        public static readonly Guid LeadConversion =
            Guid.Parse("1a7b5f4a-68bb-48f7-b4b9-d8383ae23d4c");

        public static readonly Guid LeadDuplicateDetection =
            Guid.Parse("ee528c96-89ee-4728-9f27-ade7a179d405");

        #endregion


        #region Opportunities

        public static readonly Guid OpportunityManagement =
            Guid.Parse("50ab36d8-5211-495d-88a9-db3de9d7fd2f");

        public static readonly Guid OpportunityStages =
            Guid.Parse("775c0750-9dae-4bf7-a5e0-c77c50d0866a");

        public static readonly Guid OpportunityValue =
            Guid.Parse("94b04436-fccd-46ae-847f-04edfc5da6c7");

        public static readonly Guid OpportunityProbability =
            Guid.Parse("450a0e10-1f35-4883-b2a2-1cc45110b6af");

        public static readonly Guid OpportunityForecasting =
            Guid.Parse("2b3a902c-4357-4f28-ad7a-474ed9623981");

        public static readonly Guid OpportunityActivities =
            Guid.Parse("b528b3e4-ca9b-4e0a-adfe-32f5722a6992");

        public static readonly Guid OpportunityRequirements =
            Guid.Parse("ad89af4a-c7f3-45a3-8426-df1cff0f4f21");

        #endregion


        #region Contacts

        public static readonly Guid ContactManagement =
            Guid.Parse("88293270-5873-4b8f-a4e4-1065ce494ae0");

        public static readonly Guid ContactRelationships =
            Guid.Parse("27a56e31-5a1a-4992-b01f-1e309220e9cb");

        public static readonly Guid ContactActivities =
            Guid.Parse("f974510b-51b1-4e1b-9626-c288ee965dee");

        public static readonly Guid ContactDocuments =
            Guid.Parse("c84e8997-8763-410f-b5a9-80145d12515c");

        public static readonly Guid ContactTimeline =
            Guid.Parse("160bd4df-d18e-405d-989b-b5bdba1e8528");

        #endregion


        #region Pipeline

        public static readonly Guid PipelineManagement =
            Guid.Parse("7d41fe16-f08f-48a8-a7e1-0873572f8a90");

        public static readonly Guid PipelineStages =
            Guid.Parse("5f2bad79-cef6-4a48-bd7e-d935c767abcf");

        public static readonly Guid MultiplePipelines =
            Guid.Parse("ccd21a8b-e3c7-49ae-80a6-284dc1317b3d");

        public static readonly Guid PipelineHistory =
            Guid.Parse("04f070f0-4449-47c4-8f10-e9eb319cc4d1");

        public static readonly Guid PipelineKanban =
            Guid.Parse("113179e6-2038-4ba4-ab22-47e131e80b75");

        public static readonly Guid PipelineForecasting =
            Guid.Parse("27f42f6e-dbd4-4285-b366-9f7cd2bf43d8");

        #endregion


        #region Activities

        public static readonly Guid ActivityManagement =
            Guid.Parse("26414dc5-a6ba-4825-87c1-db1636873ce8");

        public static readonly Guid CallActivity =
            Guid.Parse("a104ef97-1766-45af-bd54-b9df4118d165");

        public static readonly Guid EmailActivity =
            Guid.Parse("1bd1b732-4d31-4be4-a23a-e2b08619bcc2");

        public static readonly Guid MeetingActivity =
            Guid.Parse("b54fe2e2-3563-48e8-aeda-509e52030007");

        public static readonly Guid NoteActivity =
            Guid.Parse("afe14019-14bb-4804-aa35-b78b7fe378f4");

        public static readonly Guid CustomActivity =
            Guid.Parse("be652534-efba-466a-98a7-372ce2846460");

        #endregion


        #region Tasks

        public static readonly Guid TaskManagement =
            Guid.Parse("6b97f241-5b4f-4e79-a769-d3291f31dc19");

        public static readonly Guid TaskAssignment =
            Guid.Parse("3c2e8541-4273-42c3-bfef-5492454edeee");

        public static readonly Guid TaskPriority =
            Guid.Parse("ca63a567-711a-4d00-a8dc-4377aed508c6");

        public static readonly Guid TaskStatus =
            Guid.Parse("ab456a9c-806e-4f0c-92d9-1e8e136f5e5f");

        public static readonly Guid TaskChecklist =
            Guid.Parse("569d198b-93e8-490f-a419-ec604c23487a");

        #endregion


        #region Meetings

        public static readonly Guid MeetingManagement =
            Guid.Parse("b4e7c404-78f8-4a9d-8471-6174dd8e5ce3");

        public static readonly Guid MeetingAttendees =
            Guid.Parse("491e70af-d8bc-41ad-983c-0c38b24cebb5");

        public static readonly Guid MeetingAgenda =
            Guid.Parse("7e730dd8-42f3-4382-8ff2-a5b6b042630d");

        public static readonly Guid MeetingNotes =
            Guid.Parse("48795578-ec33-4f11-b620-c990a79a0bcd");

        public static readonly Guid MeetingReminders =
            Guid.Parse("2e54a66d-9100-4dc1-8cf2-4db40c4a927e");

        #endregion


        #region Site Visits

        public static readonly Guid SiteVisitManagement =
            Guid.Parse("5ca16678-e0d3-4bf4-a4fe-36cd965526a8");

        public static readonly Guid SiteVisitCheckin =
            Guid.Parse("62e0172e-32d8-4d49-b680-3dea19c05281");

        public static readonly Guid SiteVisitMedia =
            Guid.Parse("e0e0dcac-9dda-4ba1-82b5-94629f54dc08");

        public static readonly Guid SiteVisitNotes =
            Guid.Parse("128b4c10-2bd0-4f30-971c-6f51e3891eb7");

        public static readonly Guid SiteVisitChecklists =
            Guid.Parse("19c4c6af-fd77-4b97-99ee-de1583dcdb38");

        public static readonly Guid SiteVisitFollowup =
            Guid.Parse("d5ca37f4-5a2d-44f2-8415-62aeb2d58893");

        #endregion


        #region Requirements

        public static readonly Guid RequirementManagement =
            Guid.Parse("fecbedae-c392-499b-bc9c-ac9dc0a23eaf");

        public static readonly Guid RequirementTemplates =
            Guid.Parse("922624d0-7daf-4917-a535-61885a6d32f1");

        public static readonly Guid RequirementSections =
            Guid.Parse("5d3033e0-7909-4bef-b920-ed6a4c0f4764");

        public static readonly Guid RequirementQuestions =
            Guid.Parse("17e69f35-bf9a-4b68-815a-16ab7d8da183");

        public static readonly Guid RequirementResponses =
            Guid.Parse("e1900440-3625-41ed-bce6-eec1f924d3df");

        public static readonly Guid RequirementAttachments =
            Guid.Parse("d39f9687-8482-4fba-adf2-b269a09400e7");

        #endregion


        #region Documents

        public static readonly Guid DocumentManagement =
            Guid.Parse("b837a859-ed3d-4c03-95a1-72864e4fb2eb");

        public static readonly Guid DocumentUpload =
            Guid.Parse("6030081d-3ab0-4f5d-a477-b357cfc7f6b6");

        public static readonly Guid DocumentVersioning =
            Guid.Parse("2aa7e974-0721-4837-8c4c-4c6f35b565a7");

        public static readonly Guid DocumentSharing =
            Guid.Parse("b4ae0775-f77c-45bd-8329-621f515bb9d8");

        #endregion


        // #region Calendar

        // public static readonly Guid CalendarManagement =
        //     Guid.Parse("10000011-0001-4000-8000-000000000001");

        // public static readonly Guid CalendarMeetings =
        //     Guid.Parse("10000011-0001-4000-8000-000000000002");

        // public static readonly Guid CalendarTasks =
        //     Guid.Parse("10000011-0001-4000-8000-000000000003");

        // public static readonly Guid CalendarSiteVisits =
        //     Guid.Parse("10000011-0001-4000-8000-000000000004");

        // public static readonly Guid ExternalCalendarSync =
        //     Guid.Parse("10000011-0001-4000-8000-000000000005");

        // #endregion


        #region Campaigns

        public static readonly Guid CampaignManagement =
            Guid.Parse("01a7613f-e840-4932-a3da-5cdafeb43198");

        public static readonly Guid CampaignAudiences =
            Guid.Parse("932f5a14-0cc3-4012-b030-926578c48017");

        public static readonly Guid CampaignSources =
            Guid.Parse("7ff9cbed-25e1-4bcb-adee-391be9e558e1");

        public static readonly Guid CampaignAttribution =
            Guid.Parse("2353aac8-0935-423d-8ee9-b6981f30111a");

        public static readonly Guid CampaignPerformance =
            Guid.Parse("bb7972f1-63b0-4c6f-859c-7bf28abf98fc");

        #endregion


        #region Reports

        public static readonly Guid ReportBuilder =
            Guid.Parse("6f25bf12-7b11-44bc-aecc-3f03b9567bd8");

        public static readonly Guid ReportFilters =
            Guid.Parse("83e49858-6246-4b88-b9f3-36fe5021544c");

        public static readonly Guid ReportGrouping =
            Guid.Parse("c3fe64f5-1604-4d26-9477-a0039aa58004");

        public static readonly Guid ReportCharts =
            Guid.Parse("9d31cf55-8f81-4ff8-9548-acf8e6ab31a4");

        public static readonly Guid PipelineReports =
            Guid.Parse("11e87daf-0b2d-474f-91f8-1f639529bcac");

        public static readonly Guid ConversionReports =
            Guid.Parse("83144137-5268-4bc5-953f-df5223933abb");

        public static readonly Guid ActivityReports =
            Guid.Parse("06a470df-66d0-46e3-a7e4-407be3c6205f");

        public static readonly Guid TeamReports =
            Guid.Parse("babc7309-b920-41ea-b3fa-57957a683c3e");

        public static readonly Guid ReportExport =
            Guid.Parse("0cc30bd0-22a2-49f6-8bb2-4747283a5b63");

        public static readonly Guid ScheduledReports =
            Guid.Parse("4a0882ae-2e67-4484-8f4b-c0954b730364");

        #endregion


        #region Notifications

        public static readonly Guid NotificationManagement =
            Guid.Parse("d84def16-0452-4869-bff8-c7e8578223b2");

        public static readonly Guid AssignmentNotifications =
            Guid.Parse("e3252074-231b-49f3-8286-1fb2220c0c85");

        public static readonly Guid ReminderNotifications =
            Guid.Parse("6eb3c2c6-cf82-4b7d-9aed-e41c49b62741");

        public static readonly Guid MentionNotifications =
            Guid.Parse("9e70fec6-9a25-48a2-b21a-cce5c17c7d3e");

        #endregion
    }
}


