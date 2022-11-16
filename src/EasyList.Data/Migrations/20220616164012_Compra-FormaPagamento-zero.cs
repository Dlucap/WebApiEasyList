using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyList.Data.Migrations
{
    public partial class CompraFormaPagamentozero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "FormaPagamentoId",
                table: "COMPRA",
                type: "char(36)",
                precision: 5,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldPrecision: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "FormaPagamentoId",
                table: "COMPRA",
                type: "char(36)",
                precision: 5,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldPrecision: 5,
                oldNullable: true);
        }
    }
}
