using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyList.Data.Migrations
{
    public partial class ajusteModeloCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_FORMAPAGAMENTO_FormaPagamentoId",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_FORNECEDORES_FornecedorId",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_COMPRACOMPARTILHADA_Compra_CompraId",
                table: "COMPRACOMPARTILHADA");

            migrationBuilder.DropForeignKey(
                name: "FK_ENDERECOS_FORNECEDORES_FornecedorID",
                table: "ENDERECOS");

            migrationBuilder.DropForeignKey(
                name: "FK_ItmCompra_Compra_CompraId",
                table: "ItmCompra");

            migrationBuilder.DropIndex(
                name: "IX_COMPRACOMPARTILHADA_CompraId",
                table: "COMPRACOMPARTILHADA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Compra",
                table: "Compra");

            migrationBuilder.RenameTable(
                name: "Compra",
                newName: "COMPRAR");

            migrationBuilder.RenameColumn(
                name: "FornecedorID",
                table: "ENDERECOS",
                newName: "FornecedorId");

            migrationBuilder.RenameIndex(
                name: "IX_ENDERECOS_FornecedorID",
                table: "ENDERECOS",
                newName: "IX_ENDERECOS_FornecedorId");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_FornecedorId",
                table: "COMPRAR",
                newName: "IX_COMPRAR_FornecedorId");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_FormaPagamentoId",
                table: "COMPRAR",
                newName: "IX_COMPRAR_FormaPagamentoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_COMPRAR",
                table: "COMPRAR",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_COMPRACOMPARTILHADA_CompraId",
                table: "COMPRACOMPARTILHADA",
                column: "CompraId");

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
                name: "FK_ENDERECOS_FORNECEDORES_FornecedorId",
                table: "ENDERECOS",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_ENDERECOS_FORNECEDORES_FornecedorId",
                table: "ENDERECOS");

            migrationBuilder.DropForeignKey(
                name: "FK_ItmCompra_COMPRAR_CompraId",
                table: "ItmCompra");

            migrationBuilder.DropIndex(
                name: "IX_COMPRACOMPARTILHADA_CompraId",
                table: "COMPRACOMPARTILHADA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_COMPRAR",
                table: "COMPRAR");

            migrationBuilder.RenameTable(
                name: "COMPRAR",
                newName: "Compra");

            migrationBuilder.RenameColumn(
                name: "FornecedorId",
                table: "ENDERECOS",
                newName: "FornecedorID");

            migrationBuilder.RenameIndex(
                name: "IX_ENDERECOS_FornecedorId",
                table: "ENDERECOS",
                newName: "IX_ENDERECOS_FornecedorID");

            migrationBuilder.RenameIndex(
                name: "IX_COMPRAR_FornecedorId",
                table: "Compra",
                newName: "IX_Compra_FornecedorId");

            migrationBuilder.RenameIndex(
                name: "IX_COMPRAR_FormaPagamentoId",
                table: "Compra",
                newName: "IX_Compra_FormaPagamentoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Compra",
                table: "Compra",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_COMPRACOMPARTILHADA_CompraId",
                table: "COMPRACOMPARTILHADA",
                column: "CompraId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_FORMAPAGAMENTO_FormaPagamentoId",
                table: "Compra",
                column: "FormaPagamentoId",
                principalTable: "FORMAPAGAMENTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_FORNECEDORES_FornecedorId",
                table: "Compra",
                column: "FornecedorId",
                principalTable: "FORNECEDORES",
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
                name: "FK_ItmCompra_Compra_CompraId",
                table: "ItmCompra",
                column: "CompraId",
                principalTable: "Compra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
