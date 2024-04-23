namespace Iduff.Models;

public class ParticipacaoEvento
{
    public long Id { get; set; }
    public Evento Evento { get; set; }
    public Aluno Aluno { get; set; }
}