using Microsoft.EntityFrameworkCore;

namespace EasyList.Api.Data
{
  public class ItmCompraDbContext : DbContext
  {
    public ItmCompraDbContext(DbContextOptions<ItmCompraDbContext> options) : base(options)
    {

    }

    public DbSet<ItmCompra> ItmCompra { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

       modelBuilder.Entity<ItmCompra>()
         .Property(p => p.Id)
          .ValueGeneratedOnAdd();

      modelBuilder.Entity<ItmCompra>()
        .Property(p => p.CompraId)
         .IsRequired();

      modelBuilder.Entity<ItmCompra>()
        .Property(p => p.ProdutoId)
         .IsRequired();

      modelBuilder.Entity<ItmCompra>()
       .Property(p => p.Preco)
        .IsRequired();

      modelBuilder.Entity<ItmCompra>()
        .Property(p => p.Quantidade)
         .HasPrecision(5)
          .IsRequired();

      modelBuilder.Entity<ItmCompra>()
        .Property(p => p.Validade)
         .IsRequired();

      modelBuilder.Entity<ItmCompra>()
        .Property(p => p.UsuarioCriacao);

      modelBuilder.Entity<ItmCompra>()
        .Property(p => p.DataCriacao);

      modelBuilder.Entity<ItmCompra>()
         .Property(p => p.UsuarioModificacao);

      modelBuilder.Entity<ItmCompra>()
        .Property(p => p.DataModificacao);
    }
  }
}
//todo: https://www.youtube.com/watch?v=UZ2Qa4xosl0&ab_channel=balta.io
//todo: https://www.youtube.com/watch?v=ccVmPgxNE6c&ab_channel=CanaldotNET
//todo: https://www.youtube.com/watch?v=rn2gp5VNGKI&ab_channel=CuriousDrive
//todo: https://www.tutorialsteacher.com/webapi/web-api-controller

/*
 * https://docs.microsoft.com/pt-br/aspnet/core/web-api/?view=aspnetcore-5.0
 * https://docs.microsoft.com/pt-br/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio
 * 
 * https://github.com/KevinDockx/BuildingRESTfulAPIAspNetCore3
 * https://github.com/haacked/CodeHaacks/tree/main/src/ListModelBindingDemo/ListModelBindingWeb
 * https://haacked.com/archive/2008/10/23/model-binding-to-a-list.aspx/
 * 
 * */