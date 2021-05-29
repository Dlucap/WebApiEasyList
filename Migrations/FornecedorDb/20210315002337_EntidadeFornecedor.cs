using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiEasyList.Migrations.FornecedorDb
{
    public partial class EntidadeFornecedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(80) CHARACTER SET utf8mb4", maxLength: 80, nullable: true),
                    NomeFantasia = table.Column<string>(type: "varchar(80) CHARACTER SET utf8mb4", maxLength: 80, nullable: true),
                    Cnpj = table.Column<string>(type: "varchar(14) CHARACTER SET utf8mb4", maxLength: 14, nullable: true),
                    Cep = table.Column<string>(type: "varchar(8) CHARACTER SET utf8mb4", maxLength: 8, nullable: true),
                    Estado = table.Column<string>(type: "varchar(2) CHARACTER SET utf8mb4", maxLength: 2, nullable: true),
                    Cidade = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(80) CHARACTER SET utf8mb4", maxLength: 80, nullable: true),
                    Endereco = table.Column<string>(type: "varchar(80) CHARACTER SET utf8mb4", maxLength: 80, nullable: true),
                    Numero = table.Column<int>(type: "int", precision: 5, nullable: false),
                    RecCreatedBy = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    RecCreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecModifiedBy = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    RecModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fornecedor");
        }
    }
}
