using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Threading.Tasks;
using System.Web;
using TalentoBecario.Models.Entity;
using OracleCommand = Oracle.ManagedDataAccess.Client.OracleCommand;
using OracleConnection = Oracle.ManagedDataAccess.Client.OracleConnection;
using OracleParameter = Oracle.ManagedDataAccess.Client.OracleParameter;

namespace TalentoBecario.Models.Services
{
    public class StudentService
    {
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["BANNER"].ConnectionString;

        public static async Task<string> ObtenerMatricula(int pidm)
        {
            string matricula = "";
            try
            {
                using (var connection = new OracleConnection(_conString))
                {
                    OracleCommand command = new OracleCommand("F_UDEM_STU_ID", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        BindByName = true
                    };

                    command.Parameters.Add(new OracleParameter("salida", OracleDbType.Char, 9)
                    {
                        Direction = ParameterDirection.ReturnValue
                    });
                    command.Parameters.Add(new OracleParameter("cPidm", OracleDbType.Int32)
                    {
                        Value = pidm,
                        Direction = ParameterDirection.Input
                    });

                    connection.Open();

                    int ejecucion = await command.ExecuteNonQueryAsync();

                    try
                    {
                        matricula = Convert.ToString(command.Parameters["salida"]?.Value);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex;
            }

            return matricula;
        }

        /// <summary>
        /// Método que permite validar las credenciales de un usuario.
        /// </summary>
        /// <param name="matricula">Matricula o usuario del alumno.</param>
        /// <param name="pin"></param>
        /// <returns></returns>
        public static bool ValidaCredenciales(string matricula, string pin)
        {
            bool validas = false;
            try
            {


                using (var connection = new OracleConnection(_conString))
                {
                    OracleCommand command = new OracleCommand("SZ_BFQ_REGISTRATION.f_validar_credenciales", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        BindByName = true
                    };

                    command.Parameters.Add(new OracleParameter("salida", OracleDbType.Varchar2, 1)
                    {
                        Direction = System.Data.ParameterDirection.ReturnValue
                    });
                    command.Parameters.Add(new OracleParameter("p_matricula", OracleDbType.Varchar2, 200)
                    {
                        Value = matricula,
                        Direction = System.Data.ParameterDirection.Input
                    });

                    command.Parameters.Add(new OracleParameter("p_pin", OracleDbType.Varchar2, 200)
                    {
                        Value = pin,
                        Direction = System.Data.ParameterDirection.Input
                    });

                    connection.Open();

                    int ejecucion = command.ExecuteNonQuery();

                    try
                    {
                        validas = Convert.ToString(command.Parameters["salida"]?.Value) == "Y";
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex;
            }
            return validas;
        }

        /// <summary>
        /// Consulta los valores de nombre completo, nombre, genero, matricula y los almacena en el cache de la sesión.
        /// </summary>
        public static User FillUser(int pidm)
        {
            User user = new User();
            try
            {
                using (var connection = new OracleConnection(_conString))
                {
                    using (OracleCommand command = new OracleCommand("SZ_BFQ_REGISTRATION.f_obtener_datos_generales", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new OracleParameter("v_salida", OracleDbType.NVarchar2, 500)
                        {
                            Direction = ParameterDirection.ReturnValue
                        });

                        command.Parameters.Add("p_pidm", OracleDbType.Int32).Value = pidm;

                        command.Parameters.Add("p_matricula", OracleDbType.NVarchar2, 10, null, ParameterDirection.Output);
                        command.Parameters.Add("p_nombre", OracleDbType.NVarchar2, 100, null, ParameterDirection.Output);
                        command.Parameters.Add("p_nombre_completo", OracleDbType.NVarchar2, 300, null, ParameterDirection.Output);
                        command.Parameters.Add("p_genero", OracleDbType.NVarchar2, 50, null, ParameterDirection.Output);
                        command.Parameters.Add("p_programa", OracleDbType.NVarchar2, 300, null, ParameterDirection.Output);
                        command.Parameters.Add("p_studentType", OracleDbType.NVarchar2, 300, null, ParameterDirection.Output);

                        connection.Open();
                        command.ExecuteNonQuery();

                        if (command.Parameters["v_salida"]?.Value?.ToString() == "OP_EXITOSA")
                        {
                            user.FirstName = command.Parameters["p_nombre"]?.Value?.ToString();
                            user.FullName = command.Parameters["p_nombre_completo"]?.Value?.ToString();
                            user.Id = command.Parameters["p_matricula"]?.Value?.ToString();
                            user.Program = command.Parameters["p_programa"]?.Value?.ToString();
                            user.Gender = command.Parameters["p_genero"]?.Value?.ToString();
                            user.StudentType = command.Parameters["p_studentType"]?.Value?.ToString();
                        }

                    }
                }
                return user;
            }
            catch (Exception ex)
            {
                return user;
            }
        }

        /// <summary>
        /// Función que convierte una matricula a pidm.
        /// </summary>
        /// <param name="matricula">Matrículoa del alumno.</param>
        /// <returns>Pidm del alumno. -1 cuando la matricula no existe.</returns>
        public static int GetPidm(string matricula)
        {
            try
            {
                int pidm = -1;
                using (var connection = new OracleConnection(_conString))
                {
                    OracleCommand command = new OracleCommand("SZ_BFQ_REGISTRATION.f_obtener_pidm", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        BindByName = true
                    };

                    command.Parameters.Add(new OracleParameter("salida", OracleDbType.Int32)
                    {
                        Direction = System.Data.ParameterDirection.ReturnValue
                    });
                    command.Parameters.Add(new OracleParameter("p_usuario", OracleDbType.Varchar2, 200)
                    {
                        Value = matricula,
                        Direction = System.Data.ParameterDirection.Input
                    });

                    connection.Open();

                    int ejecucion = command.ExecuteNonQuery();
                    pidm = Convert.ToInt32(command.Parameters["salida"]?.Value.ToString());
                }
                return pidm;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static async Task<string> UserIsEmployee(int pidm)
        {
            string matricula = "";
            try
            {
                using (var connection = new OracleConnection(_conString))
                {
                    OracleCommand command = new OracleCommand("SZ_FPQ_PERSONDATA.F_UDEM_STU_IS_EMPLOYEE", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        BindByName = true
                    };

                    command.Parameters.Add(new OracleParameter("salida", OracleDbType.Char, 9)
                    {
                        Direction = ParameterDirection.ReturnValue
                    });
                    command.Parameters.Add(new OracleParameter("P_PIDM", OracleDbType.Int32)
                    {
                        Value = pidm,
                        Direction = ParameterDirection.Input
                    });

                    connection.Open();

                    int ejecucion = await command.ExecuteNonQueryAsync();

                    try
                    {
                        matricula = Convert.ToString(command.Parameters["salida"]?.Value);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex;
            }

            return matricula;
        }
    }
}
