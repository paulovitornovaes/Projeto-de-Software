using Iduff.Models;
using Iduff.Services.Interfaces;

namespace Iduff.Services;

public class CargaHorariaService : ICargaHorariaService
{
    private readonly IduffContext _context;

    public CargaHorariaService(IduffContext context)
    {
        _context = context;
    }
    
    public async Task ContabilizaHorasPalestranteOrganizador(Evento evento)
    {
        if (evento.Palestrante != null)
        {
            await ContabilizaHorasEvento(evento.Palestrante.CargaHoraria, (int)CargaHorariaEnum.MinistrarPalestras,
                CargaHorariaEnum.MinistrarPalestras);
        }

        if (evento.Organizador != null)
        {
            await ContabilizaHorasEvento(evento.Organizador.CargaHoraria, (int)CargaHorariaEnum.OrganizarPalestras,
                CargaHorariaEnum.OrganizarPalestras);
        }
    }

    public async Task ContabilizaPresencaEvento(CargaHoraria cargaHoraria, int quantidadeHoras)
    {
        var total = cargaHoraria.total;
        cargaHoraria.presencaPalestras += quantidadeHoras;
        total += quantidadeHoras;
        cargaHoraria.total = total;

        _context.CargaHoraria.Update(cargaHoraria);
        await _context.SaveChangesAsync();
        
    }
    public async Task ContabilizaHorasEvento(CargaHoraria cargaHoraria, int quantidadeHoras, CargaHorariaEnum tipoEvento)
    {
        var total = cargaHoraria.total;
        
        switch (tipoEvento)
        {
            case CargaHorariaEnum.MinistrarPalestras:
                cargaHoraria.ministrarPalestras += quantidadeHoras;
                total += quantidadeHoras;
                break;
            case CargaHorariaEnum.OrganizarPalestras:
                cargaHoraria.organizarPalestras += quantidadeHoras;
                total += quantidadeHoras;
                break;
        }

        cargaHoraria.total = total;

        _context.CargaHoraria.Update(cargaHoraria);
        await _context.SaveChangesAsync();
    }
    
}