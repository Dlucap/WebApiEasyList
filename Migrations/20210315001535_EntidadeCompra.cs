using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiEasyList.Migrations
{
    public partial class EntidadeCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdFornecedor = table.Column<int>(type: "int", precision: 5, nullable: false),
                    DataCompra = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecCreatedBy = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    RecCreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecModifiedBy = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    RecModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                  table.PrimaryKey("PK_Compra", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compra");
        }
    }
}
