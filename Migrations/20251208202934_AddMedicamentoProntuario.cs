using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetoTP3_A2.Migrations
{
    /// <inheritdoc />
    public partial class AddMedicamentoProntuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
