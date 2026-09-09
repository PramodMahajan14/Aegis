using System.ComponentModel.DataAnnotations.Schema;
using Aegis.Model.EmployeeModels;
using Aegis.Model.Master;
using Aegis.Model.OrganizationModel;
using Aegis.Utility.Enum;

namespace Aegis.Model.ProspectModel
{
    public class Prospect : OrganizationRelation
    {
        public Guid Id { get; set; }
        
        public string ProspectNo {get;set;} = null!;
        public string Name { get; set; } = string.Empty;

        public string BusinessName { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public Guid StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public ProspectStatus ProspectStatus { get; set; } = null!;

        public Decimal? EstimatedValue { get; set; }

        public DateTime? ExpectedDecisionDate { get; set; }

        public string Location { get; set; } = string.Empty;

        public string? OfficeLocation { get; set; }

        // public string? SourceId {get;set;}
        
        public Guid ProspectTemperatureId {get;set;}
        [ForeignKey(nameof(ProspectTemperatureId))]
        public ProspectTemperature ProspectTemperature {get;set;} = null!;

        public Guid ProspectSourceId {get;set;}
        [ForeignKey(nameof(ProspectSourceId))]
        public ProspectSource ProspectSource {get;set;} = null!;

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public Guid CreatedById { get; set; }

        [ForeignKey(nameof(CreatedById))]
        public Employee CreatedBy { get; set; } = null!;

        public Guid? UpdatedById { get; set; }

        [ForeignKey(nameof(UpdatedById))]
        public Employee? UpdatedBy { get; set; } = null;


    }
}