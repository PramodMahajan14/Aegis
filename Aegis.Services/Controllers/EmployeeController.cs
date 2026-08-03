
using Aegis.Model.DTO.Employee;
using Aegis.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aegis.Services.Controllers
{  
    
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    { 
        private readonly IEmployee _employee;
        public EmployeeController(IEmployee employee)
        {
           _employee = employee;   
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateEMployee([FromBody] EmployeeDto model)
        {
            var response = await _employee.CreateEmployee(model);

            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto model)
        {
            var response = await _employee.UpdateEmployee(model);

            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}