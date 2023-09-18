using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement_Backend.Entities;

[Table("Employee")]
public class Employee
{
    [Key] public string Id { get; set; } = new Guid().ToString();

    public string IdentityNumber { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
    
    public string Address { get; set; } = string.Empty;
    
    public string PhoneNumber { get; set; } = string.Empty;
    
    public DateTime BirthDate { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public bool IsDeleted { get; set; } = false;
    
    public bool IsActive { get; set; } = true;
    
    public DateTime? JoinDate { get; set; }
    
    public string CompanyId { get; set; } = string.Empty;
    public virtual Company Company { get; set; }
    
    public virtual ICollection<TR_EmployeeProject>? TrEmployeeProjects { get; set; }
}