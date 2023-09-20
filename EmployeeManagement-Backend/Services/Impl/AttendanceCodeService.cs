using EmployeeManagement_Backend.Entities;
using EmployeeManagement_Backend.Exceptions;
using EmployeeManagement_Backend.Helpers;
using EmployeeManagement_Backend.Repositories;

namespace EmployeeManagement_Backend.Services.Impl;

public class AttendanceCodeService : IAttendanceCodeService
{
    private readonly IRepository<Entities.AttendanceCode> _repository;
    private readonly IPersistence _persistence;

    public AttendanceCodeService(IRepository<Entities.AttendanceCode> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }

    public async Task<IEnumerable<AttendanceCode>> ListAttendanceCodes()
    {
        return await _repository.FindAll();
    }

    public async Task<AttendanceCode>? GetAttendanceCodeById(string id)
    {
        return await _repository.FindById(id);
    }

    public async Task UpdateAttendanceCode(AttendanceCode attendanceCode)
    {
        _repository.Update(attendanceCode);
        await _persistence.SaveChangesAsync();
    }

    public async Task CreateAttendanceCode(string name)
    {
        AttendanceCode attendCode = new AttendanceCode
        {
            Name = name
        };
        await _repository.Save(attendCode);
        await _persistence.SaveChangesAsync();
    }
}