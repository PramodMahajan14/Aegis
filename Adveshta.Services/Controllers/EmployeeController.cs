using Adveshta.Model.DTO.Employee;
using Adveshta.Services.Features.EmployeeManagement;
using Adveshta.Services.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Adveshta.Services.Controllers.EmployeeController
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
        [HttpPut("update-employee/{id}")]
        public async Task<IActionResult> UpdateEmployeeAsync(
            Guid id,
            [FromBody] EmployeeDto model,
            CancellationToken cancellationToken)
        {
            var organizationId = _helper.GetCurrentTenant();
            var projectId = Guid.Empty; // Assuming project is not mandatory or handled differently
            var command = new UpdateEmployee.UpdateEmployeeCommand(organizationId, id, model, projectId);
            var response = await _mediator.Send(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("delete-employee/{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var organizationId = _helper.GetCurrentTenant();
            var command = new DeleteEmployee.DeleteEmployeeCommand(organizationId, id);
            var response = await _mediator.Send(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("get-employee/{id}")]
        public async Task<IActionResult> GetEmployeeAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var organizationId = _helper.GetCurrentTenant();
            var query = new GetEmployeeDetails.GetEmployeeDetailsQuery(organizationId, id);
            var response = await _mediator.Send(query, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("get-employees")]
        public async Task<IActionResult> GetEmployeesAsync(
            CancellationToken cancellationToken)
        {
            var organizationId = _helper.GetCurrentTenant();
            var query = new GetEmployeeList.GetEmployeeListQuery(organizationId);
            var response = await _mediator.Send(query, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}