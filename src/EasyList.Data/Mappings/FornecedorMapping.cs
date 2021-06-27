using EasyList.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyList.Data.Mappings
{
  public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
  {
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {

      builder.HasKey(p => p.Id);

      builder.Property(p => p.Id)
       .ValueGeneratedOnAdd();

      builder.Property(p => p.Nome)
         .HasMaxLength(80);

      builder.Property(p => p.NomeFantasia)
         .HasMaxLength(80);

      builder.Property(p => p.Cnpj)
         .HasMaxLength(14);

      // 1 : 1 => Fornecedor : Endereco
      builder.HasOne(f => f.Endereco)
          .WithOne(e => e.Fornecedor);

      builder.Property(p => p.UsuarioCriacao);

      builder.Property(p => p.DataCriacao);

      builder.Property(p => p.UsuarioModificacao);

      builder.Property(p => p.DataModificacao);

      builder.ToTable("FORNECEDORES");
    }
  }
}
