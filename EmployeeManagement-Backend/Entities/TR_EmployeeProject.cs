using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement_Backend.Entities;

[Table("TR_Employee_Project")]
public class TR_EmployeeProject
{
    [Key, Column(TypeName = "varchar(50)")] public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Column(TypeName = "varchar(50)")]
    public string ProjectId { get; set; } = string.Empty;
    public virtual Project Project { get; set; }
    
    [Column(TypeName = "varchar(50)")]
    public string EmployeeId { get; set; } = string.Empty;
    public virtual Employee Employee { get; set; }
}