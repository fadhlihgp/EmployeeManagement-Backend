using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement_Backend.Entities;

[Table("Account")]
public class Account
{
    [Key]
    public string Id { get; set; } = new Guid().ToString();
    
    public string FullName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    
    public string Address { get; set; } = string.Empty;
    
    public string RoleId { get; set; } = string.Empty;
    public virtual Role Role { get; set; }
    
    public string? CompanyId { get; set; }
    public virtual Company? Company { get; set; }
}