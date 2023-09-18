using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement_Backend.Entities;

[Table("TR_Employee_Project")]
public class TR_EmployeeProject
{
    [Key] public string Id { get; set; } = new Guid().ToString();
    
    public string ProjectId { get; set; } = string.Empty;
    public virtual Project Project { get; set; }
    
    public string EmployeeId { get; set; } = string.Empty;
    public virtual Employee Employee { get; set; }
}