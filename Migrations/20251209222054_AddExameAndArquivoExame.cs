using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetoTP3_A2.Migrations
{
    /// <inheritdoc />
    public partial class AddExameAndArquivoExame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Prontuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Exames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProntuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SolicitadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcluidoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exames_Prontuario_ProntuarioId",
                        column: x => x.ProntuarioId,
                        principalTable: "Prontuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArquivosExame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeArquivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caminho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivosExame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArquivosExame_Exames_ExameId",
                        column: x => x.ExameId,
                        principalTable: "Exames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArquivosExame_ExameId",
                table: "ArquivosExame",
                column: "ExameId");

            migrationBuilder.CreateIndex(
                name: "IX_Exames_ProntuarioId",
                table: "Exames",
                column: "ProntuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArquivosExame");

            migrationBuilder.DropTable(
                name: "Exames");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Prontuario");
        }
    }
}
