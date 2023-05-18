using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TalentoBecario.Models.Entity;

namespace TalentoBecario.Models.Services
{
    public class AreaInteresService
    {
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["CONNSQL"].ConnectionString;

        public static List<AreaInteres> ObtieneListAreaIntereses()
        {
            List<AreaInteres> listAreaIntereses = new List<AreaInteres>();

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("select * from AREASINTERES", conn)
                {

                })
                {
                    conn.Open();

                 

                    try
                    {

                        command.ExecuteNonQuery();

                        SqlDataReader lector = command.ExecuteReader();

                        while (lector.Read())
                        {
                            listAreaIntereses.Add(new AreaInteres
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
                };
            }
            return listAreaIntereses;
        }

        public static AreaInteres ConsultarAreaInteres(int id)
        {
           AreaInteres habilidad = new AreaInteres();

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("select * from AREASINTERES where id=@id ", conn)
                {

                })
                  

                {
                    command.Parameters.AddWithValue("@id", id);

                    conn.Open();

                    //command.Parameters.AddWithValue("@zip","india");


                    try
                    {
                        command.ExecuteNonQuery();

                        SqlDataReader lector = command.ExecuteReader();

                        while (lector.Read())
                        {
                            habilidad = new AreaInteres
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
                };
            }
            return habilidad;
        }
        public static string guardarAreaInteres(AreaInteres registro)
        {
            DateTime dateSistem = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO AREASINTERES (descripcion) values(@descripcion)", conn)
                {

                })
                                              {
                    command.Parameters.AddWithValue("@descripcion", registro.Descripcion);
                    command.Parameters.AddWithValue("@fechasystem", dateSistem);
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
            return "Registro Ingresado Con Exito";
        }

        public static string ActualizarAreaInteres(AreaInteres registro)
        {
            DateTime dateSistem = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE AREASINTERES set descripcion=@descripcion where id=@id", conn)
                {

                })
                {
                    command.Parameters.AddWithValue("@descripcion", registro.Descripcion);
                    command.Parameters.AddWithValue("@id", registro.Id);
                    command.Parameters.AddWithValue("@fechasystem", dateSistem);
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
            return "Registro Actualizado Con Exito";
        }

        public static string EliminarAreaInteres(int id)
        {
            DateTime dateSistem = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM AREASINTERES where id=@id", conn)
                {

                })
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@fechasystem", dateSistem);
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
        public static string EliminarAreaInteresProyecto(int id)
        {
            DateTime dateSistem = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM PROYECINTE where idProyecto=@id", conn)
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