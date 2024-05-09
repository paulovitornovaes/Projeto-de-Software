using System.Globalization;
using CsvHelper.Configuration;
using Iduff.Dtos;
using Iduff.Models;
using Iduff.Services.Interfaces;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

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

    public async Task MapearFormulario(IFormFile arquivo)
    {
        using (var stream = arquivo.OpenReadStream())
        using (var reader = new StreamReader(stream))
        {
            using (var csv = new CsvReader(stream, false))
            {
                var registros = csv.Columns;
                
                //var registros = csv.GetRecords<RegistroCSV>();

                // Lista para armazenar os usuários encontrados
                //var usuariosEncontrados = new List<User>();

                // Itera sobre cada registro do CSV
                foreach (var registro in registros)
                {
                    // Busca o usuário pelo número de matrícula no banco de dados
                    //var usuario = await _context.Users.FirstOrDefaultAsync(u => u.Matricula == registro.Matricula);

                    // Se o usuário for encontrado, adiciona à lista de usuários encontrados
                    /*
                    if (usuario != null)
                    {
                        //usuariosEncontrados.Add(usuario);
                    }
                    */
                    
                }
            }
        }
    }

    public async Task<Evento> MapearEvento(EventoDto eventoDto)
    {
        Evento evento = new Evento();
    
        if (!string.IsNullOrEmpty(eventoDto.MatriculaPalestrante))
        {
            var palestrantes = _context.Users.ToList();
            var palestrante =
                await _context.Users.FirstOrDefaultAsync(c =>
                    c.Matricula == long.Parse(eventoDto.MatriculaPalestrante));
            if (palestrante != null)
            {
                evento.Palestrante = palestrante;
            }
            // Se não encontrar um palestrante válido, você pode lidar com isso de acordo com sua lógica, como lançar uma exceção ou definir como null.
        }

        if (!string.IsNullOrEmpty(eventoDto.MatriculaOrganizador))
        {
            var organizador = await _context.Users.FindAsync(eventoDto.MatriculaOrganizador);
            if (organizador != null)
            {
                evento.Organizador = organizador;
            }
            // Se não encontrar um organizador válido, você pode lidar com isso de acordo com sua lógica, como lançar uma exceção ou definir como null.
        }
    
        evento.Data = eventoDto.Data;
        evento.Local = eventoDto.Local;
        evento.Titulo = eventoDto.Titulo;
        evento.HorasComplementares = eventoDto.HorasComplementares;

        return evento;
    }

    
}