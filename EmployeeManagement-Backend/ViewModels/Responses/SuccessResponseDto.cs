namespace EmployeeManagement_Backend.ViewModels.Responses;

public class SuccessResponseDto
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; }
    public object? Data { get; set; }
}