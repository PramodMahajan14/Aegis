using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Aegis.Model.Master
{
    public class FeaturePermission
    {
        public Guid Id { get; set; }

        public Guid FeatureId { get; set; }
        [ValidateNever]
        [ForeignKey("FeatureId")]
        public Feature? Feature { get; set; }

        public string Name { get; set; } = "Feature Permission";
        public string Description { get; set; } = "Feature permission description";

        public string Key { get; set; } = "feature.permission.key";
        public bool IsEnabled { get; set; } = true;
        public bool DefaultValue { get; set; } = false;
    }
}