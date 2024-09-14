using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using prjPedidos.Models;

public class ItensPedidoMap : IEntityTypeConfiguration<ItensPedidoModel>
{
    public void Configure(EntityTypeBuilder<ItensPedidoModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PedidoId)
            .HasColumnName("PedidoId")
            .IsRequired();

        builder.HasOne(x => x.Pedido)
            .WithMany(p => p.ItensPedido)  
            .HasForeignKey(x => x.PedidoId);

        builder.Property(x => x.ProdutoId)
            .HasColumnName("ProdutoId")
            .IsRequired();

        builder.HasOne(x => x.Produto)
            .WithMany() 
            .HasForeignKey(x => x.ProdutoId);

        builder.Property(x => x.Quantidade)
            .IsRequired();
    }
}
