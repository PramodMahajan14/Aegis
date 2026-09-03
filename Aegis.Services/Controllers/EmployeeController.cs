using Aegis.Model.DTO.Employee;
using Aegis.Services.Features.EmployeeManagement;
using Aegis.Services.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aegis.Services.Controllers.EmployeeController
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly UserHelper _helper;
        private readonly IMediator _mediator;

        public EmployeeController(
            UserHelper helper,
            IMediator mediator)
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateEmployeeAsync(
            [FromBody] EmployeeDto model,
            CancellationToken cancellationToken)
        {
            // Get current organization from logged-in user's context
            var organizationId = _helper.GetCurrentTenant();

            // Get currently logged-in employee
            var currentEmployee =
                await _helper.GetCurrentEmployeeAsync();


            // Create MediatR command
            var command =
                new CreateEmployee.CreateEmployeeCommand(
                    organizationId,
                    model,
                    currentEmployee);


            // Send command to handler
            var response =
                await _mediator.Send(
                    command,
                    cancellationToken);


            return StatusCode(
                response.StatusCode,
                response);
        }
    }
}