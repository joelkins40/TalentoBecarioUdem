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
                            comando.CommandText = "SZ_BGA_RTB.F_GET_ALUMNOS";
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


                                        pidm = (lector.IsDBNull(0) ? "0" : lector.GetString(0)),
                                        matricula = (lector.IsDBNull(1) ? "" : lector.GetString(1)),
                                        estatus = (lector.IsDBNull(2) ? "" : lector.GetString(2)),
                                        formador = new Formador()
                                        {
                                            Id = (lector.IsDBNull(3) ? "0" : lector.GetString(3)),
                                            Nombre= (lector.IsDBNull(4) ? "Sin Formador" : lector.GetString(4)),
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

        public static List<Alumno> ConsultarAlumnosPorFormador(string formador,Formador item)
        {
            List<Alumno> alumnoes = new List<Alumno>();

            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {

                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BGA_RTB.F_GET_ALUMNO_BY_TRAINER";
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.BindByName = true;
                        comando.Parameters.Add(new OracleParameter("P_Formador", OracleDbType.Int16)
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

                                    pidm = (lector.IsDBNull(0) ? "0" : lector.GetString(0)),
                                    matricula = (lector.IsDBNull(1) ? "" : lector.GetString(1)),
                                    estatus = (lector.IsDBNull(2) ? "" : lector.GetString(2)),
                                    formador = item,
                                   nombre = (lector.IsDBNull(7) ? "0" : lector.GetString(7)),
                                    nivel = (lector.IsDBNull(8) ? "0" : lector.GetString(8)),
                                    programa= (lector.IsDBNull(9) ? "0" : lector.GetString(9)),
                                   avanceAcademico= (lector.IsDBNull(10) ? "0" : lector.GetString(10)),
                                    porcentajeBeca = (lector.IsDBNull(11) ? "0" : lector.GetString(11)),
                                    horas = (lector.IsDBNull(12) ? "0" : lector.GetString(12)),
                                    EmailInst = (lector.IsDBNull(13) ? "0" : lector.GetString(13)),


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
            HabilidadesService.eliminarHabilidadesRelacion(alumno.pidm, 2);
            AreaInteresService.eliminarAreaInteresRelacion(alumno.pidm, 2);
            if (alumno.listHabilidades != null)
            {
                foreach (var item in alumno.listHabilidades)
                {
                    HabilidadesService.RelacionarHabilidadProyectoAlumno(item.Id, alumno.pidm, 2,alumno.matricula);
                }
            }
            if (alumno.listAreaInteres != null)
            {
                foreach (var item in alumno.listAreaInteres)
                {
                    AreaInteresService.RelacionarInteresProyectoAlumno(item.Id, alumno.pidm, 2,alumno.matricula);
                }
            }
            return "";
        }
        
        public static Alumno HomologarAlumno(string user)
        {
            Alumno alumno= SeleccionService.ObtieneDatosAlumno(user);
            Alumno consulting = new Alumno();

            consulting = ConsultarAlumno(alumno.pidm);
            alumno.pidm = consulting.pidm;
            alumno.listAreaInteres = consulting.listAreaInteres;
            alumno.listHabilidades = consulting.listHabilidades;
            alumno.formador = consulting.formador;

            return alumno; 
        }
            public static Alumno ConsultarAlumno(string pidm)
            {
                Alumno alumno = new Alumno();
                try
                {
                    using (OracleConnection cnx = new OracleConnection(_conString))
                    {
                        using (OracleCommand comando = new OracleCommand())
                        {
                            comando.Connection = cnx;
                            comando.CommandText = "SZ_BGA_RTB.F_GET_ALUMNO";
                            comando.CommandType = System.Data.CommandType.StoredProcedure;
                            comando.BindByName = true;
                            comando.Parameters.Add(new OracleParameter("P_Id", OracleDbType.Int16)
                            {
                                Value = pidm,
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
                                        pidm = (lector.IsDBNull(0) ? "0" : lector.GetString(0)),
                                        matricula = (lector.IsDBNull(1) ? "" : lector.GetString(1)),
                                        nombre = (lector.IsDBNull(2) ? "" : lector.GetString(2)),
                                        estatus = (lector.IsDBNull(3) ? "" : lector.GetString(3)),
                                        formador = FormadorService.ConsultarFormador(lector.IsDBNull(4) ? "0" : lector.GetString(4)),


                                    listHabilidades = HabilidadesService.ConsultarHabilidadesProyectoAlumno(pidm, 2),
                                        listAreaInteres = AreaInteresService.ConsultarInteresesProyectoAlumno(pidm, 2)
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
        public static Alumno ConsultarFormadorDelAlumno(string id)
        {
            Alumno alumno = new Alumno();
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BGA_RTB.F_GET_ALUMNO";
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
                                    formador = new Formador()
                                    {
                                        Id = (lector.IsDBNull(7) ? "" : lector.GetString(7)),
                                        Nombre = (lector.IsDBNull(8) ? "" : lector.GetString(8))
                                    },

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
                        comando.CommandText = "SZ_BGA_RTB.F_GET_ALUMNO";
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
                                    pidm = (lector.IsDBNull(0) ? "0" : lector.GetString(0)),
                                    nombre = (lector.IsDBNull(1) ? "" : lector.GetString(1)),
                                    matricula = (lector.IsDBNull(2) ? "" : lector.GetString(2)),
                                    nivel = (lector.IsDBNull(3) ? "" : lector.GetString(3)),
                                    horario = (lector.IsDBNull(4) ? "" : lector.GetString(4)),
                                    programa = (lector.IsDBNull(5) ? "" : lector.GetString(5)),
                                    estatus = (lector.IsDBNull(6) ? "" : lector.GetString(6)),
                                    formador = new Formador()
                                    {
                                        Id = (lector.IsDBNull(7) ? "" : lector.GetString(7)),
                                        Nombre = (lector.IsDBNull(8) ? "" : lector.GetString(8))
                                    },
                                    
                                    listHabilidades = HabilidadesService.ConsultarHabilidadesProyectoAlumno(id, 2),
                                    listAreaInteres = AreaInteresService.ConsultarInteresesProyectoAlumno(id, 2)

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
                            comando.CommandText = "SZ_BGQ_WTB.F_UDEM_ADD_ALUM";
                            comando.CommandType = CommandType.StoredProcedure;
                            comando.BindByName = true;

                            comando.Parameters.Add(new OracleParameter("P_Pidm", OracleDbType.Int16)
                            {
                                Value = registro.pidm,
                                Direction = System.Data.ParameterDirection.Input
                            });
                        comando.Parameters.Add(new OracleParameter("P_Estatus", OracleDbType.Varchar2)
                        {
                            Value = registro.estatus,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_IdFormador", OracleDbType.Int16)
                        {
                            Value = registro.formador.Id,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_User", OracleDbType.Varchar2)
                        {
                            Value = "Admin",
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
                            comando.CommandText = "SZ_BGQ_WTB.F_UDEM_UPDATE_ALUM";
                            comando.CommandType = System.Data.CommandType.StoredProcedure;
                            comando.BindByName = true;

                        comando.Parameters.Add(new OracleParameter("P_Pidm", OracleDbType.Int16)
                        {
                            Value = registro.pidm,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_Estatus", OracleDbType.Varchar2)
                        {
                            Value = registro.estatus,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_IdFormador", OracleDbType.Int16)
                        {
                            Value = registro.formador.Id,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_User", OracleDbType.Varchar2)
                        {
                            Value = "Admin",
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
        public static String ActualizarFormador(string  formador,int nuevoFormador)
        {
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "SZ_BGQ_WTB.F_UDEM_UPDATE_ALUM_BY_FORMADOR";
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.BindByName = true;

                        comando.Parameters.Add(new OracleParameter("P_PidmFormador", OracleDbType.Int16)
                        {
                            Value = formador,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_NuevoPidmFormador", OracleDbType.Int16)
                        {
                            Value = nuevoFormador,
                            Direction = System.Data.ParameterDirection.Input
                        });
                        comando.Parameters.Add(new OracleParameter("P_User", OracleDbType.Varchar2)
                        {
                            Value = "Admin",
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
                            comando.CommandText = "SZ_BGQ_WTB.F_UDEM_DELETE_DEPA";
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