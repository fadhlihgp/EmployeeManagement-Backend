using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeeManagement_Backend.Entities;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagement_Backend.Security;

public class JwtUtil : IJwtUtil
{
    private IConfiguration _configuration;

    public JwtUtil(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Account account)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _configuration["JwtSettings:Audience"],
            Expires = DateTime.Now.AddMinutes(int.Parse(_configuration["JwtSettings:ExpiresInMinutes"])),
            Issuer = _configuration["JwtSettings:Issuer"],
            IssuedAt = DateTime.Now,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Role, account.Role.Name),
                new Claim("AccountId", account.Id),
                new Claim("UserName", account.UserName),
                new Claim("RoleId", account.RoleId),
                new Claim("CompanyId", account.CompanyId ?? "xxx"),
                new Claim("CompanyName", account.Company?.Name ?? "Super Admin" )
            })
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
}