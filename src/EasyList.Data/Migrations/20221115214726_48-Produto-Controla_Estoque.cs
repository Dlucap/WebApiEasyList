using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyList.Data.Migrations
{
    public partial class _48ProdutoControla_Estoque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "ControlaEstoque",
                table: "PRODUTO",
                type: "bit",
                nullable: false,
                defaultValue: 0ul);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ControlaEtoque",
                table: "PRODUTO");
        }
    }
}
