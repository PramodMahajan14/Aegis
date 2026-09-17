namespace Adveshta.Utility.Enum.ProspectEnum
{

    public enum TimeLineSouceType
    {
        ProspectContact = 1,
        Activity = 2,
        Task = 3,
        Meeting = 4,
        SiteVisit = 5,
        RequirementResponse = 6,
        Document = 7
    }
    public enum TimelineEventType
    {
        ProspectCreated = 1,
        prospectUpdated = 2,
        ProspectStatusChanged = 3,
        TemperatureChanged = 4,
        ContactLinked = 5,
        ActivityLogged = 6,
        TaskCreated = 7,
        TaskCompleted = 8,
        MeetingScheduled = 9,
        SiteVisitCompleted = 10,
        RequirementSubmitted = 11,
        DocumentUploaded = 12,
        ProspectConverted = 13

    }
}