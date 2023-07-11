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
            
            ViewBag.alumno = AlumnoService.ConsultarAlumno(user);
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
                int pidmResult = StudentService.GetPidm(alumno.matricula);
                User user = StudentService.FillUser(pidmResult);

                alumno.nombre = user.FullName;
                alumno.programa = user.Program;

                AlumnoService.guardarAlumno(alumno);
                
                alumno = AlumnoService.ConsultarAlumno(alumno.matricula);
            }
           
            message = AlumnoService.guardarAlumnoRelacion(alumno);
           
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}