using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement_Backend.Entities;

[Table("Account")]
public class Account
{
    [Key, Column(TypeName = "Varchar(50)")]
    public string Id { get; set; } = new Guid().ToString();
    
    [Column(TypeName = "varchar(255)")]
    public string FullName { get; set; } = string.Empty;

    [Column(TypeName = "varchar(50)")]
    public string UserName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(maximumLength:int.MaxValue, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;
    
    public string Address { get; set; } = string.Empty;
    
    
    [Column(TypeName = "varchar(50)")]
    public string RoleId { get; set; } = string.Empty;
    public virtual Role Role { get; set; }
    
    [Column(TypeName = "varchar(50)")]
    public string? CompanyId { get; set; }
    public virtual Company? Company { get; set; }
}