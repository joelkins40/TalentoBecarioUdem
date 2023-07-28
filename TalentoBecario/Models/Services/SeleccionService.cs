using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Configuration;

using System.Collections.Generic;

using System.Data;

using System.Threading.Tasks;
using System.Web;
using TalentoBecario.Models.Entity;
using System;
using System.Linq;

namespace TalentoBecario.Models.Services
{
    public class SeleccionService
    {
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["BANNER"].ConnectionString;
        public static List<Alumno> ObtieneListAlumnos()
        {
            List<Alumno> alumnos = new List<Alumno>();

            try
            {
                using (OracleConnection conn = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = conn;
                        comando.CommandText = "SZ_BMA_RTB.F_GET_ALUMNOS_BY_CODE";
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
                                alumnos.Add(new Alumno
                                {
                                    pidm = (lector.IsDBNull(0) ? "" : lector.GetString(0)),
                                    matricula = (lector.IsDBNull(1) ? "" : lector.GetString(1)),
                                    nombre = (lector.IsDBNull(2) ? "" : lector.GetString(2)),
                                    nivel = (lector.IsDBNull(3) ? "" : lector.GetString(3)),
                                    programa = (lector.IsDBNull(4) ? "" : lector.GetString(4)),
                                    avanceAcademico = (lector.IsDBNull(5) ? "" : lector.GetString(5)),
                                    porcentajeBeca = (lector.IsDBNull(6) ? "" : lector.GetString(6)),
                                    horas = (lector.IsDBNull(7) ? "" : lector.GetString(7)),
                                    formador =new Formador()
                                   {
                                       Nombre="Sin Formador"
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

            return alumnos;
        }

        public static Alumno ObtieneDatosAlumno(string id)
        {
            Alumno item = new Alumno();

            try
            {
                using (OracleConnection conn = new OracleConnection(_conString))
                {
                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = conn;
                        comando.CommandText = "SZ_BMA_RTB.F_GET_INFO_ALUMNO_BY_PIDM";
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.BindByName = true;
                        comando.Parameters.Add(new OracleParameter("P_Id", OracleDbType.Varchar2)
                        {
                            Value = id,
                            Direction = System.Data.ParameterDirection.Input
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
                                item=new Alumno
                                {
                                    pidm = (lector.IsDBNull(0) ? "" : lector.GetString(0)),
                                    matricula = (lector.IsDBNull(1) ? "" : lector.GetString(1)),
                                    nombre = (lector.IsDBNull(2) ? "" : lector.GetString(2)),
                                    nivel = (lector.IsDBNull(3) ? "" : lector.GetString(3)),
                                    programa = (lector.IsDBNull(4) ? "" : lector.GetString(4)),
                                    avanceAcademico = (lector.IsDBNull(5) ? "" : lector.GetString(5)),
                                    porcentajeBeca = (lector.IsDBNull(6) ? "" : lector.GetString(6)),
                                    horas = (lector.IsDBNull(7) ? "" : lector.GetString(7)),
                                    formador = new Formador()
                                    {
                                        Nombre = "Sin Formador"
                                    },
                                    EmailInst = (lector.IsDBNull(8) ? "" : lector.GetString(8)),



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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return item;
        }

        public static List<Alumno> AlumnosFiltro()
        {
            List<Alumno> alumnosExternos = new List<Alumno>();
            List<Alumno> alumnoslocales = new List<Alumno>();
            List<Alumno> todos = new List<Alumno>();
            alumnosExternos = SeleccionService.ObtieneListAlumnos();
            alumnoslocales = AlumnoService.ObtieneListAlumnos();

            foreach (Alumno person in alumnosExternos)
            {
                foreach (Alumno personFormador in alumnoslocales)
                {

                    if (person.matricula.Equals(personFormador.matricula))
                    {
                        person.formador = personFormador.formador;
                    }
                }
            }
            
           

            //List<Alumno> unionList = alumnosExternos.uni (alumnoslocales);

            return alumnosExternos;
        }

    }
   
}