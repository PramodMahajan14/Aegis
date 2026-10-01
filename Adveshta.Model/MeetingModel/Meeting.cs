using System.ComponentModel.DataAnnotations.Schema;
using Adveshta.Model.EmployeeModels;
using Adveshta.Model.OrganizationModel;
using Adveshta.Model.ProspectModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Adveshta.Model.MeetingModel
{
    public class Meeting  : OrganizationRelation
    {
        public Guid Id {get;set;}

        public DateTime When {get;set;}
        public string Agenda  {get;set;} = string.Empty;

        public string Subject {get;set;} = string.Empty;
        
        public string Location {get;set;} = string.Empty;

        public Guid ProspectId {get;set;}
        [ForeignKey(nameof(ProspectId))]
        public Prospect Prospect {get;set;} = null!;
        
        public string? Output {get;set;} 

        public ICollection<MeetingParticipant> Participants {get;set;} = new List<MeetingParticipant>();


        public DateTime CreatedAt {get;set;}
        public Guid? UpdatedById {get;set;}

        public Employee? UpdatedBy {get;set;}
    }
}