using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyList.Api.Migrations
{
    public partial class AddLogEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Level = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Category = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Message = table.Column<string>(type: "longtext", nullable: true),
                    Exception = table.Column<string>(type: "longtext", nullable: true),
                    HttpMethod = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Path = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    StatusCode = table.Column<int>(type: "int", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    UserName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    IpAddress = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    AdditionalInfo = table.Column<string>(type: "longtext", nullable: true),
                    UsuarioCriacao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioModificacao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataModificacao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntry", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogEntry_Timestamp",
                table: "LogEntry",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntry_Level",
                table: "LogEntry",
                column: "Level");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntry_UserName",
                table: "LogEntry",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogEntry");
        }
    }
}
