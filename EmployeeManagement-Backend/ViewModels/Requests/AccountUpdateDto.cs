using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Internal;

namespace EmployeeManagement_Backend.ViewModels.Requests;

public class AccountUpdateDto
{
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? Address { get; set; }
}