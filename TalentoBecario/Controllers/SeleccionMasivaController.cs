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
    public class SeleccionMasivaController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.NameUser = Convert.ToString(Session["nombreUser"]);
            ViewBag.listFormadores = new List<Formador>();
            ViewBag.listAlumnos = SeleccionService.AlumnosFiltro();
            return View();
        }
        [HttpPost]
        public JsonResult SaveAlumno(Alumno alumno)
        {
            string message;
            alumno.formador.Id = StudentService.GetPidm(alumno.formador.Id);
            AlumnoService.ActualizarFormador(alumno.formador.Id, 0);
            foreach (Alumno item in alumno.listAlumnos){
                Alumno itemAlumno = item;
               
                itemAlumno = AlumnoService.ConsultarAlumno(item.pidm);
                if (itemAlumno.pidm == null)

                {
                    item.formador.Id = alumno.formador.Id;
                      message = AlumnoService.guardarAlumno(item);
                }
                else
                {
                    itemAlumno.formador.Id = alumno.formador.Id;
                    message = AlumnoService.ActualizarAlumno(itemAlumno);
                }
            }
           

            return Json("Registros actualizados", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ConsultarAlumnosFormador(string id)
        {
            List<Alumno> formadores = new List<Alumno>();
            string pidmResult = StudentService.GetPidm(id);

            Formador formadorBuscar = FormadorService.ConsultarFormador(pidmResult);
            Dictionary<string, dynamic> _formador = FormadorService.ConsultarUsuario(id);

            if (_formador.Count() <= 0 || _formador["Estatus"] != "A")
            {
                formadorBuscar.Id = "";
                formadorBuscar.Nombre = "No existe el registro";
                return Json(formadorBuscar, JsonRequestBehavior.AllowGet);
            }

            if (formadorBuscar.Id == null)
            {
                formadorBuscar = FormadorService.ConsultarFormadorExternos(pidmResult);
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
                formadores = AlumnoService.ConsultarAlumnosPorFormador(pidmResult, formadorBuscar);
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


            return Json(AlumnoService.ConsultarAlumnosPorFormador(id,null), JsonRequestBehavior.AllowGet);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}