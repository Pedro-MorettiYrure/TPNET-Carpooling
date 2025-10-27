// WebAPI/ReportEndpoints.cs
using Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; // Para [FromServices]
using Microsoft.AspNetCore.Routing; // Para IEndpointRouteBuilder
using System.Security.Claims; // Para ClaimsPrincipal
using Microsoft.AspNetCore.Authorization; // Para [Authorize]
using Domain.Model; // Para TipoUsuario

namespace WebAPI
{
    public static class ReportEndpoints
    {
        public static void MapReportEndpoints(this IEndpointRouteBuilder app) // Cambiado a IEndpointRouteBuilder
        {
            // Endpoint para descargar el reporte de top conductores en PDF
            app.MapGet("/api/reports/top-conductores", [Authorize(Policy = "EsAdmin")] async ([FromServices] ReportService reportService) =>
            {
                try
                {
                    // 1. Obtener los datos
                    var topConductores = reportService.GetTopConductores(50); // Obtener top 50

                    // 2. Generar el PDF
                    byte[] pdfBytes = await reportService.GenerateTopConductoresPdfAsync(topConductores);

                    // 3. Devolver el archivo PDF
                    // Especificamos el tipo MIME y el nombre de descarga sugerido
                    return Results.File(pdfBytes, "application/pdf", "top_conductores_calificados.pdf");
                }
                catch (Exception ex)
                {
                    // Loggear el error sería ideal aquí
                    return Results.Problem($"Error al generar el reporte PDF: {ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
                }
            })
            .WithName("GetTopConductoresReportPdf")
            .Produces<FileResult>(StatusCodes.Status200OK, "application/pdf") //
            .Produces(StatusCodes.Status401Unauthorized) //
            .Produces(StatusCodes.Status403Forbidden) //
            .ProducesProblem(StatusCodes.Status500InternalServerError) //
            .RequireAuthorization("EsAdmin") // Asegura que solo Admins accedan
            .WithOpenApi(); //
        }
    }
}