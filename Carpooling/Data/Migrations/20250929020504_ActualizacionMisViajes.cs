using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionMisViajes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdVehiculo",
                table: "Viajes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Viajes_IdVehiculo",
                table: "Viajes",
                column: "IdVehiculo");

            migrationBuilder.AddForeignKey(
                name: "FK_Viajes_Vehiculos_IdVehiculo",
                table: "Viajes",
                column: "IdVehiculo",
                principalTable: "Vehiculos",
                principalColumn: "IdVehiculo",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viajes_Vehiculos_IdVehiculo",
                table: "Viajes");

            migrationBuilder.DropIndex(
                name: "IX_Viajes_IdVehiculo",
                table: "Viajes");

            migrationBuilder.DropColumn(
                name: "IdVehiculo",
                table: "Viajes");
        }
    }
}
