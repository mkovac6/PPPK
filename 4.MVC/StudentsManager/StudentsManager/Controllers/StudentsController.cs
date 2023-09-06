using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace StudentsManager.Controllers
{
    public class StudentsController : Controller
    {
        ~StudentsController()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }
        private readonly ModelContainer db = new ModelContainer();
        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students);
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Student student = db.Students
                .Include(a => a.UploadedFiles)
                .SingleOrDefault(s => s.IDStudent == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Residence, University, Email")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Student student = db.Students
                .Include(a => a.UploadedFiles)
                .SingleOrDefault(s => s.IDStudent == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditConfirmed(int id)
        {
            Student studentUpdate = db.Students.Find(id);
            if (TryUpdateModel(studentUpdate, "", new string[] {"Residence", "University", "Email"}))
            {
                db.Entry(studentUpdate).State = EntityState.Modified;
                db.SaveChanges(); return RedirectToAction("Index");
            }
            return View(studentUpdate);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Student student = db.Students
                .Include(a => a.UploadedFiles)
                .SingleOrDefault(s => s.IDStudent == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Students.Remove(db.Students.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
