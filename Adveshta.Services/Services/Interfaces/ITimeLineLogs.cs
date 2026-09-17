using Adveshta.DataAccess.Data;
using Adveshta.Utility.Enum.ProspectEnum;

namespace Adveshta.Services.Services.Interfaces
{
    public interface ITimeLineLogs
    {
        void Log(
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
            string? metadata = null
        );
    }
}
