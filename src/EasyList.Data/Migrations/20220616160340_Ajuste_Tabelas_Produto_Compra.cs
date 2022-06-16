using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyList.Data.Migrations
{
    public partial class Ajuste_Tabelas_Produto_Compra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_COMPRACOMPARTILHADA_COMPRAR_CompraId",
                table: "COMPRACOMPARTILHADA");

            migrationBuilder.DropForeignKey(
                name: "FK_COMPRAR_FORMAPAGAMENTO_FormaPagamentoId",
                table: "COMPRAR");

            migrationBuilder.DropForeignKey(
                name: "FK_COMPRAR_FORNECEDORES_FornecedorId",
                table: "COMPRAR");

            migrationBuilder.DropForeignKey(
                name: "FK_ItmCompra_COMPRAR_CompraId",
                table: "ItmCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_ItmCompra_PRODUTOS_ProdutoId",
                table: "ItmCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUTOS_CATEGORIA_CategoriaId",
                table: "PRODUTOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PRODUTOS",
                table: "PRODUTOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_COMPRAR",
                table: "COMPRAR");

            migrationBuilder.RenameTable(
                name: "PRODUTOS",
                newName: "PRODUTO");

            migrationBuilder.RenameTable(
                name: "COMPRAR",
                newName: "COMPRA");

            migrationBuilder.RenameIndex(
                name: "IX_PRODUTOS_CategoriaId",
                table: "PRODUTO",
                newName: "IX_PRODUTO_CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_COMPRAR_FornecedorId",
                table: "COMPRA",
                newName: "IX_COMPRA_FornecedorId");

            migrationBuilder.RenameIndex(
                name: "IX_COMPRAR_FormaPagamentoId",
                table: "COMPRA",
                newName: "IX_COMPRA_FormaPagamentoId");

            migrationBuilder.AddColumn<ulong>(
                name: "Ativo",
                table: "PRODUTO",
                type: "bit",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioModificacao",
                table: "COMPRA",
                type: "varchar(80)",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioCriacao",
                table: "COMPRA",
                type: "varchar(80)",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRODUTO",
                table: "PRODUTO",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_COMPRA",
                table: "COMPRA",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_COMPRA_FORMAPAGAMENTO_FormaPagamentoId",
                table: "COMPRA",
                column: "FormaPagamentoId",
                principalTable: "FORMAPAGAMENTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_COMPRA_FORNECEDORES_FornecedorId",
                table: "COMPRA",
                column: "FornecedorId",
                principalTable: "FORNECEDORES",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_COMPRACOMPARTILHADA_COMPRA_CompraId",
                table: "COMPRACOMPARTILHADA",
                column: "CompraId",
                principalTable: "COMPRA",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ItmCompra_COMPRA_CompraId",
                table: "ItmCompra",
                column: "CompraId",
                principalTable: "COMPRA",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItmCompra_PRODUTO_ProdutoId",
                table: "ItmCompra",
                column: "ProdutoId",
                principalTable: "PRODUTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUTO_CATEGORIA_CategoriaId",
                table: "PRODUTO",
                column: "CategoriaId",
                principalTable: "CATEGORIA",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_COMPRA_FORMAPAGAMENTO_FormaPagamentoId",
                table: "COMPRA");

            migrationBuilder.DropForeignKey(
                name: "FK_COMPRA_FORNECEDORES_FornecedorId",
                table: "COMPRA");

            migrationBuilder.DropForeignKey(
                name: "FK_COMPRACOMPARTILHADA_COMPRA_CompraId",
                table: "COMPRACOMPARTILHADA");

            migrationBuilder.DropForeignKey(
                name: "FK_ItmCompra_COMPRA_CompraId",
                table: "ItmCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_ItmCompra_PRODUTO_ProdutoId",
                table: "ItmCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUTO_CATEGORIA_CategoriaId",
                table: "PRODUTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PRODUTO",
                table: "PRODUTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_COMPRA",
                table: "COMPRA");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "PRODUTO");

            migrationBuilder.RenameTable(
                name: "PRODUTO",
                newName: "PRODUTOS");

            migrationBuilder.RenameTable(
                name: "COMPRA",
                newName: "COMPRAR");

            migrationBuilder.RenameIndex(
                name: "IX_PRODUTO_CategoriaId",
                table: "PRODUTOS",
                newName: "IX_PRODUTOS_CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_COMPRA_FornecedorId",
                table: "COMPRAR",
                newName: "IX_COMPRAR_FornecedorId");

            migrationBuilder.RenameIndex(
                name: "IX_COMPRA_FormaPagamentoId",
                table: "COMPRAR",
                newName: "IX_COMPRAR_FormaPagamentoId");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioModificacao",
                table: "COMPRAR",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldMaxLength: 80,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioCriacao",
                table: "COMPRAR",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldMaxLength: 80,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRODUTOS",
                table: "PRODUTOS",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_COMPRAR",
                table: "COMPRAR",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_COMPRACOMPARTILHADA_COMPRAR_CompraId",
                table: "COMPRACOMPARTILHADA",
                column: "CompraId",
                principalTable: "COMPRAR",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_COMPRAR_FORMAPAGAMENTO_FormaPagamentoId",
                table: "COMPRAR",
                column: "FormaPagamentoId",
                principalTable: "FORMAPAGAMENTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_COMPRAR_FORNECEDORES_FornecedorId",
                table: "COMPRAR",
                column: "FornecedorId",
                principalTable: "FORNECEDORES",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItmCompra_COMPRAR_CompraId",
                table: "ItmCompra",
                column: "CompraId",
                principalTable: "COMPRAR",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItmCompra_PRODUTOS_ProdutoId",
                table: "ItmCompra",
                column: "ProdutoId",
                principalTable: "PRODUTOS",
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
    }
}
