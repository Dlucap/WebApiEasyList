using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiEasyList.Migrations.CompraDb
{
    public partial class teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItmCompra_Compra_CompraId",
                table: "ItmCompra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItmCompra",
                table: "ItmCompra");

            migrationBuilder.RenameTable(
                name: "ItmCompra",
                newName: "itmCompra");

            migrationBuilder.RenameIndex(
                name: "IX_ItmCompra_CompraId",
                table: "itmCompra",
                newName: "IX_itmCompra_CompraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_itmCompra",
                table: "itmCompra",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_itmCompra_Compra_CompraId",
                table: "itmCompra",
                column: "CompraId",
                principalTable: "Compra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itmCompra_Compra_CompraId",
                table: "itmCompra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_itmCompra",
                table: "itmCompra");

            migrationBuilder.RenameTable(
                name: "itmCompra",
                newName: "ItmCompra");

            migrationBuilder.RenameIndex(
                name: "IX_itmCompra_CompraId",
                table: "ItmCompra",
                newName: "IX_ItmCompra_CompraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItmCompra",
                table: "ItmCompra",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItmCompra_Compra_CompraId",
                table: "ItmCompra",
                column: "CompraId",
                principalTable: "Compra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
