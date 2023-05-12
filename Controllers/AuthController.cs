using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Auth_Crud.Dtos;
using Auth_Crud.Dtos.Requests;
using Auth_Crud.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Auth_Crud.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest registerRequest)
    {
        var registerAdmin = await _authService.Register(registerRequest);
        var response = new CommonResponse<RegisterResponse>
        {
            Message = "Admin Created",
            Data = registerAdmin
        };

        return Created("/auth/register", response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAdmin([FromBody] LoginRequest loginRequest)
    {
        var login = await _authService.Login(loginRequest);
        var response = new CommonResponse<LoginResponse>
        {
            Message = "Successfully Login",
            Data = login
        };

        return Ok(response);
    }
}