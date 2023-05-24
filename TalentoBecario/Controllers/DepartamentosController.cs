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



            ViewBag.listDepartamentos = DepartamentosService.ObtieneListDepartamentos();

            
            ViewBag.habilidad = habilidad;

            return View();
        }

        [HttpPost]
        public JsonResult SaveDepartamento(Departamento habilidad)
        {
            string message = "";

            if (habilidad.Id == 0)
            {
                message= DepartamentosService.guardarDepartamento(habilidad);
            }
            else
            {
                message= DepartamentosService.ActualizarDepartamento(habilidad);
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ConsultarDepartamento(int id)
        {
            Departamento habilidad= DepartamentosService.ConsultarDepartamento(id);

            
            return Json(habilidad, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarDepartamento(int id)
        {
            String message = DepartamentosService.EliminarDepartamento(id);


            return Json("Registro Eliminado", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}