using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aegis.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProspectsController : ControllerBase
    {
        
    }
}