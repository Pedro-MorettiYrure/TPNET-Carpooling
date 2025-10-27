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
        private readonly ViajeRepository _viajeRepo; 
        private readonly SolicitudViajeRepository _solicitudRepo;

        public ReportService(
            UsuarioRepository usuarioRepo,
            CalificacionRepository califRepo,
            ViajeRepository viajeRepo,
            SolicitudViajeRepository solicitudRepo) 
        {
            _usuarioRepo = usuarioRepo;
            _califRepo = califRepo;
            _viajeRepo = viajeRepo; 
            _solicitudRepo = solicitudRepo; 
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

        public ReporteActividadViajesDTO GetActividadViajes(DateTime fechaInicio, DateTime fechaFin)
        {
            // Asegura que fechaFin incluya todo el día
            var fechaFinInclusive = fechaFin.Date.AddDays(1).AddTicks(-1);

            var viajesEnRango = _viajeRepo.GetViajesByDateRange(fechaInicio.Date, fechaFinInclusive);

            var reporte = new ReporteActividadViajesDTO
            {
                FechaInicio = fechaInicio.Date,
                FechaFin = fechaFin.Date,
                TotalViajesPublicados = viajesEnRango.Count() 
            };

            foreach (var viaje in viajesEnRango)
            {
                // Contar pasajeros confirmados (solicitudes aprobadas)
                int pasajerosConfirmados = _solicitudRepo.GetAllByViaje(viaje.IdViaje) 
                                                   .Count(s => s.Estado == EstadoSolicitud.Aprobada); 

                reporte.ViajesDetalle.Add(new ViajeActividadDTO
                {
                    IdViaje = viaje.IdViaje, 
                    FechaHora = viaje.FechaHora, 
                    OrigenNombre = viaje.Origen?.nombreLoc ?? "N/A", 
                    DestinoNombre = viaje.Destino?.nombreLoc ?? "N/A", 
                    ConductorNombreCompleto = $"{viaje.Conductor?.Nombre} {viaje.Conductor?.Apellido}" ?? "N/A", 
                    Estado = viaje.Estado.ToString(),
                    PasajerosConfirmados = pasajerosConfirmados
                });

                switch (viaje.Estado)
                {
                    case EstadoViaje.Realizado: 
                        reporte.TotalViajesRealizados++;
                        break;
                    case EstadoViaje.Cancelado: 
                        reporte.TotalViajesCancelados++;
                        break;
                    case EstadoViaje.Pendiente: 
                    case EstadoViaje.EnCurso: 
                        reporte.TotalViajesPendientesEnCurso++;
                        break;
                }
            }

            return reporte;
        }
        public async Task<byte[]> GenerateActividadViajesPdfAsync(ReporteActividadViajesDTO reporteData)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape()); 
                    page.Margin(1.5f, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10)); 

                    page.Header()
                        .PaddingBottom(1, Unit.Centimetre)
                        .Column(col =>
                        {
                            col.Item().Text("Reporte: Actividad de Viajes")
                                .SemiBold().FontSize(18).FontColor(Colors.Blue.Medium);
                            col.Item().Text($"Período: {reporteData.FechaInicio:dd/MM/yyyy} - {reporteData.FechaFin:dd/MM/yyyy}")
                                .FontSize(12);
                            col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                            // Resumen
                            col.Item().PaddingTop(5).Text(txt =>
                            {
                                txt.Span("Resumen: ").SemiBold();
                                txt.Span($" Publicados: {reporteData.TotalViajesPublicados} |");
                                txt.Span($" Realizados: {reporteData.TotalViajesRealizados} |");
                                txt.Span($" Cancelados: {reporteData.TotalViajesCancelados} |");
                                txt.Span($" Pendientes/En Curso: {reporteData.TotalViajesPendientesEnCurso}");
                            });
                            col.Item().PaddingTop(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                        });

                    page.Content()
                        .PaddingTop(0.5f, Unit.Centimetre)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(35); 
                                columns.RelativeColumn(3); 
                                columns.RelativeColumn(3); 
                                columns.RelativeColumn(3); 
                                columns.RelativeColumn(4); 
                                columns.RelativeColumn(2); 
                                columns.ConstantColumn(40); 
                            });

                            // Cabecera de la tabla
                            table.Header(header =>
                            {
                                static IContainer CellStyle(IContainer container) => container.Background(Colors.Blue.Lighten3).Padding(4);

                                header.Cell().Element(CellStyle).Text("ID").Bold();
                                header.Cell().Element(CellStyle).Text("Fecha y Hora").Bold();
                                header.Cell().Element(CellStyle).Text("Origen").Bold();
                                header.Cell().Element(CellStyle).Text("Destino").Bold();
                                header.Cell().Element(CellStyle).Text("Conductor").Bold();
                                header.Cell().Element(CellStyle).Text("Estado").Bold();
                                header.Cell().Element(CellStyle).AlignCenter().Text("Pasaj.").Bold();
                            });

                            // Datos de la tabla
                            foreach (var viaje in reporteData.ViajesDetalle)
                            {
                                static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(3).PaddingHorizontal(4);

                                table.Cell().Element(CellStyle).Text(viaje.IdViaje.ToString());
                                table.Cell().Element(CellStyle).Text(viaje.FechaHora.ToString("dd/MM/yy HH:mm"));
                                table.Cell().Element(CellStyle).Text(viaje.OrigenNombre);
                                table.Cell().Element(CellStyle).Text(viaje.DestinoNombre);
                                table.Cell().Element(CellStyle).Text(viaje.ConductorNombreCompleto);
                                table.Cell().Element(CellStyle).Text(viaje.Estado);
                                table.Cell().Element(CellStyle).AlignCenter().Text(viaje.PasajerosConfirmados.ToString());
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Página ");
                            x.CurrentPageNumber();
                            x.Span(" / ");
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