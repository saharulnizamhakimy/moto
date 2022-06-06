using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MengajiOne2One.Models;

namespace MengajiOne2One.Controllers
{
    [Authorize]
    public class StuProfileController : Controller
    {
        private motodbEntities db = new motodbEntities();

        // GET: Profile
        public ActionResult Index()
        {
            var student_Records = db.Student_Records.Include(s => s.User_Record);
            return View(student_Records.ToList());
        }

        // GET: Profile/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Record user_Record = db.Student_Records.Find(id);
            if (user_Record == null)
            {
                return HttpNotFound();
            }
            return View(user_Record);
        }

       

        // GET: Profile/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Record user_Record = db.Student_Records.Find(id);
            if (user_Record == null)
            {
                return HttpNotFound();
            }
            var clients = db.User_Records
                .Select(s => new
                {
                    Text = s.u_id + " - " + s.u_name,
                    Value = s.u_id
                })
                .ToList();

            ViewBag.s_teacherID = new SelectList(clients, "Value", "Text", user_Record.s_teacherID);
            return View(user_Record);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "s_id,s_pwd,s_name,s_age,s_address,s_contactNo,s_regDate,s_teacherID,s_package")] Student_Record student_Record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_Record).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = Session["UserID"] });
            }
            var clients = db.User_Records
                .Select(s => new
                {
                    Text = s.u_id + " - " + s.u_name,
                    Value = s.u_id
                })
                .ToList();

            ViewBag.s_teacherID = new SelectList(clients, "Value", "Text", student_Record.s_teacherID);
            return View(student_Record);
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
