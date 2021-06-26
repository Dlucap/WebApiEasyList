using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyList.Api.Data
{
  public class FornecedorDbContext : DbContext
  {
    public FornecedorDbContext(DbContextOptions<FornecedorDbContext> options) : base(options)
    {

    }

    public DbSet<Fornecedor> Fornecedor { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Fornecedor>()
      .Property(p => p.Id)
       .ValueGeneratedOnAdd();

      modelBuilder.Entity<Fornecedor>()
        .Property(p => p.Nome)
         .HasMaxLength(80);

      modelBuilder.Entity<Fornecedor>()
        .Property(p => p.NomeFantasia)
         .HasMaxLength(80);

      modelBuilder.Entity<Fornecedor>()
        .Property(p => p.Cnpj)
         .HasMaxLength(14);

      modelBuilder.Entity<Fornecedor>()
        .Property(p => p.Cep)
         .HasMaxLength(8);

      modelBuilder.Entity<Fornecedor>()
        .Property(p => p.Estado)
         .HasMaxLength(2);

      modelBuilder.Entity<Fornecedor>()
        .Property(p => p.Cidade);

      modelBuilder.Entity<Fornecedor>()
        .Property(p => p.Bairro)
         .HasMaxLength(80); ;

      modelBuilder.Entity<Fornecedor>()
        .Property(p => p.Endereco)
         .HasMaxLength(80); ;

      modelBuilder.Entity<Fornecedor>()
        .Property(p => p.Numero)
         .HasPrecision(5);

      modelBuilder.Entity<Fornecedor>()
         .Property(p => p.UsuarioCriacao);

      modelBuilder.Entity<Fornecedor>()
        .Property(p => p.DataCriacao);

      modelBuilder.Entity<Fornecedor>()
        .Property(p => p.UsuarioModificacao);

      modelBuilder.Entity<Fornecedor>()
        .Property(p => p.DataModificacao);

    }
  }
}
