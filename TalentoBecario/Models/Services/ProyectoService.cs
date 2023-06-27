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
    public class ProyectoService
    {
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["BANNER"].ConnectionString;

        public static List<Proyecto> ObtieneListProyectos()
        {
            List<Proyecto> proyectos = new List<Proyecto>();

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
                                proyectos.Add(new Proyecto
                                {
                                    id = (lector.IsDBNull(0) ? 0 : lector.GetInt32(0)),
                                    nombre = (lector.IsDBNull(1) ? "" : lector.GetString(1)),
                                    descripcion = (lector.IsDBNull(2) ? "" : lector.GetString(2)),
                                    estatus = (lector.IsDBNull(3) ? "" : lector.GetString(3)),
                                    alumnosRequeridos = (lector.IsDBNull(7) ? 0 : lector.GetInt32(7)),
                                    formador = new Formador()
                                    {
                                        Id = (lector.IsDBNull(4) ? "" : lector.GetString(4)),
                                        Nombre = (lector.IsDBNull(5) ? "" : lector.GetString(5))
                                    },
                                    departamento=new Departamento()
                                    {
                                        Id = (lector.IsDBNull(6) ? 0 : lector.GetInt32(6)),
                                         Descripcion = (lector.IsDBNull(8) ? "" : lector.GetString(8)),
                                    }


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

            return proyectos;
        }

        public static List<Proyecto> ObtieneListProyectosPorFormador(string id)
        {
            List<Proyecto> proyectos = new List<Proyecto>();

            try
            {
                using (OracleConnection conn = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = conn;
                        comando.CommandText = "SZ_BMA_RTB.F_GET_PROYECTOS_BY_FORMADOR";
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.BindByName = true;
                        comando.Parameters.Add(new OracleParameter("P_Id", OracleDbType.Varchar2)
                        {
                            Value = id,
                            Direction = ParameterDirection.Input
                        });

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
                                proyectos.Add(new Proyecto
                                {
                                    id = (lector.IsDBNull(0) ? 0 : lector.GetInt32(0)),
                                    nombre = (lector.IsDBNull(1) ? "" : lector.GetString(1)),
                                    descripcion = (lector.IsDBNull(2) ? "" : lector.GetString(2)),
                                    estatus = (lector.IsDBNull(3) ? "" : lector.GetString(3)),
                                    alumnosRequeridos = (lector.IsDBNull(7) ? 0 : lector.GetInt32(7)),
                                    formador = new Formador()
                                    {
                                        Id = (lector.IsDBNull(4) ? "" : lector.GetString(4)),
                                        Nombre = (lector.IsDBNull(5) ? "" : lector.GetString(5))
                                    },
                                    departamento = new Departamento()
                                    {
                                        Id = (lector.IsDBNull(6) ? 0 : lector.GetInt32(6)),
                                        Descripcion = (lector.IsDBNull(8) ? "" : lector.GetString(8)),
                                    }


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

            return proyectos;
        }

        public static Proyecto ConsultarProyecto(int id)
        {
            Proyecto proyecto = new Proyecto();
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_RTB.F_GET_PROYECTO";
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
                                proyecto = new Proyecto()
                                {
                                    id = (lector.IsDBNull(0) ? 0 : lector.GetInt32(0)),
                                    nombre = (lector.IsDBNull(1) ? "" : lector.GetString(1)),
                                    descripcion = (lector.IsDBNull(2) ? "" : lector.GetString(2)),
                                    estatus = (lector.IsDBNull(3) ? "" : lector.GetString(3)),
                                    alumnosRequeridos = (lector.IsDBNull(7) ? 0 : lector.GetInt32(7)),
                                    formador = new Formador()
                                    {
                                        Id = (lector.IsDBNull(4) ? "" : lector.GetString(4)),
                                        Nombre = (lector.IsDBNull(5) ? "" : lector.GetString(5))
                                    },
                                    departamento = new Departamento()
                                    {
                                        Id = (lector.IsDBNull(6) ? 0 : lector.GetInt32(6)),
                                        Descripcion = (lector.IsDBNull(8) ? "" : lector.GetString(8)),
                                    },
                                    listHabilidades = HabilidadesService.ConsultarHabilidadesProyectoAlumno(id, 1),
                                    listAreaInteres= AreaInteresService.ConsultarInteresesProyectoAlumno(id,1)



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
            return proyecto;

        }
        public static String guardarProyecto(Proyecto registro)
        {
            string idProyecto="0";
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_WTB.F_UDEM_ADD_PROY";
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.BindByName = true;

                        comando.Parameters.Add(new OracleParameter("P_Nombre", OracleDbType.Varchar2)
                        {
                            Value = registro.nombre,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_Descripcion", OracleDbType.Varchar2)
                        {
                            Value = registro.descripcion,
                            Direction = System.Data.ParameterDirection.Input
                        });
                       
                        comando.Parameters.Add(new OracleParameter("P_Estatus", OracleDbType.Varchar2)
                        {
                            Value = registro.estatus,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_IdFormador", OracleDbType.Varchar2)
                        {
                            Value = registro.formador.Id,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_IdDepartamento", OracleDbType.Int16)
                        {
                            Value = registro.departamento.Id,
                            Direction = System.Data.ParameterDirection.Input
                        });

                        comando.Parameters.Add(new OracleParameter("P_AlumnosRequeridos", OracleDbType.Int16)
                        {
                            Value = registro.alumnosRequeridos,
                            Direction = System.Data.ParameterDirection.Input
                        });

                        comando.Parameters.Add(new OracleParameter("V_Id", OracleDbType.Int32,5)
                        {
                            Direction = ParameterDirection.ReturnValue
                        });
                       
                        try
                        {
                            cnx.Open();
                            comando.ExecuteNonQuery();
                            idProyecto = Convert.ToString(comando.Parameters["V_Id"].Value);

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
            if (!idProyecto.Equals("0"))
            {
                if (registro.listHabilidades != null)
                {
                    foreach (var item in registro.listHabilidades)
                    {
                        HabilidadesService.RelacionarHabilidadProyectoAlumno(item.Id, Convert.ToInt16(idProyecto), 1);
                    }
                }
                if (registro.listAreaInteres != null)
                {
                    foreach (var item in registro.listAreaInteres)
                    {
                        AreaInteresService.RelacionarInteresProyectoAlumno(item.Id, Convert.ToInt16(idProyecto), 1);
                    }
                }
            }
            return "Registro Agregado Con Éxito"; 

        }
        public static String ActualizarProyecto(Proyecto registro)
        {
            int rowAffected = 0;
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_WTB.F_UDEM_UPDATE_PROY";
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.BindByName = true;
                        comando.Parameters.Add(new OracleParameter("P_Id", OracleDbType.Int16)
                        {
                            Value = registro.id,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_Nombre", OracleDbType.Varchar2)
                        {
                            Value = registro.nombre,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_Descripcion", OracleDbType.Varchar2)
                        {
                            Value = registro.descripcion,
                            Direction = System.Data.ParameterDirection.Input
                        });

                        comando.Parameters.Add(new OracleParameter("P_Estatus", OracleDbType.Varchar2)
                        {
                            Value = registro.estatus,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_IdFormador", OracleDbType.Varchar2)
                        {
                            Value = registro.formador.Id,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_IdDepartamento", OracleDbType.Int16)
                        {
                            Value = registro.departamento.Id,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_AlumnosRequeridos", OracleDbType.Int16)
                        {
                            Value = registro.alumnosRequeridos,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("V_Salida", OracleDbType.Varchar2, 400)
                        {
                            Direction = ParameterDirection.ReturnValue
                        });
                        cnx.Open();
                        try
                        {
                            rowAffected= comando.ExecuteNonQuery();

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
            if (rowAffected != 0)
            {
                HabilidadesService.eliminarHabilidadesRelacion(registro.id, 1);
                AreaInteresService.eliminarAreaInteresRelacion(registro.id, 1);
                if (registro.listHabilidades != null)
                {
                    foreach (var item in registro.listHabilidades)
                    {
                        HabilidadesService.RelacionarHabilidadProyectoAlumno(item.Id, registro.id, 1);
                    }
                }
                if (registro.listAreaInteres != null)
                {
                    foreach (var item in registro.listAreaInteres)
                    {
                        AreaInteresService.RelacionarInteresProyectoAlumno(item.Id, registro.id, 1);
                    }
                }
            }
            return "Registro Actualizado Con Éxito";

        }

        public static string EliminarProyecto(int id)
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