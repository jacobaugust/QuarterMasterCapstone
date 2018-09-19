using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using QuarterMaster.Models;

namespace QuarterMaster.Controllers
{
    public class CalendarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Calendar
        public ActionResult CalendarIndex()
        {
            var userId = User.Identity.GetUserId();
            try
            {
                var userCheck =
                    (from u in db.Users
                     where u.Id == userId
                     select u).First();
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            var events = db.events.ToList();
            ViewBag.Events = events;



            return View();
        }
        public JsonResult GetEvents()
        {

            {
                var userId = User.Identity.GetUserId();
                
                var userCurrent =
                        (from u in db.Users
                         where u.Id == userId
                         select u).First();

                var watchlistToDisplay =
                    (from x in db.watchLists
                     where x.ApplicationUserId == userCurrent.Id
                     select x).ToList();
                List<int> stockIds = new List<int>();
                foreach (var item in watchlistToDisplay)
                {
                    stockIds.Add(item.StockId);
                }

                List<Stock> myStocks = new List<Stock>();

                myStocks = db.stocks.Where(s => stockIds.Contains(s.Id)).Include(s => s.StockBasic).ToList();

                List<string> watchTickers = new List<string>();
               foreach (var item in myStocks)
                {
                    watchTickers.Add(item.StockBasic.ticker);
                }

                List<Event> events = new List<Event>();
                events = db.events.Where(e => watchTickers.Contains(e.ticker)).ToList();
               
                //var events = db.events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public ActionResult EventList()
        {
            ViewBag.Message = TempData["Message"];
            var Events = db.events.Select(e => e);
            return View(Events);
        }

        // GET: Calendar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event eventFound = db.events.Find(id);
            if (eventFound == null)
            {
                return HttpNotFound();
            }
            var articleToDisplay = db.events.Find(eventFound.Id);
            ViewBag.Date = eventFound.Start;
            ViewBag.Title = eventFound.Subject;
            ViewBag.URL = eventFound.Description;
           
            return View(eventFound);
        }

        // GET: Calendar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calendar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ticker,Subject,Start,Description")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: Calendar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Calendar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ticker,Subject,Start,Description")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Calendar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Calendar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.events.Find(id);
            db.events.Remove(@event);
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
