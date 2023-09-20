using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Services;

public interface IProjectService
{
    Task CreateProject(string companyId, ProjectRequestDto requestDto);
    Task<IEnumerable<ProjectResponseDto>> ListProjects(string companyId);
    Task<ProjectResponseDto> GetProjectById(string companyId);
    Task DeleteProject(string projectId);
    Task UpdateProject(string projectId, ProjectRequestDto projectRequestDto);
    // Task<IEnumerable<ProjectResponseDto>> ListProjectByEmployee(string employeeId);
    Task DeleteEmpProjectById(string trEmpProjectId);
    Task AssignProjectToEmployee(string projectId, TR_EmpProjectRequestDto requestDto);
}