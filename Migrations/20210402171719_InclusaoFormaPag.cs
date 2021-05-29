using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiEasyList.Migrations
{
    public partial class InclusaoFormaPag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdFormaPagamento",
                table: "Compra",
                type: "int",
                precision: 5,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdFormaPagamento",
                table: "Compra");
        }
    }
}
