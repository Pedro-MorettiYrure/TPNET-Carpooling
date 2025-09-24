using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialFix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "OrigenCodPostal",
                table: "Viajes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DestinoCodPostal",
                table: "Viajes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrigenCodPostal",
                table: "Viajes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DestinoCodPostal",
                table: "Viajes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                principalColumn: "codPostal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Viajes_Localidades_OrigenCodPostal",
                table: "Viajes",
                column: "OrigenCodPostal",
                principalTable: "Localidades",
                principalColumn: "codPostal",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
