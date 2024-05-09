using Iduff.Models;

namespace Iduff.Dtos;

public class CarregarEventoPresencaDto
{
    public Evento Evento { get; set; }
    public List<PresencaDto> AlunosPresentes { get; set; }
}