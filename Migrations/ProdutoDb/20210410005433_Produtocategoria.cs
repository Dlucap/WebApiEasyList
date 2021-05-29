using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiEasyList.Migrations.ProdutoDb
{
    public partial class Produtocategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCategoria",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCategoria",
                table: "Produtos");
        }
    }
}
