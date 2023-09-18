using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement_Backend.Entities;

[Table("projectStatus")]
public class ProjectStatus
{
    [Key, Column(TypeName = "varchar(50)")] public string Id { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; } = string.Empty;
    
    public virtual ICollection<Project>? Projects { get; set; }
}