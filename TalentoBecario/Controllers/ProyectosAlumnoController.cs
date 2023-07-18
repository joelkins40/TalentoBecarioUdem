using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Services;

namespace TalentoBecario.Controllers
{
    public class ProyectosAlumnoController : Controller
    {
        public ActionResult Index()
        {
            string matricula = Convert.ToString(Session["matricula"]);

            ViewBag.listProyectos=SolicitudesService.consultarSolicitudesAlumno(AlumnoService.ConsultarAlumno(matricula).id);
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
    }
}