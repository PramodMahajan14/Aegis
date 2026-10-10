

using Adveshta.Model.OrganizationModel;

namespace Adveshta.Model.Master
{
    public class ApplicationRole : OrganizationRelation
    {
        public Guid Id {get;set;}

        public string Name {get;set;} = string.Empty;

        public string Description {get;set;} = string.Empty;

         public bool IsSystem { get; set; } = false;
    
    }
}