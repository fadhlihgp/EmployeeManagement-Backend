using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement_Backend.Entities;

[Table("Attendance")]
public class Attendance
{
    [Key] public string Id { get; set; } = new Guid().ToString();
    
    public string EmployeeId { get; set; } = string.Empty;
    public virtual Employee Employee { get; set; }
    
    public DateTime Date { get; set; }
    
    public string AttendanceCodeId { get; set; } = string.Empty;
    public virtual AttendanceCode EmployeeCodeId { get; set; }

    public string CompanyId { get; set; } = string.Empty;
    public virtual Company Company { get; set; }
}