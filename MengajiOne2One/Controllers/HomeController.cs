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
                var numKelas = db.Class_Records.Count();
                var numAllUser = numUser + numStudent;
                var elaunDlmProses = db.Salary_Records.Where(a => a.sal_status == "Dalam Proses").Count();
                var elaunSelesai = db.Salary_Records.Where(a => a.sal_status == "Selesai").Count();
                var elaunPaid = db.Salary_Records.Where(a => a.sal_status == "Selesai").Sum(a => a.sal_amount);
                var elaunPending = db.Salary_Records.Where(a => a.sal_status == "Dalam Proses").Sum(a => a.sal_amount);
                var kelasPending = db.Class_Records.Where(a => a.c_status == "BELUM DISAHKAN").Count();
                var kelasVerified = db.Class_Records.Where(a => a.c_status == "TELAH DISAHKAN").Count();
                var studentGuru = db.Student_Records.Where(a => a.s_teacherID == User.Identity.Name.ToString()).Count();

                ViewBag.numUser = numUser;
                ViewBag.numStudent = numStudent;
                ViewBag.numAdmin = numAdmin;
                ViewBag.numGuru = numGuru;
                ViewBag.numKelas = numKelas;
                ViewBag.numAllUser = numAllUser;
                ViewBag.elaunDlmProses = elaunDlmProses;
                ViewBag.elaunSelesai = elaunSelesai;
                ViewBag.elaunPaid = elaunPaid;
                ViewBag.elaunPending = elaunPending;
                ViewBag.kelasPending = kelasPending;
                ViewBag.kelasVerified = kelasVerified;
                ViewBag.studentGuru = studentGuru;

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