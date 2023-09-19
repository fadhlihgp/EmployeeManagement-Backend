using EmployeeManagement_Backend.Entities;
using EmployeeManagement_Backend.Exceptions;
using EmployeeManagement_Backend.Helpers;
using EmployeeManagement_Backend.Repositories;
using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Services.Impl;

public class AccountService : IAccountService
{
    private readonly IRepository<Account> _accountRepository;
    private readonly IPersistence _persistence;

    public AccountService(IRepository<Account> accountRepository, IPersistence persistence)
    {
        _accountRepository = accountRepository;
        _persistence = persistence;
    }

    // =================== Create account, if superadmin can choose company id, if owner automatically company id =================
    public async Task CreateAccount(CreateAccountDto createAccountDto, string companyId)
    {
        var findByEmail = await _accountRepository.Find(a => a.Email.Equals(createAccountDto.Email));
        if (findByEmail != null)
        {
            throw new BadRequestException("Email sudah terdaftar, gunakan email lain");
        }

        var findByUsername = await _accountRepository.Find(a => a.UserName.Equals(createAccountDto.UserName));
        if (findByUsername != null)
        {
            throw new BadRequestException("Username sudah terdaftar, silahkan cari username lain");
        }
        
        var account = new Account
        {
            FullName = createAccountDto.FullName,
            UserName = createAccountDto.UserName,
            Email = createAccountDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(createAccountDto.Password),
            Address = createAccountDto.Address,
            RoleId = createAccountDto.RoleId,
            CompanyId = companyId.Equals("xxx") ? createAccountDto.CompanyId : companyId
        };

        account.LoginHistory = new LoginHistory
        {
            Id = Guid.NewGuid().ToString(),
            AccountId = account.Id,
            LastLogin = null
        };
        
        await _persistence.BeginTransactionAsync();
        try
        {
            await _accountRepository.Save(account);
            await _persistence.SaveChangesAsync();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw new Exception(e.Message);
        }
    }

    // =================== Delete Account only Superadmin =================
    public async Task DeleteAccount(string accountId, string roleId)
    {
        if (roleId.Equals("3")) throw new UnauthorizedException(DataProperties.UnauthorizedMessage);

        var findAccount = await _accountRepository.FindById(accountId);
        if (findAccount == null) throw new NotFoundException(DataProperties.NotFoundMessage);

        try
        {
            await _persistence.BeginTransactionAsync();
            _accountRepository.Delete(findAccount);
            await _persistence.SaveChangesAsync();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw new Exception(e.Message);
        }
    }

    // =================== is active change to non active and also with reverse case ================= 
    public async Task ActivateDisableAccount(string accountId)
    {
        var account = await _accountRepository.FindById(accountId);
        if (account == null) throw new NotFoundException(DataProperties.NotFoundMessage);
        
        try
        {
            await _persistence.BeginTransactionAsync();
            
            // Validasi jika isActive true maka update menjadi false, Jika false maka update menjadi true
            if (account.IsActive)
            {
                account.IsActive = false;
                DataProperties.SuccessUpdateMessage = "Akun berhasil di non-aktifkan";
            }
            else
            {
                account.IsActive = true;
                DataProperties.SuccessUpdateMessage = "Akun berhasil di aktifkan";
            }

            _accountRepository.Update(account);
            _persistence.SaveChanges();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw new Exception(e.Message);
        }
        
    }

    // ================== List Account ==========================
    public async Task<IEnumerable<AccountResponseDto>> ListAccounts(string roleId, string companyId)
    {
        
        IEnumerable<Account> accounts;
        
        // Superadmin bisa melihat semua akun, tetapi owner hanya akun di company tersebut
        switch (roleId)
        {
            case "1":
                accounts = await _accountRepository.FindAll(new[] { "LoginHistory", "Company", "Role" });
                break;
            case "2":
                accounts = await _accountRepository.FindAll(a => a.CompanyId.Equals(companyId
                ),new[] { "LoginHistory", "Company", "Role" });
                break;
            default:
                throw new UnauthorizedException(DataProperties.UnauthorizedMessage);
        }

        IEnumerable<AccountResponseDto> result = accounts.Select(a => new AccountResponseDto
        {
            Id = a.Id,
            FullName = a.FullName,
            UserName = a.UserName,
            Email = a.Email,
            Role = a.Role.Name,
            CompanyName = a.Company?.Name,
            Address = a.Address,
            IsActive = a.IsActive,
            LastLogin = a.LoginHistory?.LastLogin
        }).ToList();

        return result;
    }

