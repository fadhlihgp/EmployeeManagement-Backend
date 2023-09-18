using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement_Backend.ViewModels.Requests;

public class CreateAccountDto
{
    public string FullName { get; set; }
    public string UserName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public string RoleId { get; set; }
    public string? CompanyId { get; set; }
}