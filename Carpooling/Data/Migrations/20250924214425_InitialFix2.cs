using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinoCodPostal",
                table: "Viajes");

            migrationBuilder.DropColumn(
                name: "OrigenCodPostal",
                table: "Viajes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DestinoCodPostal",
                table: "Viajes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrigenCodPostal",
                table: "Viajes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
