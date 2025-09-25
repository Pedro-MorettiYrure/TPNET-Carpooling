using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ConductorFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdConductor",
                table: "Viajes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Viajes_IdConductor",
                table: "Viajes",
                column: "IdConductor");

            migrationBuilder.AddForeignKey(
                name: "FK_Viajes_Usuario_IdConductor",
                table: "Viajes",
                column: "IdConductor",
                principalTable: "Usuario",
                principalColumn: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viajes_Usuario_IdConductor",
                table: "Viajes");

            migrationBuilder.DropIndex(
                name: "IX_Viajes_IdConductor",
                table: "Viajes");

            migrationBuilder.DropColumn(
                name: "IdConductor",
                table: "Viajes");
        }
    }
}
