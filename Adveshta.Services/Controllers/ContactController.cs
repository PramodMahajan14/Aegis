using Adveshta.Model.DTO.Contacts;
using Adveshta.Services.Features.ContactManagement.ContactList;
using Adveshta.Services.Features.ContactManagement.CreateContact;
using Adveshta.Services.Features.ContactManagement.DeleteContact;
using Adveshta.Services.Features.ContactManagement.UpdateContact;
using Adveshta.Services.Features.GetContactDetail;
using Adveshta.Services.Helper;
using Adveshta.Utility.Common;
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
            var command = new UpdateContactCommand(OrganizationId, LoggedEmployee.Id, model);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);

        }

        [HttpGet]
        public async Task<IActionResult> GetContactList([FromQuery] Guid? ProspectId, [FromQuery] PaginationQueryParameter Pagination)
        {
            var LoggedEmployee = await _helper.GetCurrentEmployeeAsync();
            var command = new ContactListQuery(OrganizationId, LoggedEmployee.Id, ProspectId, Pagination.Page, Pagination.Limit);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid Id)
        {
            var LoggedEmployee = await _helper.GetCurrentEmployeeAsync();
            var command = new ContactDetailQuery(Id, OrganizationId);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid Id)
        {
            var LoggedEmployee = await _helper.GetCurrentEmployeeAsync();
            var command = new DeleteContactCommand(OrganizationId,LoggedEmployee.Id,Id);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);

        }
    }
}
