using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetoTP3_A2.Migrations
{
    /// <inheritdoc />
    public partial class AddFarmaceutico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           // Discriminator
            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.columns 
           WHERE Name = N'Discriminator' AND Object_ID = Object_ID(N'AspNetUsers'))
BEGIN
    ALTER TABLE AspNetUsers DROP COLUMN Discriminator;
END
");

            // IdFarmacia
            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.columns 
           WHERE Name = N'IdFarmacia' AND Object_ID = Object_ID(N'AspNetUsers'))
BEGIN
    ALTER TABLE AspNetUsers DROP COLUMN IdFarmacia;
END
");

            // Papel
            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.columns 
           WHERE Name = N'Papel' AND Object_ID = Object_ID(N'AspNetUsers'))
BEGIN
    ALTER TABLE AspNetUsers DROP COLUMN Papel;
END
");

            // RegistroProfissional
            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.columns 
           WHERE Name = N'RegistroProfissional' AND Object_ID = Object_ID(N'AspNetUsers'))
BEGIN
    ALTER TABLE AspNetUsers DROP COLUMN RegistroProfissional;
END
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdFarmacia",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Papel",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegistroProfissional",
                table: "AspNetUsers");
        }
    }
}
