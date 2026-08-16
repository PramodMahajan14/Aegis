using System.ComponentModel.DataAnnotations;

namespace Aegis.Model.Master
{
    public class Module
    {
        public Guid Id {get;set;}

        [Required]
        public string Name {get;set;} = "Module";
        public string Description {get;set;} = "Module.Description";

        public string Key {get;set;} = "module";

        public ICollection<Feature> Features {get;set;} = new List<Feature>();

    }
}
