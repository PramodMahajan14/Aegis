using Adveshta.Model.DTO.Contacts;
using Adveshta.Services.Features.ContactManagement.CreateContact;
using Adveshta.Services.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Adveshta.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly Guid OrganizationId;
        private readonly UserHelper _helper;
        public ContactController(UserHelper helper, IMediator mediator)
        {
            _helper = helper;
            _mediator = mediator;
            OrganizationId = _helper.GetCurrentTenant();
        }




        [HttpPost]
        public async Task<IActionResult> CreatedContact([FromBody] ManageContactDto model)
        {
            var LoggedEmployee = await _helper.GetCurrentEmployeeAsync();
            var command = new CreateContactCommand(OrganizationId, LoggedEmployee, model);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);

        }


        [HttpPut]
        public async Task<IActionResult> UpdateContact([FromBody] ManageContactDto model)
        {
            var LoggedEmployee = await _helper.GetCurrentEmployeeAsync();
            var command = new CreateContactCommand(OrganizationId, LoggedEmployee, model);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);

        }
    }
}
