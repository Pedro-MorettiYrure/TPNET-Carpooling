using Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Routing; 
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization; 
using Domain.Model; 

namespace WebAPI
{
    public static class ReportEndpoints
    {
        public static void MapReportEndpoints(this IEndpointRouteBuilder app) 
        {
            app.MapGet("/api/reports/top-conductores", [Authorize(Policy = "EsAdmin")] async ([FromServices] ReportService reportService) =>
            {
                try
                {
                    var topConductores = reportService.GetTopConductores(50); // Obtener top 50

                    byte[] pdfBytes = await reportService.GenerateTopConductoresPdfAsync(topConductores);

                    return Results.File(pdfBytes, "application/pdf", "top_conductores_calificados.pdf");
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Error al generar el reporte PDF: {ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
                }
            })
            .WithName("GetTopConductoresReportPdf")
            .Produces<FileResult>(StatusCodes.Status200OK, "application/pdf") 
            .Produces(StatusCodes.Status401Unauthorized) 
            .Produces(StatusCodes.Status403Forbidden) 
            .ProducesProblem(StatusCodes.Status500InternalServerError) 
            .RequireAuthorization("EsAdmin") 
            .WithOpenApi(); 

            app.MapGet("/api/reports/actividad-viajes", [Authorize(Policy = "EsAdmin")] async (
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin,
            [FromServices] ReportService reportService) =>
                {
                    // Validación básica de fechas
                    if (fechaInicio > fechaFin)
                    {
                        return Results.BadRequest(new { error = "La fecha de inicio no puede ser posterior a la fecha de fin." });
                    }

                    try
                    {
                        var reporteData = reportService.GetActividadViajes(fechaInicio, fechaFin);

                        byte[] pdfBytes = await reportService.GenerateActividadViajesPdfAsync(reporteData);

                        string fileName = $"reporte_actividad_{fechaInicio:yyyyMMdd}_{fechaFin:yyyyMMdd}.pdf";
                        return Results.File(pdfBytes, "application/pdf", fileName);
                    }
                    catch (Exception ex)
                    {
                        return Results.Problem($"Error al generar el reporte PDF de actividad: {ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
                    }
                })
            .WithName("GetActividadViajesReportPdf")
            .Produces<FileResult>(StatusCodes.Status200OK, "application/pdf")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization("EsAdmin")
            .WithOpenApi();
            }


    }

}