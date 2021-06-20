using Microsoft.EntityFrameworkCore;

namespace WebApiEasyList.Data
{
  public class CompraCompartilhadaDbContext : DbContext
  {
    public CompraCompartilhadaDbContext(DbContextOptions<CompraCompartilhadaDbContext> options) : base(options)
    {

    }

    public DbSet<CompraCompartilhada> CompraCompartilhada { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<CompraCompartilhada>()
        .Property(p => p.Id)
         .ValueGeneratedOnAdd();

      modelBuilder.Entity<CompraCompartilhada>()
        .Property(p => p.CompraId)
         .IsRequired();

      modelBuilder.Entity<CompraCompartilhada>()
        .Property(p => p.UsuariosCompartilhados)
         .IsRequired();

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
