using SistemaVentaBoletas.Domain.Entities;
using SistemaVentaBoletas.Infrastructure.Context;
using SistemaVentaBoletas.Infrastructure.Core;
using SistemaVentaBoletas.Infrastructure.Interfaces;

namespace SistemaVentaBoletas.Infrastructure.Repositories
{
    public class VentaRepository : BaseRepository<Venta>, IVentaRepository
    {
        public VentaRepository(AppDbContext context) : base(context)
        {
        }
    }
}