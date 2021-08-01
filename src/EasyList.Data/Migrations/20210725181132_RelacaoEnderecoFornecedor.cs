using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyList.Data.Migrations
{
    public partial class RelacaoEnderecoFornecedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_FormaPagamento_FormaPagamentoId",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_CompraCompartilhada_Compra_CompraId",
                table: "CompraCompartilhada");

            migrationBuilder.DropForeignKey(
                name: "FK_FORNECEDORES_ENDERECOS_EnderecoId",
                table: "FORNECEDORES");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUTOS_Categoria_CategoriaId",
                table: "PRODUTOS");

            migrationBuilder.DropIndex(
                name: "IX_FORNECEDORES_EnderecoId",
                table: "FORNECEDORES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormaPagamento",
                table: "FormaPagamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompraCompartilhada",
                table: "CompraCompartilhada");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "FORNECEDORES");

            migrationBuilder.RenameTable(
                name: "FormaPagamento",
                newName: "FORMAPAGAMENTO");

            migrationBuilder.RenameTable(
                name: "CompraCompartilhada",
                newName: "COMPRACOMPARTILHADA");

            migrationBuilder.RenameTable(
                name: "Categoria",
                newName: "CATEGORIA");

            migrationBuilder.RenameIndex(
                name: "IX_CompraCompartilhada_CompraId",
                table: "COMPRACOMPARTILHADA",
                newName: "IX_COMPRACOMPARTILHADA_CompraId");

            migrationBuilder.AlterColumn<string>(
                name: "NomeFormaPagamento",
                table: "FORMAPAGAMENTO",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<ulong>(
                name: "Ativo",
                table: "FORMAPAGAMENTO",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "ENDERECOS",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataModificacao",
                table: "ENDERECOS",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "FornecedorID",
                table: "ENDERECOS",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacao",
                table: "ENDERECOS",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacao",
                table: "ENDERECOS",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UsuariosCompartilhados",
                table: "COMPRACOMPARTILHADA",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeCategoria",
                table: "CATEGORIA",
                type: "varchar(100)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<ulong>(
                name: "Ativo",
                table: "CATEGORIA",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FORMAPAGAMENTO",
                table: "FORMAPAGAMENTO",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_COMPRACOMPARTILHADA",
                table: "COMPRACOMPARTILHADA",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CATEGORIA",
                table: "CATEGORIA",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ENDERECOS_FornecedorID",
                table: "ENDERECOS",
                column: "FornecedorID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_FORMAPAGAMENTO_FormaPagamentoId",
                table: "Compra",
                column: "FormaPagamentoId",
                principalTable: "FORMAPAGAMENTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_COMPRACOMPARTILHADA_Compra_CompraId",
                table: "COMPRACOMPARTILHADA",
                column: "CompraId",
                principalTable: "Compra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ENDERECOS_FORNECEDORES_FornecedorID",
                table: "ENDERECOS",
                column: "FornecedorID",
                principalTable: "FORNECEDORES",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUTOS_CATEGORIA_CategoriaId",
                table: "PRODUTOS",
                column: "CategoriaId",
                principalTable: "CATEGORIA",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_FORMAPAGAMENTO_FormaPagamentoId",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_COMPRACOMPARTILHADA_Compra_CompraId",
                table: "COMPRACOMPARTILHADA");

            migrationBuilder.DropForeignKey(
                name: "FK_ENDERECOS_FORNECEDORES_FornecedorID",
                table: "ENDERECOS");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUTOS_CATEGORIA_CategoriaId",
                table: "PRODUTOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FORMAPAGAMENTO",
                table: "FORMAPAGAMENTO");

            migrationBuilder.DropIndex(
                name: "IX_ENDERECOS_FornecedorID",
                table: "ENDERECOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_COMPRACOMPARTILHADA",
                table: "COMPRACOMPARTILHADA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CATEGORIA",
                table: "CATEGORIA");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "ENDERECOS");

            migrationBuilder.DropColumn(
                name: "DataModificacao",
                table: "ENDERECOS");

            migrationBuilder.DropColumn(
                name: "FornecedorID",
                table: "ENDERECOS");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacao",
                table: "ENDERECOS");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacao",
                table: "ENDERECOS");

            migrationBuilder.RenameTable(
                name: "FORMAPAGAMENTO",
                newName: "FormaPagamento");

            migrationBuilder.RenameTable(
                name: "COMPRACOMPARTILHADA",
                newName: "CompraCompartilhada");

            migrationBuilder.RenameTable(
                name: "CATEGORIA",
                newName: "Categoria");

            migrationBuilder.RenameIndex(
                name: "IX_COMPRACOMPARTILHADA_CompraId",
                table: "CompraCompartilhada",
                newName: "IX_CompraCompartilhada_CompraId");

            migrationBuilder.AddColumn<Guid>(
                name: "EnderecoId",
                table: "FORNECEDORES",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "NomeFormaPagamento",
                table: "FormaPagamento",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "FormaPagamento",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "UsuariosCompartilhados",
                table: "CompraCompartilhada",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "NomeCategoria",
                table: "Categoria",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "Categoria",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "bit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormaPagamento",
                table: "FormaPagamento",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompraCompartilhada",
                table: "CompraCompartilhada",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FORNECEDORES_EnderecoId",
                table: "FORNECEDORES",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_FormaPagamento_FormaPagamentoId",
                table: "Compra",
                column: "FormaPagamentoId",
                principalTable: "FormaPagamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompraCompartilhada_Compra_CompraId",
                table: "CompraCompartilhada",
                column: "CompraId",
                principalTable: "Compra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FORNECEDORES_ENDERECOS_EnderecoId",
                table: "FORNECEDORES",
                column: "EnderecoId",
                principalTable: "ENDERECOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUTOS_Categoria_CategoriaId",
                table: "PRODUTOS",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
