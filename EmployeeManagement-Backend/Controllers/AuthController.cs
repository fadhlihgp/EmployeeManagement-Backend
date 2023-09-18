using EmployeeManagement_Backend.Services;
using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_Backend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var loginResponseDto = await _authService.Login(loginRequestDto);
        return Ok(new SuccessResponseDto { StatusCode = 200, IsSuccess = true, Message = "Login berhasil", Data = loginResponseDto });
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAccount([FromBody] RegisterRequestDto registerRequestDto)
    {
        await _authService.RegisterAccount(registerRequestDto);
        return Created("api/auth/register", new SuccessResponseDto
        {
            StatusCode = 201,
            IsSuccess = true,
            Message = "Berhasil membuat akun"
        });
    }
}