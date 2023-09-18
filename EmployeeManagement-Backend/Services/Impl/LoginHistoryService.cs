using EmployeeManagement_Backend.Entities;
using EmployeeManagement_Backend.Exceptions;
using EmployeeManagement_Backend.Helpers;
using EmployeeManagement_Backend.Repositories;
using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Services.Impl;

public class LoginHistoryService : ILoginHistoryService
{
    private readonly IRepository<LoginHistory> _repository;
    private readonly IPersistence _persistence;
    
    public LoginHistoryService(IRepository<LoginHistory> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }

    public async Task<IEnumerable<LoginHistoryDto>> ListLoginHistory(string roleId, string companyId)
    {
        IEnumerable<LoginHistory> loginHistories = new List<LoginHistory>();
        
        switch (roleId)
        {
            case "1" :
                loginHistories = await _repository
                    .FindAll(new[] { "Account", "Account.Role", "Account.Company" });
                break;
            case "2" :
                loginHistories = await _repository
                    .FindAll(l => l.Account.CompanyId.Equals(companyId),new[] { "Account", "Account.Role", "Account.Company" });
                break;
            default:
                throw new UnauthorizedException(DataProperties.UnauthorizedMessage);
        }

        IEnumerable<LoginHistoryDto> results = loginHistories.Select(l => new LoginHistoryDto
        {
            Id = l.Id,
            Name = l.Account.FullName,
            CompanyName = l.Account.Company?.Name ?? "Super Admin",
            RoleName = l.Account.Role.Name,
            LastLogin = l.LastLogin
        }).ToList();

        return results;
    }
}