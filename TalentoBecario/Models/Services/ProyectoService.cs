using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TalentoBecario.Models.Entity;

namespace TalentoBecario.Models.Services
{
    public class ProyectoService
    {
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["CONNSQL"].ConnectionString;

        public static List<Proyecto> ObtieneListProyectos()
        {
            List<Proyecto> listProyecto = new List<Proyecto>();

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("select * from PROYECTOS", conn)
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
                            listProyecto.Add(new Proyecto
                            {
                                id = (lector.IsDBNull(0) ? 0 : lector.GetInt32(0)),
                                nombre = (lector.IsDBNull(1) ? " " : lector.GetString(1)),
                                descripcion = (lector.IsDBNull(1) ? " " : lector.GetString(2)),
                                estatus = (lector.IsDBNull(1) ? " " : lector.GetString(3))
                              
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
            return listProyecto;
        }

        public static List<Habilidad> ConsultarHabilidadesProyecto(int id)
        {
            List<Habilidad> listaHabilidades = new List<Habilidad>();
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("select HABILIDADES.* from  PROYECHABIL inner join HABILIDADES on HABILIDADES.id = idHabilidad where PROYECHABIL.idProyecto=@id", conn)
                {

                })
                {
                    command.Parameters.AddWithValue("@id", id);
                    conn.Open();



                    try
                    {

                        command.ExecuteNonQuery();

                        SqlDataReader lector = command.ExecuteReader();

                        while (lector.Read())
                        {
                            listaHabilidades.Add(new Habilidad
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

            
            return listaHabilidades;
        }

        public static List<AreaInteres> ConsultarAreaInteresProyecto(int id)
        {
            List<AreaInteres> listaInteres = new List<AreaInteres>();
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("select AREASINTERES.* from  PROYECINTE inner join AREASINTERES on AREASINTERES.id = idInteres where PROYECINTE.idProyecto=@id", conn)
                {

                })
                {
                    command.Parameters.AddWithValue("@id", id);

                    conn.Open();



                    try
                    {

                        command.ExecuteNonQuery();

                        SqlDataReader lector = command.ExecuteReader();

                        while (lector.Read())
                        {
                            listaInteres.Add(new AreaInteres
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


            return listaInteres;
        }
        public static Proyecto ConsultarProyecto(int id)
        {
           Proyecto proyecto = new Proyecto();

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("select * from PROYECTOS where id=@id ", conn)
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
                            proyecto = new Proyecto
                            {
                                id = (lector.IsDBNull(0) ? 0 : lector.GetInt32(0)),
                                nombre = (lector.IsDBNull(1) ? " " : lector.GetString(1)),
                                descripcion = (lector.IsDBNull(1) ? " " : lector.GetString(2)),
                                estatus = (lector.IsDBNull(1) ? " " : lector.GetString(3)),
                                 listAreaInteres = ConsultarAreaInteresProyecto(lector.GetInt32(0)),
                                listHabilidades = ConsultarHabilidadesProyecto(lector.GetInt32(0))
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
            return proyecto;
        }
        public static string guardarProyecto(Proyecto registro)
        {
            DateTime dateSistem = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO PROYECTO (nombre,descripcion,estatus) values(@nombre,@descripcion,@estatus)", conn)
                {

                })
                                              {
                    command.Parameters.AddWithValue("@nombre", registro.nombre);
                    command.Parameters.AddWithValue("@descripcion", registro.descripcion);
                    command.Parameters.AddWithValue("@estatus", registro.estatus);

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
            guardarHabilidadesInteresProyecto(registro);
            return "Registro Ingresado Con Exito";
        }

        public static void guardarHabilidadesInteresProyecto(Proyecto registro)
        {
            HabilidadesService.EliminarHabilidadProyecto(registro.id);
            AreaInteresService.EliminarAreaInteresProyecto(registro.id);
            foreach (Habilidad item in registro.listHabilidades)
            {
                HabilidadesService.guardarHabilidad(item);

            }
            foreach (AreaInteres item in registro.listAreaInteres)
            {
                AreaInteresService.guardarAreaInteres(item);

            }
        }
        public static string ActualizarProyecto(Proyecto registro)
        {
            DateTime dateSistem = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE PROYECTOS set nombre=@nombre,descripcion=@descripcion,estatus=@estatus where id=@id", conn)
                {

                })
                {
                    command.Parameters.AddWithValue("@nombre", registro.nombre);
                    command.Parameters.AddWithValue("@descripcion", registro.descripcion);
                    command.Parameters.AddWithValue("@estatus", registro.estatus);

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
            guardarHabilidadesInteresProyecto(registro);
            return "Registro Actualizado Con Exito";
        }

        public static string EliminarProyecto(Proyecto registro)
        {
          
            DateTime dateSistem = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM PROYECTOS where id=@id", conn)
                {

                })
                {
                    command.Parameters.AddWithValue("@id", registro.id);
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
            HabilidadesService.EliminarHabilidadProyecto(registro.id);
            AreaInteresService.EliminarAreaInteresProyecto(registro.id);
            return "Registro Eliminado Con Exito";
        }

    }
}