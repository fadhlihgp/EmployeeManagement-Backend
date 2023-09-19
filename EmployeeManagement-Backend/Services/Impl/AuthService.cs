using EmployeeManagement_Backend.Entities;
using EmployeeManagement_Backend.Exceptions;
using EmployeeManagement_Backend.Repositories;
using EmployeeManagement_Backend.Security;
using EmployeeManagement_Backend.ViewModels.Requests;
using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Services.Impl;

public class AuthService : IAuthService
{
    private readonly IRepository<Account> _accountRepository;
    private readonly IRepository<LoginHistory> _loginHistoryRepository;
    private readonly IPersistence _persistence;
    private readonly IJwtUtil _jwtUtil;

    public AuthService(IRepository<Account> accountRepository, IPersistence persistence, IJwtUtil jwtUtil, IRepository<LoginHistory> loginHistoryRepository)
    {
        _accountRepository = accountRepository;
        _persistence = persistence;
        _jwtUtil = jwtUtil;
        _loginHistoryRepository = loginHistoryRepository;
    }
    
    // =============== Register account + Create new company + Login History ==================
    public async Task RegisterAccount(RegisterRequestDto registerRequestDto)
    {
        await RegisterAccountValidate(registerRequestDto.Email, registerRequestDto.Username);

        var companySave = new Company
        {
            Id = Guid.NewGuid().ToString(),
            Name = registerRequestDto.CompanyName,
            Email = registerRequestDto.CompanyEmail,
            PhoneNumber = registerRequestDto.CompanyPhone,
            Address = registerRequestDto.CompanyAddress,
        };

        var accountSave = new Account
        {
            Id = Guid.NewGuid().ToString(),
            FullName = registerRequestDto.FullName,
            UserName = registerRequestDto.Username,
            Email = registerRequestDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(registerRequestDto.Password),
            Address = registerRequestDto.Address,
            RoleId = "2",
            CompanyId = companySave.Id,
            Company = companySave
        };

        accountSave.LoginHistory = new LoginHistory
        {
            Id = Guid.NewGuid().ToString(),
            AccountId = accountSave.Id,
            LastLogin = null
        };
        
        await _persistence.BeginTransactionAsync();
        try
        {
            await _accountRepository.Save(accountSave);
            await _persistence.SaveChangesAsync();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw new Exception(e.Message);
        }
    }

    // =============== Create Account + Login History =============
    public async Task CreateAccount(CreateAccountDto createAccount, string? companyId)
    {
        await RegisterAccountValidate(createAccount.Email, createAccount.UserName);

        var account = new Account
        {
            FullName = createAccount.FullName,
            UserName = createAccount.UserName,
            Email = createAccount.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(createAccount.Password),
            Address = createAccount.Address,
            RoleId = createAccount.RoleId,
            CompanyId = companyId.Equals("xxx") ? createAccount.CompanyId : companyId
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

    // =============== Login =============
    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var findAccount = await _accountRepository.Find(a =>
            a.Email.Equals(loginRequestDto.Username) || a.UserName.Equals(loginRequestDto.Username), new []{"Company", "Role"});
        if (findAccount == null) throw new UnauthorizedException("Invalid username or password");
        
        bool isValid = BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, findAccount.Password);
        if (!isValid) throw new UnauthorizedException("Invalid username or password");

        if (!findAccount.IsActive)
            throw new UnauthorizedException("Akun anda di non-aktifkan, hubungi owner atau admin");
        
        var jwt = _jwtUtil.GenerateToken(findAccount);

        var loginHistory = await _loginHistoryRepository.Find(l => l.AccountId.Equals(findAccount.Id));
        if (loginHistory == null)
        {
            var loginHis = new LoginHistory
            {
                AccountId = findAccount.Id,
                LastLogin = DateTime.Now
            };

            try
            {
				await _loginHistoryRepository.Save(loginHis);
				await _persistence.SaveChangesAsync();
			} catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        else
        {
            loginHistory.LastLogin = DateTime.Now;
            _loginHistoryRepository.Update(loginHistory);
            _persistence.SaveChanges();
        }
        
        Console.WriteLine("Test" + loginHistory.LastLogin);
        return new LoginResponseDto
        {
            Username = findAccount.UserName,
            Role = findAccount.Role.Name,
            Token = jwt
        };
    }

    // ============== Validasi Register tidak boleh ada email dan username duplikat ==================
    private async Task RegisterAccountValidate(string email, string username)
    {
        var findByEmail = await _accountRepository.Find(a => a.Email.Equals(email));
        if (findByEmail != null)
        {
            throw new BadRequestException("Email sudah terdaftar, silahkan login");
        }

        var findByUsername = await _accountRepository.Find(a => a.UserName.Equals(username));
        if (findByUsername != null)
        {
            throw new BadRequestException("Username sudah terdaftar, silahkan cari username lain");
        }
    }
}