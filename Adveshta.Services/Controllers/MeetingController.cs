using Adveshta.Model.DTO.MeetingDto;
using Adveshta.Model.OrganizationModel;
using Adveshta.Services.Features.MeetingManagement.CreateMeeting;
using Adveshta.Services.Helper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Adveshta.Services.Controllers
{
    public class  MeetingController : ControllerBase
    {

        private readonly UserHelper _helper;
        private readonly IMediator _mediator;
        private readonly Guid organizationId;
        public MeetingController(UserHelper helper,IMediator mediator)
        {
            _helper = helper;
            _mediator = mediator;
            organizationId = _helper.GetCurrentTenant();
        }


        public async Task<IActionResult> CreateMeeting([FromBody] ManageMeetingDto model)
        {
            var LoggedEmployee = await _helper.GetCurrentEmployeeAsync();
            var command = new CreateMeetingCommand(organizationId,LoggedEmployee.Id,model);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode,response);
        }


        // [HttpGet("/lookup")]
        // public async Task<IActionResult> GetEmployeeAndContact([FromQuery] string SearchTerm)
        // {
        //     var LoggedEmployee = await _helper.GetCurrentEmployeeAsync();

          
        // }
    }
}
