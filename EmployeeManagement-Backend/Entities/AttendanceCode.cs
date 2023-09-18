using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement_Backend.Entities;

[Table(name:"AttendanceCode")]
public class AttendanceCode
{
    [Key, Column(TypeName = "Varchar(50)")]
    public string Id { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; } = string.Empty;
}