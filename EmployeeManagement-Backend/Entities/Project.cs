using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement_Backend.Entities;

[Table("Project")]
public class Project
{
    [Key, Column(TypeName = "varchar(50)")] public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Column(TypeName = "varchar(50)")]
    public string ProjectCode { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(50)")]
    public string ProjectName { get; set; } = string.Empty;

    [Column(TypeName = "varchar(50)")]
    public string? ClientName { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public int LongDay { get; set; }
    
    [Column(TypeName = "varchar(50)")]
    public string CompanyId { get; set; }
    public virtual Company Company { get; set; }
    
    [Column(TypeName = "varchar(50)")]
    public string ProjectStatusId { get; set; }
    public virtual ProjectStatus ProjectStatus { get; set; }
    
    public bool IsDeleted { get; set; } = false;
    
    public virtual ICollection<TR_EmployeeProject>? TrEmployeeProjects { get; set; }
}