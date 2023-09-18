using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement_Backend.Entities;

[Table("Employee")]
public class Employee
{
    [Key, Column(TypeName = "Varchar(50)")] public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column(TypeName = "varchar(50)")]
    public string IdentityNumber { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; } = string.Empty;
    
    public string Address { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(50)")]
    public string PhoneNumber { get; set; } = string.Empty;
    
    public DateTime BirthDate { get; set; }
    
    [Column(TypeName = "varchar(50)")]
    public string? ImageUrl { get; set; }
    
    public bool IsDeleted { get; set; } = false;
    
    public bool IsActive { get; set; } = true;
    
    public DateTime? JoinDate { get; set; }
    
    [Column(TypeName = "varchar(50)")]
    public string CompanyId { get; set; } = string.Empty;
    public virtual Company Company { get; set; }
    
    public virtual ICollection<TR_EmployeeProject>? TrEmployeeProjects { get; set; }
}