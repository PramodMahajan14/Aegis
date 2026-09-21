using Adveshta.Model.DTO.Prospect;
using Adveshta.Model.EmployeeModels;
using Adveshta.Services.Features.MasterManagement;
using Adveshta.Services.Features.ProspectManagement;
using Adveshta.Services.Features.ProspectManagement.CreateProspect;
using Adveshta.Services.Features.ProspectManagement.GetProspectDetails;
using Adveshta.Services.Features.ProspectManagement.ProspectList;
using Adveshta.Services.Features.ProspectManagement.UpdateStatusOrTemp;
using Adveshta.Services.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Adveshta.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProspectsController : ControllerBase
    {
        private readonly Guid _organizationId;
        private readonly IMediator _mediator;
        private readonly UserHelper _userHelper;

        public ProspectsController(IMediator mediator, UserHelper userHelper)
        {
            _userHelper = userHelper;
            _mediator = mediator;
            _organizationId = _userHelper.GetCurrentTenant();
        }

        /// <summary>
        /// Create a new prospect.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateProspect([FromBody] ManageProspectDto model)
        {
            var loggedEmployee = await _userHelper.GetCurrentEmployeeAsync();

            var command = new CreateProspectCommand(_organizationId, loggedEmployee, model);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }


        /// <summary>
        /// Create a new prospect.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateProspect([FromBody] ManageProspectDto model)
        {
            var loggedEmployee = await _userHelper.GetCurrentEmployeeAsync();

            var command = new UpdateProspectCommand(_organizationId, loggedEmployee, model);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Create a new prospect.
        /// </summary>
        [HttpGet("list")]
        public async Task<IActionResult> GetProspects()
        {
            var command = new ProspectListQuery(_organizationId);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProspectDetails([FromRoute] Guid Id)
        {
            var LoggedEmployee = await _userHelper.GetCurrentEmployeeAsync();
            var command = new GetProspectDetailsQeury(_organizationId, LoggedEmployee, Id);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }



        [HttpPatch("{Id}")]
        public async Task<IActionResult> UpdateStatusOrTemp([FromRoute] Guid Id, [FromBody] JsonPatchDocument<ProspectPatchDto> patchd)
        {
            var LoggedEmployee = await _userHelper.GetCurrentEmployeeAsync();
            var command = new UpdateStatusOrTempCommand(Id, patchd, _organizationId, LoggedEmployee);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }
    }
}