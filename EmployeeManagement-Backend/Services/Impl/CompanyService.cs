using EmployeeManagement_Backend.Entities;
using EmployeeManagement_Backend.Exceptions;
using EmployeeManagement_Backend.Helpers;
using EmployeeManagement_Backend.Repositories;
using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Services.Impl;

public class CompanyService : ICompanyService
{
    private readonly IRepository<Company> _companyRepository;
    private readonly IPersistence _persistence;

    public CompanyService(IRepository<Company> companyRepository, IPersistence persistence)
    {
        _companyRepository = companyRepository;
        _persistence = persistence;
    }

    // ======================= Company profile only owner can see =======================
    public async Task<CompanyResponseDto> GetMyCompany(string roleId, string companyId)
    {
        if (!roleId.Equals("2")) throw new UnauthorizedException(DataProperties.UnauthorizedMessage);
        
        var company = await _companyRepository.FindById(companyId);
        if (company == null) throw new NotFoundException(DataProperties.NotFoundMessage);

        var companyResponseDto = new CompanyResponseDto
        {
            Id = company.Id,
            Name = company.Name,
            Email = company.Email,
            PhoneNumber = company.PhoneNumber,
            Address = company.Address
        };
        return companyResponseDto;
    }

    // ========================== Only super admin can see company =========================
    public async Task<IEnumerable<CompanyResponseDto>> ListCompany(string roleId)
    {
        if (!roleId.Equals("1")) throw new UnauthorizedException(DataProperties.UnauthorizedMessage);
        
        var companies = await _companyRepository.FindAll();
        IEnumerable<CompanyResponseDto> result = companies.Select(c => new CompanyResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
            Address = c.Address
        });
        return result;
    }

    // ========================== Update company =========================
    public async Task UpdateCompany(CompanyUpdateDto companyUpdateDto)
    {
        var company = await _companyRepository.FindById(companyUpdateDto.Id);
        if (company == null) throw new NotFoundException(DataProperties.NotFoundMessage);

        try
        {
            await _persistence.BeginTransactionAsync();
            company.Email = companyUpdateDto.Email;
            company.PhoneNumber = companyUpdateDto.PhoneNumber;
            company.Name = companyUpdateDto.Name;
            company.Address = companyUpdateDto.Address;
            _companyRepository.Update(company);
            await _persistence.SaveChangesAsync();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw new Exception(e.Message);
        }
    }

    // ========================== Get company =========================
    public async Task<CompanyResponseDto> GetCompanyById(string companyId)
    {
        var company = await _companyRepository.FindById(companyId);
        if (company == null) throw new NotFoundException(DataProperties.NotFoundMessage);

        return new CompanyResponseDto
        {
            Id = company.Id,
            Name = company.Name,
            Email = company.Email,
            PhoneNumber = company.PhoneNumber,
            Address = company.Address
        };
    }
}