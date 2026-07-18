using SistemaVentaBoletas.Domain.Entities;
using SistemaVentaBoletas.Infrastructure.Context;
using SistemaVentaBoletas.Infrastructure.Core;
using SistemaVentaBoletas.Infrastructure.Interfaces;

namespace SistemaVentaBoletas.Infrastructure.Repositories
{
    public class BoletaRepository : BaseRepository<Boleta>, IBoletaRepository
    {
        public BoletaRepository(AppDbContext context) : base(context)
        {
        }
    }
}