using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class CambiosYCalificaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViajeIdViaje",
                table: "SolicitudesViaje",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    IdCalificacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdViaje = table.Column<int>(type: "int", nullable: false),
                    IdCalificador = table.Column<int>(type: "int", nullable: false),
                    IdCalificado = table.Column<int>(type: "int", nullable: false),
                    RolCalificado = table.Column<int>(type: "int", nullable: false),
                    Puntaje = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.IdCalificacion);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Usuario_IdCalificado",
                        column: x => x.IdCalificado,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Usuario_IdCalificador",
                        column: x => x.IdCalificador,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Viajes_IdViaje",
                        column: x => x.IdViaje,
                        principalTable: "Viajes",
                        principalColumn: "IdViaje",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesViaje_ViajeIdViaje",
                table: "SolicitudesViaje",
                column: "ViajeIdViaje");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_IdCalificado",
                table: "Calificaciones",
                column: "IdCalificado");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_IdCalificador",
                table: "Calificaciones",
                column: "IdCalificador");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_IdViaje",
                table: "Calificaciones",
                column: "IdViaje");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesViaje_Viajes_ViajeIdViaje",
                table: "SolicitudesViaje",
                column: "ViajeIdViaje",
                principalTable: "Viajes",
                principalColumn: "IdViaje");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesViaje_Viajes_ViajeIdViaje",
                table: "SolicitudesViaje");

            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudesViaje_ViajeIdViaje",
                table: "SolicitudesViaje");

            migrationBuilder.DropColumn(
                name: "ViajeIdViaje",
                table: "SolicitudesViaje");
        }
    }
}
