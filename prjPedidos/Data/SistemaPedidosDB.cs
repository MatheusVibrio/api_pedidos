using Microsoft.EntityFrameworkCore;
using prjPedidos.Data.Map;
using prjPedidos.Models;

namespace prjPedidos.Data
{
    public class SistemaPedidosDB : DbContext
    {
        public SistemaPedidosDB(DbContextOptions<SistemaPedidosDB> options) : base(options)
        {
        }

        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<PedidoModel> Pedidos { get; set; }
        public DbSet<ItensPedidoModel> ItensPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações específicas para cada modelo
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new PedidoMap());
            modelBuilder.ApplyConfiguration(new ItensPedidoMap());

            // Configuração do relacionamento entre PedidoModel e ItensPedidoModel
            modelBuilder.Entity<PedidoModel>()
                .HasMany(p => p.ItensPedido)
                .WithOne(i => i.Pedido)
                .HasForeignKey(i => i.PedidoId);

            // Configuração do relacionamento entre ItensPedidoModel e ProdutoModel
            modelBuilder.Entity<ItensPedidoModel>()
                .HasOne(i => i.Produto)
                .WithMany()  // Se Produto não tem uma coleção de ItensPedido
                .HasForeignKey(i => i.ProdutoId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
