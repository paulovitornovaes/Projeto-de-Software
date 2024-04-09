using Microsoft.AspNetCore.Identity;

namespace Iduff.Models;

public class Usuario : IdentityUser
{
    public long Id { get; set; }
    public string Name { get; set; }

}