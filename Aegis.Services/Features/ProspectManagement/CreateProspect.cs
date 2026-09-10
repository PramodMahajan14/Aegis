using Aegis.Model.DTO.Prospect;
using Aegis.Model.EmployeeModels;
using Aegis.Utility.Common;
using MediatR;

namespace Aegis.Services.Features.ProspectManagement.CreateProspect
{
    // public static class CreateProspect
    // {
    public class CreateProspectCommand : IRequest<ApiResponse<object>>
    {
        public Guid OrganizationId { get; set; }

        public Employee LoggedEmployee { get; set; }

        public ManageProspectDto Request { get; set; }


        public CreateProspectCommand(Guid organizationId, Employee employee, ManageProspectDto model)
        {
            OrganizationId = organizationId;
            LoggedEmployee = employee;
            Request = model;

        }
    }


    public class CreateProspectHander : IRequestHandler(CreateProspectCommand, ApiResponse<object>)
    {

    }

    // }
}