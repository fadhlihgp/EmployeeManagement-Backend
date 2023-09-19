namespace EmployeeManagement_Backend.ViewModels.Responses;

public class AccountResponseDto
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string? CompanyName { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastLogin { get; set; }
}