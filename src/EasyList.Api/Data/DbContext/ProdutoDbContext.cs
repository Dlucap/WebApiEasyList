using Microsoft.EntityFrameworkCore;

namespace EasyList.Api.Data
{
  public class ProdutoDbContext : DbContext
  {
    public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options) : base(options)
    {

    }

    public DbSet<Produto> Produto { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

      modelBuilder.Entity<Produto>()
        .Property(p => p.Id)
         .ValueGeneratedOnAdd();

      modelBuilder.Entity<Produto>()
        .Property(p => p.CategoriaId)
         .IsRequired();

      modelBuilder.Entity<Produto>()
        .Property(p => p.Marca)
         .HasMaxLength(80)
          .IsRequired(); ;

      modelBuilder.Entity<Produto>()
        .Property(p => p.Nome)
         .HasMaxLength(80)
          .IsRequired(); ;

      modelBuilder.Entity<Produto>()
        .Property(p => p.Descricao)
         .HasMaxLength(80);          

      modelBuilder.Entity<Produto>()
        .Property(p => p.UsuarioCriacao);

      modelBuilder.Entity<Produto>()
        .Property(p => p.DataCriacao);

      modelBuilder.Entity<Produto>()
         .Property(p => p.UsuarioModificacao);

      modelBuilder.Entity<Produto>()
        .Property(p => p.DataModificacao);
    }

  }
}