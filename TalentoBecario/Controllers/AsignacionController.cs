using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;

namespace TalentoBecario.Controllers
{
    public class AsignacionController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

   

      
     

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}