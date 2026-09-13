using Adveshta.Model.ProspectModel;

namespace Adveshta.Model.Master
{
    public class ProspectStatus
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Prospect> Prospects {get;set;} = new List<Prospect>();

    }
}