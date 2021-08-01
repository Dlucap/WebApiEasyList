using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyList.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    NomeCategoria = table.Column<string>(type: "varchar(100)", nullable: true),
                    UsuarioCriacao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioModificacao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataModificacao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ENDERECOS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Cep = table.Column<string>(type: "varchar(8)", nullable: false),
                    Estado = table.Column<string>(type: "varchar(50)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    Bairro = table.Column<string>(type: "varchar(100)", nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(200)", nullable: false),
                    Complemento = table.Column<string>(type: "varchar(250)", nullable: true),
                    Numero = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENDERECOS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormaPagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    NomeFormaPagamento = table.Column<string>(type: "varchar(100)", nullable: true),
                    UsuarioCriacao = table.Column<string>(type: "varchar(100)", nullable: true),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioModificacao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataModificacao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTOS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Marca = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Nome = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true),
                    UsuarioCriacao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioModificacao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataModificacao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PRODUTOS_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FORNECEDORES",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true),
                    NomeFantasia = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true),
                    Cnpj = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: true),
                    EnderecoId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UsuarioCriacao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioModificacao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataModificacao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORNECEDORES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FORNECEDORES_ENDERECOS_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "ENDERECOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "char(36)", nullable: false),
                    FormaPagamentoId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Compartilhado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StatusCompra = table.Column<int>(type: "int", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioCriacao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioModificacao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataModificacao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compra_FormaPagamento_FormaPagamentoId",
                        column: x => x.FormaPagamentoId,
                        principalTable: "FormaPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Compra_FORNECEDORES_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "FORNECEDORES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompraCompartilhada",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CompraId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UsuariosCompartilhados = table.Column<string>(type: "varchar(100)", nullable: true),
                    UsuarioCriacao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioModificacao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataModificacao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraCompartilhada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompraCompartilhada_Compra_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItmCompra",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CompraId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Validade = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioCriacao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioModificacao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataModificacao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItmCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItmCompra_Compra_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItmCompra_PRODUTOS_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "PRODUTOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compra_FormaPagamentoId",
                table: "Compra",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_FornecedorId",
                table: "Compra",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraCompartilhada_CompraId",
                table: "CompraCompartilhada",
                column: "CompraId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FORNECEDORES_EnderecoId",
                table: "FORNECEDORES",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItmCompra_CompraId",
                table: "ItmCompra",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_ItmCompra_ProdutoId",
                table: "ItmCompra",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTOS_CategoriaId",
                table: "PRODUTOS",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompraCompartilhada");

            migrationBuilder.DropTable(
                name: "ItmCompra");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "PRODUTOS");

            migrationBuilder.DropTable(
                name: "FormaPagamento");

            migrationBuilder.DropTable(
                name: "FORNECEDORES");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "ENDERECOS");
        }
    }
}
