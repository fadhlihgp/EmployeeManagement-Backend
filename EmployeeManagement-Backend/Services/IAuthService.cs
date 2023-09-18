using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Services;

public interface IAuthService
{
    Task RegisterAccount(RegisterRequestDto registerRequestDto);
    Task CreateAccount(CreateAccountDto createAccount, string? companyId);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
}