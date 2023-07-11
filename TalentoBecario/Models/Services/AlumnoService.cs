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
    public class AlumnoService
    {
       
            private static readonly string _conString = ConfigurationManager.ConnectionStrings["BANNER"].ConnectionString;

            public static List<Alumno> ObtieneListAlumnos()
            {
                List<Alumno> alumnoes = new List<Alumno>();

                try
                {
                    using (OracleConnection cnx = new OracleConnection(_conString))
                    {

                        using (OracleCommand comando = new OracleCommand())
                        {
                            comando.Connection = cnx;
                            comando.CommandText = "SZ_BMA_RTB.F_GET_ALUMNOS";
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



                                    alumnoes.Add(new Alumno()
                                    {


                                        id = (lector.IsDBNull(0) ? 1 : lector.GetInt16(0)),
                                        nombre = (lector.IsDBNull(1) ? "" : lector.GetString(1)),
                                        matricula = (lector.IsDBNull(2) ? "" : lector.GetString(2)),
                                        nivel = (lector.IsDBNull(3) ? "" : lector.GetString(3)),
                                        horario = (lector.IsDBNull(4) ? "" : lector.GetString(4)),
                                        carrera = (lector.IsDBNull(5) ? "" : lector.GetString(5)),
                                        estatus = (lector.IsDBNull(6) ? "" : lector.GetString(6)),
                                        formador = new Formador()
                                        {
                                            Id = (lector.IsDBNull(7) ? "" : lector.GetString(7)),
                                            Nombre = (lector.IsDBNull(8) ? "" : lector.GetString(8))
                                        },


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
                return alumnoes;

            }

        public static List<Alumno> ConsultarAlumnosPorFormador(string formador)
        {
            List<Alumno> alumnoes = new List<Alumno>();

            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {

                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_RTB.F_GET_ALUMNO_BY_TRAINER";
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.BindByName = true;
                        comando.Parameters.Add(new OracleParameter("P_Formador", OracleDbType.Varchar2)
                        {
                            Value = formador,
                            Direction = System.Data.ParameterDirection.Input
                        });
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



                                alumnoes.Add(new Alumno()
                                {


                                    id = (lector.IsDBNull(0) ? 0 : lector.GetInt16(0)),
                                    nombre = (lector.IsDBNull(1) ? "" : lector.GetString(1)),
                                    matricula = (lector.IsDBNull(2) ? "" : lector.GetString(2)),
                                    nivel = (lector.IsDBNull(3) ? "" : lector.GetString(3)),
                                    horario = (lector.IsDBNull(4) ? "" : lector.GetString(4)),
                                    carrera = (lector.IsDBNull(5) ? "" : lector.GetString(5)),
                                    estatus = (lector.IsDBNull(6) ? "" : lector.GetString(6)),
                                    formador = new Formador()
                                    {
                                        Id = (lector.IsDBNull(7) ? "" : lector.GetString(7)),
                                        Nombre = (lector.IsDBNull(8) ? "" : lector.GetString(8))
                                    },


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
      
            return alumnoes;

        }

     public static String guardarAlumnoRelacion(Alumno alumno)
        {
            HabilidadesService.eliminarHabilidadesRelacion(alumno.id, 2);
            AreaInteresService.eliminarAreaInteresRelacion(alumno.id, 2);
            if (alumno.listHabilidades != null)
            {
                foreach (var item in alumno.listHabilidades)
                {
                    HabilidadesService.RelacionarHabilidadProyectoAlumno(item.Id, alumno.id, 2);
                }
            }
            if (alumno.listAreaInteres != null)
            {
                foreach (var item in alumno.listAreaInteres)
                {
                    AreaInteresService.RelacionarInteresProyectoAlumno(item.Id, alumno.id, 2);
                }
            }
            return "";
        }
        public static Alumno ConsultarAlumno(string id)
            {
                Alumno alumno = new Alumno();
                try
                {
                    using (OracleConnection cnx = new OracleConnection(_conString))
                    {
                        using (OracleCommand comando = new OracleCommand())
                        {
                            comando.Connection = cnx;
                            comando.CommandText = "SZ_BMA_RTB.F_GET_ALUMNO";
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
                                    alumno = new Alumno()
                                    {
                                        id = (lector.IsDBNull(0) ? 0 : lector.GetInt16(0)),
                                        nombre = (lector.IsDBNull(1) ? "" : lector.GetString(1)),
                                        matricula = (lector.IsDBNull(2) ? "" : lector.GetString(2)),
                                        nivel = (lector.IsDBNull(3) ? "" : lector.GetString(3)),
                                        horario = (lector.IsDBNull(4) ? "" : lector.GetString(4)),
                                        carrera = (lector.IsDBNull(5) ? "" : lector.GetString(5)),
                                        estatus = (lector.IsDBNull(6) ? "" : lector.GetString(6)),
                                        formador = new Formador()
                                        {
                                            Id = (lector.IsDBNull(7) ? "" : lector.GetString(7)),
                                            Nombre = (lector.IsDBNull(8) ? "" : lector.GetString(8))
                                        },


                                    listHabilidades = HabilidadesService.ConsultarHabilidadesProyectoAlumno(lector.GetInt16(0), 2),
                                        listAreaInteres = AreaInteresService.ConsultarInteresesProyectoAlumno(lector.GetInt16(0), 2)
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
                return alumno;

            }
        public static Alumno ConsultarAlumnoByCode(string id)
        {
            Alumno alumno = new Alumno();
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_RTB.F_GET_ALUMNO";
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
                                alumno = new Alumno()
                                {
                                    id = (lector.IsDBNull(0) ? 0 : lector.GetInt16(0)),
                                    nombre = (lector.IsDBNull(1) ? "" : lector.GetString(1)),
                                    matricula = (lector.IsDBNull(2) ? "" : lector.GetString(2)),
                                    nivel = (lector.IsDBNull(3) ? "" : lector.GetString(3)),
                                    horario = (lector.IsDBNull(4) ? "" : lector.GetString(4)),
                                    carrera = (lector.IsDBNull(5) ? "" : lector.GetString(5)),
                                    estatus = (lector.IsDBNull(6) ? "" : lector.GetString(6)),
                                    formador = new Formador()
                                    {
                                        Id = (lector.IsDBNull(7) ? "" : lector.GetString(7)),
                                        Nombre = (lector.IsDBNull(8) ? "" : lector.GetString(8))
                                    },
                                    
                                    listHabilidades = HabilidadesService.ConsultarHabilidadesProyectoAlumno(lector.GetInt16(0), 2),
                                    listAreaInteres = AreaInteresService.ConsultarInteresesProyectoAlumno(lector.GetInt16(0), 2)

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
            return alumno;

        }
        public static String guardarAlumno(Alumno registro)
            {
                try
                {
                    using (OracleConnection cnx = new OracleConnection(_conString))
                    {
                        using (OracleCommand comando = new OracleCommand())
                        {
                            comando.Connection = cnx;
                            comando.CommandText = "SZ_BMA_WTB.F_UDEM_ADD_ALUM";
                            comando.CommandType = CommandType.StoredProcedure;
                            comando.BindByName = true;

                            comando.Parameters.Add(new OracleParameter("P_Nombre", OracleDbType.Varchar2)
                            {
                                Value = registro.nombre,
                                Direction = System.Data.ParameterDirection.Input
                            });
                        comando.Parameters.Add(new OracleParameter("P_Matricula", OracleDbType.Varchar2)
                        {
                            Value = registro.matricula,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_Nivel", OracleDbType.Varchar2)
                        {
                            Value = registro.nivel,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_Horario", OracleDbType.Varchar2)
                        {
                            Value = registro.horario,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_Carrera", OracleDbType.Varchar2)
                        {
                            Value = registro.carrera,
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
            public static String ActualizarAlumno(Alumno registro)
            {
                try
                {
                    using (OracleConnection cnx = new OracleConnection(_conString))
                    {
                        using (OracleCommand comando = new OracleCommand())
                        {
                            comando.Connection = cnx;
                            comando.CommandText = "SZ_BMA_WTB.F_UDEM_UPDATE_ALUM";
                            comando.CommandType = System.Data.CommandType.StoredProcedure;
                            comando.BindByName = true;

                            comando.Parameters.Add(new OracleParameter("P_Id", OracleDbType.Int32)
                            {
                                Value = registro.id,
                                Direction = System.Data.ParameterDirection.Input
                            });
                        comando.Parameters.Add(new OracleParameter("P_Nombre", OracleDbType.Varchar2)
                        {
                            Value = registro.nombre,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_Matricula", OracleDbType.Varchar2)
                        {
                            Value = registro.matricula,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_Nivel", OracleDbType.Varchar2)
                        {
                            Value = registro.nivel,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_Horario", OracleDbType.Varchar2)
                        {
                            Value = registro.horario,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_Carrera", OracleDbType.Varchar2)
                        {
                            Value = registro.carrera,
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
                        comando.Parameters.Add(new OracleParameter("V_Salida", OracleDbType.Varchar2,200)
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
        public static String ActualizarFormador(string  formador,string nuevoFormador)
        {
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BMA_WTB.F_UDEM_UPDATE_ALUM_BY_FORMADOR";
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.BindByName = true;

                        comando.Parameters.Add(new OracleParameter("P_IdFormador", OracleDbType.Varchar2)
                        {
                            Value = formador,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_NuevoIdFormador", OracleDbType.Varchar2)
                        {
                            Value = nuevoFormador,
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
        public static string EliminarAlumno(int id)
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


        }
    
}