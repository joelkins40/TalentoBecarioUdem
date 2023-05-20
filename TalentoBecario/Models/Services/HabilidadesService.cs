using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TalentoBecario.Models.Entity;

namespace TalentoBecario.Models.Services
{
    public class HabilidadesService
    {
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["BANNER"].ConnectionString;

        public static List<Habilidad> ObtieneListHabilidades()
        {
            List<Habilidad> listHabilidades = new List<Habilidad>();

            using (var conn = new OracleConnection(_conString))
            {
                var command = new OracleCommand("SELECT * FROM GCBHABI", conn);
                conn.Open();



                try
                {

                    command.ExecuteNonQuery();

                    var lector = command.ExecuteReader();

                    while (lector.Read())
                    {
                        listHabilidades.Add(new Habilidad
                        {
                            Id = (lector.IsDBNull(0) ? 0 : lector.GetInt32(0)),
                            Descripcion = (lector.IsDBNull(1) ? " " : lector.GetString(1)),

                        });
                    }
                    conn.Close();
                }
                finally
                {
                    conn.Close();
                }
            }
            return listHabilidades;
        }

        public static Habilidad ConsultarHabilidad(int id)
        {
           Habilidad habilidad = new Habilidad();

            using (var conn = new OracleConnection(_conString))
            {
                var command = new OracleCommand("SELECT * FROM GCBHABI WHERE IDHABILIDAD = @Id", conn);
                command.Parameters.AddWithValue("@Id", id);

                conn.Open();
                try
                {
                    command.ExecuteNonQuery();

                    var lector = command.ExecuteReader();

                    while (lector.Read())
                    {
                        habilidad = new Habilidad
                        {


                            Id = (lector.IsDBNull(0) ? 0 : lector.GetInt32(0)),
                            Descripcion = (lector.IsDBNull(1) ? " " : lector.GetString(1)),
                        };
                    }
                    conn.Close();
                }
                finally
                {
                    conn.Close();
                }
            }
            return habilidad;
        }
        public static string guardarHabilidad(Habilidad registro)
        {
            DateTime dateSistem = DateTime.Now;
            using (var conn = new OracleConnection(_conString))
            {
                var command = new OracleCommand("P_UDEM_ADD_HAB", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("P_Descripcion", registro.Descripcion);
                // command.Parameters.AddWithValue("@fechasystem", dateSistem);
                conn.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
            return "Registro Ingresado Con Exito";
        }

        public static string ActualizarHabilidad(Habilidad registro)
        {
            DateTime dateSistem = DateTime.Now;
            using (var conn = new OracleConnection(_conString))
            {
                var command = new OracleCommand("P_UDEM_UPDATE_HAB", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("P_Id", registro.Descripcion);
                command.Parameters.AddWithValue("P_Descripcion", registro.Descripcion);
                conn.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
            return "Registro Actualizado Con Exito";
        }

        public static string EliminarHabilidad(int id)
        {
            DateTime dateSistem = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM HABILIDADES where id=@id", conn)
                {

                })
                {
                    command.Parameters.AddWithValue("@id", id);
                    
                    conn.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    finally
                    {
                        conn.Close();
                    }
                };
            }
            return "Registro Eliminado Con Exito";
        }

        public static string EliminarHabilidadProyecto(int id)
        {
            DateTime dateSistem = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM PROYECHABIL where idProyecto=@id", conn)
                {

                })
                {
                    command.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    finally
                    {
                        conn.Close();
                    }
                };
            }
            return "Registro Eliminado Con Exito";
        }
    }
}