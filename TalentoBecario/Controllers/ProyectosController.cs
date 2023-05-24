using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            ViewBag.listAreaIntereses = AreaInteresService.ObtieneListAreaIntereses();
            ViewBag.listHabilidades = HabilidadesService.ObtieneListHabilidades();

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