using EasyList.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyList.Data
{
  public class FormaPagamentoMapping : IEntityTypeConfiguration<FormaPagamento>
  {
    public void Configure(EntityTypeBuilder<FormaPagamento> builder)
    {
      builder.HasKey(p => p.Id);

      builder.Property(p => p.Id)
        .ValueGeneratedOnAdd();

      builder.Property(p => p.NomeFormaPagamento)
         .IsRequired();
      builder.Property(p => p.Ativo)
        .HasColumnType("bit");

      builder.Property(p => p.UsuarioCriacao);

      builder.Property(p => p.DataCriacao);

      builder.Property(p => p.UsuarioModificacao);

      builder.Property(p => p.DataModificacao);

      builder.ToTable("FORMAPAGAMENTO");
    }
  }
}
