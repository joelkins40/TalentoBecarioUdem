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
    public class AsignacionController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.NameUser = Convert.ToString(Session["nombreUser"]);
            string user = Convert.ToString(Session["matricula"]);
            ViewBag.listSolicitudes = SolicitudesService.consultarSolicitudesFormador(user).Where(o => o.estatus == "En Revisión"); ;
            return View();
        }

   

      
     

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}