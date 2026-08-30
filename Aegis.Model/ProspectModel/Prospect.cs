using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using Aegis.Model.EmployeeModels;
using Aegis.Model.Master;
using Aegis.Model.OrganizationModel;

namespace Aegis.Model.ProspectModel
{
    public class Prospect : OrganizationRelation
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public Guid StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public ProspectStatus ProspectStatus { get; set; } = null!;

        public Decimal? EstimatedValue { get; set; }

        public DateTime? ExpectedDecisionDate { get; set; }

        public string? Location { get; set; }

        // public string? SourceId {get;set;}

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public Guid CreatedById { get; set; }

        [ForeignKey(nameof(CreatedById))]
        public Employee Employee { get; set; } = null!;

    }
}