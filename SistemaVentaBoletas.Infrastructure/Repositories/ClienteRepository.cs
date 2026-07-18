using SistemaVentaBoletas.Domain.Entities;
using SistemaVentaBoletas.Infrastructure.Context;
using SistemaVentaBoletas.Infrastructure.Core;
using SistemaVentaBoletas.Infrastructure.Interfaces;

namespace SistemaVentaBoletas.Infrastructure.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext context) : base(context)
        {
        }
    }
}