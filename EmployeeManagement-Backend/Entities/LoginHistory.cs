using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement_Backend.Entities;

[Table("LoginHistory")]
public class LoginHistory
{
    [Key] public string Id { get; set; } = new Guid().ToString();
    
    public string AccountId { get; set; } = string.Empty;
    public virtual Account Account { get; set; }
    
    public DateTime LastLogin { get; set; } = DateTime.Now;
}