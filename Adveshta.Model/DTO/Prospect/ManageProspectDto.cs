using Adveshta.Utility.Enum;

namespace Adveshta.Model.DTO.Prospect
{
    public class ManageProspectDto
    {
        public Guid? Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string BusinessName { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public Guid StatusId { get; set; }

        public Decimal? EstimatedValue { get; set; }

        public DateTime? ExpectedDecisionDate { get; set; }

        public string Location { get; set; } = string.Empty;

        public string? OfficeLocation { get; set; }


        public Guid ProspectTemperatureId { get; set; }

        public Guid ProspectSourceId { get; set; }

    }
}