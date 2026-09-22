
using Adveshta.Model.ContactModel;
using Adveshta.Model.OrganizationModel;

namespace Adveshta.Model.Master
{
    public class JobRole : OrganizationRelation
    {
        public Guid Id {get;set;}

        public string Name {get;set;} = string.Empty;

        public string Description {get;set;} = string.Empty;


        public ICollection<Contact> Contacts {get;set;} = new List<Contact>();

    }
}