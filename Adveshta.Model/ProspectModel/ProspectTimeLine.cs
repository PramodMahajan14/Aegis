using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Adveshta.Model.EmployeeModels;
using Adveshta.Model.OrganizationModel;
using Adveshta.Utility.Enum.ProspectEnum;

namespace Adveshta.Model.ProspectModel
{
    public class ProspectTimeLine : OrganizationRelation
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ProspectId { get; set; }
        [ForeignKey(nameof(ProspectId))]
        public Prospect Prospect { get; set; } = null!;

        public Guid? EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee { get; set; }

        public TimelineEventType EventType { get; set; }

        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(500)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? Summary { get; set; }

        public TimeLineSouceType? SourceEntityType { get; set; }
        public Guid? SourceEntityId { get; set; }

        [Column(TypeName = "json")]
        public string? Metadata { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
