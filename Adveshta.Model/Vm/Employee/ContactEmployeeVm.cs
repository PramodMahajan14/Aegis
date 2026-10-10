using Adveshta.Utility.Enum.MeetingEnum;

namespace Adveshta.Model.Vm.Employee
{
    public class ContactEmployeeVm
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public ParticipantType ParticipantType { get; set; }
  
    }
}