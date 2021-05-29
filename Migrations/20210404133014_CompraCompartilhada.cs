using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiEasyList.Migrations
{
    public partial class CompraCompartilhada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Compartilhado",
                table: "Compra",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Compartilhado",
                table: "Compra");
        }
    }
}
