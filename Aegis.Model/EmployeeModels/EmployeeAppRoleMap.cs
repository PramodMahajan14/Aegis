using System.ComponentModel.DataAnnotations.Schema;
using Aegis.Model.Master;
using Aegis.Model.OrganizationModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Aegis.Model.EmployeeModels
{
    public class EmployeeAppRoleMap:OrganizationRelation
    {
        public Guid Id {get;set;}

        public Guid EmployeeId {get;set;}
        [ValidateNever]
        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee {get;set;}

        public Guid AppRoleId {get;set;}
        [ValidateNever]
        [ForeignKey(nameof(AppRoleId))]
        public ApplicationRole? ApplicationRole {get;set;}

        public bool IsEnabled {get;set;}

        public DateTime? AssignedAt {get;set;}
        public Guid? AssignedById {get;set;}
        [ValidateNever]
        [ForeignKey(nameof(AssignedById))]
        public Employee? AssignedBy {get;set;}

        public DateTime? UnassignedAt {get;set;}
        public Guid? UnassignedById {get;set;}
        [ValidateNever]
        [ForeignKey(nameof(UnassignedById))]
        public Employee? ReassignedBy {get;set;}
   
    }
}