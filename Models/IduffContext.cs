using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Iduff.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace Iduff.Models;

public class IduffContext(DbContextOptions options) : IdentityDbContext<Usuario>(options)
{
    public DbSet<Certificado> Certificados { get; set; } = default!;
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>()
            .HasDiscriminator<string>("UserType")
            //.HasValue<Administrador>("Administrador")
            .HasValue<Aluno>("Aluno");
        
        modelBuilder.Entity<Aluno>()
            .Property(u => u.matricula)
            .HasMaxLength(50);

    }

}