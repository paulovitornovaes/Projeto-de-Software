using System.Data.Entity;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Iduff.Contracts;
using Iduff.Dtos;
using Iduff.Models;
using Iduff.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Iduff.Services;

public class EventoService : IEventoService
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IEventoRepository _eventoRepository;
    private readonly IduffContext _context;
    private readonly ICargaHorariaService _cargaHorariaService;
    private readonly ICertificadoService _certificadoService;

    public EventoService(IAlunoRepository alunoRepository, IEventoRepository eventoRepository, IduffContext context, ICargaHorariaService cargaHorariaService, ICertificadoService certificadoService)
    {
        _alunoRepository = alunoRepository;
        _eventoRepository = eventoRepository;
        _context = context;
        _cargaHorariaService = cargaHorariaService;
        _certificadoService = certificadoService;
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

            var aluno = 
            EntityFrameworkQueryableExtensions.Include(_context.Alunos, u => u.CargaHoraria)
                .FirstOrDefault(c => c.matricula == matricula);
            if (aluno != null)
            {
                alunos.Add(aluno);
            }
        }

        return alunos;
    }

    private async Task<Evento> MapearEvento(EventoDto eventoDto)
    {
        var palestrante =  EntityFrameworkQueryableExtensions.Include(_context.Alunos, u => u.CargaHoraria)
            .FirstOrDefault(a => a.matricula == long.Parse(eventoDto.MatriculaPalestrante));
        
        var organizador =  EntityFrameworkQueryableExtensions.Include(_context.Alunos, u => u.CargaHoraria)
            .FirstOrDefault(a => a.matricula == long.Parse(eventoDto.MatriculaOrganizador));
        
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
        await _cargaHorariaService.ContabilizaHorasPalestranteOrganizador(evento);
        
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

            await _cargaHorariaService.ContabilizaPresencaEvento(aluno.CargaHoraria, evento.HorasComplementares);
            await _certificadoService.CriarCertificado(certificado);
            
        }
        await _context.SaveChangesAsync();
        
    }
    
}