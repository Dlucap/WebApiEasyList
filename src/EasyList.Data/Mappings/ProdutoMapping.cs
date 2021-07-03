using EasyList.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyList.Data
{
  public class ProdutoMapping : IEntityTypeConfiguration<Produto>
  {
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
      //builder.Conventions.Remove<PluralizingTableNameConvention>();

      builder.HasKey(p => p.Id);

      builder.Property(p => p.Id)
         .ValueGeneratedOnAdd();

      builder.Property(p => p.CategoriaId)
         .IsRequired();

      builder.Property(p => p.Marca)
         .HasMaxLength(80)
          .IsRequired(); 

      builder.Property(p => p.Nome)
         .HasMaxLength(80)
          .IsRequired(); 

      builder.Property(p => p.Descricao)
         .HasMaxLength(80);

      builder.Property(p => p.UsuarioCriacao);

      builder.Property(p => p.DataCriacao);

      builder.Property(p => p.UsuarioModificacao);

      builder.Property(p => p.DataModificacao);

      builder.ToTable("PRODUTOS");
    }
  }
}
