
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DTOs;

namespace Data
{
    public class ReporteAdoRepository
    {
        private readonly string _connectionString;

        public ReporteAdoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<TopConductorDTO> ObtenerMejoresConductoresADO()
        {
            var lista = new List<TopConductorDTO>();

            string query = @"
                SELECT 
                    u.IdUsuario, 
                    u.Nombre, 
                    u.Apellido, 
                    u.Email, 
                    AVG(CAST(c.Puntaje AS FLOAT)) as Promedio,
                    COUNT(c.IdCalificacion) as Cantidad
                FROM Usuario u
                INNER JOIN Calificaciones c ON u.IdUsuario = c.IdCalificado
                WHERE c.RolCalificado = 0 -- 0 es Conductor
                GROUP BY u.IdUsuario, u.Nombre, u.Apellido, u.Email
                ORDER BY Promedio DESC";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new TopConductorDTO
                            {
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                Nombre = reader["Nombre"].ToString() ?? "",
                                Apellido = reader["Apellido"].ToString() ?? "",
                                Email = reader["Email"].ToString() ?? "",
                                PromedioCalificacion = reader["Promedio"] != DBNull.Value ? Convert.ToDouble(reader["Promedio"]) : 0,
                                CantidadCalificaciones = reader["Cantidad"] != DBNull.Value ? Convert.ToInt32(reader["Cantidad"]) : 0
                            };

                            lista.Add(item);
                        }
                    }
                }
            }

            return lista;
        }
    }
}