using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;

namespace TalentoBecario.Controllers
{
    public class ProyectosController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.listProyectos = ProyectoService.ObtieneListProyectos();

            return View();
        }

        public ActionResult Agregar(int id=0)
        {
            if (id != 0)
            {
                ViewBag.proyecto = ProyectoService.ConsultarProyecto(id);
            }
            else
            {
                ViewBag.proyecto = new Proyecto()
                {
                    id = 0,
                    nombre = "",
                    departamento= new Departamento() { Id='0'},
                   
                    descripcion=""
                };
            }
            ViewBag.listAreaInteres = AreaInteresService.ObtieneListAreaIntereses();
            ViewBag.listHabilidades = HabilidadesService.ObtieneListHabilidades();
            ViewBag.listDepartamentos = DepartamentoService.ObtieneListDepartamentos();
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpPost]
        public JsonResult SaveProyecto(Proyecto proyecto)
        {
            string message = "";

            if (proyecto.id == 0)
            {
                message = ProyectoService.guardarProyecto(proyecto);
            }
            else
            {
                message = ProyectoService.ActualizarProyecto(proyecto);
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ConsultarProyecto(int id)
        {
            Proyecto proyecto = ProyectoService.ConsultarProyecto(id);

            return Json(proyecto, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}