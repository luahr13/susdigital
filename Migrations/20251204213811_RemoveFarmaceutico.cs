using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetoTP3_A2.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFarmaceutico : Migration
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
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdFarmacia",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Papel",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistroProfissional",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
