using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iduff.Models;

public class CargaHoraria
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }
    public int presencaPalestras { get; set; }
    public int organizarPalestras { get; set; }
    public int ministrarPalestras { get; set; }
    public int estagio { get; set; }
    public int total { get; set; }
    
}