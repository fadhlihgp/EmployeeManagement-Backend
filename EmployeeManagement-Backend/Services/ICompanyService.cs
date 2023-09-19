using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Services;

public interface ICompanyService
{
    Task<CompanyResponseDto> GetMyCompany(string roleId, string companyId);
    Task<IEnumerable<CompanyResponseDto>> ListCompany(string roleId);
    Task UpdateCompany(CompanyUpdateDto companyUpdateDto);
    Task<CompanyResponseDto> GetCompanyById(string companyId);
}