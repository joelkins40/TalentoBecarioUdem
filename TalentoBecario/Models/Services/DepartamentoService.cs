using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TalentoBecario.Models.Entity;

namespace TalentoBecario.Models.Services
{
    public class DepartamentoService
    {
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["CONNSQL"].ConnectionString;

        public static List<Departamento> ObtieneListDepartamentos()
        {
            List<Departamento> listDepartamento = new List<Departamento>();

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("select * from DEPARTAMENTO", conn)
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
                            listDepartamento.Add(new Departamento
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
            return listDepartamento;
        }

        public static Departamento ConsultarDepartamento(int id)
        {
           Departamento habilidad = new Departamento();

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("select * from DEPARTAMENTO where id=@id ", conn)
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
                            habilidad = new Departamento
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
        public static string guardarDepartamento(Departamento registro)
        {
            DateTime dateSistem = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO DEPARTAMENTO (descripcion) values(@descripcion)", conn)
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

        public static string ActualizarDepartamento(Departamento registro)
        {
            DateTime dateSistem = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE DEPARTAMENTO set descripcion=@descripcion where id=@id", conn)
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

        public static string EliminarDepartamento(Departamento registro)
        {
            DateTime dateSistem = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM DEPARTAMENTO where id=@id", conn)
                {

                })
                {
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
            return "Registro Eliminado Con Exito";
        }

    }
}