﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;

namespace TalentoBecario.Controllers
{
    [Authorize]
    public class ProyectosController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.NameUser = "Joel Vargas Osorio";

            string matricula = Convert.ToString(Session["matricula"]);
            ViewBag.NameUser = Convert.ToString(Session["nombreUser"]);
           
            ViewBag.listProyectos = ProyectoService.ObtieneListProyectosPorFormador(matricula);

            return View();
        }

        public ActionResult Agregar(string id="")
        {
            if (id == "")
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
            ViewBag.NameUser = Convert.ToString(Session["nombreUser"]);
            ViewBag.listAreaInteres = AreaInteresService.ObtieneListAreaIntereses();
            ViewBag.listHabilidades = HabilidadesService.ObtieneListHabilidades();
            ViewBag.listDepartamentos = DepartamentoService.ObtieneListDepartamentos();
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpPost]
        public JsonResult SaveProyecto(Proyecto proyecto)
        {
            proyecto.formador.Id= Convert.ToString(Session["matricula"]);
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

        public JsonResult ActualizarProyecto(string id,string estatus)
        {
            Proyecto proyecto = ProyectoService.ConsultarProyecto(id);
            proyecto.estatus = estatus;
            ProyectoService.ActualizarProyecto(proyecto);

            return Json(proyecto, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult ConsultarProyecto(string id)
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