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
    public class Student_RecordController : Controller
    {
        private motodbEntities db = new motodbEntities();

        // GET: Student_Record
        public ActionResult Index()
        {
            var student_Records = db.Student_Records.Include(s => s.User_Record);
            return View(student_Records.ToList());
        }

        // GET: Student_Record/Details/5
        
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Record student_Record = db.Student_Records.Find(id);
            if (student_Record == null)
            {
                return HttpNotFound();
            }
            return View(student_Record);
        }

        // GET: Student_Record/Create
        public ActionResult Create()
        {
            var clients = db.User_Records.Where(a => a.u_type == 2)
                .Select(s => new
                {
                    Text = s.u_id + " - " + s.u_name,
                    Value = s.u_id
                })
                .ToList();

            ViewBag.s_teacherID = new SelectList(clients, "Value", "Text");
            ViewBag.s_package = new SelectList(db.Packages, "pkg_id", "pkg_id");
            return View();
        }

        // POST: Student_Record/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "s_id,s_pwd,s_name,s_age,s_address,s_contactNo,s_regDate,s_teacherID,s_package")] Student_Record student_Record)
        {
            if (ModelState.IsValid)
            {
                db.Student_Records.Add(student_Record);
                db.SaveChanges();
                TempData["AlertMessage"] = "Rekod berjaya disimpan.";
                return RedirectToAction("Index");
            }
            var clients = db.User_Records.Where(a => a.u_type == 2)
                .Select(s => new
                {
                    Text = s.u_id + " - " + s.u_name,
                    Value = s.u_id
                })
                .ToList();

            ViewBag.s_teacherID = new SelectList(clients, "Value", "Text", student_Record.s_teacherID);
            ViewBag.s_package = new SelectList(db.Packages, "pkg_id", "pkg_id");
            return View(student_Record);
        }

        // GET: Student_Record/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Record student_Record = db.Student_Records.Find(id);
            if (student_Record == null)
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

            ViewBag.s_teacherID = new SelectList(clients, "Value", "Text", student_Record.s_teacherID);
            ViewBag.s_package = new SelectList(db.Packages, "pkg_id", "pkg_id", student_Record.s_package);
            return View(student_Record);
        }

        // POST: Student_Record/Edit/5
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
                TempData["AlertMessage"] = "Rekod berjaya dikemaskini.";
                return RedirectToAction("Index");
            }
            var clients = db.User_Records.Where(a => a.u_type == 2)
                .Select(s => new
                {
                    Text = s.u_id + " - " + s.u_name,
                    Value = s.u_id
                })
                .ToList();

            ViewBag.s_teacherID = new SelectList(clients, "Value", "Text", student_Record.s_teacherID);
            ViewBag.s_package = new SelectList(db.Packages, "pkg_id", "pkg_id", student_Record.s_package);
            return View(student_Record);
        }

        // GET: Student_Record/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Record student_Record = db.Student_Records.Find(id);
            if (student_Record == null)
            {
                return HttpNotFound();
            }
            return View(student_Record);
        }

        // POST: Student_Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student_Record student_Record = db.Student_Records.Find(id);
            db.Student_Records.Remove(student_Record);
            db.SaveChanges();
            TempData["AlertMessage"] = "Rekod berjaya dipadam.";
            return RedirectToAction("Index");
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
