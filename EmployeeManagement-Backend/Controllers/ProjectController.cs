using EmployeeManagement_Backend.Helpers;
using EmployeeManagement_Backend.Services;
using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_Backend.Controllers;

[ApiController]
[Authorize]
[Route("api/project")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] ProjectRequestDto requestDto)
    {
        var companyId = User.FindFirst("CompanyId")?.Value;
        await _projectService.CreateProject(companyId, requestDto);
        return Created("api/project", new SuccessResponseDto
        {
            StatusCode = 201,
            Message = DataProperties.SuccessCreateMessage
        });
    }

    [HttpGet]
    public async Task<IActionResult> ListProjects()
    {
        var companyId = User.FindFirst("CompanyId")?.Value;
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessGetMessage,
            Data = await _projectService.ListProjects(companyId)
        });
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetProjectById([FromRoute] string projectId)
    {
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessGetMessage,
            Data = await _projectService.GetProjectById(projectId)
        });
    }

    [HttpPut("delete/{projectId}")]
    public async Task<IActionResult> DeleteProjectById([FromRoute] string projectId)
    {
        await _projectService.DeleteProject(projectId);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessDeleteMessage
        });
    }

    [HttpPut("update/{projectId}")]
    public async Task<IActionResult> UpdateProject([FromRoute] string projectId, [FromBody] ProjectRequestDto projectRequestDto)
    {
        await _projectService.UpdateProject(projectId, projectRequestDto);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessUpdateMessage
        });
    }

    [HttpDelete("delete/tr/{id}")]
    public async Task<IActionResult> DeleteTrEmpProject([FromRoute] string id)
    {
        await _projectService.DeleteEmpProjectById(id);
        return Ok(new SuccessResponseDto
        {
            StatusCode = 200,
            Message = DataProperties.SuccessDeleteMessage
        });
    }

    [HttpPost("assignToEmployee/{projectId}")]
    public async Task<IActionResult> AssignToEmployee([FromRoute] string projectId,
        [FromBody] TR_EmpProjectRequestDto requestDto)
    {
        await _projectService.AssignProjectToEmployee(projectId, requestDto);
        return Created("api/project/assignToEmployee",new SuccessResponseDto
        {
            StatusCode = 201,
            Message = DataProperties.SuccessCreateMessage
        });
    }
    
}