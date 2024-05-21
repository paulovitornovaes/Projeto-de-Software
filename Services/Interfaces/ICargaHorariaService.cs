using Iduff.Models;

namespace Iduff.Services.Interfaces;

public interface ICargaHorariaService
{ 
    Task ContabilizaHorasEvento(CargaHoraria cargaHoraria, int quantidadeHoras, CargaHorariaEnum tipoEvento);
    Task ContabilizaHorasPalestranteOrganizador(Evento evento);
    Task ContabilizaPresencaEvento(CargaHoraria cargaHoraria, int quantidadeHoras);
}