using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Services;

public interface ITrEmpProjectService
{
    Task SaveBatch(IEnumerable<TR_EmpProjectRequestDto> trEmpProjects);
    Task<IEnumerable<EmployeeResponseDto>>? ListEmployeeByProjectId(string projectId);
    Task<IEnumerable<ProjectResponseDto>> ListProjectsByEmployeeId(string employeeId);
    Task DeleteEmpProjectById(string trEmpProjectId);
    Task CreateTrEmpProject(string projectId, TR_EmpProjectRequestDto trEmpProjectDto);
}