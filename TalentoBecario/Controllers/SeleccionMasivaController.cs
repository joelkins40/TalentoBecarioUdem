using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;

namespace TalentoBecario.Controllers
{
    public class SeleccionMasivaController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.listFormadores = FormadorService.ObtieneListFormadores();
            ViewBag.listAlumnos = SeleccionService.ObtieneListAlumnos();
            return View();
        }

        public JsonResult SaveAlumno(Alumno alumno)
        {
            string message;

            foreach (Alumno item in alumno.listAlumnos){
                Alumno itemAlumno = item;
                itemAlumno = AlumnoService.ConsultarAlumno(item.matricula);
                if (itemAlumno.matricula == null)

                {
                    message = AlumnoService.guardarAlumno(item);
                }
                else
                {
                    message = AlumnoService.ActualizarAlumno(itemAlumno);
                }
            }
           

            return Json("Registros actualizados", JsonRequestBehavior.AllowGet);
        }





        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}