using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Services;

public interface ILoginHistoryService
{
    Task<IEnumerable<LoginHistoryDto>> ListLoginHistory(string roleId, string companyId);
}