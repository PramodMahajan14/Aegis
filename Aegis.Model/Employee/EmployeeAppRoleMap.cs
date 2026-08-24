using System.ComponentModel.DataAnnotations.Schema;
using Aegis.Model.Master;
using Aegis.Model.TenantModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Aegis.Model.Employee
{
    public class EmployeeAppRoleMap:TenantRelation
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

        public DateTime AssignedAt {get;set;}
        public Guid AssignedById {get;set;}
        [ValidateNever]
        [ForeignKey(nameof(AssignedById))]
        public Employee? AssignedBy {get;set;}

        public DateTime ReassignedAt {get;set;}
        public Guid ReassignedById {get;set;}
        [ValidateNever]
        [ForeignKey(nameof(ReassignedById))]
        public Employee? ReassignedBy {get;set;}
   
    }
}