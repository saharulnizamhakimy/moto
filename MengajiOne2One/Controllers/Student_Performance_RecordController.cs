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
    public class Student_Performance_RecordController : Controller
    {
        private motodbEntities db = new motodbEntities();

        // GET: Student_Performance_Record
        public ActionResult Index(string SearchBulan, string SearchTahun)
        {
            var roww = db.Student_Performance_Records.Include(s => s.Student_Record).ToList();

            foreach(var Sreport in roww)
            {
                var mnt = Sreport.per_month;
                var yr = Sreport.per_year;
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
                var year = Sreport.per_date.Year;
                var Creport = db.Class_Records.Include(s => s.User_Record).Where(s => s.c_studentID == Sreport.per_studentID).Where(c => c.c_date.Month == month).Where(c => c.c_date.Year == year).Where(c => c.c_status == "TELAH DISAHKAN");
                var report = Creport.ToList();
                var package = db.Packages.FirstOrDefault(s => s.pkg_id == Sreport.Student_Record.s_package);
                float duration = 0;
                foreach (var item in report)
                {
                    duration = ((float)(duration + item.c_duration));
                }
                float hour = duration / 60;
                float total = 0;
                if (hour >= package.pkg_minhour)
                {
                    total = (float)((hour) * package.pkg_discount);
                }
                else
                {
                    total = (float)((hour) * package.pkg_rate);
                }
                Student_Performance_Record ss = db.Student_Performance_Records.Find(Sreport.per_ID);
                ss.per_amaunt = total;
                db.Entry(ss).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (!String.IsNullOrEmpty(SearchBulan) && !String.IsNullOrEmpty(SearchTahun))
            {
                int tahun = Int32.Parse(SearchTahun);   
                if (@User.IsInRole("Guru"))
                {
                    var student_Performance_Records = db.Student_Performance_Records.Include(s => s.Student_Record).Where(s => s.Student_Record.s_teacherID == User.Identity.Name).Where(s => s.per_month==SearchBulan).Where(s => s.per_year == tahun);
                    return View(student_Performance_Records.ToList());
                }
                else if (@User.IsInRole("Admin"))
                {
                    var student_Performance_Records = db.Student_Performance_Records.Include(s => s.Student_Record).Where(s => s.per_month == SearchBulan).Where(s => s.per_year == tahun);
                    return View(student_Performance_Records.ToList());
                }
                else
                {
                    var student_Performance_Records = db.Student_Performance_Records.Include(s => s.Student_Record).Where(s => s.per_studentID == @User.Identity.Name).Where(s => s.per_month == SearchBulan).Where(s => s.per_year == tahun);
                    return View(student_Performance_Records.ToList());
                }

            }
            else
            {
                if (@User.IsInRole("Guru"))
                {
                    var student_Performance_Records = db.Student_Performance_Records.Include(s => s.Student_Record).Where(s => s.Student_Record.s_teacherID == User.Identity.Name);
                    return View(student_Performance_Records.ToList());
                }
                else if (@User.IsInRole("Admin"))
                {
                    var student_Performance_Records = db.Student_Performance_Records.Include(s => s.Student_Record);
                    return View(student_Performance_Records.ToList());
                }
                else
                {
                    var student_Performance_Records = db.Student_Performance_Records.Include(s => s.Student_Record).Where(s => s.per_studentID == @User.Identity.Name);
                    return View(student_Performance_Records.ToList());
                }
            }

               
        }

        // GET: Student_Performance_Record/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Performance_Record student_Performance_Record = db.Student_Performance_Records.Find(id);
            if (student_Performance_Record == null)
            {
                return HttpNotFound();
            }
            return View(student_Performance_Record);
        }

        // GET: Student_Performance_Record/Create
        public ActionResult Create()
        {
            var clients = db.Student_Records
               .Select(s => new
               {
                   Text = s.s_id + " - " + s.s_name,
                   Value = s.s_id
               })
               .ToList();

            if (@User.IsInRole("Guru"))
                {
                clients = db.Student_Records.Where(a => a.s_teacherID == User.Identity.Name)
                .Select(s => new
                {
                    Text = s.s_id + " - " + s.s_name,
                    Value = s.s_id
                })
                .ToList();
            }
            else
            {
                clients = db.Student_Records
               .Select(s => new
               {
                   Text = s.s_id + " - " + s.s_name,
                   Value = s.s_id
               })
               .ToList();

            }
           

            ViewBag.per_studentID = new SelectList(clients, "Value", "Text");
            return View();
        }

        // POST: Student_Performance_Record/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "per_ID,per_desc,per_date,per_studentID,per_month,per_year,per_amaunt,per_status")] Student_Performance_Record student_Performance_Record)
        {
            if (ModelState.IsValid)
            {
                db.Student_Performance_Records.Add(student_Performance_Record);
                db.SaveChanges();
                TempData["AlertMessage"] = "Rekod berjaya disimpan.";
                return RedirectToAction("Index");
            }
            var clients = db.Student_Records
              .Select(s => new
              {
                  Text = s.s_id + " - " + s.s_name,
                  Value = s.s_id
              })
              .ToList();

            if (@User.IsInRole("Guru"))
                {
                clients = db.Student_Records.Where(a => a.s_teacherID == User.Identity.Name)
                               .Select(s => new
                               {
                                   Text = s.s_id + " - " + s.s_name,
                                   Value = s.s_id
                               })
                               .ToList();
            }
            else
            {
                clients = db.Student_Records
               .Select(s => new
               {
                   Text = s.s_id + " - " + s.s_name,
                   Value = s.s_id
               })
               .ToList();

            }

            ViewBag.per_studentID = new SelectList(clients, "Value", "Text", student_Performance_Record.per_studentID);
            return View(student_Performance_Record);
        }

        // GET: Student_Performance_Record/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Performance_Record student_Performance_Record = db.Student_Performance_Records.Find(id);
            if (student_Performance_Record == null)
            {
                return HttpNotFound();
            }
            var clients = db.Student_Records
              .Select(s => new
              {
                  Text = s.s_id + " - " + s.s_name,
                  Value = s.s_id
              })
              .ToList();


            if (@User.IsInRole("Guru"))
                {
                clients = db.Student_Records.Where(a => a.s_teacherID == User.Identity.Name)
                               .Select(s => new
                               {
                                   Text = s.s_id + " - " + s.s_name,
                                   Value = s.s_id
                               })
                               .ToList();
            }
            else
            {
                clients = db.Student_Records
               .Select(s => new
               {
                   Text = s.s_id + " - " + s.s_name,
                   Value = s.s_id
               })
               .ToList();

            }

            ViewBag.per_studentID = new SelectList(clients, "Value", "Text", student_Performance_Record.per_studentID);
            return View(student_Performance_Record);
        }

        // POST: Student_Performance_Record/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "per_ID,per_desc,per_date,per_studentID,per_month,per_year,per_amaunt,per_status")] Student_Performance_Record student_Performance_Record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_Performance_Record).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Rekod berjaya dikemaskini.";
                return RedirectToAction("Index");
            }
            var clients = db.Student_Records
              .Select(s => new
              {
                  Text = s.s_id + " - " + s.s_name,
                  Value = s.s_id
              })
              .ToList();


            if (@User.IsInRole("Guru"))
                {
               clients = db.Student_Records.Where(a => a.s_teacherID == User.Identity.Name)
                               .Select(s => new
                               {
                                   Text = s.s_id + " - " + s.s_name,
                                   Value = s.s_id
                               })
                               .ToList();
            }
            else
            {
                clients = db.Student_Records
               .Select(s => new
               {
                   Text = s.s_id + " - " + s.s_name,
                   Value = s.s_id
               })
               .ToList();

            }

            ViewBag.per_studentID = new SelectList(clients, "Value", "Text", student_Performance_Record.per_studentID);
            return View(student_Performance_Record);
        }

        // GET: Student_Performance_Record/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Performance_Record student_Performance_Record = db.Student_Performance_Records.Find(id);
            if (student_Performance_Record == null)
            {
                return HttpNotFound();
            }
            return View(student_Performance_Record);
        }

        // POST: Student_Performance_Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student_Performance_Record student_Performance_Record = db.Student_Performance_Records.Find(id);
            db.Student_Performance_Records.Remove(student_Performance_Record);
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
        public ActionResult ViewReport(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var Sreport = db.Student_Performance_Records.Include(s => s.Student_Record).FirstOrDefault(x => x.per_ID == id);
            var mnt = Sreport.per_month;
            var yr = Sreport.per_year;
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
            var year = Sreport.per_date.Year;
            var Creport = db.Class_Records.Include(s => s.User_Record).Where(s => s.c_studentID == Sreport.per_studentID).Where(c=>c.c_date.Month == month).Where(c => c.c_date.Year == year).Where(c => c.c_status == "TELAH DISAHKAN");
            var report = Creport.ToList();
            var package = db.Packages.FirstOrDefault(s=>s.pkg_id==Sreport.Student_Record.s_package);
            float duration = 0;
            foreach(var item in report)
            {
                duration = ((float)(duration + item.c_duration));
            }
            float hour = duration / 60;
            float total = 0;
            if(hour>=package.pkg_minhour)
            {
                total = (float)((hour) * package.pkg_discount);
            }
            else
            {
                total = (float)((hour) * package.pkg_rate);
            }
            Student_Performance_Record student_Performance_Record = db.Student_Performance_Records.Find(Sreport.per_ID);
            student_Performance_Record.per_amaunt = total;
            db.Entry(student_Performance_Record).State = EntityState.Modified;
            db.SaveChanges();
            Sreport = db.Student_Performance_Records.Include(s => s.Student_Record).FirstOrDefault(x => x.per_ID == id);

            //var stuPer = from p in tb_performance
            // join s in tb_student on p.StudentID equals s.ID
            // where p.ID == id
            // select new stuPer (tb_performancevm = p, tb_studentvm = s);

            return View(Tuple.Create(Sreport, Creport.ToList()));
            }

    }
    }
