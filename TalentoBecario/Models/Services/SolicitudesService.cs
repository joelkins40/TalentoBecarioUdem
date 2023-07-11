using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Configuration;

using System.Collections.Generic;

using System.Data;

using System.Threading.Tasks;
using System.Web;
using TalentoBecario.Models.Entity;
using System;

namespace TalentoBecario.Models.Services
{
    public class SolicitudesService
    {
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["BANNER"].ConnectionString;
        public static List<Solicitud> ObtieneListSolicituds()
        {
            List<Solicitud> solicituds = new List<Solicitud>();

            try
            {
                using (OracleConnection conn = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = conn;
                        comando.CommandText = "SZ_BMA_RTB.F_GET_PROYECTOS";
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.BindByName = true;
                        comando.Parameters.Add(new OracleParameter("V_SALIDA", OracleDbType.RefCursor)
                        {
                            Direction = ParameterDirection.ReturnValue
                        });
                        conn.Open();

                        try
                        {
                            OracleDataReader lector = comando.ExecuteReader();

                            while (lector.Read())
                            {
                                solicituds.Add(new Solicitud
                                {
                                    id = (lector.IsDBNull(0) ? 0 : lector.GetInt32(0)),
                                    alumno = AlumnoService.ConsultarAlumno((lector.IsDBNull(1) ? "0" : lector.GetString(1))),


                                    proyecto = ProyectoService.ConsultarProyecto((lector.IsDBNull(2) ? 0 : lector.GetInt32(2))),
                                    comentario = (lector.IsDBNull(3) ? "" : lector.GetString(3)),
                                    estatus = (lector.IsDBNull(4) ? "" : lector.GetString(4))

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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return solicituds;
        }
       
        public static Solicitud ConsultarSolicitud(int id)
        {
            Solicitud solicitud = new Solicitud();
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_RTB.F_GET_HISTORIALALUMNO";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.BindByName = true;
                        comando.Parameters.Add(new OracleParameter("P_Id", OracleDbType.Int32)
                        {
                            Value = id,
                            Direction =ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("V_SALIDA", OracleDbType.RefCursor)
                        {
                            Direction = ParameterDirection.ReturnValue
                        });
                        cnx.Open();
                        try
                        {
                            OracleDataReader lector = comando.ExecuteReader();
                            while (lector.Read())
                            {
                                solicitud = new Solicitud()
                                {
                                    id = (lector.IsDBNull(0) ? 0 : lector.GetInt32(0)),
                                    alumno = AlumnoService.ConsultarAlumno((lector.IsDBNull(1) ? "0" : lector.GetString(1))),


                                    proyecto = ProyectoService.ConsultarProyecto((lector.IsDBNull(2) ? 0 : lector.GetInt32(2))),
                                  comentario= (lector.IsDBNull(3) ? "" : lector.GetString(3)),
                                   estatus= (lector.IsDBNull(4) ? "" : lector.GetString(4))


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
            return solicitud;

        }
        public static List<Solicitud> ConsultarSiExisteSolicitud(int idAlumno,int idProyecto)
        {
          
            List<Solicitud> solicituds = new List<Solicitud>();
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_RTB.F_GET_PROJECT_BY_ALUMNO";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.BindByName = true;
                        comando.Parameters.Add(new OracleParameter("P_IdAlumno", OracleDbType.Int32)
                        {
                            Value = idAlumno,
                            Direction = ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_IdProyecto", OracleDbType.Int32)
                        {
                            Value = idProyecto,
                            Direction = ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("V_SALIDA", OracleDbType.RefCursor)
                        {
                            Direction = ParameterDirection.ReturnValue
                        });
                        cnx.Open();
                        try
                        {
                            OracleDataReader lector = comando.ExecuteReader();
                            while (lector.Read())
                            {
                                solicituds.Add(new Solicitud
                                {
                                    id = (lector.IsDBNull(0) ? 0 : lector.GetInt32(0)),
                                    alumno = AlumnoService.ConsultarAlumno((lector.IsDBNull(1) ? "0" : lector.GetString(1))),


                                    proyecto = ProyectoService.ConsultarProyecto((lector.IsDBNull(2) ? 0 : lector.GetInt32(2))),
                                    comentario = (lector.IsDBNull(3) ? "" : lector.GetString(3)),
                                    estatus = (lector.IsDBNull(4) ? "" : lector.GetString(4))

                                });
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
            return solicituds;

        }

        public static List<Solicitud> consultarSolicitudesFormador(string formador)
        {

            List<Solicitud> solicituds = new List<Solicitud>();
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_RTB.F_GET_HISTORIAL_BY_FORMADOR";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.BindByName = true;
                       
                        comando.Parameters.Add(new OracleParameter("P_IdFormador", OracleDbType.Varchar2)
                        {
                            Value = formador,
                            Direction = ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("V_SALIDA", OracleDbType.RefCursor)
                        {
                            Direction = ParameterDirection.ReturnValue
                        });
                        cnx.Open();
                        try
                        {
                            OracleDataReader lector = comando.ExecuteReader();
                            while (lector.Read())
                            {
                                solicituds.Add(new Solicitud
                                {
                                    id = (lector.IsDBNull(0) ? 0 : lector.GetInt32(0)),
                                    alumno=new Alumno()
                                    {
                                        matricula=lector.IsDBNull(2) ? "" : lector.GetString(2),
                                        nombre= lector.IsDBNull(3) ? "" : lector.GetString(3)
                                    },
                                    

                                    proyecto = new Proyecto()
                                    {
                                        nombre= lector.IsDBNull(5) ? "" : lector.GetString(5),
                                        formador=new Formador()
                                        {
                                            Nombre= lector.IsDBNull(7) ? "" : lector.GetString(7)
                                        }

                                    }
                                   
                                    ,
                                    comentario = (lector.IsDBNull(8) ? "" : lector.GetString(8)),
                                    estatus = (lector.IsDBNull(9) ? "" : lector.GetString(9))

                                });
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
            return solicituds;

        }
        public static String guardarSolicitud(Solicitud registro)
        {
            string idSolicitud="0";
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_WTB.F_UDEM_ADD_HIAL";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.BindByName = true;

                        comando.Parameters.Add(new OracleParameter("P_IdAlumno", OracleDbType.Int16)
                        {
                            Value = registro.alumno.id,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_IdProyecto", OracleDbType.Int16)
                        {
                            Value = registro.proyecto.id,
                            Direction = System.Data.ParameterDirection.Input
                        });
                       
                        comando.Parameters.Add(new OracleParameter("P_Comentarios", OracleDbType.Varchar2)
                        {
                            Value = registro.comentario,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_Estatus", OracleDbType.Varchar2)
                        {
                            Value = registro.estatus,
                            Direction = System.Data.ParameterDirection.Input
                        });

                        comando.Parameters.Add(new OracleParameter("V_Salida", OracleDbType.Varchar2, 200)
                        {
                            Direction = ParameterDirection.ReturnValue
                        });

                        try
                        {
                            cnx.Open();
                            comando.ExecuteNonQuery();
                            idSolicitud = Convert.ToString(comando.Parameters["V_Id"].Value);

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
           
            return "Registro Agregado Con Éxito"; 

        }
        public static String ActualizarSolicitud(Solicitud registro)
        {
            int rowAffected = 0;
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_WTB.F_UDEM_UPDATE_HIAL";
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.BindByName = true;

                        comando.Parameters.Add(new OracleParameter("P_Id", OracleDbType.Int16)
                        {
                            Value = registro.alumno.id,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_IdAlumno", OracleDbType.Int16)
                        {
                            Value = registro.alumno.id,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_IdProyecto", OracleDbType.Int16)
                        {
                            Value = registro.proyecto.id,
                            Direction = System.Data.ParameterDirection.Input
                        });

                        comando.Parameters.Add(new OracleParameter("P_Comentarios", OracleDbType.Varchar2)
                        {
                            Value = registro.comentario,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_Estatus", OracleDbType.Varchar2)
                        {
                            Value = registro.estatus,
                            Direction = System.Data.ParameterDirection.Input
                        });

                        comando.Parameters.Add(new OracleParameter("V_Salida", OracleDbType.Varchar2, 200)
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

        public static string EliminarSolicitud(int id)
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

                        comando.Parameters.Add(new OracleParameter("V_Salida", OracleDbType.Varchar2, 400)
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