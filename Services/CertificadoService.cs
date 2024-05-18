using System.Globalization;
using CsvHelper;
using Iduff.Dtos;
using Iduff.Models;
using Iduff.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace Iduff.Services;

public class CertificadoService : ICertificadoService
{
    private readonly IduffContext _context;
    public CertificadoService(IduffContext context)
    {
        _context = context;
    }
    public int ObterQuantidadeHorasTotal(Usuario usuario)
    {
        return 0;
    }
    /*
    public async Task MapearFormulario(IFormFile arquivo)
    {
        using (var reader = new StreamReader("path\\to\\file.csv"))
        using (var csv = new CsvReader(reader))
        {
            var records = csv.GetRecords<Foo>();
        }
        
        var stream = arquivo.OpenReadStream();
        
        using (var csvReader = new CsvReader(arquivo.OpenReadStream(), CultureInfo.InvariantCulture))
        {
            csvReader.NextRow();
            var tesste = csvReader.ToJson();
            var teste = csvReader.Columns;
        }
        
    }
*/
    /*
    public async Task<Evento> MapearEvento(EventoDto eventoDto)
    {
        Evento evento = new Evento();
    
        if (!string.IsNullOrEmpty(eventoDto.MatriculaPalestrante))
        {
            var palestrante =
                await _context.Users.FirstOrDefaultAsync(c =>
                    c.matricula == long.Parse(eventoDto.MatriculaPalestrante));
            if (palestrante != null)
            {
                evento.Palestrante = palestrante;
            }
        }

        if (!string.IsNullOrEmpty(eventoDto.MatriculaOrganizador))
        {
            var organizador = await _context.Users.FirstOrDefaultAsync(c => c.Matricula == long.Parse(eventoDto.MatriculaOrganizador));
            if (organizador != null)
            {
                evento.Organizador = organizador;
            }
        }
    
        evento.Data = eventoDto.Data;
        evento.Local = eventoDto.Local;
        evento.Titulo = eventoDto.Titulo;
        evento.HorasComplementares = eventoDto.HorasComplementares;

        return evento;
    }
*/
    
}