    // ====================== Get Account By Id =====================
    public async Task<AccountResponseDto> GetAccountById(string accountId)
    {
        var account = await _accountRepository.Find(a => a.Id.Equals(accountId), new []{ "Company", "Role", "LoginHistory"});
        if (account == null) throw new NotFoundException(DataProperties.NotFoundMessage);

        var result = new AccountResponseDto
        {
            Id = account.Id,
            FullName = account.FullName,
            Email = account.Email,
            UserName = account.UserName,
            Role = account.Role.Name,
            CompanyName = account.Company?.Name,
            Address = account.Address,
            IsActive = account.IsActive,
            LastLogin = account.LoginHistory?.LastLogin
        };

        return result;
    }

    // ======================= Reset password account =======================
    public async Task ResetPassword(string accountId, string roleId)
    {
        if (!roleId.Equals("1")) throw new UnauthorizedException(DataProperties.UnauthorizedMessage);
        
        var account = await _accountRepository.FindById(accountId);
        if (account == null) throw new NotFoundException(DataProperties.NotFoundMessage);
        
        try
        {
            await _persistence.BeginTransactionAsync();
            account.Password = BCrypt.Net.BCrypt.HashPassword("Password123/");
            _accountRepository.Update(account);
            await _persistence.SaveChangesAsync();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw new Exception(e.Message);
        }
    }

    // ========================= Change Password =========================
    public async Task ChangePassword(ChangePasswordRequestDto changePasswordRequestDto)
    {
        var account = await _accountRepository.FindById(changePasswordRequestDto.AccountId);
        if (account == null) throw new NotFoundException(DataProperties.NotFoundMessage);

        bool isValid = BCrypt.Net.BCrypt.Verify(changePasswordRequestDto.OldPassword, account.Password);
        if (!isValid) throw new BadRequestException("Password lama anda tidak valid");

        try
        {
            await _persistence.BeginTransactionAsync();
            account.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordRequestDto.NewPassword);
            _accountRepository.Update(account);
            await _persistence.SaveChangesAsync();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw new Exception(e.Message);
        }
    }

    // =========================== Get My profile ===========================
    public async Task<AccountResponseDto> GetMyProfile(string accountId)
    {
        var account = await _accountRepository.Find(a => a.Id.Equals(accountId), new []{ "Company", "Role", "LoginHistory"});
        if (account == null) throw new NotFoundException(DataProperties.NotFoundMessage);

        var result = new AccountResponseDto
        {
            Id = account.Id,
            FullName = account.FullName,
            UserName = account.UserName,
            Email = account.Email,
            Role = account.Role.Name,
            CompanyName = account.Company?.Name,
            Address = account.Address,
            IsActive = account.IsActive,
            LastLogin = account.LoginHistory?.LastLogin
        };

        return result;
    }

    // =========================== Update Profile =========================
    public async Task UpdateProfile(string accountId, AccountUpdateDto updateDto)
    {
        var account = await _accountRepository.Find(a => a.Id.Equals(accountId), new []{ "Company", "Role", "LoginHistory"});
        if (account == null) throw new NotFoundException(DataProperties.NotFoundMessage);

        var findEmail = await _accountRepository.Find(a => a.Email.Equals(updateDto.Email));
        if (findEmail != null) throw new BadRequestException("Email telah digunakan, mohon masukkan email lain");

        var findUsername = await _accountRepository.Find(a => a.UserName.Equals(updateDto.UserName));
        if (findUsername != null)
            throw new BadRequestException("Username telah digunakan, mohon masukkan usernname lain");

        try
        {
            await _persistence.BeginTransactionAsync();
            account.Email = updateDto.Email;
            account.Address = updateDto.Address;
            account.FullName = updateDto.FullName;
            account.UserName = updateDto.UserName;
            _accountRepository.Update(account);
            await _persistence.SaveChangesAsync();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw new Exception(e.Message);
        }
    }
}