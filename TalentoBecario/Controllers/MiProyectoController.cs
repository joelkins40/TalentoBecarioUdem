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
    public class MiProyectoController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.NameUser = Convert.ToString(Session["nombreUser"]);
            string matricula = Convert.ToString(Session["matricula"]);
            ViewBag.mensaje = ComunicadosService.ConsultarComunicado(0, 1);
            Proyecto itemValor = new Proyecto();
            Alumno alumno = AlumnoService.ConsultarAlumno(matricula);
            foreach (Solicitud item in  SolicitudesService.consultarSolicitudesAlumno(alumno.pidm))
            {
                if (item.estatus.Equals("Asignado"))
                {
                    itemValor = item.proyecto;
                }
            }
            ViewBag.proyecto = itemValor;
            if (itemValor.id == 0)

            {
                ViewBag.formador = FormadorService.HomologarFormador(alumno.formador); ;
                
                 }
            else
            {
                ViewBag.formador =   FormadorService.HomologarFormador(itemValor.formador);
            }
           
            return View("index", itemValor);
        }

      

        [HttpPost]
        public JsonResult ConsultarHabilidad(int id)
        {
            Habilidad habilidad= HabilidadesService.ConsultarHabilidad(id);

            
            return Json(habilidad, JsonRequestBehavior.AllowGet);
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