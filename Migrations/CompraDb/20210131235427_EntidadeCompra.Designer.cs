﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiEasyList.Data;

namespace WebApiEasyList.Migrations.CompraDb
{
    [DbContext(typeof(CompraDbContext))]
    [Migration("20210131235427_EntidadeCompra")]
    partial class EntidadeCompra
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("WebApiEasyList.Data.Compra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCompra")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdFornecedor")
                        .HasPrecision(5)
                        .HasColumnType("int");

                    b.Property<string>("RecCreatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("RecCreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RecModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("RecModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Compra");
                });

            modelBuilder.Entity("WebApiEasyList.Data.ItmCompra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(5)
                        .HasColumnType("int");

                    b.Property<int?>("CompraId")
                        .HasColumnType("int");

                    b.Property<int>("IdCompra")
                        .HasPrecision(5)
                        .HasColumnType("int");

                    b.Property<int>("IdProduto")
                        .HasPrecision(5)
                        .HasColumnType("int");

                    b.Property<string>("RecCreatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("RecCreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RecModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("RecModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CompraId");

                    b.ToTable("ItmCompra");
                });

            modelBuilder.Entity("WebApiEasyList.Data.ItmCompra", b =>
                {
                    b.HasOne("WebApiEasyList.Data.Compra", null)
                        .WithMany("ItemCompra")
                        .HasForeignKey("CompraId");
                });

            modelBuilder.Entity("WebApiEasyList.Data.Compra", b =>
                {
                    b.Navigation("ItemCompra");
                });
#pragma warning restore 612, 618
        }
    }
}
