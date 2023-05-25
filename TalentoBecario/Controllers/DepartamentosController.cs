using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;
using static TalentoBecario.Models.Services.DepartamentoService;

namespace TalentoBecario.Controllers
{
    public class DepartamentosController : Controller
    {
        public ActionResult Index()
        {
            Departamento habilidad = new Departamento();



            ViewBag.listDepartamentos = DepartamentoService.ObtieneListDepartamentos();

            
            ViewBag.habilidad = habilidad;

            return View();
        }

        [HttpPost]
        public JsonResult SaveDepartamento(Departamento habilidad)
        {
            string message = "";

            if (habilidad.Id == 0)
            {
                message= DepartamentoService.guardarDepartamento(habilidad);
            }
            else
            {
                message= DepartamentoService.ActualizarDepartamento(habilidad);
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ConsultarDepartamento(int id)
        {
            Departamento habilidad= DepartamentoService.ConsultarDepartamento(id);

            
            return Json(habilidad, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarDepartamento(int id)
        {
            String message = DepartamentoService.EliminarDepartamento(id);


            return Json("Registro Eliminado", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}