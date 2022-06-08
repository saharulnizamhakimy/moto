using MengajiOne2One.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MengajiOne2One.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (motodbEntities db = new motodbEntities())
            {
                var numUser = db.User_Records.Count();
                var numStudent = db.Student_Records.Count();
                var numAdmin = db.User_Records.Where(a => a.u_type == 1).Count();
                var numGuru = db.User_Records.Where(a => a.u_type == 2).Count();
                ViewBag.numUser = numUser;
                ViewBag.numStudent = numStudent;
                ViewBag.numAdmin = numAdmin;
                ViewBag.numGuru = numGuru;
                return View();
            }
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
    }
}