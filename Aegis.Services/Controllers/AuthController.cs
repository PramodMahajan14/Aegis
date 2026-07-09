using Microsoft.AspNetCore.Mvc;


namespace Aegis.Services.Controllers;


[ApiController]
[Route("api/[controller]")]

public class AuthController  : ControllerBase
{
    
    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        await Task.CompletedTask;

        return Ok(new
        {
            success = true,
            message = "Login successful."
        });
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(){
         await Task.CompletedTask;

        return Ok(new
        {
            success = true,
            message = "Login successful."
        });
    }



}