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
    public class ProfileController : Controller
    {
        private motodbEntities db = new motodbEntities();

        // GET: Profile
        public ActionResult Index()
        {
            var user_Records = db.User_Records.Include(u => u.User_Type);
            return View(user_Records.ToList());
        }

        // GET: Profile/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Record user_Record = db.User_Records.Find(id);
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
            User_Record user_Record = db.User_Records.Find(id);
            if (user_Record == null)
            {
                return HttpNotFound();
            }
            ViewBag.u_type = new SelectList(db.User_Types, "t_ID", "t_desc", user_Record.u_type);
            return View(user_Record);
        }

        // POST: Profile/Edit/5
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
                return RedirectToAction("Details", new { id = Session["UserID"] });
            }
            ViewBag.u_type = new SelectList(db.User_Types, "t_ID", "t_desc", user_Record.u_type);
            return View(user_Record);
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
