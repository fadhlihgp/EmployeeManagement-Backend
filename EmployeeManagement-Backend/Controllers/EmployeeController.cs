using EmployeeManagement_Backend.Helpers;
using EmployeeManagement_Backend.Services;
using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_Backend.Controllers;

[ApiController]
[Authorize]
[Route("api/employee")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateRequestDto employeeCreateRequestDto)
    {
        var companyId = User.FindFirst("CompanyId")?.Value;
        await _employeeService.CreateEmployee(companyId, employeeCreateRequestDto);
        return Created("api/employee/creare", new SuccessResponseDto
        {
            StatusCode = 201,
            Message = DataProperties.SuccessCreateMessage,
            Data = null
        });
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListEmployee()
    {
        var companyId = User.FindFirst("CompanyId")?.Value;
        var employeeResponseDtos = await _employeeService.ListEmployee(companyId);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessGetMessage,
            Data = employeeResponseDtos
        });
    }

    [HttpGet("tr/{projectId}")]
    public async Task<IActionResult> ListEmployeeByProjectId([FromRoute] string projectId)
    {
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessGetMessage,
            Data = await _employeeService.ListEmployeeByProjectId(projectId)
        });
    }
    
    [HttpGet("{employeeId}")]
    public async Task<IActionResult> GetEmployeeById([FromRoute] string employeeId)
    {
        var employee = await _employeeService.GetEmployeeById(employeeId);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessGetMessage,
            Data = employee
        });
    }

    [HttpPut("{employeeId}")]
    public async Task<IActionResult> UpdateEmployee([FromRoute] string employeeId,
        [FromBody] EmployeeCreateRequestDto requestDto)
    {
        await _employeeService.UpdateEmployee(employeeId, requestDto);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessUpdateMessage
        });
    }

    [HttpPost("delete/{employeeId}")]
    public async Task<IActionResult> DeleteEmployee([FromRoute] string employeeId)
    {
        await _employeeService.DeleteEmployee(employeeId);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessDeleteMessage
        });
    }
}