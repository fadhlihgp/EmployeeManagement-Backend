namespace EmployeeManagement_Backend.ViewModels.Requests;

public class ChangePasswordRequestDto
{
    public string AccountId { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}