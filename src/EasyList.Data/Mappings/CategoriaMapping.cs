using EasyList.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyList.Data
{
  public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
  {
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {

      builder.HasKey(p => p.Id);

      builder.Property(p => p.Id)
       .ValueGeneratedOnAdd();

      builder.Property(p => p.NomeCategoria)
         .HasMaxLength(50)
          .IsRequired();

      builder.Property(p => p.UsuarioCriacao);

      builder.Property(p => p.DataCriacao);

      builder.Property(p => p.UsuarioModificacao);

      builder.Property(p => p.DataModificacao);

      builder.ToTable("CATEGORIA");
    }
  }
}
