using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PaperSchoolTrainers.Models;
using PagedList;

namespace PaperSchoolTrainers.Controllers
{


    public class TrainersController : Controller
    {
        private TrainersDb db = new TrainersDb();

        // GET: Trainers
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            // SORTING
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "fname_desc" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "lname_desc" : "";
            ViewBag.CurrentSort = sortOrder;

            var trainers = from t in db.Trainers
                         select t;

            // PAGE NUMBERS
            if (searchString != null)
            {
                page = 1;
            }  
            else
            {
                searchString = currentFilter;
            }   
            ViewBag.CurrentFilter = searchString;


            // FILTER
            if (!String.IsNullOrEmpty(searchString))
            {
                trainers = trainers.Where(tr => (tr.FirstName.Contains(searchString)) || (tr.LastName.Contains(searchString)) || (tr.Subject.Contains(searchString)));
            }

            // SORTING
            switch (sortOrder)
            {
                case "fname_desc":
                    trainers = trainers.OrderByDescending(s => s.FirstName);
                    break;
                case "lname_desc":
                    trainers = trainers.OrderByDescending(s => s.LastName);
                    break;
                default:
                    trainers = trainers.OrderBy(s => s.LastName);
                    break;
            }

            // Number of Pages
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(trainers.ToPagedList(pageNumber, pageSize));
        }

        // GET: Trainers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // GET: Trainers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trainers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Subject,Salary")] Trainer trainer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Trainers.Add(trainer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(trainer);
        }

        // GET: Trainers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Trainers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var trainerToUpdate = db.Trainers.Find(id);
            if (TryUpdateModel(trainerToUpdate, "",
               new string[] { "FirstName,LastName,Subject,Salary" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(trainerToUpdate);
        }

        // GET: Trainers/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Trainers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Trainer trainer = db.Trainers.Find(id);
                db.Trainers.Remove(trainer);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
