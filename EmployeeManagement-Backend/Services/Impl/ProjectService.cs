using EmployeeManagement_Backend.Entities;
using EmployeeManagement_Backend.Exceptions;
using EmployeeManagement_Backend.Helpers;
using EmployeeManagement_Backend.Repositories;
using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Services.Impl;

public class ProjectService : IProjectService
{
    private readonly IRepository<Project> _projectRepository;
    private readonly ITrEmpProjectService _trEmpProjectService;
    private readonly IPersistence _persistence;

    public ProjectService(IRepository<Project> projectRepository, IPersistence persistence, ITrEmpProjectService trEmpProjectService)
    {
        _projectRepository = projectRepository;
        _persistence = persistence;
        _trEmpProjectService = trEmpProjectService;
    }

    // ========================== Create Project =========================
    public async Task CreateProject(string companyId, ProjectRequestDto requestDto)
    {
        var projectFind = await _projectRepository.Find(p =>
            p.ProjectCode.Equals(requestDto.ProjectCode) && p.CompanyId.Equals(companyId) && !p.IsDeleted);
        if (projectFind != null) throw new BadRequestException("Tidak boleh terdapat project dengan kode yang sama");

        TimeSpan duration = requestDto.EndDate - requestDto.StartDate;
        var projectId = Guid.NewGuid().ToString();
        var projectSave = new Project
        {
            Id = projectId,
            ProjectCode = requestDto.ProjectCode,
            ProjectName = requestDto.ProjectName,
            ClientName = requestDto.ClientName,
            Description = requestDto.Description,
            StartDate = requestDto.StartDate,
            EndDate = requestDto.EndDate,
            LongDay = duration.Days,
            CompanyId = companyId,
            ProjectStatusId = requestDto.ProjectStatusId,
            TrEmployeeProjects = requestDto.EmpProjects.Select(tr => new TR_EmployeeProject
            {
                ProjectId = projectId,
                EmployeeId = tr.EmployeeId,
            }).ToList()
        };

        try
        {
            await _persistence.BeginTransactionAsync();
            await _projectRepository.Save(projectSave);
            await _persistence.SaveChangesAsync();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw new Exception(e.Message);
        }
    }

    // =========================== List Project =========================
    public async Task<IEnumerable<ProjectResponseDto>> ListProjects(string companyId)
    {
        var project = await _projectRepository.FindAll(p => p.CompanyId.Equals(companyId) && !p.IsDeleted, new []{"ProjectStatus"});
        IEnumerable<ProjectResponseDto> result = project.Select(p => new ProjectResponseDto
        {
            Id = p.Id,
            ProjectCode = p.ProjectCode,
            ProjectName = p.ProjectName,
            ClientName = p.ClientName,
            Description = p.Description,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            LongDay = p.LongDay,
            ProjectStatusId = p.ProjectStatusId,
            ProjectStatusName = p.ProjectStatus.Name
        });
        return result;
    }

    // =========================== Get Project Detail/by id =========================
    public async Task<ProjectResponseDto> GetProjectById(string projectId)
    {
        var project = await _projectRepository.Find(p => p.Id.Equals(projectId), new[] { "ProjectStatus" });
        if (project == null) throw new NotFoundException(DataProperties.NotFoundMessage);
        
        return new ProjectResponseDto
        {
            Id = project.Id,
            ProjectCode = project.ProjectCode,
            ProjectName = project.ProjectName,
            ClientName = project.ClientName,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            LongDay = project.LongDay,
            ProjectStatusId = project.ProjectStatusId,
            ProjectStatusName = project.ProjectStatus.Name
        };
    }

    // ========================== Soft Delete Project =========================
    public async Task DeleteProject(string projectId)
    {
        var project = await _projectRepository.Find(p => p.Id.Equals(projectId), new[] { "ProjectStatus" });
        if (project == null) throw new NotFoundException(DataProperties.NotFoundMessage);

        try
        {
            await _persistence.BeginTransactionAsync();
            project.IsDeleted = true;
            _projectRepository.Update(project);
            await _persistence.SaveChangesAsync();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw new Exception(e.Message);
        }
    }

    // ==================== Update project =================
    public async Task UpdateProject(string projectId, ProjectRequestDto projectRequestDto)
    {
        var project = await _projectRepository.FindById(projectId);
        if (project == null) throw new NotFoundException(DataProperties.NotFoundMessage);

        TimeSpan duration = projectRequestDto.EndDate - projectRequestDto.StartDate;
        
        project.ProjectCode = projectRequestDto.ProjectCode;
        project.ProjectName = projectRequestDto.ProjectName;
        project.Description = projectRequestDto.Description;
        project.ProjectStatusId = projectRequestDto.ProjectStatusId;
        project.ClientName = projectRequestDto.ClientName;
        project.StartDate = projectRequestDto.StartDate;
        project.EndDate = projectRequestDto.EndDate;
        project.LongDay = duration.Days;

        try
        {
            await _persistence.BeginTransactionAsync();
            _projectRepository.Update(project);
            await _persistence.SaveChangesAsync();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw new Exception(e.Message);
        }
    }

    // public Task<IEnumerable<ProjectResponseDto>> ListProjectByEmployee(string employeeId)
    // {
    //     throw new NotImplementedException();
    // }

    public async Task DeleteEmpProjectById(string trEmpProjectId)
    {
        await _trEmpProjectService.DeleteEmpProjectById(trEmpProjectId);
    }

    public async Task AssignProjectToEmployee(string projectId, TR_EmpProjectRequestDto requestDto)
    {
        await _trEmpProjectService.CreateTrEmpProject(projectId, requestDto);
    }
}