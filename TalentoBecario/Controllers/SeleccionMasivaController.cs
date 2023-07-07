using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;

namespace TalentoBecario.Controllers
{
    public class SeleccionMasivaController : Controller
    {
        public ActionResult Index()
        {
            var listFormadores = FormadorService.ObtieneListaFormadoresExternos();

            List<Formador> _formadores = new List<Formador>();

            foreach(var user in listFormadores)
            {
                var _formador = FormadorService.ConsultarUsuario(user.Id);

                if(_formador.Count() > 0 && _formador["Estatus"] == "A")
                {
                    user.Email = _formador["Email"];
                    user.Departamento = _formador["Departamento"];

                    _formadores.Add(user);
                }
            }

            ViewBag.listFormadores = _formadores;
            ViewBag.listAlumnos = SeleccionService.AlumnosFiltro();
            return View();
        }
        [HttpPost]
        public JsonResult SaveAlumno(Alumno alumno)
        {
            string message;
            AlumnoService.ActualizarFormador(alumno.formador.Id, "000000");
            foreach (Alumno item in alumno.listAlumnos){
                Alumno itemAlumno = item;
                itemAlumno = AlumnoService.ConsultarAlumno(item.matricula);
                if (itemAlumno.matricula == null)

                {
                    message = AlumnoService.guardarAlumno(item);
                }
                else
                {
                    itemAlumno.formador.Id = item.formador.Id;
                    message = AlumnoService.ActualizarAlumno(itemAlumno);
                }
            }
           

            return Json("Registros actualizados", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ConsultarAlumnosFormador(string id)
        {
            List<Alumno> formadores = new List<Alumno>();
            Formador formadorBuscar = FormadorService.ConsultarFormador(id);
            if (formadorBuscar.Id == null)
            {
                formadorBuscar = FormadorService.ConsultarFormadorExternos(id);
                if (formadorBuscar.Nombre != null)
                {
                    FormadorService.guardarFormador(formadorBuscar);
                }
                else
                {
                    formadorBuscar.Id = "";
                    formadorBuscar.Nombre = "No existe el registro";
                }
               
                return Json(formadorBuscar, JsonRequestBehavior.AllowGet);
            }
            else
            {
                formadores = AlumnoService.ConsultarAlumnosPorFormador(id);
                if (formadores.Count == 0)
                {
                    
                    return Json(formadorBuscar, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(formadores, JsonRequestBehavior.AllowGet);

                }
            }
           
        }
        public JsonResult ConsultarFormador(string id)
        {


            return Json(AlumnoService.ConsultarAlumnosPorFormador(id), JsonRequestBehavior.AllowGet);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}