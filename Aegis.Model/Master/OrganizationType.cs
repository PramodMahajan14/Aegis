namespace Aegis.Model.Master
{
    public class OrganizationType
    {
        public Guid Id {get;set;}

        public string Name {get;set;} = string.Empty;

        public string Description {get;set;} = string.Empty;
        
    }
}


// DirectBusiness
//     → Companies are generally customers/prospects

// SalesAgency
//     → Companies may be businesses whose sales/marketing
//       the agency manages,  