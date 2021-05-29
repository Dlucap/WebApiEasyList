using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiEasyList.Migrations.CompraCompartilhadaDb
{
    public partial class CompraCompartilhadaAjustada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_compraCompartilhadas",
                table: "compraCompartilhadas");

            migrationBuilder.DropColumn(
                name: "Validade",
                table: "compraCompartilhadas");

            migrationBuilder.RenameTable(
                name: "compraCompartilhadas",
                newName: "CompraCompartilhadas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompraCompartilhadas",
                table: "CompraCompartilhadas",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompraCompartilhadas",
                table: "CompraCompartilhadas");

            migrationBuilder.RenameTable(
                name: "CompraCompartilhadas",
                newName: "compraCompartilhadas");

            migrationBuilder.AddColumn<DateTime>(
                name: "Validade",
                table: "compraCompartilhadas",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_compraCompartilhadas",
                table: "compraCompartilhadas",
                column: "Id");
        }
    }
}
