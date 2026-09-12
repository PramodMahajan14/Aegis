using Adveshta.Model.DTO.Prospect;
using Adveshta.Services.Features.ProspectManagement;
using Adveshta.Services.Features.ProspectManagement.CreateProspect;
using Adveshta.Services.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    }
}