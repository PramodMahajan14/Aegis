using Aegis.Model.DTO.Prospect;
using Aegis.Services.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aegis.Services.Controllers
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



        [HttpPost]
        public async Task<IActionResult> CreateProspect([FromBody] ManageProspectDto model)
        {
            var loggedEmployee = await _userHelper.GetCurrentEmployeeAsync();

            
        }
    }
}