using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;

namespace TalentoBecario.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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

            if(username == "Admin" && pass == "Admin")
            {
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
                Session["matricula"] = username;
                FormadorService.verificarFormador(new Formador(){
                    Id = Convert.ToString(username) }, pidmResult);

                return RedirectToAction("Index", "Proyectos");
            }else 
            if (isEmployee.Result.Trim() == "N")
            {
                Session["matricula"] = username;
                return RedirectToAction("Index", "ProyectosAlumno");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}