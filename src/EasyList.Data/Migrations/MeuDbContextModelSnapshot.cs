﻿// <auto-generated />
using System;
using EasyList.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EasyList.Data.Migrations
{
    [DbContext(typeof(MeuDbContext))]
    partial class MeuDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("EasyList.Business.Models.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<ulong>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NomeCategoria")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsuarioCriacao")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsuarioModificacao")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("CATEGORIA");
                });

            modelBuilder.Entity("EasyList.Business.Models.Compra", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Compartilhado")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("DataCompra")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("FormaPagamentoId")
                        .HasPrecision(5)
                        .HasColumnType("char(36)");

                    b.Property<Guid>("FornecedorId")
                        .HasPrecision(5)
                        .HasColumnType("char(36)");

                    b.Property<int>("StatusCompra")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioCriacao")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("UsuarioModificacao")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("Id");

                    b.HasIndex("FormaPagamentoId");

                    b.HasIndex("FornecedorId");

                    b.ToTable("COMPRA");
                });

            modelBuilder.Entity("EasyList.Business.Models.CompraCompartilhada", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CompraId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UsuarioCriacao")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsuarioModificacao")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsuariosCompartilhados")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CompraId");

                    b.ToTable("COMPRACOMPARTILHADA");
                });

            modelBuilder.Entity("EasyList.Business.Models.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(250)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("FornecedorId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UsuarioCriacao")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsuarioModificacao")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId")
                        .IsUnique();

                    b.ToTable("ENDERECOS");
                });

            modelBuilder.Entity("EasyList.Business.Models.FormaPagamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<ulong>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NomeFormaPagamento")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsuarioCriacao")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsuarioModificacao")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("FORMAPAGAMENTO");
                });

            modelBuilder.Entity("EasyList.Business.Models.Fornecedor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<ulong>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Cnpj")
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("NomeFantasia")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("UsuarioCriacao")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsuarioModificacao")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("FORNECEDORES");
                });

            modelBuilder.Entity("EasyList.Business.Models.ItmCompra", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("CompraId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(65,30)");

                    b.Property<Guid>("ProdutoId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioCriacao")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsuarioModificacao")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Validade")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CompraId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ItmCompra");
                });

            modelBuilder.Entity("EasyList.Business.Models.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<ulong>("Ativo")
                        .HasColumnType("bit");

                    b.Property<Guid>("CategoriaId")
                        .HasColumnType("char(36)");

                    b.Property<ulong>("ControlaEtoque")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(0ul);

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("UsuarioCriacao")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsuarioModificacao")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("PRODUTO");
                });

            modelBuilder.Entity("EasyList.Business.Models.Compra", b =>
                {
                    b.HasOne("EasyList.Business.Models.FormaPagamento", "FormaPagamento")
                        .WithMany()
                        .HasForeignKey("FormaPagamentoId");

                    b.HasOne("EasyList.Business.Models.Fornecedor", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("FornecedorId")
                        .IsRequired();

                    b.Navigation("FormaPagamento");

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("EasyList.Business.Models.CompraCompartilhada", b =>
                {
                    b.HasOne("EasyList.Business.Models.Compra", "Compra")
                        .WithMany()
                        .HasForeignKey("CompraId")
                        .IsRequired();

                    b.Navigation("Compra");
                });

            modelBuilder.Entity("EasyList.Business.Models.Endereco", b =>
                {
                    b.HasOne("EasyList.Business.Models.Fornecedor", "Fornecedor")
                        .WithOne("Endereco")
                        .HasForeignKey("EasyList.Business.Models.Endereco", "FornecedorId")
                        .IsRequired();

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("EasyList.Business.Models.ItmCompra", b =>
                {
                    b.HasOne("EasyList.Business.Models.Compra", "Compra")
                        .WithMany("ItemsCompra")
                        .HasForeignKey("CompraId")
                        .IsRequired();

                    b.HasOne("EasyList.Business.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .IsRequired();

                    b.Navigation("Compra");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("EasyList.Business.Models.Produto", b =>
                {
                    b.HasOne("EasyList.Business.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("EasyList.Business.Models.Compra", b =>
                {
                    b.Navigation("ItemsCompra");
                });

            modelBuilder.Entity("EasyList.Business.Models.Fornecedor", b =>
                {
                    b.Navigation("Endereco");
                });
#pragma warning restore 612, 618
        }
    }
}
