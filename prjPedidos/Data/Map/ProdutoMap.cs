using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using prjPedidos.Models;

namespace prjPedidos.Data.Map
{
    public class ProdutoMap : IEntityTypeConfiguration<ProdutoModel>
    {
        public void Configure(EntityTypeBuilder<ProdutoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.NomeProduto).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Valor).IsRequired();
        }
    }
}
