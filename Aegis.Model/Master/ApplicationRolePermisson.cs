using System.ComponentModel.DataAnnotations.Schema;

namespace Aegis.Model.Master
{
    public class ApplicationRolePermisson
    {
        public Guid Id { get; set; }

        public Guid ApplicationRoleId { get; set; }

        public Guid FeaturePermissionId { get; set; }

        [ForeignKey(nameof(ApplicationRoleId))]
        public ApplicationRole ApplicationRole { get; set; } = default!;

        [ForeignKey(nameof(FeaturePermissionId))]
        public FeaturePermission FeaturePermission { get; set; } = default!;
    }
}