namespace EmployeeManagement_Backend.ViewModels.Requests;

public class EmployeeCreateRequestDto
{
    public string IdentityNumber { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? JoinDate { get; set; }
}