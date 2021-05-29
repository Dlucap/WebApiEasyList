using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiEasyList.Migrations.ItmCompraDb
{
    public partial class ItmPrecoCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Preco",
                table: "ItmCompra",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preco",
                table: "ItmCompra");
        }
    }
}
