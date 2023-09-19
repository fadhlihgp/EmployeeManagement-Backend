using EmployeeManagement_Backend.Helpers;
using EmployeeManagement_Backend.Services;
using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_Backend.Controllers;

[ApiController]
[Route("api/account")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto createAccountDto)
    {
        var companyId = User.FindFirst("CompanyId")?.Value;

        await _accountService.CreateAccount(createAccountDto, companyId);
        return Created("api/account/create", new SuccessResponseDto
        {
            StatusCode = 201,
            Message = DataProperties.SuccessCreateMessage,
            Data = null
        });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAccount()
    {
        var accountId = User.FindFirst("AccountId")?.Value;
        var roleId = User.FindFirst("RoleId")?.Value;

        await _accountService.DeleteAccount(accountId, roleId);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessDeleteMessage,
            Data = null
        });
    }

    [HttpPut("activate-disable/{accountId}")]
    public async Task<IActionResult> ActivateDisable([FromRoute] string accountId)
    {
        await _accountService.ActivateDisableAccount(accountId);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessUpdateMessage,
            Data = null
        });
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListAccount()
    {
        var companyId = User.FindFirst("CompanyId")?.Value;
        var roleId = User.FindFirst("RoleId")?.Value;

        var listAccounts = await _accountService.ListAccounts(roleId, companyId);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessGetMessage,
            Data = listAccounts
        });
    }

    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetAccountById([FromRoute] string accountId)
    {
        var accountResponseDto = await _accountService.GetAccountById(accountId);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessGetMessage,
            Data = accountResponseDto
        });
    }

    [HttpPut("resetPassword/{accountId}")]
    public async Task<IActionResult> ResetPassword([FromRoute] string accountId)
    {
        var roleId = User.FindFirst("RoleId")?.Value;

        await _accountService.ResetPassword(accountId, roleId);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = "Password berhasil di reset, jangan lupa untuk mengganti password segera",
            Data = null
        });
    }

    [HttpPut("changePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto changePasswordRequestDto)
    {
        await _accountService.ChangePassword(changePasswordRequestDto);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = "Password berhasil di update",
            Data = null
        });
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetMyProfile()
    {
        var accountId = User.FindFirst("AccountId")?.Value;
        var account = await _accountService.GetMyProfile(accountId);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessGetMessage,
            Data = account
        });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateMyProfile([FromBody] AccountUpdateDto accountUpdateDto)
    {
        var accountId = User.FindFirst("AccountId")?.Value;
        await _accountService.UpdateProfile(accountId, accountUpdateDto);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessUpdateMessage,
            Data = null
        });
    }
}