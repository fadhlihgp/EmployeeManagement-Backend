using EmployeeManagement_Backend.Helpers;
using EmployeeManagement_Backend.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_Backend.Services.Impl;

[ApiController]
[Authorize]
[Route("api/projectStatus")]
public class ProjectStatusController : ControllerBase
{
    private readonly IProjectStatusService _projectStatusService;

    public ProjectStatusController(IProjectStatusService projectStatusService)
    {
        _projectStatusService = projectStatusService;
    }

    [HttpGet]
    public async Task<IActionResult> ListProjectStatus()
    {
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessGetMessage,
            Data = await _projectStatusService.ListProjectStatus()
        });
    }
}