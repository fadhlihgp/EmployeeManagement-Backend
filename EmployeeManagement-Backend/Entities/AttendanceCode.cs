using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement_Backend.Entities;

[Table(name:"AttendanceCode")]
public class AttendanceCode
{
    [Key]
    public string Id { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
}