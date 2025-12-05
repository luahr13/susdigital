using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetoTP3_A2.Migrations
{
    /// <inheritdoc />
    public partial class MedicamentosAltereTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtivarLembrete",
                table: "Medicamento");

            migrationBuilder.DropColumn(
                name: "HorarioLembrete",
                table: "Medicamento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AtivarLembrete",
                table: "Medicamento",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HorarioLembrete",
                table: "Medicamento",
                type: "time",
                nullable: true);
        }
    }
}
