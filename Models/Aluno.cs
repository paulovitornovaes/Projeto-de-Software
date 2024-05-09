namespace Iduff.Models;

public class Aluno : Usuario
{
    public List<Certificado> Certificados { get; set; }
}