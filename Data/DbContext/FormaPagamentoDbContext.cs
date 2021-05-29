using Microsoft.EntityFrameworkCore;

namespace WebApiEasyList.Data
{
  public class FormaPagamentoDbContext : DbContext
  {
    public FormaPagamentoDbContext(DbContextOptions<FormaPagamentoDbContext> options) : base(options)
    {

    }

    public DbSet<FormaPagamento> FormaPagamento { get; set; }
     
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<FormaPagamento>()
       .Property(p => p.Id)
       .ValueGeneratedOnAdd();

      modelBuilder.Entity<FormaPagamento>()
        .Property(p => p.NomeFormaPagamento) 
        .IsRequired();

      modelBuilder.Entity<FormaPagamento>()
            .Property(p => p.RecCreatedBy);

      modelBuilder.Entity<FormaPagamento>()
            .Property(p => p.RecCreatedOn);

      modelBuilder.Entity<FormaPagamento>()
            .Property(p => p.RecModifiedBy);

      modelBuilder.Entity<FormaPagamento>()
            .Property(p => p.RecModifiedOn);
    }
  }
}
