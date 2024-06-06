using System.ComponentModel.DataAnnotations;

namespace Iduff.Dtos;

public enum UserRole
{
    Admin = 0,
    User = 1
}
public class UserDto
{
    
    [Microsoft.Build.Framework.Required]
    public string Name { get; set; } = string.Empty;

    [Microsoft.Build.Framework.Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Microsoft.Build.Framework.Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Microsoft.Build.Framework.Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;
    
    //public UserRole Role { get; set; }
    public string Matricula { get; set; }
}