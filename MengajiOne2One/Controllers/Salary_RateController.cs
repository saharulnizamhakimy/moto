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
    public class Salary_RateController : Controller
    {
        private motodbEntities db = new motodbEntities();

        // GET: Salary_Rate
        public ActionResult Index()
        {
            return View(db.Salary_Rate.ToList());
        }

        // GET: Salary_Rate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary_Rate salary_Rate = db.Salary_Rate.Find(id);
            if (salary_Rate == null)
            {
                return HttpNotFound();
            }
            return View(salary_Rate);
        }

        // GET: Salary_Rate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salary_Rate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sr_id,sr_val")] Salary_Rate salary_Rate)
        {
            if (ModelState.IsValid)
            {
                db.Salary_Rate.Add(salary_Rate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salary_Rate);
        }

        // GET: Salary_Rate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary_Rate salary_Rate = db.Salary_Rate.Find(id);
            if (salary_Rate == null)
            {
                return HttpNotFound();
            }
            return View(salary_Rate);
        }

        // POST: Salary_Rate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sr_id,sr_val")] Salary_Rate salary_Rate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salary_Rate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new {id=1});
            }
            return View(salary_Rate);
        }

        // GET: Salary_Rate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary_Rate salary_Rate = db.Salary_Rate.Find(id);
            if (salary_Rate == null)
            {
                return HttpNotFound();
            }
            return View(salary_Rate);
        }

        // POST: Salary_Rate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salary_Rate salary_Rate = db.Salary_Rate.Find(id);
            db.Salary_Rate.Remove(salary_Rate);
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
