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
    public DbSet<CargaHoraria> CargaHoraria { get; set; }
    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<Aluno>())
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity.CargaHoraria == null)
                {
                    entry.Entity.CargaHoraria = new CargaHoraria();
                }

                // Adicionar o novo registro de CargaHoraria
                if (entry.Entity.CargaHorariaId == 0)
                {
                    var cargaHoraria = new CargaHoraria
                    {
                        total = 0,
                        estagio = 0,
                        ministrarPalestras = 0,
                        organizarPalestras = 0,
                        presencaPalestras = 0,
                        Aluno = entry.Entity
                    };
                    entry.Entity.CargaHoraria = cargaHoraria;
                    entry.Entity.CargaHorariaId = cargaHoraria.Id;
                    CargaHoraria.Add(cargaHoraria);
                }
            }
        }
        return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>()
            .HasDiscriminator<string>("UserType")
            .HasValue<Aluno>("Aluno")
            .HasValue<Admin>("Admin");

    
        
        modelBuilder.Entity<Aluno>()
            .Property(u => u.matricula)
            .HasMaxLength(50);
        
        modelBuilder.Entity<Aluno>()
            .HasOne(a => a.CargaHoraria)
            .WithOne(ch => ch.Aluno)
            .HasForeignKey<Aluno>(a => a.CargaHorariaId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Certificado>()
            .HasOne(c => c.Aluno)
            .WithMany(a => a.Certificados)
            .HasForeignKey(c => c.AlunoId);

        modelBuilder.Entity<Certificado>()
            .HasOne(c => c.Evento)
            .WithMany(e => e.Certificados)
            .HasForeignKey(c => c.EventoId);
        
        modelBuilder.Entity<Evento>()
            .Property(e => e.Data)
            .HasColumnType("date");
    }

}