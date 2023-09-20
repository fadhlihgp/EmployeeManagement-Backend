using EmployeeManagement_Backend.Entities;
using EmployeeManagement_Backend.Exceptions;
using EmployeeManagement_Backend.Helpers;
using EmployeeManagement_Backend.Repositories;
using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Services.Impl;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<TR_EmployeeProject> _trEmployeeProjectRepository;
    private readonly IPersistence _persistence;

    public EmployeeService(IRepository<Employee> employeeRepository, IPersistence persistence, IRepository<TR_EmployeeProject> trEmployeeProjectRepository)
    {
        _employeeRepository = employeeRepository;
        _persistence = persistence;
        _trEmployeeProjectRepository = trEmployeeProjectRepository;
    }

    // ======================== Create new employee =================
    public async Task CreateEmployee(string companyId, EmployeeCreateRequestDto createRequestDto)
    {
        var employeeIdentity = await _employeeRepository.Find(e =>
            e.IdentityNumber.Equals(createRequestDto.IdentityNumber) && e.CompanyId.Equals(companyId) && !e.IsDeleted);
        if (employeeIdentity != null) throw new BadRequestException("Terdapat duplikat nomor pokok karyawan");
        
        var employeeSave = new Employee
        {
            IdentityNumber = createRequestDto.IdentityNumber,
            Name = createRequestDto.Name,
            Address = createRequestDto.Address,
            PhoneNumber = createRequestDto.PhoneNumber,
            BirthDate = createRequestDto.BirthDate,
            ImageUrl = createRequestDto.ImageUrl,
            IsActive = createRequestDto.IsActive,
            JoinDate = createRequestDto.JoinDate,
            CompanyId = companyId
        };

        try
        {
            await _persistence.BeginTransactionAsync();
            await _employeeRepository.Save(employeeSave);
            await _persistence.SaveChangesAsync();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw new Exception(e.Message);
        }
    }

    // ======================== List Employee =================
    public async Task<IEnumerable<EmployeeResponseDto>> ListEmployee(string companyId)
    {
        var employees =
            await _employeeRepository.FindAll(e => e.CompanyId.Equals(companyId) && !e.IsDeleted);

        IEnumerable<EmployeeResponseDto> results = employees.Select(e => new EmployeeResponseDto
        {
            Id = e.Id,
            IdentityNumber = e.IdentityNumber,
            Name = e.Name,
            Address = e.Address,
            PhoneNumber = e.PhoneNumber,
            BirthDate = e.BirthDate,
            ImageUrl = e.ImageUrl,
            IsActive = e.IsActive,
            JoinDate = e.JoinDate,
            Projects = null
        });

        return results;
    }

    public async Task<IEnumerable<EmployeeResponseDto>>? ListEmployeeByProjectId(string projectId)
    {
        var employees = await _trEmployeeProjectRepository
            .FindAll(tr => tr.ProjectId.Equals(projectId) && !tr.Employee.IsDeleted, new []{"Employee"});
        IEnumerable<EmployeeResponseDto>? results = employees.Select(tr => new EmployeeResponseDto
        {
            TrId = tr.Id,
            Id = tr.Employee.Id,
            IdentityNumber = tr.Employee.IdentityNumber,
            Name = tr.Employee.Name,
            Address = tr.Employee.Address,
            PhoneNumber = tr.Employee.PhoneNumber,
            BirthDate = tr.Employee.BirthDate,
            ImageUrl = tr.Employee.ImageUrl,
            IsActive = tr.Employee.IsActive,
            JoinDate = tr.Employee.JoinDate,
        });
        return results;
    }
    // ======================== Find employee detail with project details ========================
    public async Task<EmployeeResponseDto> GetEmployeeById(string employeeId)
    {
        var employee = await _employeeRepository.Find(e => e.Id.Equals(employeeId) && !e.IsDeleted);
        if (employee == null) throw new NotFoundException(DataProperties.NotFoundMessage);
        
        var trEmpProjects = await _trEmployeeProjectRepository.FindAll(tr => tr.EmployeeId.Equals(employeeId),
            new[] { "Employee", "Project", "Project.ProjectStatus"});

        var projectResponses = trEmpProjects.Select(tr => new ProjectResponseDto
        {
            Id = tr.Project.Id,
            ProjectCode = tr.Project.ProjectCode,
            ProjectName = tr.Project.ProjectName,
            ClientName = tr.Project.ClientName,
            Description = tr.Project.Description,
            StartDate = tr.Project.StartDate,
            EndDate = tr.Project.EndDate,
            LongDay = tr.Project.LongDay,
            ProjectStatusId = tr.Project.ProjectStatus.Id,
            ProjectStatusName = tr.Project.ProjectStatus.Name
        });

        EmployeeResponseDto responseDto = new EmployeeResponseDto
        {
            Id = employee.Id,
            IdentityNumber = employee.IdentityNumber,
            Name = employee.Name,
            Address = employee.Address,
            PhoneNumber = employee.PhoneNumber,
            BirthDate = employee.BirthDate,
            ImageUrl = employee.ImageUrl,
            IsActive = employee.IsActive,
            JoinDate = employee.JoinDate,
            Projects = projectResponses
        };

        return responseDto;
    }

    // ======================== update employee ========================
    public async Task UpdateEmployee(string employeeId, EmployeeCreateRequestDto employeeCreateRequestDto)
    {
        var employee = await _employeeRepository.FindById(employeeId);
        if (employee == null) throw new NotFoundException(DataProperties.NotFoundMessage);

        // validasi dalam satu perusahaan tidak boleh ada lebih dari satu Identity Employee Number
        var empIdentityValidate = await _employeeRepository.Find(e =>
            e.IdentityNumber.Equals(employeeCreateRequestDto.IdentityNumber) &&
            e.CompanyId.Equals(employee.CompanyId) && !e.Id.Equals(employeeId));
        if (empIdentityValidate != null)
            throw new BadRequestException("Tidak dapat menggunakan nomor karyawan yang telah atau pernah digunakan sebelumnya");
        
        // update employee
        employee.Address = employeeCreateRequestDto.Address;
        employee.Name = employeeCreateRequestDto.Name;
        employee.BirthDate = employeeCreateRequestDto.BirthDate;
        employee.IdentityNumber = employeeCreateRequestDto.IdentityNumber;
        employee.ImageUrl = employeeCreateRequestDto.ImageUrl;
        employee.IsActive = employeeCreateRequestDto.IsActive;
        employee.JoinDate = employeeCreateRequestDto.JoinDate;
        employee.PhoneNumber = employeeCreateRequestDto.PhoneNumber;

        try
        {
            await _persistence.BeginTransactionAsync();
            _employeeRepository.Update(employee);
            await _persistence.SaveChangesAsync();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw new Exception(e.Message);
        }
    }

    // ======================== Delete employee ========================
    public async Task DeleteEmployee(string employeeId)
    {
        var employee = await _employeeRepository.FindById(employeeId);
        if (employee == null) throw new NotFoundException(DataProperties.NotFoundMessage);

        employee.IsDeleted = true;
        _employeeRepository.Update(employee);
        await _persistence.SaveChangesAsync();

    }
}