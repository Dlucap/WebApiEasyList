using Microsoft.EntityFrameworkCore;


namespace WebApiEasyList.Data
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
         .HasMaxLength(50);
      
      modelBuilder.Entity<Categoria>()
          .Property(p => p.RecCreatedBy);

      modelBuilder.Entity<Categoria>()
            .Property(p => p.RecCreatedOn);

      modelBuilder.Entity<Categoria>()
            .Property(p => p.RecModifiedBy);

      modelBuilder.Entity<Categoria>()
            .Property(p => p.RecModifiedOn);
    }
  }
}
