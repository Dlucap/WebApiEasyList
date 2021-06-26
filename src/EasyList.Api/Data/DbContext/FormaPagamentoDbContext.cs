using Microsoft.EntityFrameworkCore;

namespace EasyList.Api.Data
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
        .Property(p => p.UsuarioCriacao);

      modelBuilder.Entity<FormaPagamento>()
         .Property(p => p.DataCriacao);

      modelBuilder.Entity<FormaPagamento>()
         .Property(p => p.UsuarioModificacao);

      modelBuilder.Entity<FormaPagamento>()
         .Property(p => p.DataModificacao);
    }
  }
}
