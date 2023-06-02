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
                                    Pidm = (lector.IsDBNull(0) ? "" : lector.GetString(0)),
                                    nombre = (lector.IsDBNull(1) ? "" : lector.GetString(1)),
                                    nivel = (lector.IsDBNull(2) ? "" : lector.GetString(2)),
                                    programa = (lector.IsDBNull(3) ? "" : lector.GetString(3)),
                                   


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


    }
}