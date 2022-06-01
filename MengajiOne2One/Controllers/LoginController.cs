using MengajiOne2One.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MengajiOne2One.Controllers
{
    public class LoginController : Controller
    {
        motodbEntities db = new motodbEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User_Record usermodel)
        {
                using(motodbEntities db = new motodbEntities())
                {
                    var obj = db.User_Record.Where(a => a.u_id == usermodel.u_id && a.u_pwd == usermodel.u_pwd).FirstOrDefault();

                    if (obj != null)
                    {
                        
                        Session["UserID"] = obj.u_id.ToString();
                        Session["Username"] = obj.u_name.ToString();
                        Session["Usertype"] = obj.u_type.ToString();
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        ModelState.AddModelError("", "ID Pengguna atau Kata Laluan tidak sah");
                    }

                }

            
            return View();
        }

        public ActionResult Logout()
        { 
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}