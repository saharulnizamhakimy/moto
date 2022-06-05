using MengajiOne2One.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MengajiOne2One.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        motodbEntities db = new motodbEntities();

        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index(User_Record usermodel,string ReturnUrl)
        {
                using(motodbEntities db = new motodbEntities())
                {
                    var obj = db.User_Records.Where(a => a.u_id == usermodel.u_id && a.u_pwd == usermodel.u_pwd).FirstOrDefault();
                   

                if (obj != null)
                    {
                    FormsAuthentication.SetAuthCookie(usermodel.u_id, false);
                    Session["UserID"] = obj.u_id.ToString();
                        Session["Username"] = obj.u_name.ToString();
                    if(ReturnUrl != null)
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    }
                    else
                    {
                        ModelState.AddModelError("", "ID Pengguna atau Kata Laluan tidak sah");
                    }

                }

            
            return View();
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["UserID"] = null;
            Session["Username"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}