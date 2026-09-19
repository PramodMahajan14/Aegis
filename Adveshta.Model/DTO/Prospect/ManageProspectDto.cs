using Adveshta.Utility.Enum;

namespace Adveshta.Model.DTO.Prospect
{
    public class ManageProspectDto
    {
        public Guid? Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string BusinessName { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;


        public Decimal? EstimatedValue { get; set; }

        public DateTime? ExpectedDecisionDate { get; set; }
        
        public string ProjectLocation { get; set; } = string.Empty;

        public string? OfficeLocation { get; set; }

        public Guid? ProgressId {get;set;}
        public Guid TemperatureId { get; set; }

        public Guid SourceId { get; set; }

    }
}