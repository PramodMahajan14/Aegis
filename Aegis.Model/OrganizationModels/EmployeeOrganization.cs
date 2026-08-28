using System.ComponentModel.DataAnnotations.Schema;
using Aegis.Model.EmployeeModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace Aegis.Model.OrganizationModel
{
 public class EmployeeOrganization
{
    public Guid Id { get; set; }

    public Guid EmployeeId { get; set; }
    public Guid OrganizationId { get; set; }

    public bool IsSystem { get; set; }

    [ValidateNever]
    [ForeignKey(nameof(EmployeeId))]
    public Employee Employee { get; set; } = null!;

    [ValidateNever]
    [ForeignKey(nameof(OrganizationId))]
    public Organization Organization { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
}