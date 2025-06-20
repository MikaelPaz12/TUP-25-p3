using Microsoft.EntityFrameworkCore;
using servidor.Models;

namespace servidor.Data
{
    public class TiendaDbContext : DbContext
    {
        public TiendaDbContext(DbContextOptions<TiendaDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ItemCarrito> ItemsCarrito { get; set; }
    }
}