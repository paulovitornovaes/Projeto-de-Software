using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Iduff.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace Iduff.Models;

public class IduffContext(DbContextOptions options) : IdentityDbContext<Usuario>(options)
{
    public DbSet<Certificado> Certificados { get; set; } = default!;
    public DbSet<Evento> Eventos { get; set; }
}