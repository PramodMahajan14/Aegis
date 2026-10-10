using Adveshta.Model.Vm.Employee;
using Adveshta.Model.Vm.Prospect;
using Adveshta.Utility.Enum.MeetingEnum;

namespace Adveshta.Model.Vm.MeetingVms
{


    public enum MeetingSortBy
    {
        EarliestFirst = 1,
        LatestFirst = 2
    }

    public class MeetingFilter
    {
        public Guid? ProspectId { get; set; }
        public string? SearchQuery { get; set; }

        public DateRange? Range { get; set; }

        public MeetingSortBy? Sorting {get;set;} = MeetingSortBy.EarliestFirst;

        public int Limit {get;set;} = 10;

        public int Page {get;set;} = 1;
 
    }
    public class MeetingVm
    {
        public Guid Id { get; set; }

        public DateTime When { get; set; }
        public string Agenda { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public BasicProspectVm Prospect { get; set; } = null!;

        public MeetingStatus Status { get; set; }

        public string? Output { get; set; }

        public DateTime CreatedAt { get; set; }

        public BasicEmployeeVm? CreatedBy { get; set; }


        public ICollection<ContactEmployeeVm> Participants { get; set; } = new List<ContactEmployeeVm>();


    }
}