using Iduff.Dtos;
using Iduff.Models;

namespace Iduff.Services.Interfaces;

public interface ICertificadoService
{
    //Task<Evento> MapearEvento(EventoDto eventoDto);
    //Task MapearFormulario(IFormFile arquivo);
    Task CriarCertificado(Certificado certificado);
}