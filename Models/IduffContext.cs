using Microsoft.EntityFrameworkCore;

namespace Iduff.Models;

public class IduffContext : DbContext
{
    public IduffContext(DbContextOptions<IduffContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; } = null!;
}