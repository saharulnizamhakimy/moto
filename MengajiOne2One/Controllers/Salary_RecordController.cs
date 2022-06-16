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
    [Authorize(Roles = "Admin")]
    public class Salary_RecordController : Controller
    {
        private motodbEntities db = new motodbEntities();

        // GET: Salary_Record
        public ActionResult Index(string SearchBulan, string SearchTahun)
        {
            var roww = db.Salary_Records.Include(p => p.User_Record).ToList();
            foreach (var Sreport in roww)
            {
                double total = 0;
                var rate = (from s in db.Salary_Rate where s.sr_id == 1 select s.sr_val).ToArray();
                var id = Sreport.sal_month;
                if (id == "Januari")
                {
                    id = "January";
                }
                else if (id == "Februari")
                {
                    id = "February";
                }
                else if (id == "Mac")
                {
                    id = "March";
                }
                else if (id == "Mei")
                {
                    id = "May";
                }
                else if (id == "Jun")
                {
                    id = "June";
                }
                else if (id == "Julai")
                {
                    id = "July";
                }
                else if (id == "Ogos")
                {
                    id = "August";
                }
                else if (id == "Oktober")
                {
                    id = "October";
                }
                else if (id == "Disember")
                {
                    id = "December";
                }
                int month = DateTime.ParseExact(id, "MMMM", CultureInfo.CurrentCulture).Month;
                int year = Int32.Parse(Sreport.sal_year);
                var hours = (from s in db.Class_Records where s.c_teacherID == Sreport.sal_teacherID where s.c_date.Month == month where s.c_date.Year == year where s.c_status == "TELAH DISAHKAN" select s.c_duration).ToArray();
                for (int i = 0; i < hours.Length; i++)
                {

                    total = (double)(total + hours[i]);
                }
                total = total / 60;
                var salary = total * rate[0];
                Salary_Record ss = db.Salary_Records.Find(Sreport.sal_ID);
                ss.sal_amount = salary;
                db.Entry(ss).State = EntityState.Modified;
                db.SaveChanges();
            }

            if (!String.IsNullOrEmpty(SearchBulan) && !String.IsNullOrEmpty(SearchTahun))
            {
                var salary_Records = db.Salary_Records.Include(s => s.User_Record).Where(s => s.sal_month == SearchBulan).Where(s => s.sal_year == SearchTahun);
                return View(salary_Records.ToList());

            }
            else
            {
                var salary_Records = db.Salary_Records.Include(s => s.User_Record);
                return View(salary_Records.ToList());
            }
               
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
            var clients = db.User_Records.Where(s => s.u_type == 2)
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
        public ActionResult Create([Bind(Include = "sal_ID,sal_amount,sal_date,sal_teacherID,sal_month,sal_year,sal_status")] Salary_Record salary_Record)
        {
            if (ModelState.IsValid)
            {
                //int month = DateTime.ParseExact(salary_Record.sal_month, "MMMM", CultureInfo.CurrentCulture).Month;
                //var gaji = db.Class_Records.Include(c => c.Student_Record).Include(c => c.User_Record).Where(c => c.c_date.Month == month).Sum(c => c.c_duration);
                //var rate = (from s in db.Salary_Rate where s.sr_id == 1 select s.sr_val).ToArray();
                //double timee = 60;
                //var salary = gaji * (rate[0]/timee);

                //salary_Record.sal_amount = salary;
                db.Salary_Records.Add(salary_Record);
                db.SaveChanges();
                TempData["AlertMessage"] = "Rekod berjaya disimpan.";
                return RedirectToAction("Index");
            }

            var clients = db.User_Records.Where(s => s.u_type == 2)
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

            var clients = db.User_Records.Where(s => s.u_type == 2)
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
        public ActionResult Edit([Bind(Include = "sal_ID,sal_amount,sal_date,sal_teacherID,sal_month,sal_year,sal_status")] Salary_Record salary_Record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salary_Record).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Rekod berjaya dikemaskini.";
                return RedirectToAction("Index");
            }
            var clients = db.User_Records.Where(s => s.u_type == 2)
               .Select(s => new
               {
                   Text = s.u_id + " - " + s.u_name,
                   Value = s.u_id
               })
               .ToList();

            ViewBag.sal_teacherID = new SelectList(clients, "Value", "Text", salary_Record.sal_teacherID);
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

        [HttpPost]
        public ActionResult GetSalary(string id, string u, string y)
        {
            try
            {
                double total = 0;
                var rate = (from s in db.Salary_Rate where s.sr_id == 1 select s.sr_val).ToArray();
                if(id=="Januari")
                {
                    id = "January";
                }
                else if (id == "Februari")
                {
                    id = "February";
                }
                else if (id == "Mac")
                {
                    id = "March";
                }
                else if (id == "Mei")
                {
                    id = "May";
                }
                else if (id == "Jun")
                {
                    id = "June";
                }
                else if (id == "Julai")
                {
                    id = "July";
                }
                else if (id == "Ogos")
                {
                    id = "August";
                }
                else if (id == "Oktober")
                {
                    id = "October";
                }
                else if (id == "Disember")
                {
                    id = "December";
                }
                int month = DateTime.ParseExact(id, "MMMM", CultureInfo.CurrentCulture).Month;
                int year = Int32.Parse(y);
                var hours = (from s in db.Class_Records where s.c_teacherID == u where s.c_date.Month == month where s.c_date.Year == year where s.c_status == "TELAH DISAHKAN" select s.c_duration).ToArray();
                for (int i = 0; i < hours.Length; i++)
                {

                    total = (double)(total + hours[i]);
                }
                total = total / 60;
                var salary = total * rate[0];
                return Json(salary, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }

        }

        public ActionResult ViewInvoice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var Sreport = db.Salary_Records.Include(p => p.User_Record).FirstOrDefault(x => x.sal_ID == id);
            var mnt = Sreport.sal_month;
            if (mnt == "Januari")
            {
                mnt = "January";
            }
            else if (mnt == "Februari")
            {
                mnt = "February";
            }
            else if (mnt == "Mac")
            {
                mnt = "March";
            }
            else if (mnt == "Mei")
            {
                mnt = "May";
            }
            else if (mnt == "Jun")
            {
                mnt = "June";
            }
            else if (mnt == "Julai")
            {
                mnt = "July";
            }
            else if (mnt == "Ogos")
            {
                mnt = "August";
            }
            else if (mnt == "Oktober")
            {
                mnt = "October";
            }
            else if (mnt == "Disember")
            {
                mnt = "December";
            }
            int month = DateTime.ParseExact(mnt, "MMMM", CultureInfo.CurrentCulture).Month;
            int year = Int32.Parse(Sreport.sal_year);
            var Creport = db.Class_Records.Include(s => s.User_Record).Where(s => s.c_teacherID == Sreport.sal_teacherID).Where(c => c.c_date.Month == month).Where(c => c.c_date.Year == year).Where(c => c.c_status == "TELAH DISAHKAN");
            //var stuPer = from p in tb_performance
            // join s in tb_student on p.StudentID equals s.ID
            // where p.ID == id
            // select new stuPer (tb_performancevm = p, tb_studentvm = s);

            return View(Tuple.Create(Sreport, Creport.ToList()));
        }
    }
}
