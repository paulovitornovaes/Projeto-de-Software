using System.ComponentModel.DataAnnotations.Schema;

namespace Iduff.Models;

public class Aluno : Usuario
{
    public long matricula { get; set; }
    public List<Certificado> Certificados { get; set; }
    public CargaHoraria CargaHoraria { get; set; }
    
    [ForeignKey("CargaHorariaId")]
    public long CargaHorariaId { get; set; }
}