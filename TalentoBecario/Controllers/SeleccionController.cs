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
    public class SeleccionController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.NameUser = Convert.ToString(Session["nombreUser"]);
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
                AlumnoService.guardarAlumno(itemAlumno);
                message = "A";
            }
            else
            {
                
                if (itemAlumno.formador.Id == "000000")
                {
                    itemAlumno.formador.Id = idFormador;
                    AlumnoService.ActualizarAlumno(itemAlumno);
                    message = "A";

                }
                else if(itemAlumno.formador.Id == idFormador)
                {
                    message = "El alumno ya pertenece a su grupo de alumnos asignados.";
                }
                else
                {
                    message = "El alumno ya pertenece a un formador.";
                }
               
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