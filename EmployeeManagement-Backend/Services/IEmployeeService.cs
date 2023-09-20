using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Services;

public interface IEmployeeService
{
    Task CreateEmployee(string companyId, EmployeeCreateRequestDto createRequestDto);
    Task<IEnumerable<EmployeeResponseDto>> ListEmployee(string companyId);
    Task<IEnumerable<EmployeeResponseDto>>? ListEmployeeByProjectId(string projectId);
    Task<EmployeeResponseDto> GetEmployeeById(string employeeId);
    Task UpdateEmployee(string employeeId, EmployeeCreateRequestDto employeeCreateRequestDto);
    Task DeleteEmployee(string employeeId);
}