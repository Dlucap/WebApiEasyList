//using EasyList.Business.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace EasyList.Data
//{
//  public class CompraMapping : IEntityTypeConfiguration<Compra>
//  {
//    public void Configure(EntityTypeBuilder<Compra> builder)
//    {
//      builder.HasKey(p => p.Id);

//      builder.Property(p => p.Id)
//         .ValueGeneratedOnAdd();

//      builder.Property(p => p.FornecedorId)
//         .HasPrecision(5)
//          .IsRequired();

//      builder.Property(p => p.FormaPagamentoId)
//         .HasPrecision(5)
//          .IsRequired();

//      builder.Property(p => p.Compartilhado);

//      builder.Property(p => p.StatusCompra);

//      //builder.Entity<Compra>()        
//      //  .HasMany(c => c.ItemCompra);


//      builder.Property(p => p.DataCompra);

//      builder.Property(p => p.UsuarioCriacao);

//      builder.Property(p => p.DataCriacao);

//      builder.Property(p => p.UsuarioModificacao);

//      builder.Property(p => p.DataModificacao);

//      builder.ToTable("COMPRAR");
//    }
//  }
//}
