using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetoTP3_A2.Migrations
{
    /// <inheritdoc />
    public partial class UserUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Papel",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Raca",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sexo",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AlergiaPaciente",
                columns: table => new
                {
                    AlergiasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacientesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlergiaPaciente", x => new { x.AlergiasId, x.PacientesId });
                    table.ForeignKey(
                        name: "FK_AlergiaPaciente_Alergia_AlergiasId",
                        column: x => x.AlergiasId,
                        principalTable: "Alergia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlergiaPaciente_AspNetUsers_PacientesId",
                        column: x => x.PacientesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PacientePatologia",
                columns: table => new
                {
                    PacientesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatologiasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacientePatologia", x => new { x.PacientesId, x.PatologiasId });
                    table.ForeignKey(
                        name: "FK_PacientePatologia_AspNetUsers_PacientesId",
                        column: x => x.PacientesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacientePatologia_Patologia_PatologiasId",
                        column: x => x.PatologiasId,
                        principalTable: "Patologia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlergiaPaciente_PacientesId",
                table: "AlergiaPaciente",
                column: "PacientesId");

            migrationBuilder.CreateIndex(
                name: "IX_PacientePatologia_PatologiasId",
                table: "PacientePatologia",
                column: "PatologiasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlergiaPaciente");

            migrationBuilder.DropTable(
                name: "PacientePatologia");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Papel",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Raca",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "AspNetUsers");
        }
    }
}
