using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iduff.Models;

[Table("EVENTO")]
public class Evento
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }
    public Usuario Palestrante { get; set; }
    public DateTime Data { get; set; }
    public String Local { get; set; }
    public long HorasComplementares { get; set; }
    public Usuario Organizador { get; set; }
    public string Titulo { get; set; }
}