using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiEasyList.Migrations.CompraCompartilhadaDb
{
    public partial class CompraCompartilhadaAjustada2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompraCompartilhadas",
                table: "CompraCompartilhadas");

            migrationBuilder.RenameTable(
                name: "CompraCompartilhadas",
                newName: "CompraCompartilhada");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompraCompartilhada",
                table: "CompraCompartilhada",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompraCompartilhada",
                table: "CompraCompartilhada");

            migrationBuilder.RenameTable(
                name: "CompraCompartilhada",
                newName: "CompraCompartilhadas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompraCompartilhadas",
                table: "CompraCompartilhadas",
                column: "Id");
        }
    }
}
