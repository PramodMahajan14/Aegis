namespace Aegis.Utility.Common
{
    public static class AppConstants
    {

        public const string CONFIG_SECTION_NAME = "SuperAdminAccount";
        public static string Email { get; set; } = "admin@marcoaiot.com";
        public static string Password { get; set; } = "User@123";
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

        // public static readonly Guid Calendar = Guid.Parse("2624c7ab-baec-4b9f-8002-55c627c532a5");
        public static readonly Guid Campaigns = Guid.Parse("93d57136-92fc-4a0c-9f36-9fd9f85e23b5");

        public static readonly Guid Reports = Guid.Parse("82d77160-b060-480d-9d22-244a3b011b8b");

        public static readonly Guid Notifications = Guid.Parse("fe25290f-1402-4401-8933-c9aad35dfaec");


    }

    public static class FeatureMaster
    {
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
            Guid.Parse("3c510e3-7baf-45c7-8008-e3287cf8f3f5");

        public static readonly Guid OpportunityStages =
            Guid.Parse("775c0750-9dae-4bf7-a5e0-c77c50d0866a");

        public static readonly Guid OpportunityValue =
            Guid.Parse("c4de154-4f75-4373-a8b0-9705b6a1e06e");

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
            Guid.Parse("27a56e31-5a1a-4992-b01f-1e309220e9cb");

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
            Guid.Parse("10000007-0001-4000-8000-000000000001");

        public static readonly Guid MeetingAttendees =
            Guid.Parse("10000007-0001-4000-8000-000000000002");

        public static readonly Guid MeetingAgenda =
            Guid.Parse("10000007-0001-4000-8000-000000000003");

        public static readonly Guid MeetingNotes =
            Guid.Parse("10000007-0001-4000-8000-000000000004");

        public static readonly Guid MeetingReminders =
            Guid.Parse("10000007-0001-4000-8000-000000000005");

        #endregion


        #region Site Visits

        public static readonly Guid SiteVisitManagement =
            Guid.Parse("10000008-0001-4000-8000-000000000001");

        public static readonly Guid SiteVisitCheckin =
            Guid.Parse("10000008-0001-4000-8000-000000000002");

        public static readonly Guid SiteVisitMedia =
            Guid.Parse("10000008-0001-4000-8000-000000000003");

        public static readonly Guid SiteVisitNotes =
            Guid.Parse("10000008-0001-4000-8000-000000000004");

        public static readonly Guid SiteVisitChecklists =
            Guid.Parse("10000008-0001-4000-8000-000000000005");

        public static readonly Guid SiteVisitFollowup =
            Guid.Parse("10000008-0001-4000-8000-000000000006");

        #endregion


        #region Requirements

        public static readonly Guid RequirementManagement =
            Guid.Parse("10000009-0001-4000-8000-000000000001");

        public static readonly Guid RequirementTemplates =
            Guid.Parse("10000009-0001-4000-8000-000000000002");

        public static readonly Guid RequirementSections =
            Guid.Parse("10000009-0001-4000-8000-000000000003");

        public static readonly Guid RequirementQuestions =
            Guid.Parse("10000009-0001-4000-8000-000000000004");

        public static readonly Guid RequirementResponses =
            Guid.Parse("10000009-0001-4000-8000-000000000005");

        public static readonly Guid RequirementAttachments =
            Guid.Parse("10000009-0001-4000-8000-000000000006");

        #endregion


        #region Documents

        public static readonly Guid DocumentManagement =
            Guid.Parse("10000010-0001-4000-8000-000000000001");

        public static readonly Guid DocumentUpload =
            Guid.Parse("10000010-0001-4000-8000-000000000002");

        public static readonly Guid DocumentVersioning =
            Guid.Parse("10000010-0001-4000-8000-000000000003");

        public static readonly Guid DocumentSharing =
            Guid.Parse("10000010-0001-4000-8000-000000000004");

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
            Guid.Parse("10000012-0001-4000-8000-000000000001");

        public static readonly Guid CampaignAudiences =
            Guid.Parse("10000012-0001-4000-8000-000000000002");

        public static readonly Guid CampaignSources =
            Guid.Parse("10000012-0001-4000-8000-000000000003");

        public static readonly Guid CampaignAttribution =
            Guid.Parse("10000012-0001-4000-8000-000000000004");

        public static readonly Guid CampaignPerformance =
            Guid.Parse("10000012-0001-4000-8000-000000000005");

        #endregion


        #region Reports

        public static readonly Guid ReportBuilder =
            Guid.Parse("10000013-0001-4000-8000-000000000001");

        public static readonly Guid ReportFilters =
            Guid.Parse("10000013-0001-4000-8000-000000000002");

        public static readonly Guid ReportGrouping =
            Guid.Parse("10000013-0001-4000-8000-000000000003");

        public static readonly Guid ReportCharts =
            Guid.Parse("10000013-0001-4000-8000-000000000004");

        public static readonly Guid PipelineReports =
            Guid.Parse("10000013-0001-4000-8000-000000000005");

        public static readonly Guid ConversionReports =
            Guid.Parse("10000013-0001-4000-8000-000000000006");

        public static readonly Guid ActivityReports =
            Guid.Parse("10000013-0001-4000-8000-000000000007");

        public static readonly Guid TeamReports =
            Guid.Parse("10000013-0001-4000-8000-000000000008");

        public static readonly Guid ReportExport =
            Guid.Parse("10000013-0001-4000-8000-000000000009");

        public static readonly Guid ScheduledReports =
            Guid.Parse("10000013-0001-4000-8000-000000000010");

        #endregion


        #region Notifications

        public static readonly Guid NotificationManagement =
            Guid.Parse("10000014-0001-4000-8000-000000000001");

        public static readonly Guid AssignmentNotifications =
            Guid.Parse("10000014-0001-4000-8000-000000000002");

        public static readonly Guid ReminderNotifications =
            Guid.Parse("10000014-0001-4000-8000-000000000003");

        public static readonly Guid MentionNotifications =
            Guid.Parse("10000014-0001-4000-8000-000000000004");

        #endregion
    }
}

