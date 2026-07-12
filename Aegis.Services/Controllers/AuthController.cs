using Aegis.Model.DTO.Auth;
using Aegis.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Aegis.Services.Controllers;


[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var response = await _authService.LoginAsync(login);

        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto register)
    {
        var response = await _authService.RegisterAsync(register);

        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }



}