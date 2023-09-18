using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement_Backend.Entities;

[Table("Role")]
public class Role
{
    [Key]
    public String Id { get; set; } = string.Empty;
    
    [Column(TypeName = "Varchar(255)")]
    public String Name { get; set; } = string.Empty;
    
    public virtual ICollection<Account>? Accounts { get; set; }
}