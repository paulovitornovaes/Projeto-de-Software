using Iduff.Contracts;
using Iduff.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Iduff.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]

public class EventoController : ControllerBase
{
    
    private readonly IduffContext _context;

    public EventoController(IduffContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<EntityEntry<Evento>> CarregarEvento(Evento evento)
    {
        return await _context.Eventos.AddAsync(evento);
    }
}