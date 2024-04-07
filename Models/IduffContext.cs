using Microsoft.EntityFrameworkCore;
using Iduff.Models;

namespace Iduff.Models;

public class IduffContext : DbContext
{
    public IduffContext(DbContextOptions<IduffContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; } = null!;

public DbSet<Iduff.Models.Certificado> Certificado { get; set; } = default!;
}