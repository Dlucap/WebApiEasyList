using EasyList.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyList.Data
{
  public class ItmCompraMapping : IEntityTypeConfiguration<ItmCompra>
  {
    public void Configure(EntityTypeBuilder<ItmCompra> builder)
    {
      builder.HasKey(p => p.Id);

      builder.Property(p => p.Id)
         .ValueGeneratedOnAdd();

      builder.Property(p => p.CompraId)
         .IsRequired();

      builder.Property(p => p.ProdutoId)
         .IsRequired();

      builder.Property(p => p.Preco)
        .IsRequired();

      builder.Property(p => p.Quantidade)
         .HasPrecision(5)
          .IsRequired();

      builder.Property(p => p.Validade)
         .IsRequired();

      builder.Property(p => p.UsuarioCriacao);

      builder.Property(p => p.DataCriacao);

      builder.Property(p => p.UsuarioModificacao);

      builder.Property(p => p.DataModificacao);

      builder.ToTable("ITEMCOMPRA");
    }
  }
}
