using UnivCtaCte.Domain.Entities;
using UnivCtaCte.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace UnivCtaCte.Infrastructure.Repositories
{
    public class CtaCteRepository : ICtaCteRepository
    {
        private readonly string _connectionString;

        public CtaCteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("dbCtaCte")!;
        }

        public async Task<PagoMatricula?> ConsultarPagoMatriculaAsync(int estudianteId, int semestreId, int categoriaId)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                using var cmd = new NpgsqlCommand("select * from tf_consulta_pago_matricula(@estudianteId, @semestreId, @categoriaId);", conn)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("estudianteId", estudianteId);
                cmd.Parameters.AddWithValue("semestreId", semestreId);
                cmd.Parameters.AddWithValue("categoriaId", categoriaId);

                await conn.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new PagoMatricula
                    {
                        respuesta = Convert.ToInt32(reader["rpta"]),
                        mensaje = reader["mensaje"].ToString() ?? ""
                    };
                }

                return null;
            }
            catch (Exception ex) {
                return new PagoMatricula
                {
                    respuesta = -500,
                    mensaje = ex.Message
                };
            }
        }

        public async Task<List<EstadoCtaCte>> ConsultarEstadoCtaCteAsync(int estudianteId, int semestreId)
        {
            var lista = new List<EstadoCtaCte>();

            using var conn = new NpgsqlConnection(_connectionString);
            using var cmd = new NpgsqlCommand("select * from tf_estado_cuenta(@estudianteId, @semestreId);", conn)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.AddWithValue("estudianteId", estudianteId);
            cmd.Parameters.AddWithValue("semestreId", semestreId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var estado = new EstadoCtaCte
                {
                    semestre = reader["semestre"].ToString()!,
                    nro = Convert.ToInt32(reader["nro"]),
                    fecha_vencimiento = Convert.ToDateTime(reader["fecha_vencimiento"]),
                    importe = Convert.ToDecimal(reader["importe"]),
                    pagado = reader["pagado"].ToString()!,
                    cuenta_id = Convert.ToInt32(reader["cuenta_id"]),
                    compromiso_id = Convert.ToInt32(reader["compromiso_id"]),
                    costo_id = Convert.ToInt32(reader["costo_id"]),
                    concepto = reader["concepto"].ToString()!,
                    categoria = reader["categoria"].ToString()!
                };

                lista.Add(estado);
            }

            return lista;
        }

        public async Task<EstadoPension?> GenerarPensionPorMatriculaAsync(int estudianteId, int semestreId, int categoriaId, int creditos)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                using var cmd = new NpgsqlCommand("select * from tfi_genera_pension(@estudianteId, @semestreId, @categoriaId, @creditos);", conn)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("estudianteId", estudianteId);
                cmd.Parameters.AddWithValue("semestreId", semestreId);
                cmd.Parameters.AddWithValue("categoriaId", categoriaId);
                cmd.Parameters.AddWithValue("creditos", creditos);

                await conn.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new EstadoPension
                    {
                        respuesta = Convert.ToInt32(reader["rpta"]),
                        mensaje = reader["mensaje"].ToString() ?? ""
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                return new EstadoPension
                {
                    respuesta = -500,
                    mensaje = ex.Message
                };
            }
        }
    }
}
