namespace Iduff.Models
{
    public class Certificado
    {
        public long Id { get; set; }
        public string NomeEvento { get; set; }
        public string? NomePalestrante { get; set; }
        public long QuantidadeHoras { get; set; }

        public string AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        public long EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}