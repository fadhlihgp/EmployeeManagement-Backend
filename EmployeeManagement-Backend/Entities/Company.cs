using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement_Backend.Entities;

[Table("Company")]
public class Company
{
    [Key]
    public string Id { get; set; } = new Guid().ToString();
    
    public string Name { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string PhoneNumber { get; set; } = string.Empty;
    
    [Column(TypeName = "Text")]
    public string Address { get; set; } = string.Empty;
    
    public virtual ICollection<Account>? Accounts { get; set; }
    public virtual ICollection<Employee>? Employees { get; set; }
    public virtual ICollection<Attendance>? Attendances { get; set; }
    public virtual ICollection<Project>? Projects { get; set; }
}