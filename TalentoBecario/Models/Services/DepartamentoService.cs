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
    public class DepartamentoService
    {
        public class DepartamentosService
        {
            private static readonly string _conString = ConfigurationManager.ConnectionStrings["BANNER"].ConnectionString;

            public static List<Departamento> ObtieneListDepartamentos()
            {
                List<Departamento> departamentoes = new List<Departamento>();

                try
                {
                    using (OracleConnection cnx = new OracleConnection(_conString))
                    {

                        using (OracleCommand comando = new OracleCommand())
                        {
                            comando.Connection = cnx;
                            comando.CommandText = "SZ_BMA_RTB.F_GET_DEPARTAMENTOS";
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



                                    departamentoes.Add(new Departamento()
                                    {

                                        Id = lector.GetInt32(0),
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
                return departamentoes;

            }


            public static Departamento ConsultarDepartamento(int id)
            {
                Departamento departamento = new Departamento();
                try
                {
                    using (OracleConnection cnx = new OracleConnection(_conString))
                    {
                        using (OracleCommand comando = new OracleCommand())
                        {
                            comando.Connection = cnx;
                            comando.CommandText = "SZ_BMA_RTB.F_GET_DEPARTAMENTO";
                            comando.CommandType = System.Data.CommandType.StoredProcedure;
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
                                    departamento = new Departamento()
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
                return departamento;

            }
            public static String guardarDepartamento(Departamento registro)
            {
                try
                {
                    using (OracleConnection cnx = new OracleConnection(_conString))
                    {
                        using (OracleCommand comando = new OracleCommand())
                        {
                            comando.Connection = cnx;
                            comando.CommandText = "SZ_BMA_WTB.F_UDEM_ADD_DEPA";
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
            public static String ActualizarDepartamento(Departamento registro)
            {
                try
                {
                    using (OracleConnection cnx = new OracleConnection(_conString))
                    {
                        using (OracleCommand comando = new OracleCommand())
                        {
                            comando.Connection = cnx;
                            comando.CommandText = "SZ_BMA_WTB.F_UDEM_UPDATE_DEPA";
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

            public static string EliminarDepartamento(int id)
            {
                try
                {
                    using (OracleConnection cnx = new OracleConnection(_conString))
                    {
                        using (OracleCommand comando = new OracleCommand())
                        {
                            comando.Connection = cnx;
                            comando.CommandText = "SZ_BMA_WTB.F_UDEM_UPDATE_DEPA";
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
}