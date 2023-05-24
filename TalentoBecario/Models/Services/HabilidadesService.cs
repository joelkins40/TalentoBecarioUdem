using System;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;


using System.Data;

using TalentoBecario.Models.Entity;
using System.Collections.Generic;

namespace TalentoBecario.Models.Services
{
    public class HabilidadesService
    {
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["BANNER"].ConnectionString;

        public static   List<Habilidad> ObtieneListHabilidades()
        {
            List<Habilidad> habilidades = new List<Habilidad>();

            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {

                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_RTB.F_GET_HABILIDADES";
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.BindByName = true;
                        comando.Parameters.Add(new OracleParameter("salida", OracleDbType.RefCursor)
                        {
                            Direction = ParameterDirection.ReturnValue
                        });

                     


                        // Revisamos si se pudo ejecutar la consulta
                        cnx.Open();
                      
                        try
                        {

                            OracleDataReader lector = comando.ExecuteReader();

                            // Revisamos cada contacto

                            while (lector.Read())
                            {



                                habilidades.Add(new Habilidad()
                                {
                             
                                    Id= lector.GetInt32(0),
                                    Descripcion = lector.GetString(1),



                                });
                               



                            }


                        }
                        finally
                        {

                            cnx.Close();
                        }

                    }

                    //cnx.Close();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }
            return habilidades;

        }


        public static Habilidad ConsultarHabilidad(int id)
        {
            Habilidad habilidad = new Habilidad();
            try{
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_RTB.F_GET_HABILIDAD";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.BindByName = true;
                        comando.Parameters.Add(new OracleParameter("P_Id", OracleDbType.Int16)
                        {
                            Value = id,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("salida", OracleDbType.RefCursor)
                        {
                            Direction = ParameterDirection.ReturnValue
                        });
                       cnx.Open();
                        try
                        {
                            OracleDataReader lector = comando.ExecuteReader();
                            while (lector.Read())
                            {
                                habilidad=new Habilidad()
                                {
                                    Id = lector.GetInt32(0),
                                    Descripcion = lector.GetString(1)
                                };
                            }
                        }
                        finally
                        {
                            cnx.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return habilidad;

        }
        public static String guardarHabilidad(Habilidad registro)
        {
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_WTB.F_UDEM_ADD_HAB";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.BindByName = true;

                        comando.Parameters.Add(new OracleParameter("P_Descripcion", OracleDbType.Varchar2)
                        {
                            Value = registro.Descripcion,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("V_Salida", OracleDbType.RefCursor)
                        {
                            Direction = ParameterDirection.ReturnValue
                        });
                     
                        try
                        {
                            cnx.Open();
                            comando.ExecuteNonQuery();

                        }
                        finally
                        {
                            cnx.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "Registro Ingresado Con Éxito";

        }
        public static String ActualizarHabilidad(Habilidad registro)
        {
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_WTB.F_UDEM_UPDATE_HAB";
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.BindByName = true;

                        comando.Parameters.Add(new OracleParameter("P_Id", OracleDbType.Int32)
                        {
                            Value = registro.Id,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_Descripcion", OracleDbType.Varchar2)
                        {
                            Value = registro.Descripcion,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("V_Salida", OracleDbType.RefCursor)
                        {
                            Direction = ParameterDirection.ReturnValue
                        });
                        cnx.Open();
                        try
                        {
                            comando.ExecuteNonQuery();

                        }
                        finally
                        {
                            cnx.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "Registro Actualizado Con Éxito";

        }

        public static string EliminarHabilidad(int id)
        {
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_WTB.F_UDEM_UPDATE_HAB";
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.BindByName = true;

                        comando.Parameters.Add(new OracleParameter("P_Id", OracleDbType.Int32)
                        {
                            Value = id,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        
                        comando.Parameters.Add(new OracleParameter("salida", OracleDbType.RefCursor)
                        {
                            Direction = ParameterDirection.ReturnValue
                        });
                        cnx.Open();
                        try
                        {
                            comando.ExecuteNonQuery();

                        }
                        finally
                        {
                            cnx.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "Registro Actualizado Con Éxito";

        }
        

    }
}