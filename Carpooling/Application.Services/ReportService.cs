// Application.Services/ReportService.cs
using Data;
using Domain.Model;
using DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;
using System.IO; 
using System.Linq;
using System.Threading.Tasks; 

namespace Application.Services
{
    public class ReportService
    {
        private readonly UsuarioRepository _usuarioRepo;
        private readonly CalificacionRepository _califRepo;

        public ReportService(UsuarioRepository usuarioRepo, CalificacionRepository califRepo)
        {
            _usuarioRepo = usuarioRepo;
            _califRepo = califRepo;
        }

        public IEnumerable<TopConductorDTO> GetTopConductores(int count = 50)
        {
            
            var conductores = _usuarioRepo.GetAll() 
                                      .Where(u => u.TipoUsuario == TipoUsuario.PasajeroConductor)
                                      .ToList();

            var conductoresConCalificaciones = new List<TopConductorDTO>();

            foreach (var conductor in conductores)
            {
                // Obtener calificaciones recibidas COMO CONDUCTOR
                var calificaciones = _califRepo.GetCalificacionesRecibidas(conductor.IdUsuario, RolCalificado.Conductor).ToList(); //

                double promedio = 0;
                if (calificaciones.Any()) 
                {
                    // Calcular promedio calificaciones
                    promedio = calificaciones.Average(c => c.Puntaje); 
                }

                conductoresConCalificaciones.Add(new TopConductorDTO
                {
                    IdUsuario = conductor.IdUsuario, 
                    Nombre = conductor.Nombre, 
                    Apellido = conductor.Apellido, 
                    Email = conductor.Email, 
                    PromedioCalificacion = promedio,
                    CantidadCalificaciones = calificaciones.Count 
                });
            }

            // Ordenar
            return conductoresConCalificaciones
                   .OrderByDescending(c => c.PromedioCalificacion) 
                   .ThenByDescending(c => c.CantidadCalificaciones) 
                   .Take(count)
                   .ToList(); 
        }

        public async Task<byte[]> GenerateTopConductoresPdfAsync(IEnumerable<TopConductorDTO> data)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text("Reporte: Conductores Mejor Calificados")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3); 
                                columns.RelativeColumn(4);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(1); 
                            });

                            // Cabecera de la tabla
                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Blue.Lighten3).Padding(5).Text("Nombre").Bold();
                                header.Cell().Background(Colors.Blue.Lighten3).Padding(5).Text("Apellido").Bold();
                                header.Cell().Background(Colors.Blue.Lighten3).Padding(5).Text("Email").Bold();
                                header.Cell().Background(Colors.Blue.Lighten3).Padding(5).AlignRight().Text("Promedio").Bold();
                                header.Cell().Background(Colors.Blue.Lighten3).Padding(5).AlignCenter().Text("Cant").Bold();
                            });

                            // Datos de la tabla
                            foreach (var conductor in data)
                            {
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(conductor.Nombre);
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(conductor.Apellido);
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(conductor.Email);
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).AlignRight().Text(conductor.PromedioCalificacion.ToString("N2")); // Formato con 2 decimales
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).AlignCenter().Text(conductor.CantidadCalificaciones.ToString());
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Página ");
                            x.CurrentPageNumber();
                            x.Span(" de ");
                            x.TotalPages();
                        });
                });
            });

            // Generar el PDF en memoria
            using var stream = new MemoryStream();
            await Task.Run(() => document.GeneratePdf(stream)); 
            return stream.ToArray();
        }
    }
}