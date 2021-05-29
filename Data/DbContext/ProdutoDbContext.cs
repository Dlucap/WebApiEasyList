using Microsoft.EntityFrameworkCore;

namespace WebApiEasyList.Data
{
  public class ProdutoDbContext : DbContext
  {
    public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options) : base(options)
    {

    }

    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

      modelBuilder.Entity<Produto>()
       .Property(p => p.Id)
        .ValueGeneratedOnAdd();

      modelBuilder.Entity<Produto>()
        .Property(p => p.Marca)
         .HasMaxLength(80);

      modelBuilder.Entity<Produto>()
        .Property(p => p.Nome)
         .HasMaxLength(80);

      modelBuilder.Entity<Produto>()
      .Property(p => p.Descricao)
       .HasMaxLength(80);

      modelBuilder.Entity<Produto>()
       .Property(p => p.Validade);

      modelBuilder.Entity<Produto>()
        .Property(p => p.IdCategoria);

      modelBuilder.Entity<Produto>()
          .Property(p => p.RecCreatedBy);

      modelBuilder.Entity<Produto>()
            .Property(p => p.RecCreatedOn);

      modelBuilder.Entity<Produto>()
            .Property(p => p.RecModifiedBy);

      modelBuilder.Entity<Produto>()
            .Property(p => p.RecModifiedOn);
    }

  }
}