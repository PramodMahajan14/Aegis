using Adveshta.Model.DTO.MasterDtos;
using Adveshta.Services.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// Make sure this matches your feature's namespace
using static Adveshta.Services.Features.MasterManagement.CreateJobRole.CreateJobeRole;
using static Adveshta.Services.Features.MasterManagement.GetJobRoles.GetJobRoles;
using static Adveshta.Services.Features.MasterManagement.GetJobRole.GetJobRole;
using static Adveshta.Services.Features.MasterManagement.UpdateJobRole.UpdateJobRole;
using static Adveshta.Services.Features.MasterManagement.DeleteJobRole.DeleteJobRole;
using Adveshta.Model.Master;
using Adveshta.Model.DTO;
using Adveshta.Services.Features.MasterManagement;
using Adveshta.Services.Features.MasterManagement.GetSourceList;
using Adveshta.Services.Features.MasterManagement.GetTemperaturesList;
namespace Adveshta.Services.Controllers
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


        #region Job Role

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
        public async Task<IActionResult> UpdateJobRoleAsync([FromBody] ManageJobRoleDto model, [FromRoute] Guid Id)
        {
            var employee = await _userHelper.GetCurrentEmployeeAsync();
            var command = new UpdateJobRoleCommand(model, _organizationId, Id);
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

        #endregion

        #region Project Stage
        [HttpPost("create-project-stage")]
        public async Task<IActionResult> CreateProjectState([FromBody] ProjectStatgeDto model)
        {
            var command = new CreateProjectStageCommand(_organizationId, model);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("project-stage-update/{id}")]
        public async Task<IActionResult> UpdateProjectState([FromBody] ProjectStatgeDto model, [FromRoute] Guid id)
        {
            var command = new UpdateProjectStageCommand(_organizationId, model, id);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("project-stage-delete/{id}")]
        public async Task<IActionResult> DeleteProjectState([FromRoute] Guid id)
        {
            var command = new DeleteProjectStageCommand(_organizationId, id);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("project-stages")]
        public async Task<IActionResult> GetProjectStage()
        {
            var command = new GetProjectStageListQuery(_organizationId);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }
        #endregion



        #region Sourcs
        [HttpGet("sources")]
        public async Task<IActionResult> GetProspectSourceList()
        {
            var command = new GetSourceListQuery();
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }
        #endregion



        #region Sourcs
        [HttpGet("temperatures")]
        public async Task<IActionResult> GetTemperaturesList()
        {
            var command = new GetTemperaturesListQuery();
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }
        #endregion
    }
}
