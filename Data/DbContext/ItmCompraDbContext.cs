using Microsoft.EntityFrameworkCore;

namespace WebApiEasyList.Data
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
     .Property(p => p.IdCompra)
      .IsRequired();

      modelBuilder.Entity<ItmCompra>()
     .Property(p => p.IdProduto)
      .IsRequired();

      modelBuilder.Entity<ItmCompra>()
    .Property(p => p.IdCategoria)
     .IsRequired();

      modelBuilder.Entity<ItmCompra>()
        .Property(p => p.Quantidade)
        .HasPrecision(5);

      modelBuilder.Entity<ItmCompra>()
      .Property(p => p.Preco)
      .IsRequired();

      modelBuilder.Entity<ItmCompra>()
          .Property(p => p.RecCreatedBy);

      modelBuilder.Entity<ItmCompra>()
            .Property(p => p.RecCreatedOn);

      modelBuilder.Entity<ItmCompra>()
            .Property(p => p.RecModifiedBy);

      modelBuilder.Entity<ItmCompra>()
            .Property(p => p.RecModifiedOn);
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