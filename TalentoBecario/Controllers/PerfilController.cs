using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;

namespace TalentoBecario.Controllers
{
    public class PerfilController : Controller
    {
        public ActionResult Index()
        {
            string user = Convert.ToString(Session["matricula"]);

            
            ViewBag.alumno = AlumnoService.HomologarAlumno(user);

            ViewBag.listAreaInteres = AreaInteresService.ObtieneListAreaIntereses();
            ViewBag.listHabilidades = HabilidadesService.ObtieneListHabilidades();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public JsonResult SavePerfil(Alumno alumno)
        {
            string message = "";
            Alumno consultingAlumno = new Alumno();
            consultingAlumno = AlumnoService.ConsultarAlumno(alumno.matricula);

            if (consultingAlumno.id == null)
            {
                

                AlumnoService.guardarAlumno(alumno);
                
                alumno = AlumnoService.ConsultarAlumno(alumno.matricula);
            }
            consultingAlumno.listHabilidades = alumno.listHabilidades;
            consultingAlumno.listAreaInteres = alumno.listAreaInteres;

            message = AlumnoService.guardarAlumnoRelacion(consultingAlumno);
           
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}