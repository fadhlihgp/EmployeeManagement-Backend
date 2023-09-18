using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement_Backend.ViewModels.Requests;

public class RegisterRequestDto
{
    public string FullName { get; set; }
    public string Username { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    
    public string CompanyName { get; set; }
    [EmailAddress]
    public string CompanyEmail { get; set; }
    public string CompanyPhone { get; set; }
    public string CompanyAddress { get; set; }
}