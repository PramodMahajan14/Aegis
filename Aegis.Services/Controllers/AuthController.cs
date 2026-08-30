using Aegis.Model.DTO;
using Aegis.Model.DTO.Auth;
using Aegis.Services.Helper;
using Aegis.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Aegis.Services.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{

    private readonly IAuthService _authService;
    public readonly UserHelper _helper;

    public AuthController(IAuthService authService, UserHelper helper)
    {
        _authService = authService;
        _helper = helper;
    }



    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {
        var response = await _authService.Profile();

        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
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


    [Authorize]
    [HttpGet("get-workspace")]
    public async Task<IActionResult> WorkSpace()
    {


        var user = await _helper.GetCurrentUserAsync();

        if (user == null)
        {
            return BadRequest("User not found");
        }
        var response = await _authService.GetWorkSpacesAsync(user.Id);

        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [Authorize]
    [HttpPost("select-workspace")]
    public async Task<IActionResult> SelectWorkSpace([FromBody] WorkSpaceSelectDto model)
    {
     
       if(model == null)
        {
            return BadRequest("Invalid request");
        }
        var user = await _helper.GetCurrentUserAsync();

        if (user == null)
        {
            return BadRequest("User not found");
        }
        var response = await _authService.SelectWorkSpacesAsync(model.WorkspaceId,user);

        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }


    // [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenDto model)
    {

        if(model.RefreshToken == null)
        {
             return BadRequest("Invalid not found");
        }
     
        var response = await _authService.RefreshToken(model.RefreshToken);

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