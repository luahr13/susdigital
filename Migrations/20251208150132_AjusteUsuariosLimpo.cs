using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetoTP3_A2.Migrations
{
    /// <inheritdoc />
    public partial class AjusteUsuariosLimpo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CRM",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Especialidade",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Prontuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prontuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prontuario_AspNetUsers_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);

                    table.ForeignKey(
                        name: "FK_Prontuario_AspNetUsers_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArquivoProntuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProntuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeArquivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caminho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoProntuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArquivoProntuario_Prontuario_ProntuarioId",
                        column: x => x.ProntuarioId,
                        principalTable: "Prontuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArquivoProntuario_ProntuarioId",
                table: "ArquivoProntuario",
                column: "ProntuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Prontuario_MedicoId",
                table: "Prontuario",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Prontuario_PacienteId",
                table: "Prontuario",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArquivoProntuario");

            migrationBuilder.DropTable(
                name: "Prontuario");

            migrationBuilder.DropColumn(
                name: "CRM",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Especialidade",
                table: "AspNetUsers");
        }
    }
}
