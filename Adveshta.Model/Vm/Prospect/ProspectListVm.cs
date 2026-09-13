using System.Security.Cryptography.X509Certificates;
using Adveshta.Model.Master;

namespace Adveshta.Model.Vm.Prospect
{
    public class ProspectListVm
    {
        public Guid Id { get; set; }
        
        public string ProspectNo {get;set;} = null!;
        public string Name { get; set; } = string.Empty;

        public string BusinessName { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public ProspectStatusVm Status { get; set; } = null!;

        public Decimal? EstimatedValue { get; set; }

        public DateTime? ExpectedDecisionDate { get; set; }

        
        public ProspectTemperatureVm Temperature {get;set;} = null!;

        public string? NextAction {get;set;} = null;

        public string? LastAction  {get;set;} = null;
  

        // public ProjectStageVm? ProjectStage {get;set;}


    }
}