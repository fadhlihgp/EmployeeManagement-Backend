using EmployeeManagement_Backend.Entities;

namespace EmployeeManagement_Backend.Security;

public interface IJwtUtil
{
    string GenerateToken(Account account);
}