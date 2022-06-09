using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MengajiOne2One.Models;

namespace MengajiOne2One.Controllers
{
    [Authorize]
    public class Class_RecordController : Controller
    {
        private motodbEntities db = new motodbEntities();

        // GET: Class_Record
        public ActionResult Index()
        {
            if (@User.IsInRole("Guru"))
            {
                var class_Records = db.Class_Records.Include(c => c.Student_Record).Include(c => c.User_Record).Where(c => c.c_teacherID == @User.Identity.Name);
                return View(class_Records.ToList());
            }
            else if (@User.IsInRole("Admin"))
            {
                var class_Records = db.Class_Records.Include(c => c.Student_Record).Include(c => c.User_Record);
                return View(class_Records.ToList());
            }
            else
            {
                var class_Records = db.Class_Records.Include(c => c.Student_Record).Include(c => c.User_Record).Where(c => c.c_studentID == @User.Identity.Name);
                return View(class_Records.ToList());
            }
            
        }

        // GET: Class_Record/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class_Record class_Record = db.Class_Records.Find(id);
            if (class_Record == null)
            {
                return HttpNotFound();
            }
            return View(class_Record);
        }

        // GET: Class_Record/Create
        public ActionResult Create()
        {
            var clients = db.Student_Records.Where(a => a.s_teacherID == User.Identity.Name)
               .Select(s => new
               {
                   Text = s.s_id + " - " + s.s_name,
                   Value = s.s_id
               })
               .ToList();

            ViewBag.c_studentID = new SelectList(clients, "Value", "Text");
            ViewBag.c_teacherID = new SelectList(db.User_Records, "u_id", "u_name");
            return View();
        }

        // POST: Class_Record/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "c_id,c_date,c_studentID,c_duration,c_teacherID,c_timeStart,c_timeEnd,c_status")] Class_Record class_Record)
        {
            if (ModelState.IsValid)
            {
                db.Class_Records.Add(class_Record);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var clients = db.Student_Records.Where(a => a.s_teacherID == User.Identity.Name)
               .Select(s => new
               {
                   Text = s.s_id + " - " + s.s_name,
                   Value = s.s_id
               })
               .ToList();

            ViewBag.c_studentID = new SelectList(clients, "Value", "Text", class_Record.c_studentID);
            ViewBag.c_teacherID = new SelectList(db.User_Records, "u_id", "u_pwd", class_Record.c_teacherID);
            return View(class_Record);
        }

        // GET: Class_Record/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class_Record class_Record = db.Class_Records.Find(id);
            if (class_Record == null)
            {
                return HttpNotFound();
            }
            var clients = db.Student_Records.Where(a => a.s_teacherID == User.Identity.Name)
               .Select(s => new
               {
                   Text = s.s_id + " - " + s.s_name,
                   Value = s.s_id
               })
               .ToList();

            ViewBag.c_studentID = new SelectList(clients, "Value", "Text", class_Record.c_studentID);
            ViewBag.c_teacherID = new SelectList(db.User_Records, "u_id", "u_pwd", class_Record.c_teacherID);
            return View(class_Record);
        }

        // POST: Class_Record/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "c_id,c_date,c_studentID,c_duration,c_teacherID,c_timeStart,c_timeEnd,c_status")] Class_Record class_Record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(class_Record).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var clients = db.Student_Records.Where(a => a.s_teacherID == User.Identity.Name)
               .Select(s => new
               {
                   Text = s.s_id + " - " + s.s_name,
                   Value = s.s_id
               })
               .ToList();

            ViewBag.c_studentID = new SelectList(clients, "Value", "Text", class_Record.c_studentID);
            ViewBag.c_teacherID = new SelectList(db.User_Records, "u_id", "u_pwd", class_Record.c_teacherID);
            return View(class_Record);
        }

        public ActionResult EditEndClass(int? id)
        {
            using (motodbEntities db = new motodbEntities())
            {
                Class_Record updateRecord = (from c in db.Class_Records
                                            where c.c_id == id
                                            select c).FirstOrDefault();
                updateRecord.c_timeEnd = @DateTime.Now.ToString("HH:mm:ss");
                var now = @DateTime.Now.ToString("HH:mm:ss");
                var timeEnd = Convert.ToDateTime(now);
                var timeStart= Convert.ToDateTime(updateRecord.c_timeStart);
                var diff = timeEnd - timeStart;
                var formatteddiff = (int)diff.TotalMinutes;

                if (formatteddiff >= 90)
                {
                    formatteddiff = 90;
                }
                else if (formatteddiff >= 60)
                {
                    formatteddiff = 60;
                }
                else if (formatteddiff >= 30)
                {
                    formatteddiff = 30;
                }
                else
                {
                    formatteddiff = 0;
                }
                updateRecord.c_duration = formatteddiff;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult EditVerifyClass(int? id)
        {
            using (motodbEntities db = new motodbEntities())
            {
                Class_Record updateRecord = (from c in db.Class_Records
                                             where c.c_id == id
                                             select c).FirstOrDefault();
                updateRecord.c_status = "TELAH DISAHKAN";
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }



        // GET: Class_Record/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class_Record class_Record = db.Class_Records.Find(id);
            if (class_Record == null)
            {
                return HttpNotFound();
            }
            return View(class_Record);
        }

        // POST: Class_Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Class_Record class_Record = db.Class_Records.Find(id);
            db.Class_Records.Remove(class_Record);
            db.SaveChanges();
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
