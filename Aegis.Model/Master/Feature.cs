using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace Aegis.Model.Master
{
    public class Feature
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "Feature";

        public Guid ModuleId { get; set; }

        public string Description { get; set; } = "Featuer Description";

        public bool IsActive { get; set; }

        public string Key { get; set; } = "feature.key";
        [ValidateNever]
        [ForeignKey("ModuleId")]
        public Module? Module { get; set; }


        public ICollection<FeaturePermission> FeaturePermissions {get;set;} = new List<FeaturePermission>();

    }
}

