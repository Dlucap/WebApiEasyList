using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiEasyList.Migrations.FornecedorDb
{
    public partial class FornecedorCepCnpj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Fornecedor",
                type: "varchar(14) CHARACTER SET utf8mb4",
                maxLength: 14,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(80) CHARACTER SET utf8mb4",
                oldMaxLength: 80,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "Fornecedor",
                type: "longtext CHARACTER SET utf8mb4",
                precision: 8,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeFantasia",
                table: "Fornecedor",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cep",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "NomeFantasia",
                table: "Fornecedor");

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Fornecedor",
                type: "varchar(80) CHARACTER SET utf8mb4",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(14) CHARACTER SET utf8mb4",
                oldMaxLength: 14,
                oldNullable: true);
        }
    }
}
