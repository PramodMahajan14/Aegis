using Aegis.Model.DTO.MasterDtos;
using Aegis.Services.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// Make sure this matches your feature's namespace
using static Aegis.Services.Features.MasterManagement.CreateJobRole.CreateJobeRole;
using static Aegis.Services.Features.MasterManagement.GetJobRoles.GetJobRoles;
using static Aegis.Services.Features.MasterManagement.GetJobRole.GetJobRole;
using static Aegis.Services.Features.MasterManagement.UpdateJobRole.UpdateJobRole;
using static Aegis.Services.Features.MasterManagement.DeleteJobRole.DeleteJobRole;
namespace Aegis.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MasterController : ControllerBase
    {
        private readonly UserHelper _userHelper;
        private readonly IMediator _mediator;
        private readonly Guid _organizationId;
        public MasterController(IMediator mediator, UserHelper userHelper)
        {
            _mediator = mediator;
            _userHelper = userHelper;

            _organizationId = userHelper.GetCurrentTenant();
        }

        [HttpPost("create-jobrole")]
        public async Task<IActionResult> CreateJobRoleAsync([FromBody] ManageJobRoleDto model)
        {
            var employee = await _userHelper.GetCurrentEmployeeAsync();

            var command = new CreateJobeRoleCommand(employee, model, _organizationId);

            var response = await _mediator.Send(command);

            return StatusCode(response.StatusCode, response);
        }


        [HttpGet("get-jobroles")]
        public async Task<IActionResult> GetJobRoleListAsync()
        {

            var command = new GetJobRolesQuery(_organizationId);

            var response = await _mediator.Send(command);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("get-jobrole/{id}")]
        public async Task<IActionResult> GetJobRoleAsync([FromRoute] Guid Id)
        {

            var command = new GetJobRoleQuery(_organizationId, Id);

            var response = await _mediator.Send(command);

            return StatusCode(response.StatusCode, response);
        }


        [HttpPut("update-jobrole/{id}")]
        public async Task<IActionResult> UpdateJobRoleAsync([FromBody] ManageJobRoleDto model,[FromRoute] Guid Id)
        {
            var employee = await _userHelper.GetCurrentEmployeeAsync();
            var command = new UpdateJobRoleCommand( model, _organizationId,Id);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("delete-jobrole/{id}")]
        public async Task<IActionResult> DeleteJobRoleAsync([FromRoute] Guid Id)
        {

            var command = new DeleteJobRoleCommand(_organizationId, Id);

            var response = await _mediator.Send(command);

            return StatusCode(response.StatusCode, response);
        }

    }
}
