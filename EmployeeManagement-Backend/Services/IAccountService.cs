using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Services;

public interface IAccountService
{
    Task CreateAccount(CreateAccountDto createAccountDto, string companyId);
    Task DeleteAccount(string accountId, string roleId);
    Task ActivateDisableAccount(string accountId);
    Task<IEnumerable<AccountResponseDto>> ListAccounts(string roleId, string companyId);
    Task<AccountResponseDto> GetAccountById(string accountId);
    Task ResetPassword(string accountId, string roleId);
    Task ChangePassword(ChangePasswordRequestDto changePasswordRequestDto);
    Task<AccountResponseDto> GetMyProfile(string accountId);
    Task UpdateProfile(string accountId, AccountUpdateDto updateDto);
}