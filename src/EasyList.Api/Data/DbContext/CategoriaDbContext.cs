using Microsoft.EntityFrameworkCore;


namespace EasyList.Api.Data
{
  public class CategoriaDbContext : DbContext
  {
    public CategoriaDbContext(DbContextOptions<CategoriaDbContext> options) : base(options)
    {
        
    }

    public DbSet<Categoria> Categoria { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

      modelBuilder.Entity<Categoria>()
       .Property(p => p.Id)
        .ValueGeneratedOnAdd();

      modelBuilder.Entity<Categoria>()
        .Property(p => p.NomeCategoria)
         .HasMaxLength(50)
          .IsRequired();

      modelBuilder.Entity<Categoria>()
       .Property(p => p.UsuarioCriacao);

      modelBuilder.Entity<Categoria>()
        .Property(p => p.DataCriacao);

      modelBuilder.Entity<Categoria>()
        .Property(p => p.UsuarioModificacao);

      modelBuilder.Entity<Categoria>()
        .Property(p => p.DataModificacao);
    }
  }
}
