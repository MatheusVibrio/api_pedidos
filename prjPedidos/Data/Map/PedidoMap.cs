using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using prjPedidos.Models;

namespace prjPedidos.Data.Map
{
    public class PedidoMap : IEntityTypeConfiguration<PedidoModel>
    {
        public void Configure(EntityTypeBuilder<PedidoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.NomeCliente).IsRequired().HasMaxLength(255);
            builder.Property(x => x.EmailCliente).IsRequired().HasMaxLength(150);
            builder.Property(x => x.DataCriacao).IsRequired();
            builder.Property(x => x.Pago).IsRequired();

            // Configuração do relacionamento com ItensPedidoModel
            builder.HasMany(p => p.ItensPedido)
                   .WithOne(i => i.Pedido)
                   .HasForeignKey(i => i.PedidoId);
        }
    }
}
