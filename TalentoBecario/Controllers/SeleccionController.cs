using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;

namespace TalentoBecario.Controllers
{
    public class SeleccionController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.listAlumnos = SeleccionService.AlumnosFiltro();
            return View();
        }




        [HttpPost]
        public JsonResult AsignarAlumnoFormador(string matricula)
        {
           string idFormador= Convert.ToString(Session["matricula"]);
            String message="";
           Alumno itemAlumno = AlumnoService.ConsultarAlumno(matricula);
            if (itemAlumno.matricula == null)

            {
                itemAlumno = SeleccionService.ObtieneDatosAlumno(matricula);
                itemAlumno.formador.Id = idFormador;
                itemAlumno.horario = "NA";
                itemAlumno.estatus = "A";
                message = AlumnoService.guardarAlumno(itemAlumno);
            }
            else
            {
                itemAlumno.formador.Id = idFormador;
                message = AlumnoService.ActualizarAlumno(itemAlumno);
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }

            public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}