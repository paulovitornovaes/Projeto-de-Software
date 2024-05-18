using Microsoft.AspNetCore.Identity;

namespace Iduff.Models;

public class Usuario : IdentityUser
{
    public string Name { get; set; }
}