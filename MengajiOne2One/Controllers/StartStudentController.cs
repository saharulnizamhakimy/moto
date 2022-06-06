using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MengajiOne2One.Controllers
{
    [Authorize]
    public class StartStudentController : Controller
    {
        // GET: StartPage
        public ActionResult Index()
        {
            return View();
        }
    }
}