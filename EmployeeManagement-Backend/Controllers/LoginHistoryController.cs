using EmployeeManagement_Backend.Helpers;
using EmployeeManagement_Backend.Services;
using EmployeeManagement_Backend.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_Backend.Controllers;

[ApiController]
[Authorize]
[Route("api/loginHistory")]
public class LoginHistoryController : ControllerBase
{
    private readonly ILoginHistoryService _loginHistoryService;

    public LoginHistoryController(ILoginHistoryService loginHistoryService)
    {
        _loginHistoryService = loginHistoryService;
    }

    [HttpGet]
    public async Task<IActionResult> LoginHistories()
    {
        var roleId = User.FindFirst("RoleId")?.Value;
        var companyId = User.FindFirst("CompanyId")?.Value;
        var listLoginHistory = await _loginHistoryService.ListLoginHistory(roleId, companyId);

        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessGetMessage,
            Data = listLoginHistory
        });
    }
}