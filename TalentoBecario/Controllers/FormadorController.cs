using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;

namespace TalentoBecario.Controllers
{
    public class FormadorController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.NameUser = Convert.ToString(Session["nombreUser"]);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public User SearchByPidm(int pidm)
        {
            var user = StudentService.FillUser(pidm);
            user.IsEmployee = StudentService.UserIsEmployee(pidm).Result.Trim();

            return user;
        }
    }
}