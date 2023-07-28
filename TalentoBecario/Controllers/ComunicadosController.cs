using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;

namespace TalentoBecario.Controllers
{
    public class ComunicadosController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.NameUser = Convert.ToString(Session["nombreUser"]);

            ViewBag.listComunicados = ComunicadosService.ObtieneListComunicadoes();

            
            

            return View();
        }

        [HttpPost]
        public JsonResult SaveComunicado(Comunicado comunicado)
        {
            string message = "";

            if (comunicado.id == 0)
            {
                message= ComunicadosService.guardarComunicado(comunicado);
            }
            else
            {
                message= ComunicadosService.ActualizarComunicado(comunicado);
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ConsultarComunicado(int id,int tipo)
        {
            Comunicado comunicado= ComunicadosService.ConsultarComunicado(id,tipo)[0];

            
            return Json(comunicado, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarComunicado(int id)
        {
            String message = ComunicadosService.EliminarComunicado(id);


            return Json("Registro Eliminado", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}