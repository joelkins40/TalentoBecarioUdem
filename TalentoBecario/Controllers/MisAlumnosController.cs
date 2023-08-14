using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;

namespace TalentoBecario.Controllers
{
    [Authorize]
    public class MisAlumnosController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.NameUser = Convert.ToString(Session["nombreUser"]);
            string matricula = Convert.ToString(Session["matricula"]);
            
            ViewBag.listAlumnos = AlumnoService.ConsultarAlumnosPorFormador(matricula,null);


           
            return View();
        }

       
        [HttpPost]
        public JsonResult ConsultarAlumno(string matricula)
        {
            Alumno alumno= AlumnoService.HomologarAlumno(matricula);

            
            return Json(alumno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarHabilidad(int id)
        {
            String message = HabilidadesService.EliminarHabilidad(id);


            return Json("Registro Eliminado", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}