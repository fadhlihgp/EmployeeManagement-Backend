using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement_Backend.Entities;

[Table("Company")]
public class Company
{
    [Key, Column(TypeName = "Varchar(50)")]
    public string Id { get; set; } = new Guid().ToString();
    
    [Column(TypeName = "varchar(255)")]
    public string Name { get; set; } = string.Empty;
    
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(50)")]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [Column(TypeName = "Text")]
    public string Address { get; set; } = string.Empty;
    
    public virtual ICollection<Account>? Accounts { get; set; }
    public virtual ICollection<Employee>? Employees { get; set; }
    public virtual ICollection<Attendance>? Attendances { get; set; }
    public virtual ICollection<Project>? Projects { get; set; }
}