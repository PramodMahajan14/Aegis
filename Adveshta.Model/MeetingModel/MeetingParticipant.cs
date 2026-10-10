using System.ComponentModel.DataAnnotations.Schema;
using Adveshta.Model.ContactModel;
using Adveshta.Model.EmployeeModels;
using Adveshta.Utility.Enum.MeetingEnum;


namespace Adveshta.Model.MeetingModel
{
    public class MeetingParticipant
    {
        public Guid Id {get;set;}

        public Guid MeetingId {get;set;}
        [ForeignKey(nameof(MeetingId))]
        public Meeting Meeting {get;set;} = null!;


        public Guid? EmployeeId {get;set;}
        public Guid? ContactId {get;set;}

        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee {get;set;}

        [ForeignKey(nameof(ContactId))]
        public Contact? Contact  {get;set;}

        public ParticipantType ParticipantType {get;set;}

    }
}