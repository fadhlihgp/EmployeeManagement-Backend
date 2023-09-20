using EmployeeManagement_Backend.Entities;
using EmployeeManagement_Backend.Helpers;
using EmployeeManagement_Backend.Services;
using EmployeeManagement_Backend.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_Backend.Controllers;

[ApiController]
[Authorize]
[Route("api/attendanceCode")]
public class AttendanceCodeController : ControllerBase
{
    private readonly IAttendanceCodeService _codeService;

    public AttendanceCodeController(IAttendanceCodeService codeService)
    {
        _codeService = codeService;
    }

    [HttpGet]
    public async Task<IActionResult> ListAttendanceCode()
    {
        var roleId = User.FindFirst("RoleId")?.Value;
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessGetMessage,
            Data = await _codeService.ListAttendanceCodes()
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAttendanceCcode([FromRoute] string id)
    {
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessGetMessage,
            Data = await _codeService.GetAttendanceCodeById(id)
        });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAttendanceCode([FromBody] AttendanceCode attendanceCode)
    {
        await _codeService.UpdateAttendanceCode(attendanceCode);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessUpdateMessage,
            Data = null
        });
    }

    [HttpPost("{name}")]
    public async Task<IActionResult> CreateAttendanceCode([FromRoute] string name)
    {
        await _codeService.CreateAttendanceCode(name);
        return Created("api/attendanceCode", new SuccessResponseDto
        {
            StatusCode = 201,
            Message = DataProperties.SuccessCreateMessage,
            Data = null
        });
    }
}
