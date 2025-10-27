using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class CorregirRelacionViajeSolicitud2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesViaje_Viajes_IdViaje",
                table: "SolicitudesViaje");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesViaje_Viajes_ViajeIdViaje",
                table: "SolicitudesViaje");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudesViaje_ViajeIdViaje",
                table: "SolicitudesViaje");

            migrationBuilder.DropColumn(
                name: "ViajeIdViaje",
                table: "SolicitudesViaje");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesViaje_Viajes_IdViaje",
                table: "SolicitudesViaje",
                column: "IdViaje",
                principalTable: "Viajes",
                principalColumn: "IdViaje",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesViaje_Viajes_IdViaje",
                table: "SolicitudesViaje");

            migrationBuilder.AddColumn<int>(
                name: "ViajeIdViaje",
                table: "SolicitudesViaje",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesViaje_ViajeIdViaje",
                table: "SolicitudesViaje",
                column: "ViajeIdViaje");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesViaje_Viajes_IdViaje",
                table: "SolicitudesViaje",
                column: "IdViaje",
                principalTable: "Viajes",
                principalColumn: "IdViaje");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesViaje_Viajes_ViajeIdViaje",
                table: "SolicitudesViaje",
                column: "ViajeIdViaje",
                principalTable: "Viajes",
                principalColumn: "IdViaje");
        }
    }
}
