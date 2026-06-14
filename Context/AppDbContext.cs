using Microsoft.EntityFrameworkCore;
using SistemaVentaBoletasAPI.Models;

namespace SistemaVentaBoletasAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Boleta> Boletas { get; set; }

        public DbSet<Venta> Ventas { get; set; }
    }
}