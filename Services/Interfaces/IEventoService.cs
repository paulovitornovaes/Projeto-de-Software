using Iduff.Dtos;
using Iduff.Models;

namespace Iduff.Services.Interfaces;

public interface IEventoService
{ 
    Task SalvaPresencaEvento(IFormFile file, Evento evento);
    Task<Evento> SalvaEvento(EventoDto eventoDto);
}