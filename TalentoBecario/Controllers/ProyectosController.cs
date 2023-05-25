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

        public ActionResult Agregar()
        {
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
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}