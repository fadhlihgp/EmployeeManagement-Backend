using System.Collections;
using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Services;

public interface IProjectStatusService
{
    Task<IEnumerable<ProjectStatusResponseDto>> ListProjectStatus();
}