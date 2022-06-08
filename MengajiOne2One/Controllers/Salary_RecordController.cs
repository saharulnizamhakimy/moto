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
    [Authorize(Roles ="Admin")]
    public class Salary_RecordController : Controller
    {
        private motodbEntities db = new motodbEntities();

        // GET: Salary_Record
        public ActionResult Index()
        {
            var salary_Records = db.Salary_Records.Include(s => s.User_Record);
            return View(salary_Records.ToList());
        }

        // GET: Salary_Record/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary_Record salary_Record = db.Salary_Records.Find(id);
            if (salary_Record == null)
            {
                return HttpNotFound();
            }
            return View(salary_Record);
        }

        // GET: Salary_Record/Create
        public ActionResult Create()
        {
            var clients = db.User_Records
                .Select(s => new
                {
                    Text = s.u_id + " - " + s.u_name,
                    Value = s.u_id
                })
                .ToList();

            ViewBag.sal_teacherID = new SelectList(clients, "Value", "Text");
          
            return View();
        }

        // POST: Salary_Record/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sal_ID,sal_amount,sal_date,sal_teacherID,sal_month,sal_status")] Salary_Record salary_Record)
        {
            if (ModelState.IsValid)
            {
                db.Salary_Records.Add(salary_Record);
                db.SaveChanges();
                TempData["AlertMessage"] = "Rekod berjaya disimpan.";
                return RedirectToAction("Index");
            }

            var clients = db.User_Records
                .Select(s => new
                {
                    Text = s.u_id + " - " + s.u_name,
                    Value = s.u_id
                })
                .ToList();

            ViewBag.sal_teacherID = new SelectList(clients, "Value", "Text", salary_Record.sal_teacherID);

            return View(salary_Record);
        }

        // GET: Salary_Record/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary_Record salary_Record = db.Salary_Records.Find(id);
            if (salary_Record == null)
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

            ViewBag.sal_teacherID = new SelectList(clients, "Value", "Text", salary_Record.sal_teacherID);
            return View(salary_Record);
        }

        // POST: Salary_Record/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sal_ID,sal_amount,sal_date,sal_teacherID,sal_month,sal_status")] Salary_Record salary_Record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salary_Record).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Rekod berjaya dikemaskini.";
                return RedirectToAction("Index");
            }
            ViewBag.sal_teacherID = new SelectList(db.User_Records, "u_id", "u_name", salary_Record.sal_teacherID);
            return View(salary_Record);
        }

        // GET: Salary_Record/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary_Record salary_Record = db.Salary_Records.Find(id);
            if (salary_Record == null)
            {
                return HttpNotFound();
            }
            return View(salary_Record);
        }

        // POST: Salary_Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salary_Record salary_Record = db.Salary_Records.Find(id);
            db.Salary_Records.Remove(salary_Record);
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
