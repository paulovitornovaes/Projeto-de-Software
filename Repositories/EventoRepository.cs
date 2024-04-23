using Iduff.Contracts;
using Iduff.Models;

namespace Iduff.Repositories;

public class EventoRepository : BaseRepository<Evento>, IEventoRepository
{
    public EventoRepository(IduffContext context) : base(context) {} 
}