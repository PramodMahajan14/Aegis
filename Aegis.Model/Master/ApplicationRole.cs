using Aegis.Model.Tenant;

namespace Aegis.Model.Master
{
    public class ApplicationRole : TenantRelation
    {
        public Guid Id {get;set;}

        public string Name {get;set;} = string.Empty;

        public string Description {get;set;} = string.Empty;
    
    }
}