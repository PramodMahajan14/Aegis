using Adveshta.Model.OrganizationModel;
using Adveshta.Services.Helper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Adveshta.Services.Controllers
{
    public class  MeetingController : ControllerBase
    {

        private readonly UserHelper _helper;
        private readonly IMediator _mediator;
        private readonly Guid OrganizationId;
        public MeetingController(UserHelper helper,IMediator mediator)
        {
            _helper = helper;
            _mediator = mediator;
            OrganizationId = _helper.GetCurrentTenant();
        }


        // [HttpGet("/lookup")]
        // public async Task<IActionResult> GetEmployeeAndContact([FromQuery] string SearchTerm)
        // {
        //     var LoggedEmployee = await _helper.GetCurrentEmployeeAsync();

          
        // }
    }
}
