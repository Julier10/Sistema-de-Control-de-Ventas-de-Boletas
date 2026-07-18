using SistemaVentaBoletas.Domain.Entities;
using SistemaVentaBoletas.Infrastructure.Context;
using SistemaVentaBoletas.Infrastructure.Core;
using SistemaVentaBoletas.Infrastructure.Interfaces;

namespace SistemaVentaBoletas.Infrastructure.Repositories
{
    public class EventoRepository : BaseRepository<Evento>, IEventoRepository
    {
        public EventoRepository(AppDbContext context) : base(context)
        {
        }
    }
}