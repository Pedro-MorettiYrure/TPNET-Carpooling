using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialFix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DestinoCodPostal",
                table: "Viajes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrigenCodPostal",
                table: "Viajes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Viajes_DestinoCodPostal",
                table: "Viajes",
                column: "DestinoCodPostal");

            migrationBuilder.CreateIndex(
                name: "IX_Viajes_OrigenCodPostal",
                table: "Viajes",
                column: "OrigenCodPostal");

            migrationBuilder.AddForeignKey(
                name: "FK_Viajes_Localidades_DestinoCodPostal",
                table: "Viajes",
                column: "DestinoCodPostal",
                principalTable: "Localidades",
                principalColumn: "codPostal");

            migrationBuilder.AddForeignKey(
                name: "FK_Viajes_Localidades_OrigenCodPostal",
                table: "Viajes",
                column: "OrigenCodPostal",
                principalTable: "Localidades",
                principalColumn: "codPostal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viajes_Localidades_DestinoCodPostal",
                table: "Viajes");

            migrationBuilder.DropForeignKey(
                name: "FK_Viajes_Localidades_OrigenCodPostal",
                table: "Viajes");

            migrationBuilder.DropIndex(
                name: "IX_Viajes_DestinoCodPostal",
                table: "Viajes");

            migrationBuilder.DropIndex(
                name: "IX_Viajes_OrigenCodPostal",
                table: "Viajes");

            migrationBuilder.DropColumn(
                name: "DestinoCodPostal",
                table: "Viajes");

            migrationBuilder.DropColumn(
                name: "OrigenCodPostal",
                table: "Viajes");
        }
    }
}
