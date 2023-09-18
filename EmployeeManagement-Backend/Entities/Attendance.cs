using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement_Backend.Entities;

[Table("Attendance")]
public class Attendance
{
    [Key, Column(TypeName = "Varchar(50)")] public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Column(TypeName = "varchar(50)")]
    public string EmployeeId { get; set; } = string.Empty;
    public virtual Employee Employee { get; set; }
    
    public DateTime Date { get; set; }
    
    [Column(TypeName = "varchar(50)")]
    public string AttendanceCodeId { get; set; } = string.Empty;
    public virtual AttendanceCode EmployeeCodeId { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string CompanyId { get; set; } = string.Empty;
    public virtual Company Company { get; set; }
}