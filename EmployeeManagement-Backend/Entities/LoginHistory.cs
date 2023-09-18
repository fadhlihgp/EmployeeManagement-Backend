using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement_Backend.Entities;

[Table("LoginHistory")]
public class LoginHistory
{
    [Key, Column(TypeName = "varchar(50)")] public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Column(TypeName = "varchar(50)")]
    public string AccountId { get; set; } = string.Empty;
    public virtual Account Account { get; set; }
    
    public DateTime? LastLogin { get; set; }
}