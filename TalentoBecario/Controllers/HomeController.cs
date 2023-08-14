using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;
using System.Web.Security;

namespace TalentoBecario.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult SignIn()
        {
            string username = Request.Params["username"];
            string pass = Request.Params["pass"];
            string recuerdame = Request.Params["remember-me"];
            bool persitente = recuerdame == "Y";

            if (username == "Admin" && pass == "Admin")
            {
                FormsAuthentication.SetAuthCookie(username, persitente);
                return RedirectToAction("Index", "Habilidades");

            }

            bool valida = StudentService.ValidaCredenciales(username, pass);
            if (!valida)
            {
                ViewBag.ErrorLogin = "Cuenta o contraseña incorrecta";
                return RedirectToAction("Index", "Home");
            }

            var pidmResult = StudentService.GetPidm(username);

            var matricula = StudentService.ObtenerMatricula(pidmResult);

            var isEmployee = StudentService.UserIsEmployee(pidmResult);

            if (isEmployee.Result.Trim() == "Y")
            {
                Formador consulting= FormadorService.verificarFormador(new Formador(){
                    Id = pidmResult
                } );
                FormsAuthentication.SetAuthCookie(pidmResult, persitente);
                Session["matricula"] = pidmResult;

                Session["nombreUser"] = consulting.Nombre;
                return RedirectToAction("Index", "MisAlumnos");
            }else 
            if (isEmployee.Result.Trim() == "N")
            {
               Alumno AlumnoConsulting= AlumnoService.ConsultarAlumno(pidmResult);
                if (AlumnoConsulting.pidm == null)
                {
                    AlumnoConsulting = SeleccionService.ObtieneDatosAlumno(pidmResult);
                    if (AlumnoConsulting.pidm == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        AlumnoConsulting.formador.Id = "000000";
                        AlumnoConsulting.horario = "NA";
                        AlumnoConsulting.estatus = "Activo";
                        AlumnoService.guardarAlumno(AlumnoConsulting);
                    }
                   
                }
                FormsAuthentication.SetAuthCookie(pidmResult, persitente);

                Session["matricula"] = pidmResult;
                Session["nombreUser"] = AlumnoConsulting.nombre;
              
                return RedirectToAction("Index", "MiProyecto");

            }

            return RedirectToAction("Index", "Home");
        }
    }
}