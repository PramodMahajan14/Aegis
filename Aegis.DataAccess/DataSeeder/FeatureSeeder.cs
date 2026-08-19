using Aegis.Model.Master;
using Aegis.Utility.Common;
using Microsoft.EntityFrameworkCore;

namespace Aegis.DataAccess.DataSeeder
{
    public static class FeatureSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feature>().HasData(

                #region LEADS
                new Feature
                {
                    Id = FeatureMaster.LeadManagement,
                    Name = "Lead Management",
                    ModuleId = ModuleMaster.Leads,
                    Key = "lead_management",
                    Description = "Create, view, update and manage leads."
                },

                new Feature
                {
                    Id = FeatureMaster.LeadCapture,
                    Name = "Lead Capture",
                    ModuleId = ModuleMaster.Leads,
                    Key = "lead_capture",
                    Description = "Capture leads from different sources."
                },

                new Feature
                {
                    Id = FeatureMaster.LeadSources,
                    Name = "Lead Sources",
                    ModuleId = ModuleMaster.Leads,
                    Key = "lead_sources",
                    Description = "Manage lead sources and acquisition channels."
                },

                new Feature
                {
                    Id = FeatureMaster.LeadQualification,
                    Name = "Lead Qualification",
                    ModuleId = ModuleMaster.Leads,
                    Key = "lead_qualification",
                    Description = "Qualify leads based on business criteria."
                },

                new Feature
                {
                    Id = FeatureMaster.LeadScoring,
                    Name = "Lead Scoring",
                    ModuleId = ModuleMaster.Leads,
                    Key = "lead_scoring",
                    Description = "Score and prioritize leads."
                },

                new Feature
                {
                    Id = FeatureMaster.LeadConversion,
                    Name = "Lead Conversion",
                    ModuleId = ModuleMaster.Leads,
                    Key = "lead_conversion",
                    Description = "Convert qualified leads into opportunities and contacts."
                },

                new Feature
                {
                    Id = FeatureMaster.LeadDuplicateDetection,
                    Name = "Duplicate Detection",
                    ModuleId = ModuleMaster.Leads,
                    Key = "lead_duplicate_detection",
                    Description = "Detect and manage duplicate leads."
                },

                #endregion

                #region OPPORTUNITIES
                new Feature
                {
                    Id = FeatureMaster.OpportunityManagement,
                    Name = "Opportunity Management",
                    ModuleId = ModuleMaster.Opportunities,
                    Key = "opportunity_management",
                    Description = "Create, view, update and manage opportunities."
                },

                new Feature
                {
                    Id = FeatureMaster.OpportunityStages,
                    Name = "Opportunity Stages",
                    ModuleId = ModuleMaster.Opportunities,
                    Key = "opportunity_stages",
                    Description = "Manage opportunity stages and progression."
                },

                new Feature
                {
                    Id = FeatureMaster.OpportunityValue,
                    Name = "Opportunity Value",
                    ModuleId = ModuleMaster.Opportunities,
                    Key = "opportunity_value",
                    Description = "Manage opportunity value and expected revenue."
                },

                new Feature
                {
                    Id = FeatureMaster.OpportunityProbability,
                    Name = "Opportunity Probability",
                    ModuleId = ModuleMaster.Opportunities,
                    Key = "opportunity_probability",
                    Description = "Manage probability of opportunity closure."
                },

                new Feature
                {
                    Id = FeatureMaster.OpportunityForecasting,
                    Name = "Opportunity Forecasting",
                    ModuleId = ModuleMaster.Opportunities,
                    Key = "opportunity_forecasting",
                    Description = "Forecast opportunity revenue and expected closures."
                },

                new Feature
                {
                    Id = FeatureMaster.OpportunityActivities,
                    Name = "Opportunity Activities",
                    ModuleId = ModuleMaster.Opportunities,
                    Key = "opportunity_activities",
                    Description = "Manage activities related to opportunities."
                },

                new Feature
                {
                    Id = FeatureMaster.OpportunityRequirements,
                    Name = "Opportunity Requirements",
                    ModuleId = ModuleMaster.Opportunities,
                    Key = "opportunity_requirements",
                    Description = "Manage requirements associated with opportunities."
                },
                #endregion


                #region  CONTACTS

                new Feature
                {
                    Id = FeatureMaster.ContactManagement,
                    Name = "Contact Management",
                    ModuleId = ModuleMaster.Contacts,
                    Key = "contact_management",
                    Description = "Create, view, update and manage contacts."
                },

                new Feature
                {
                    Id = FeatureMaster.ContactRelationships,
                    Name = "Contact Relationships",
                    ModuleId = ModuleMaster.Contacts,
                    Key = "contact_relationships",
                    Description = "Manage relationships between contacts and business records."
                },

                new Feature
                {
                    Id = FeatureMaster.ContactActivities,
                    Name = "Contact Activities",
                    ModuleId = ModuleMaster.Contacts,
                    Key = "contact_activities",
                    Description = "Manage activities related to contacts."
                },

                new Feature
                {
                    Id = FeatureMaster.ContactDocuments,
                    Name = "Contact Documents",
                    ModuleId = ModuleMaster.Contacts,
                    Key = "contact_documents",
                    Description = "Manage documents associated with contacts."
                },

                new Feature
                {
                    Id = FeatureMaster.ContactTimeline,
                    Name = "Contact Timeline",
                    ModuleId = ModuleMaster.Contacts,
                    Key = "contact_timeline",
                    Description = "View the history and timeline of contact interactions."
                },
                #endregion


                #region PIPELINE

                new Feature
                {
                    Id = FeatureMaster.PipelineManagement,
                    Name = "Pipeline Management",
                    ModuleId = ModuleMaster.Pipeline,
                    Key = "pipeline_management",
                    Description = "Create, view and manage sales pipelines."
                },

                new Feature
                {
                    Id = FeatureMaster.PipelineStages,
                    Name = "Pipeline Stages",
                    ModuleId = ModuleMaster.Pipeline,
                    Key = "pipeline_stages",
                    Description = "Configure and manage pipeline stages."
                },

                new Feature
                {
                    Id = FeatureMaster.MultiplePipelines,
                    Name = "Multiple Pipelines",
                    ModuleId = ModuleMaster.Pipeline,
                    Key = "multiple_pipelines",
                    Description = "Manage multiple sales pipelines."
                },

                new Feature
                {
                    Id = FeatureMaster.PipelineHistory,
                    Name = "Pipeline History",
                    ModuleId = ModuleMaster.Pipeline,
                    Key = "pipeline_history",
                    Description = "Track changes and movement across pipeline stages."
                },

                new Feature
                {
                    Id = FeatureMaster.PipelineKanban,
                    Name = "Pipeline Kanban",
                    ModuleId = ModuleMaster.Pipeline,
                    Key = "pipeline_kanban",
                    Description = "View and manage pipeline records using a Kanban board."
                },

                new Feature
                {
                    Id = FeatureMaster.PipelineForecasting,
                    Name = "Pipeline Forecasting",
                    ModuleId = ModuleMaster.Pipeline,
                    Key = "pipeline_forecasting",
                    Description = "Forecast pipeline performance and expected revenue."
                },
                #endregion


                #region ACTIVITIES

                new Feature
                {
                    Id = FeatureMaster.ActivityManagement,
                    Name = "Activity Management",
                    ModuleId = ModuleMaster.Activities,
                    Key = "activity_management",
                    Description = "Create, view, update and manage activities."
                },

                new Feature
                {
                    Id = FeatureMaster.CallActivity,
                    Name = "Call Activity",
                    ModuleId = ModuleMaster.Activities,
                    Key = "call_activity",
                    Description = "Create and manage call activities."
                },

                new Feature
                {
                    Id = FeatureMaster.EmailActivity,
                    Name = "Email Activity",
                    ModuleId = ModuleMaster.Activities,
                    Key = "email_activity",
                    Description = "Create and manage email activities."
                },

                new Feature
                {
                    Id = FeatureMaster.MeetingActivity,
                    Name = "Meeting Activity",
                    ModuleId = ModuleMaster.Activities,
                    Key = "meeting_activity",
                    Description = "Create and manage meeting activities."
                },

                new Feature
                {
                    Id = FeatureMaster.NoteActivity,
                    Name = "Note Activity",
                    ModuleId = ModuleMaster.Activities,
                    Key = "note_activity",
                    Description = "Create and manage notes and activity notes."
                },

                new Feature
                {
                    Id = FeatureMaster.CustomActivity,
                    Name = "Custom Activity",
                    ModuleId = ModuleMaster.Activities,
                    Key = "custom_activity",
                    Description = "Create and manage custom activity types."
                },
                #endregion


                #region TASKS

                new Feature
                {
                    Id = FeatureMaster.TaskManagement,
                    Name = "Task Management",
                    ModuleId = ModuleMaster.Tasks,
                    Key = "task_management",
                    Description = "Create, view, update and manage tasks."
                },

                new Feature
                {
                    Id = FeatureMaster.TaskAssignment,
                    Name = "Task Assignment",
                    ModuleId = ModuleMaster.Tasks,
                    Key = "task_assignment",
                    Description = "Assign tasks to users and teams."
                },

                new Feature
                {
                    Id = FeatureMaster.TaskPriority,
                    Name = "Task Priority",
                    ModuleId = ModuleMaster.Tasks,
                    Key = "task_priority",
                    Description = "Manage task priorities."
                },

                new Feature
                {
                    Id = FeatureMaster.TaskStatus,
                    Name = "Task Status",
                    ModuleId = ModuleMaster.Tasks,
                    Key = "task_status",
                    Description = "Manage task statuses."
                },

                new Feature
                {
                    Id = FeatureMaster.TaskChecklist,
                    Name = "Task Checklist",
                    ModuleId = ModuleMaster.Tasks,
                    Key = "task_checklist",
                    Description = "Manage checklists within tasks."
                },
                #endregion


                #region MEETINGS

                new Feature
                {
                    Id = FeatureMaster.MeetingManagement,
                    Name = "Meeting Management",
                    ModuleId = ModuleMaster.Meetings,
                    Key = "meeting_management",
                    Description = "Create, view, update and manage meetings."
                },

                new Feature
                {
                    Id = FeatureMaster.MeetingAttendees,
                    Name = "Meeting Attendees",
                    ModuleId = ModuleMaster.Meetings,
                    Key = "meeting_attendees",
                    Description = "Manage meeting attendees and participants."
                },

                new Feature
                {
                    Id = FeatureMaster.MeetingAgenda,
                    Name = "Meeting Agenda",
                    ModuleId = ModuleMaster.Meetings,
                    Key = "meeting_agenda",
                    Description = "Create and manage meeting agendas."
                },

                new Feature
                {
                    Id = FeatureMaster.MeetingNotes,
                    Name = "Meeting Notes",
                    ModuleId = ModuleMaster.Meetings,
                    Key = "meeting_notes",
                    Description = "Record and manage meeting notes."
                },

                new Feature
                {
                    Id = FeatureMaster.MeetingReminders,
                    Name = "Meeting Reminders",
                    ModuleId = ModuleMaster.Meetings,
                    Key = "meeting_reminders",
                    Description = "Configure reminders for scheduled meetings."
                },
                #endregion


                #region SITE VISITS

                new Feature
                {
                    Id = FeatureMaster.SiteVisitManagement,
                    Name = "Site Visit Management",
                    ModuleId = ModuleMaster.SiteVisits,
                    Key = "site_visit_management",
                    Description = "Create, view, update and manage site visits."
                },

                new Feature
                {
                    Id = FeatureMaster.SiteVisitCheckin,
                    Name = "Site Visit Check-in",
                    ModuleId = ModuleMaster.SiteVisits,
                    Key = "site_visit_checkin",
                    Description = "Record site visit check-in and check-out."
                },

                new Feature
                {
                    Id = FeatureMaster.SiteVisitMedia,
                    Name = "Site Visit Media",
                    ModuleId = ModuleMaster.SiteVisits,
                    Key = "site_visit_media",
                    Description = "Upload and manage photos and media from site visits."
                },

                new Feature
                {
                    Id = FeatureMaster.SiteVisitNotes,
                    Name = "Site Visit Notes",
                    ModuleId = ModuleMaster.SiteVisits,
                    Key = "site_visit_notes",
                    Description = "Record notes and observations from site visits."
                },

                new Feature
                {
                    Id = FeatureMaster.SiteVisitChecklists,
                    Name = "Site Visit Checklists",
                    ModuleId = ModuleMaster.SiteVisits,
                    Key = "site_visit_checklists",
                    Description = "Manage checklists for site visits."
                },

                new Feature
                {
                    Id = FeatureMaster.SiteVisitFollowup,
                    Name = "Site Visit Follow-up",
                    ModuleId = ModuleMaster.SiteVisits,
                    Key = "site_visit_followup",
                    Description = "Manage follow-up actions after site visits."
                },
                #endregion



                #region REQUIREMENTS

                new Feature
                {
                    Id = FeatureMaster.RequirementManagement,
                    Name = "Requirement Management",
                    ModuleId = ModuleMaster.Requirements,
                    Key = "requirement_management",
                    Description = "Create, view, update and manage requirements."
                },

                new Feature
                {
                    Id = FeatureMaster.RequirementTemplates,
                    Name = "Requirement Templates",
                    ModuleId = ModuleMaster.Requirements,
                    Key = "requirement_templates",
                    Description = "Create and manage requirement templates."
                },

                new Feature
                {
                    Id = FeatureMaster.RequirementSections,
                    Name = "Requirement Sections",
                    ModuleId = ModuleMaster.Requirements,
                    Key = "requirement_sections",
                    Description = "Organize requirements into sections."
                },

                new Feature
                {
                    Id = FeatureMaster.RequirementQuestions,
                    Name = "Requirement Questions",
                    ModuleId = ModuleMaster.Requirements,
                    Key = "requirement_questions",
                    Description = "Create and manage requirement questions."
                },

                new Feature
                {
                    Id = FeatureMaster.RequirementResponses,
                    Name = "Requirement Responses",
                    ModuleId = ModuleMaster.Requirements,
                    Key = "requirement_responses",
                    Description = "Capture and manage responses to requirements."
                },

                new Feature
                {
                    Id = FeatureMaster.RequirementAttachments,
                    Name = "Requirement Attachments",
                    ModuleId = ModuleMaster.Requirements,
                    Key = "requirement_attachments",
                    Description = "Upload and manage requirement attachments."
                },
                #endregion


                #region DOCUMENTS

                new Feature
                {
                    Id = FeatureMaster.DocumentManagement,
                    Name = "Document Management",
                    ModuleId = ModuleMaster.Documents,
                    Key = "document_management",
                    Description = "Create, view, update and manage documents."
                },

                new Feature
                {
                    Id = FeatureMaster.DocumentUpload,
                    Name = "Document Upload",
                    ModuleId = ModuleMaster.Documents,
                    Key = "document_upload",
                    Description = "Upload and manage documents."
                },

                new Feature
                {
                    Id = FeatureMaster.DocumentVersioning,
                    Name = "Document Versioning",
                    ModuleId = ModuleMaster.Documents,
                    Key = "document_versioning",
                    Description = "Manage document versions and revisions."
                },

                new Feature
                {
                    Id = FeatureMaster.DocumentSharing,
                    Name = "Document Sharing",
                    ModuleId = ModuleMaster.Documents,
                    Key = "document_sharing",
                    Description = "Share documents with authorized users."
                },
                #endregion


                #region CAMPAIGNS

                new Feature
                {
                    Id = FeatureMaster.CampaignManagement,
                    Name = "Campaign Management",
                    ModuleId = ModuleMaster.Campaigns,
                    Key = "campaign_management",
                    Description = "Create, view, update and manage campaigns."
                },

                new Feature
                {
                    Id = FeatureMaster.CampaignAudiences,
                    Name = "Campaign Audiences",
                    ModuleId = ModuleMaster.Campaigns,
                    Key = "campaign_audiences",
                    Description = "Define and manage campaign audiences."
                },

                new Feature
                {
                    Id = FeatureMaster.CampaignSources,
                    Name = "Campaign Sources",
                    ModuleId = ModuleMaster.Campaigns,
                    Key = "campaign_sources",
                    Description = "Manage campaign sources and acquisition channels."
                },

                new Feature
                {
                    Id = FeatureMaster.CampaignAttribution,
                    Name = "Campaign Attribution",
                    ModuleId = ModuleMaster.Campaigns,
                    Key = "campaign_attribution",
                    Description = "Track campaign attribution and lead sources."
                },

                new Feature
                {
                    Id = FeatureMaster.CampaignPerformance,
                    Name = "Campaign Performance",
                    ModuleId = ModuleMaster.Campaigns,
                    Key = "campaign_performance",
                    Description = "Analyze campaign performance and results."
                },


                // ============================================================
                // REPORTS
                // ============================================================

                new Feature
                {
                    Id = FeatureMaster.ReportBuilder,
                    Name = "Report Builder",
                    ModuleId = ModuleMaster.Reports,
                    Key = "report_builder",
                    Description = "Create and configure reports."
                },

                new Feature
                {
                    Id = FeatureMaster.ReportFilters,
                    Name = "Report Filters",
                    ModuleId = ModuleMaster.Reports,
                    Key = "report_filters",
                    Description = "Filter report data using configurable criteria."
                },

                new Feature
                {
                    Id = FeatureMaster.ReportGrouping,
                    Name = "Report Grouping",
                    ModuleId = ModuleMaster.Reports,
                    Key = "report_grouping",
                    Description = "Group report data by selected fields."
                },

                new Feature
                {
                    Id = FeatureMaster.ReportCharts,
                    Name = "Report Charts",
                    ModuleId = ModuleMaster.Reports,
                    Key = "report_charts",
                    Description = "Display report data using charts and visualizations."
                },

                new Feature
                {
                    Id = FeatureMaster.PipelineReports,
                    Name = "Pipeline Reports",
                    ModuleId = ModuleMaster.Reports,
                    Key = "pipeline_reports",
                    Description = "Generate reports related to pipeline performance."
                },

                new Feature
                {
                    Id = FeatureMaster.ConversionReports,
                    Name = "Conversion Reports",
                    ModuleId = ModuleMaster.Reports,
                    Key = "conversion_reports",
                    Description = "Generate reports for lead and opportunity conversions."
                },

                new Feature
                {
                    Id = FeatureMaster.ActivityReports,
                    Name = "Activity Reports",
                    ModuleId = ModuleMaster.Reports,
                    Key = "activity_reports",
                    Description = "Generate reports for sales activities."
                },

                new Feature
                {
                    Id = FeatureMaster.TeamReports,
                    Name = "Team Reports",
                    ModuleId = ModuleMaster.Reports,
                    Key = "team_reports",
                    Description = "Generate reports for team performance."
                },

                new Feature
                {
                    Id = FeatureMaster.ReportExport,
                    Name = "Report Export",
                    ModuleId = ModuleMaster.Reports,
                    Key = "report_export",
                    Description = "Export reports in supported formats."
                },

                new Feature
                {
                    Id = FeatureMaster.ScheduledReports,
                    Name = "Scheduled Reports",
                    ModuleId = ModuleMaster.Reports,
                    Key = "scheduled_reports",
                    Description = "Schedule reports for automatic generation and delivery."
                },
                #endregion


                #region NOTIFICATIONS

                new Feature
                {
                    Id = FeatureMaster.NotificationManagement,
                    Name = "Notification Management",
                    ModuleId = ModuleMaster.Notifications,
                    Key = "notification_management",
                    Description = "Create, view and manage notifications."
                },

                new Feature
                {
                    Id = FeatureMaster.AssignmentNotifications,
                    Name = "Assignment Notifications",
                    ModuleId = ModuleMaster.Notifications,
                    Key = "assignment_notifications",
                    Description = "Notify users when records or tasks are assigned."
                },

                new Feature
                {
                    Id = FeatureMaster.ReminderNotifications,
                    Name = "Reminder Notifications",
                    ModuleId = ModuleMaster.Notifications,
                    Key = "reminder_notifications",
                    Description = "Manage reminders for upcoming activities and tasks."
                },

                new Feature
                {
                    Id = FeatureMaster.MentionNotifications,
                    Name = "Mention Notifications",
                    ModuleId = ModuleMaster.Notifications,
                    Key = "mention_notifications",
                    Description = "Notify users when they are mentioned in records or activities."
                }
                #endregion
            );
        }
    }
}