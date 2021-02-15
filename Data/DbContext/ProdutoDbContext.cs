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
        .Property(p => p.Preco)
         .HasPrecision(10, 2);

      modelBuilder.Entity<Produto>()
       .Property(p => p.Quantidade);

      modelBuilder.Entity<Produto>()
       .Property(p => p.QuantidadeEstoque);

      modelBuilder.Entity<Produto>()
       .Property(p => p.Validade);
    }

  }
}