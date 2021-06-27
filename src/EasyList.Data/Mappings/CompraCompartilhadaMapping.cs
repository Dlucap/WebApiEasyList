using EasyList.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyList.Data
{
  public class CompraCompartilhadaMapping : IEntityTypeConfiguration<CompraCompartilhada>
  {
    public void Configure(EntityTypeBuilder<CompraCompartilhada> builder)
    {
      builder.HasKey(p => p.Id);

      builder.Property(p => p.Id)
        .ValueGeneratedOnAdd();

      builder.Property(p => p.CompraId)
         .IsRequired();

      builder.Property(p => p.UsuariosCompartilhados)
         .IsRequired();

      builder.Property(p => p.UsuarioCriacao);

      builder.Property(p => p.DataCriacao);

      builder.Property(p => p.UsuarioModificacao);

      builder.Property(p => p.DataModificacao);

      builder.ToTable("COMPRACOMPARTILHADA");
    }
  }
}
