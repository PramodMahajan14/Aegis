using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Aegis.Model.TenantModels
{
    public class TenantRelation
    {
        public Guid TenantId {get;set;}
        
        [ValidateNever]
        [ForeignKey(nameof(TenantId))]
        public Tenant? Tenant {get;set;}
    }
}