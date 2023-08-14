using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;
using static TalentoBecario.Models.Services.FormadorService;

namespace TalentoBecario.Controllers
{
    [Authorize]
    public class FormadoresController : Controller
    {
        public ActionResult Index()
        {
            Formador formador = new Formador();
            ViewBag.NameUser = Convert.ToString(Session["nombreUser"]);


            ViewBag.listFormadores = FormadorService.ObtieneListFormadores();

            
            ViewBag.formador = formador;

            return View();
        }

        [HttpPost]
        public JsonResult SaveFormador(Formador formador)
        {
            string message = "";

            if (formador.Id.Equals(""))
            {
                message= FormadorService.guardarFormador(formador);
            }
            else
            {
                message= FormadorService.ActualizarFormador(formador);
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ConsultarFormador(string id)
        {
            Formador formador= FormadorService.ConsultarFormador(id);

            
            return Json(formador, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarFormador(int id)
        {
            String message = FormadorService.EliminarFormador(id);


            return Json("Registro Eliminado", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}