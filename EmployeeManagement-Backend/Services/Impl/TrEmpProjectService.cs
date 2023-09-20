using System.Collections;
using EmployeeManagement_Backend.Entities;
using EmployeeManagement_Backend.Exceptions;
using EmployeeManagement_Backend.Helpers;
using EmployeeManagement_Backend.Repositories;
using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Services.Impl;

public class TrEmpProjectService : ITrEmpProjectService
{
    private readonly IRepository<TR_EmployeeProject> _repository;
    private readonly IPersistence _persistence;

    public TrEmpProjectService(IRepository<TR_EmployeeProject> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }

    // =================== Method ketika add project, maka table ini terisi bedasarkan employee yg terlibat dalam project ============
    public async Task SaveBatch(IEnumerable<TR_EmpProjectRequestDto> trEmpProjects)
    {
        // IEnumerable<TR_EmployeeProject> saveList = trEmpProjects.Select(tr => new TR_EmployeeProject
        // {
        //     ProjectId = tr.ProjectId,
        //     EmployeeId = tr.EmployeeId,
        // });
        // try
        // {
        //     await _repository.SaveAll(saveList);
        //     await _persistence.SaveChangesAsync();
        // }
        // catch (Exception e)
        // {
        //     throw new Exception(e.Message);
        // }
        throw new NotImplementedException();

    }

    // ========================== List Employee berdasarkan project =========================
    public async Task<IEnumerable<EmployeeResponseDto>>? ListEmployeeByProjectId(string projectId)
    {
        var employees = await _repository
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

    // ========================== List project berdasarkan employee =================
    public async Task<IEnumerable<ProjectResponseDto>>? ListProjectsByEmployeeId(string employeeId)
    {
        var projects = await _repository.FindAll(tr => tr.EmployeeId.Equals(employeeId) && !tr.Project.IsDeleted,
            new[] { "Project", "Project.ProjectStatus" });
        IEnumerable<ProjectResponseDto>? results = projects.Select(tr => new ProjectResponseDto
        {
            Id = tr.Project.Id,
            ProjectCode = tr.Project.ProjectCode,
            ProjectName = tr.Project.ProjectName,
            ClientName = tr.Project.ClientName,
            Description = tr.Project.Description,
            StartDate = tr.Project.StartDate,
            EndDate = tr.Project.EndDate,
            LongDay = tr.Project.LongDay,
            ProjectStatusId = tr.Project.ProjectStatusId,
            ProjectStatusName = tr.Project.ProjectStatus.Name
        });
        return results;
    }

    // =========================== Delete =========================
    public async Task DeleteEmpProjectById(string trEmpProjectId)
    {
        var tr = await _repository.FindById(trEmpProjectId);
        if (tr == null) throw new NotFoundException(DataProperties.NotFoundMessage);

        try
        {
            await _persistence.BeginTransactionAsync();
            _repository.Delete(tr);
            await _persistence.SaveChangesAsync();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw new Exception();
        }
    }

    public async Task CreateTrEmpProject(string projectId, TR_EmpProjectRequestDto trEmpProjectDto)
    {
        var trEmpProject = new TR_EmployeeProject
        {
            ProjectId = projectId,
            EmployeeId = trEmpProjectDto.EmployeeId,
        };

        try
        {
            await _persistence.BeginTransactionAsync();
            await _repository.Save(trEmpProject);
            await _persistence.SaveChangesAsync();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw new Exception(e.Message);
        }
    }
}