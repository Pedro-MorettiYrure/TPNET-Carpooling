using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSolicitudViajeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolicitudesViaje",
                columns: table => new
                {
                    IdSolicitud = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    IdViaje = table.Column<int>(type: "int", nullable: false),
                    IdPasajero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesViaje", x => x.IdSolicitud);
                    table.ForeignKey(
                        name: "FK_SolicitudesViaje_Usuario_IdPasajero",
                        column: x => x.IdPasajero,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                    table.ForeignKey(
                        name: "FK_SolicitudesViaje_Viajes_IdViaje",
                        column: x => x.IdViaje,
                        principalTable: "Viajes",
                        principalColumn: "IdViaje");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesViaje_IdPasajero",
                table: "SolicitudesViaje",
                column: "IdPasajero");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesViaje_IdViaje",
                table: "SolicitudesViaje",
                column: "IdViaje");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitudesViaje");
        }
    }
}
