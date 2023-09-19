namespace EmployeeManagement_Backend.ViewModels.Responses;

public class ProjectResponseDto
{
    public string Id { get; set; }
    public string ProjectCode { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string? ClientName { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int LongDay { get; set; }
    public string ProjectStatusId { get; set; }
    public string ProjectStatusName { get; set; }
}