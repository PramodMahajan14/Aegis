using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Aegis.Model.OrganizationModel
{
    public class OrganizationRelation
    {
        public Guid OrganizationId {get;set;}
        
        [ValidateNever]
        [ForeignKey(nameof(OrganizationId))]
        public Organization? Organization {get;set;}
    }
}