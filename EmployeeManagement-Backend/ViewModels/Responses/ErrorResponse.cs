namespace EmployeeManagement_Backend.ViewModels.Responses;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; } = false;
    public string Message { get; set; }
}