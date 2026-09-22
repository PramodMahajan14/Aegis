using Adveshta.Services.Helper;
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

        private readonly UserHelper _helper;
        private readonly IMediator _mediator;
        private readonly Guid OrganizationId;
        public ContactController(UserHelper helper, IMediator mediator)
        {
            _helper = helper;
            _mediator = mediator;
            OrganizationId = _helper.GetCurrentTenant();
        }




        [HttpPost]
        public async Task<IActionResult> CreatedContact()
        {
            var LoggedEmployee = await _helper.GetCurrentEmployeeAsync();

        }
    }
}
