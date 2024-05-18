using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Iduff.Contracts;
using Iduff.Models;
using Iduff.Services.Interfaces;

namespace Iduff.Services;

public class EventoService : IEventoService
{
    private readonly IAlunoRepository _alunoRepository;

    public EventoService(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    private async Task<List<long>> ListarMatriculasPresentes(IFormFile arquivo)
    {
        var matriculas = new List<long>();

        using (var stream = arquivo.OpenReadStream())
        using (var reader = new StreamReader(stream))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            await foreach (var record in csv.GetRecordsAsync<long>())
            {
                matriculas.Add(record);
            }
        }

        return matriculas;
    }
    public async Task<List<Aluno>> ListarAlunos(IFormFile arquivo)
    {
        var alunos = new List<Aluno>();
        var matriculas = await ListarMatriculasPresentes(arquivo);
        foreach (var matricula in matriculas)
        {
            var aluno = await _alunoRepository.GetByAsync(c => c.matricula == matricula);
            alunos.Add(aluno);
        }

        return alunos;
    }

    public async Task SalvaPresencaEvento(IFormFile arquivo)
    {
        var alunos = await ListarAlunos(arquivo);
        foreach (var aluno in alunos)
        {
            
        }
    }
    
}