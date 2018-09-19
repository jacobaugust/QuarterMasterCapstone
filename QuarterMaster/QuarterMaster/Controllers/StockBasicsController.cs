using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuarterMaster.Models;

namespace QuarterMaster.Controllers
{
    public class StockBasicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StockBasics
        public ActionResult Index()
        {
            return View(db.stockBasics.ToList());
        }

        // GET: StockBasics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockBasic stockBasic = db.stockBasics.Find(id);
            if (stockBasic == null)
            {
                return HttpNotFound();
            }
            return View(stockBasic);
        }

        // GET: StockBasics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockBasics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ticker,name,stock_exchange,sector,company_url")] StockBasic stockBasic)
        {
            if (ModelState.IsValid)
            {
                db.stockBasics.Add(stockBasic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stockBasic);
        }

        // GET: StockBasics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockBasic stockBasic = db.stockBasics.Find(id);
            if (stockBasic == null)
            {
                return HttpNotFound();
            }
            return View(stockBasic);
        }

        // POST: StockBasics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ticker,name,stock_exchange,sector,company_url")] StockBasic stockBasic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockBasic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stockBasic);
        }

        // GET: StockBasics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockBasic stockBasic = db.stockBasics.Find(id);
            if (stockBasic == null)
            {
                return HttpNotFound();
            }
            return View(stockBasic);
        }

        // POST: StockBasics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockBasic stockBasic = db.stockBasics.Find(id);
            db.stockBasics.Remove(stockBasic);
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
