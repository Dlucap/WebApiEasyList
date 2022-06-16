using EasyList.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyList.Data
{
    public class CompraMapping : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
               .ValueGeneratedOnAdd();

            builder.Property(p => p.FornecedorId)
               .HasPrecision(5)
                .IsRequired();

            builder.Property(p => p.FormaPagamentoId)
               .HasPrecision(5)
                .IsRequired();

            builder.Property(p => p.Compartilhado);

            builder.Property(p => p.StatusCompra);

            //builder.Entity<Compra>()        
            //  .HasMany(c => c.ItemCompra);

            builder.Property(p => p.DataCompra);

            builder.Property(p => p.UsuarioCriacao)
                      .HasMaxLength(80)
                      .HasColumnType("varchar(80)");

            builder.Property(p => p.DataCriacao);

            builder.Property(p => p.UsuarioModificacao)
                      .HasMaxLength(80)
                      .HasColumnType("varchar(80)");

            builder.Property(p => p.DataModificacao);

            builder.ToTable("COMPRA");
        }
    }
}
/**
 * Avaliar o mapping para que possa ser gerado da forma correta
 * link de apoio;
 * https://docs.microsoft.com/en-gb/ef/ef6/fundamentals/relationships?redirectedfrom=MSDN
 * 
 * **/