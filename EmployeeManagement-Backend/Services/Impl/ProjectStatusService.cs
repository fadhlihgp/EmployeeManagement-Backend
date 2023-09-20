using System.Collections;
using EmployeeManagement_Backend.Entities;
using EmployeeManagement_Backend.Repositories;
using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Services.Impl;

public class ProjectStatusService : IProjectStatusService
{
    private readonly IRepository<ProjectStatus> _repository;

    public ProjectStatusService(IRepository<ProjectStatus> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProjectStatusResponseDto>> ListProjectStatus()
    {
        var projectStatus = await _repository.FindAll();
        IEnumerable<ProjectStatusResponseDto> results = projectStatus.Select(p => new ProjectStatusResponseDto
        {
            Id = p.Id,
            Name = p.Name
        });
        return results;
    }
}