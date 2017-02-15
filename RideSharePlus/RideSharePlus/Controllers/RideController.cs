using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RideSharePlus.Models;

namespace RideSharePlus.Controllers
{
    public class RideController : Controller
    {
        private RideShareDB db = new RideShareDB();

        // GET: Ride
        public ActionResult Index()
        {
            var rides = db.Rides.Include(r => r.Campus);

            ViewBag.CampusId = new SelectList(db.Campus, 
                "CampusId", "Name", 0);

            ViewBag.DayOfWeek = SetupDayOfWeek();

            return View(rides.ToList());
        }

        // GET: Ride/Search?campusid=value&dayofweek=value
        public ActionResult Search(int? campusId, string dayOfWeek)
        {
            var rides = db.Rides.Include(r => r.Campus);

            if (campusId != null)
            {
                rides = rides.Where(r => r.CampusId == campusId);
            }
            if (dayOfWeek != null && dayOfWeek.Length > 0)
            {
                rides = rides.Where(r => r.DayOfWeek.Contains(dayOfWeek));
            }

            ViewBag.CampusId = new SelectList(db.Campus, 
                "CampusId", "Name", campusId);

            ViewBag.DayOfWeek = SetupDayOfWeek();

            return View("Index", rides.ToList());
        }

        public ActionResult CampusSearch(string term)
        {
            var campus = GetCampus(term).Select(c => new { value = c.Name });

            return Json(campus, JsonRequestBehavior.AllowGet);
        }

        private List<Campus> GetCampus(string searchString)
        {
            return db.Campus
                     .Where(c => c.Name.Contains(searchString))
                     .ToList();
        }

        /*
        public ActionResult DaySearch(string term)
        {
            var day = GetDay(term).Select(d => new { value = d. });

            return Json(day, JsonRequestBehavior.AllowGet);
        }

        private List<DayOfWeek> GetDay(string searchString)
        {
            return db.Rides
                     .Where(d => d.DayOfWeek.Contains(searchString))
                     .ToList();
        }
        */

        // GET: Ride/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ride ride = db.Rides.Find(id);
            if (ride == null)
            {
                return HttpNotFound();
            }
            return View(ride);
        }

        public ActionResult SameTownSearch(string startingTown)
        {
            var students = GetStudents(startingTown);

            return PartialView("_SameTownSearch", students);
        }

        private List<Ride> GetStudents(string searchString)
        {
            return db.Rides.Where(s => s.StartingTown.Contains(searchString)).ToList();
        }

        // GET: Ride/Create
        public ActionResult Create()
        {
            ViewBag.CampusId = new SelectList(db.Campus, "CampusId", "Name");
            return View();
        }

        // POST: Ride/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RideId,CampusId,StudentEmail,StartingCrossroads,StartingTown,DayOfWeek,TimeStart,TimeEnd,Requirements")] Ride ride)
        {
            if (ModelState.IsValid)
            {
                db.Rides.Add(ride);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampusId = new SelectList(db.Campus, "CampusId", "Name", ride.CampusId);
            return View(ride);
        }

        // GET: Ride/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ride ride = db.Rides.Find(id);
            if (ride == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusId = new SelectList(db.Campus, "CampusId", "Name", ride.CampusId);
            return View(ride);
        }

        // POST: Ride/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RideId,CampusId,StudentEmail,StartingCrossroads,StartingTown,DayOfWeek,TimeStart,TimeEnd,Requirements")] Ride ride)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ride).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampusId = new SelectList(db.Campus, "CampusId", "Name", ride.CampusId);
            return View(ride);
        }

        // GET: Ride/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ride ride = db.Rides.Find(id);
            if (ride == null)
            {
                return HttpNotFound();
            }
            return View(ride);
        }

        // POST: Ride/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ride ride = db.Rides.Find(id);
            db.Rides.Remove(ride);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private List<SelectListItem> SetupDayOfWeek()
        {
            var weekdays = new List<SelectListItem>();
            weekdays.Add(new SelectListItem { Text = "Sunday", Value = "Sunday" });
            weekdays.Add(new SelectListItem { Text = "Monday", Value = "Monday" });
            weekdays.Add(new SelectListItem { Text = "Tuesday", Value = "Tuesday" });
            weekdays.Add(new SelectListItem { Text = "Wednesday", Value = "Wednesday" });
            weekdays.Add(new SelectListItem { Text = "Thursday", Value = "Thursday" });
            weekdays.Add(new SelectListItem { Text = "Friday", Value = "Friday" });
            weekdays.Add(new SelectListItem { Text = "Saturday", Value = "Saturday" });

            return weekdays;
        }

        //public ActionResult SameStartingTown()
        //{
        //    var album = GetDailyDeal();
        //
        //    return PartialView("_DailyDeal", album);
        //}
        //
        //// Select an album and discount it by 50%
        //private Album GetSameStartingTown()
        //{
        //    var album = storeDB.Albums
        //        .OrderBy(a => System.Guid.NewGuid())
        //        .First();
        //
        //    album.Price *= 0.5m;
        //    return album;
        //}

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
