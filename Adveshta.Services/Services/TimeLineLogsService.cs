using Adveshta.DataAccess.Data;
using Adveshta.Model.ProspectModel;
using Adveshta.Services.Services.Interfaces;
using Adveshta.Utility.Enum.ProspectEnum;

namespace Adveshta.Services.Services
{
    public class TimeLineLogsService : ITimeLineLogs
    {
        public void Log(
        ApplicationDbContext context,
        Guid organizationId,
        Guid prospectId,
        Guid? employeeId,
        TimelineEventType eventType,
        string title,
        string? summary = null,
        DateTime? occurredAt = null,
        TimeLineSouceType? sourceEntityType = null,
        Guid? sourceEntityId = null,
        string? metadata = null)
        {
            var entry = new ProspectTimeLine
            {
                Id = Guid.NewGuid(),
                OrganizationId = organizationId,
                ProspectId = prospectId,
                EmployeeId = employeeId,
                EventType = eventType,
                OccurredAt = occurredAt ?? DateTime.UtcNow,
                Title = title,
                Summary = summary,
                SourceEntityType = sourceEntityType,
                SourceEntityId = sourceEntityId,
                Metadata = metadata,
                CreatedAt = DateTime.UtcNow
            };
            context.ProspectTimeLines.Add(entry);
        }
    }
}