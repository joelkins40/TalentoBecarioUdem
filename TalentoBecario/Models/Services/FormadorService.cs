using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TalentoBecario.Models.Entity;

namespace TalentoBecario.Models.Services
{
    public class FormadorService
    {
       
            private static readonly string _conString = ConfigurationManager.ConnectionStrings["BANNER"].ConnectionString;
            private static readonly string _conPeopleSoftString = ConfigurationManager.ConnectionStrings["PeopleSoft"].ConnectionString;

            public static List<Formador> ObtieneListFormadores()
            {
                List<Formador> formadores = new List<Formador>();

                try
                {
                    using (OracleConnection cnx = new OracleConnection(_conString))
                    {

                        using (OracleCommand comando = new OracleCommand())
                        {
                            comando.Connection = cnx;
                            comando.CommandText = "SZ_BMA_RTB.F_GET_FORMADORES";
                            comando.CommandType = System.Data.CommandType.StoredProcedure;
                            comando.BindByName = true;
                            comando.Parameters.Add(new OracleParameter("V_SALIDA", OracleDbType.RefCursor)
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



                                    formadores.Add(new Formador()
                                    {
                                        Id = lector.GetString(0),
                                        Nombre = lector.GetString(1),
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
           
            return formadores;

            }

        public static List<Formador> ObtieneListaFormadoresExternos()
        {
            List<Formador> formadores = new List<Formador>();

            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {

                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_RTB.F_GET_ALL_EMPLOYEES";
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.BindByName = true;
                        comando.Parameters.Add(new OracleParameter("V_SALIDA", OracleDbType.RefCursor)
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



                                formadores.Add(new Formador()
                                {
                                    Id = lector.GetString(1),
                                    Nombre = lector.GetString(2),
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

            return formadores;

        }

        public static Formador ConsultarFormadorExternos(string id)
        {
            Formador formador = new Formador();
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_RTB.F_GET_EMPLOYEE";
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.BindByName = true;
                        comando.Parameters.Add(new OracleParameter("P_Id", OracleDbType.Varchar2)
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
                                formador = new Formador()
                                {
                                    Id = lector.GetString(1),
                                    Nombre = lector.GetString(2),

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
            return formador;

        }
        public static Formador ConsultarFormador(string id)
            {
                Formador formador = new Formador();
                try
                {
                    using (OracleConnection cnx = new OracleConnection(_conString))
                    {
                        using (OracleCommand comando = new OracleCommand())
                        {
                            comando.Connection = cnx;
                            comando.CommandText = "SZ_BMA_RTB.F_GET_FORMADOR";
                            comando.CommandType = System.Data.CommandType.StoredProcedure;
                            comando.BindByName = true;
                            comando.Parameters.Add(new OracleParameter("P_Id", OracleDbType.Varchar2)
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
                                    formador = new Formador()
                                    {
                                        Id = lector.GetString(0),
                                        Nombre = lector.GetString(1),

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
                return formador;

            }

        public static Formador verificarFormador(Formador registro,int pidm)
        {
          Formador  registroConsulta = ConsultarFormador(registro.Id);
            if(registroConsulta == null || registroConsulta.Id == null)
            {
                var user = StudentService.FillUser(pidm);
                registro.Nombre = user.FullName;
                guardarFormador(registro);
                return registro;
            }
            else
            {
                return registroConsulta;
            }
          
        }
            public static String guardarFormador(Formador registro)
            {
                try
                {
                    using (OracleConnection cnx = new OracleConnection(_conString))
                    {
                        using (OracleCommand comando = new OracleCommand())
                        {
                            comando.Connection = cnx;
                            comando.CommandText = "SZ_BMA_WTB.F_UDEM_ADD_FORM";
                            comando.CommandType = CommandType.StoredProcedure;
                            comando.BindByName = true;


                        comando.Parameters.Add(new OracleParameter("P_Id", OracleDbType.Varchar2)
                        {
                            Value = registro.Id,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_Nombre", OracleDbType.Varchar2)
                            {
                                Value = registro.Nombre,
                                Direction = System.Data.ParameterDirection.Input
                            });
                            comando.Parameters.Add(new OracleParameter("V_Salida", OracleDbType.Varchar2,400)
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
            public static String ActualizarFormador(Formador registro)
            {
                try
                {
                    using (OracleConnection cnx = new OracleConnection(_conString))
                    {
                        using (OracleCommand comando = new OracleCommand())
                        {
                            comando.Connection = cnx;
                            comando.CommandText = "SZ_BMA_WTB.F_UDEM_UPDATE_FORM";
                            comando.CommandType = System.Data.CommandType.StoredProcedure;
                            comando.BindByName = true;

                        comando.Parameters.Add(new OracleParameter("P_Id", OracleDbType.Varchar2)
                        {
                            Value = registro.Id,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_Nombre", OracleDbType.Varchar2)
                        {
                            Value = registro.Nombre,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("V_Salida", OracleDbType.Varchar2,400)
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

            public static string EliminarFormador(int id)
            {
                try
                {
                    using (OracleConnection cnx = new OracleConnection(_conString))
                    {
                        using (OracleCommand comando = new OracleCommand())
                        {
                            comando.Connection = cnx;
                            comando.CommandText = "SZ_BMA_WTB.F_UDEM_DELETE_DEPA";
                            comando.CommandType = System.Data.CommandType.StoredProcedure;
                            comando.BindByName = true;

                            comando.Parameters.Add(new OracleParameter("P_Id", OracleDbType.Int32)
                            {
                                Value = id,
                                Direction = System.Data.ParameterDirection.Input
                            });

                            comando.Parameters.Add(new OracleParameter("V_Salida", OracleDbType.Varchar2,400)
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

        public static Dictionary<string, dynamic> ConsultarUsuario(string matricula)
        {
            Dictionary<string, dynamic> user = new Dictionary<string, dynamic>();

            using (var conn = new OracleConnection(_conPeopleSoftString))
            {
                var command = new OracleCommand("SELECT * FROM sysadm.PS_UDEM_RHPS_VW WHERE ALTER_EMPLID = '" + matricula + "'", conn);
                //command.Parameters.Add("@Matricula", matricula);

                conn.Open();
                try
                {
                    command.ExecuteNonQuery();

                    var lector = command.ExecuteReader();

                    while (lector.Read())
                    {
                        user = new Dictionary<string, dynamic>
                        {
                            { "Nombre", (lector.IsDBNull(4) ? "Sin nombre" : lector.GetString(4))},
                            { "Email", (lector.IsDBNull(6) ? "Sin correo" : lector.GetString(6))},
                            { "Estatus", (lector.IsDBNull(10) ? " " : lector.GetString(10)) },
                            { "Departamento", (lector.IsDBNull(17) ? " " : lector.GetString(17)) },
                        };
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return user;
        }


    }
    
}