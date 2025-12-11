using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetoTP3_A2.Migrations
{
    /// <inheritdoc />
    public partial class AddPrescricaoFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoPrescricao",
                table: "MedicamentoProntuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataBaixa",
                table: "MedicamentoProntuario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FarmaceuticoId",
                table: "MedicamentoProntuario",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "MedicamentoProntuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MedicamentoProntuario_FarmaceuticoId",
                table: "MedicamentoProntuario",
                column: "FarmaceuticoId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicamentoProntuario_AspNetUsers_FarmaceuticoId",
                table: "MedicamentoProntuario",
                column: "FarmaceuticoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicamentoProntuario_AspNetUsers_FarmaceuticoId",
                table: "MedicamentoProntuario");

            migrationBuilder.DropIndex(
                name: "IX_MedicamentoProntuario_FarmaceuticoId",
                table: "MedicamentoProntuario");

            migrationBuilder.DropColumn(
                name: "CodigoPrescricao",
                table: "MedicamentoProntuario");

            migrationBuilder.DropColumn(
                name: "DataBaixa",
                table: "MedicamentoProntuario");

            migrationBuilder.DropColumn(
                name: "FarmaceuticoId",
                table: "MedicamentoProntuario");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MedicamentoProntuario");
        }
    }
}
