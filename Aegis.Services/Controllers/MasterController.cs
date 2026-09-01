using Aegis.Model.DTO.MasterDtos;
using Aegis.Services.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// Make sure this matches your feature's namespace
using static Aegis.Services.Features.MasterManagement.CreateJobRole.CreateJobeRole;
using static Aegis.Services.Features.MasterManagement.GetJobRoles.GetJobRoles;

namespace Aegis.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MasterController : ControllerBase
    {
        private readonly UserHelper _userHelper;
        private readonly IMediator _mediator;
        private readonly Guid OrganizationId;
        public MasterController(IMediator mediator, UserHelper userHelper)
        {
            _mediator = mediator;
            _userHelper = userHelper;

            OrganizationId = userHelper.GetCurrentTenant();
        }

        [HttpPost("create-joberole")]
        public async Task<IActionResult> CreateJobRoleAsync([FromBody] ManageJobRoleDto model)
        {
            var employee = await _userHelper.GetCurrentEmployeeAsync();

            var command = new CreateJobeRoleCommand(employee, model, OrganizationId);

            var response = await _mediator.Send(command);

            return StatusCode(response.StatusCode, response);
        }


        [HttpGet("get-joberoles")]
        public async Task<IActionResult> GetJobRoleAsync()
        {

            var command = new GetJobRolesQuery(OrganizationId);

            var response = await _mediator.Send(command);

            return StatusCode(response.StatusCode, response);
        }

    }
}
