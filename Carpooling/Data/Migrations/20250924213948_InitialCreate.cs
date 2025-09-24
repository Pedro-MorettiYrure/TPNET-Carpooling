using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Localidades",
                columns: table => new
                {
                    codPostal = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nombreLoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localidades", x => x.codPostal);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TipoUsuario = table.Column<int>(type: "int", nullable: false),
                    ContraseñaHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    nroLicenciaConductor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    fechaVencimientoLicencia = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Viajes",
                columns: table => new
                {
                    IdViaje = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CantLugares = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precio = table.Column<float>(type: "real", nullable: false),
                    OrigenCodPostal = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DestinoCodPostal = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viajes", x => x.IdViaje);
                    table.ForeignKey(
                        name: "FK_Viajes_Localidades_DestinoCodPostal",
                        column: x => x.DestinoCodPostal,
                        principalTable: "Localidades",
                        principalColumn: "codPostal",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Viajes_Localidades_OrigenCodPostal",
                        column: x => x.OrigenCodPostal,
                        principalTable: "Localidades",
                        principalColumn: "codPostal",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    IdVehiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Patente = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CantLugares = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.IdVehiculo);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Localidades",
                columns: new[] { "codPostal", "nombreLoc" },
                values: new object[,]
                {
                    { "2000", "Rosario" },
                    { "2607", "Villa Cañas" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_IdUsuario",
                table: "Vehiculos",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Viajes_DestinoCodPostal",
                table: "Viajes",
                column: "DestinoCodPostal");

            migrationBuilder.CreateIndex(
                name: "IX_Viajes_OrigenCodPostal",
                table: "Viajes",
                column: "OrigenCodPostal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Viajes");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Localidades");
        }
    }
}
