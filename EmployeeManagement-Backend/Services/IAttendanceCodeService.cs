using EmployeeManagement_Backend.Entities;

namespace EmployeeManagement_Backend.Services;

public interface IAttendanceCodeService
{
    Task<IEnumerable<AttendanceCode>> ListAttendanceCodes();
    Task<AttendanceCode>? GetAttendanceCodeById(string id);
    Task UpdateAttendanceCode(AttendanceCode attendanceCode);
    Task CreateAttendanceCode(string id);
}