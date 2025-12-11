using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetoTP3_A2.Migrations
{
    /// <inheritdoc />
    public partial class CreateMedicamentoProntuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicamento_Prontuario_ProntuarioId",
                table: "Medicamento");

            migrationBuilder.DropIndex(
                name: "IX_Medicamento_ProntuarioId",
                table: "Medicamento");

            migrationBuilder.DropColumn(
                name: "ProntuarioId",
                table: "Medicamento");

            migrationBuilder.CreateTable(
                name: "MedicamentoProntuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProntuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicamentoId = table.Column<int>(type: "int", nullable: false),
                    Dosagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Frequencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicamentoProntuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicamentoProntuario_Medicamento_MedicamentoId",
                        column: x => x.MedicamentoId,
                        principalTable: "Medicamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicamentoProntuario_Prontuario_ProntuarioId",
                        column: x => x.ProntuarioId,
                        principalTable: "Prontuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicamentoProntuario_MedicamentoId",
                table: "MedicamentoProntuario",
                column: "MedicamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicamentoProntuario_ProntuarioId",
                table: "MedicamentoProntuario",
                column: "ProntuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicamentoProntuario");

            migrationBuilder.AddColumn<Guid>(
                name: "ProntuarioId",
                table: "Medicamento",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicamento_ProntuarioId",
                table: "Medicamento",
                column: "ProntuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamento_Prontuario_ProntuarioId",
                table: "Medicamento",
                column: "ProntuarioId",
                principalTable: "Prontuario",
                principalColumn: "Id");
        }
    }
}
