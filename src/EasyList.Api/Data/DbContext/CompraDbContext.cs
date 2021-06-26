using Microsoft.EntityFrameworkCore;

namespace EasyList.Api.Data
{
  public class CompraDbContext : DbContext
  {
    public CompraDbContext(DbContextOptions<CompraDbContext> options) : base(options)
    {

    }

    public DbSet<Compra> Compra { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Compra>()
        .Property(p => p.Id)
         .ValueGeneratedOnAdd();

      modelBuilder.Entity<Compra>()
        .Property(p => p.FornecedorId)
         .HasPrecision(5)
          .IsRequired();

      modelBuilder.Entity<Compra>()
        .Property(p => p.FormaPagamentoId)
         .HasPrecision(5)
          .IsRequired();

      modelBuilder.Entity<Compra>()
        .Property(p => p.Compartilhado);

      modelBuilder.Entity<Compra>()
        .Property(p => p.StatusCompra);

      //modelBuilder.Entity<Compra>()        
      //  .HasMany(c => c.ItemCompra);
         

      modelBuilder.Entity<Compra>()
        .Property(p => p.DataCompra);

      modelBuilder.Entity<Compra>()
        .Property(p => p.UsuarioCriacao);

      modelBuilder.Entity<Compra>()
        .Property(p => p.DataCriacao);

      modelBuilder.Entity<Compra>()
        .Property(p => p.UsuarioModificacao);

      modelBuilder.Entity<Compra>()
        .Property(p => p.DataModificacao);

    }
  }
}

/* 
 * https://www.devmedia.com.br/entity-framework-tutorial/27764
       * https://www.devmedia.com.br/entity-framework-tutorial/27764
       * https://www.entityframeworktutorial.net/efcore/configure-one-to-many-relationship-using-fluent-api-in-ef-core.aspx
         modelBuilder.Entity<ItmCompra>()
           .HasOne(c => c.Id)
         .Property(p => p.Id)
         .ValueGeneratedOnAdd()
         .HasPrecision(5);
//.IsRequired();
*/