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
    public class User_RecordController : Controller
    {
        private motodbEntities db = new motodbEntities();

        // GET: User_Record
        public ActionResult Index()
        {
            var user_Records = db.User_Record.Include(u => u.User_Type);
            return View(user_Records.ToList());
        }

        // GET: User_Record/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Record user_Record = db.User_Record.Find(id);
            if (user_Record == null)
            {
                return HttpNotFound();
            }
            return View(user_Record);
        }

        // GET: User_Record/Create
        public ActionResult Create()
        {
            ViewBag.u_type = new SelectList(db.User_Types, "t_ID", "t_desc");
            return View();
        }

        // POST: User_Record/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "u_id,u_pwd,u_type,u_name,u_contactNo,u_email")] User_Record user_Record)
        {
            if (ModelState.IsValid)
            {
                db.User_Record.Add(user_Record);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.u_type = new SelectList(db.User_Types, "t_ID", "t_desc", user_Record.u_type);
            return View(user_Record);
        }

        // GET: User_Record/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Record user_Record = db.User_Record.Find(id);
            if (user_Record == null)
            {
                return HttpNotFound();
            }
            ViewBag.u_type = new SelectList(db.User_Types, "t_ID", "t_desc", user_Record.u_type);
            return View(user_Record);
        }

        // POST: User_Record/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "u_id,u_pwd,u_type,u_name,u_contactNo,u_email")] User_Record user_Record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_Record).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.u_type = new SelectList(db.User_Types, "t_ID", "t_desc", user_Record.u_type);
            return View(user_Record);
        }

        // GET: User_Record/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Record user_Record = db.User_Record.Find(id);
            if (user_Record == null)
            {
                return HttpNotFound();
            }
            return View(user_Record);
        }

        // POST: User_Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User_Record user_Record = db.User_Record.Find(id);
            db.User_Record.Remove(user_Record);
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
