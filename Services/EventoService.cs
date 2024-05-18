using System.Data.Entity;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Iduff.Contracts;
using Iduff.Dtos;
using Iduff.Models;
using Iduff.Services.Interfaces;

namespace Iduff.Services;

public class EventoService : IEventoService
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IEventoRepository _eventoRepository;
    private readonly IduffContext _context;


    public EventoService(IAlunoRepository alunoRepository, IEventoRepository eventoRepository, IduffContext context)
    {
        _alunoRepository = alunoRepository;
        _eventoRepository = eventoRepository;
        _context = context;
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

            var aluno = _context.Alunos.FirstOrDefault(c => c.matricula == matricula);
            if (aluno != null)
            {
                alunos.Add(aluno);
            }
        }

        return alunos;
    }

    private async Task<Evento> MapearEvento(EventoDto eventoDto)
    {
        var palestrante =  _context.Alunos.FirstOrDefault(a => a.matricula == long.Parse(eventoDto.MatriculaPalestrante));
        var organizador =  _context.Alunos.FirstOrDefault(a => a.matricula == long.Parse(eventoDto.MatriculaOrganizador));
        
        var evento = new Evento
        {
            Titulo = eventoDto.Titulo,
            Data = eventoDto.Data,
            Local = eventoDto.Local,
            HorasComplementares = eventoDto.HorasComplementares,
            PalestranteId = palestrante?.Id,
            Palestrante = palestrante,
            OrganizadorId = organizador?.Id,
            Organizador = organizador
        };


        return evento;
    }
    public async Task<Evento> SalvaEvento(EventoDto eventoDto)
    {
        var evento = await MapearEvento(eventoDto);
        
        _eventoRepository.Create(evento);
        await _context.SaveChangesAsync();
        return evento;
    }
    public async Task SalvaPresencaEvento(IFormFile arquivo, Evento evento)
    {
        var alunos = await ListarAlunos(arquivo);
        foreach (var aluno in alunos)
        {
            var certificado = new Certificado
            {
                Evento = evento,
                Aluno = aluno,
                NomeEvento = evento.Titulo,
                NomePalestrante = evento.Palestrante?.Name,
                QuantidadeHoras = evento.HorasComplementares,
                AlunoId = aluno.Id,
                EventoId = evento.Id
            };

            _context.Certificados.Add(certificado);
            await _context.SaveChangesAsync();
        }
    }
    
}