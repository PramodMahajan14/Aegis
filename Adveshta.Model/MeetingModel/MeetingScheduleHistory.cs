using System.ComponentModel.DataAnnotations.Schema;
using Adveshta.Model.EmployeeModels;

namespace Adveshta.Model.MeetingModel
{
    public class MeetingScheduleHistory
    {
        public Guid Id {get;set;} 

        public Guid MeetingId {get;set;}
        [ForeignKey(nameof(MeetingId))]
        public Meeting Meeting {get;set;} = null!;


        public DateTime OldTime {get;set;} 
        public DateTime NewTime {get;set;}

        public Guid ChangedById {get;set;}
        public Employee ChangedByEmployee { get; set; } = null!;

        public string? Reason  {get;set;}

        public DateTime CreatedAt {get;set;}
    }
}