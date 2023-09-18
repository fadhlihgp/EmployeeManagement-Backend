namespace EmployeeManagement_Backend.ViewModels.Responses;

public class LoginHistoryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string RoleName { get; set; }
    public DateTime? LastLogin { get; set; }
}