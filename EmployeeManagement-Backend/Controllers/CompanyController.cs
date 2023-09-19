using EmployeeManagement_Backend.Helpers;
using EmployeeManagement_Backend.Services;
using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_Backend.Controllers;

[ApiController]
[Route("api/company")]
[Authorize]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetMyCompany()
    {
        var roleId = User.FindFirst("RoleId")?.Value;
        var companyId = User.FindFirst("CompanyId")?.Value;

        var companyResponseDto = await _companyService.GetMyCompany(roleId, companyId);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessGetMessage,
            Data = companyResponseDto
        });
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListMyCompany()
    {
        var roleId = User.FindFirst("RoleId")?.Value;

        var companies = await _companyService.ListCompany(roleId);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessGetMessage,
            Data = companies
        });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateCompany([FromBody] CompanyUpdateDto companyUpdateDto)
    {
        await _companyService.UpdateCompany(companyUpdateDto);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessUpdateMessage,
            Data = null
        });
    }

    [HttpGet("{companyId}")]
    public async Task<IActionResult> GetCompanyById([FromRoute] string companyId)
    {
        var company = await _companyService.GetCompanyById(companyId);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessGetMessage,
            Data = company
        });
    }
}