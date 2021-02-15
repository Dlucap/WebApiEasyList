﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiEasyList.Data;

namespace WebApiEasyList.Migrations.FornecedorDb
{
    [DbContext(typeof(FornecedorDbContext))]
    [Migration("20210124223912_FornecedorAtualizado")]
    partial class FornecedorAtualizado
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("WebApiEasyList.Data.Fornecedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Bairro")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4");

                    b.Property<string>("Cep")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8) CHARACTER SET utf8mb4");

                    b.Property<string>("Cidade")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Cnpj")
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14) CHARACTER SET utf8mb4");

                    b.Property<string>("Endereco")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4");

                    b.Property<string>("Estado")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2) CHARACTER SET utf8mb4");

                    b.Property<string>("Nome")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4");

                    b.Property<string>("NomeFantasia")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Numero")
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

                    b.ToTable("Fornecedor");
                });
#pragma warning restore 612, 618
        }
    }
}
