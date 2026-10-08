using System.Collections.ObjectModel;
using Adveshta.Model.MeetingModel;
using Adveshta.Utility.Enum.MeetingEnum;

namespace Adveshta.Model.DTO.MeetingDto
{

    public class MeetingParticipantList
    {
        public Guid Id {get;set;}
        public ParticipantType Type {get;set;}
        public bool IsActive {get;set;}
    }
    public class ManageMeetingDto
    {
        public Guid? Id {get;set;}

        public string Subject {get;set;} = string.Empty;

        public string Agenda {get;set;} = string.Empty;

        public DateTime When {get;set;}

        public string Location {get;set;} = string.Empty;

        public Guid ProspectId {get;set;}

        public ICollection<MeetingParticipantList> MeetingParticipantList {get;set;} = new List<MeetingParticipantList>();
    }
}