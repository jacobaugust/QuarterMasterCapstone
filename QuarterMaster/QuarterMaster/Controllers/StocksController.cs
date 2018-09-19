 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using QuarterMaster.Models;

namespace QuarterMaster.Controllers
{
    public class StocksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Stocks
        public ActionResult Index(string sortOrder)
        {
            string id = User.Identity.GetUserId();
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
                var plainStocks = db.stocks.Include(s => s.StockBalanceSheet).Include(s => s.StockBasic).Include(s => s.StockIncomeStatement).Include(s => s.StockMetrics);
                return View(plainStocks);
            }
            var userCurrent =
                   (from u in db.Users
                    where u.Id == id
                    select u).First();
            var stocks = db.stocks.Include(s => s.StockBalanceSheet).Include(s => s.StockBasic).Include(s => s.StockIncomeStatement).Include(s => s.StockMetrics);
            if (id == null)
            {
               
                return View(stocks.ToList());
            }

            
            stocks = stocks.Distinct();
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SectorSortParm = String.IsNullOrEmpty(sortOrder) ? "sector_desc" : "";
            ViewBag.NetIncomeSortParm = String.IsNullOrEmpty(sortOrder) ? "netincome_desc" : "";
            ViewBag.EPSSortParm = String.IsNullOrEmpty(sortOrder) ? "eps_desc" : "";
            ViewBag.RetainedEarningsSortParm = String.IsNullOrEmpty(sortOrder) ? "retainedearnings_desc" : "";
            ViewBag.TotalAssetsSortParm = String.IsNullOrEmpty(sortOrder) ? "totalassets_desc" : "";
            ViewBag.ProfitMarginSortParm = String.IsNullOrEmpty(sortOrder) ? "profitmargin_desc" : "";
            ViewBag.DividendYieldSortParm = String.IsNullOrEmpty(sortOrder) ? "dividendyield_desc" : "";
            
           

            switch (sortOrder)
            {
                case "name_desc":
                    stocks = stocks.OrderByDescending(s => s.StockBasic.name);
                    break;
                case "sector_desc":
                    stocks = stocks.OrderByDescending(s => s.StockBasic.sector);
                    break;
                case "netincome_desc":
                    stocks = stocks.OrderByDescending(s => s.StockIncomeStatement.netincome);
                    break;
                case "eps_desc":
                    stocks = stocks.OrderByDescending(s => s.StockIncomeStatement.basiceps);
                    break;
                case "retainedearnings_desc":
                    stocks = stocks.OrderByDescending(s => s.StockBalanceSheet.retainedearnings);
                    break;
                case "totalassets_desc":
                    stocks = stocks.OrderByDescending(s => s.StockBalanceSheet.totalassets);
                    break;
                case "profitmargin_desc":
                    stocks = stocks.OrderByDescending(s => s.StockMetrics.profitmargin);
                    break;
                case "dividendyield_desc":
                    stocks = stocks.OrderByDescending(s => s.StockMetrics.dividendyield);
                    break;
                default:
                    stocks = stocks.OrderBy(s => s.StockBasic.sector);
                    break;
                    
            }
            
            return View(stocks.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details()
        {
            return RedirectToAction("WatchList");
        }

        // GET: Stocks/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Stock stock = db.stocks.Include(s =>s.StockBasic).Include(s => s.StockIncomeStatement).Include(s => s.StockBalanceSheet).Include(s => s.StockMetrics).SingleOrDefault(s => s.Id == id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            ViewBag.Name = stock.StockBasic.name;
            //bar chart
            ViewBag.NetIncome = stock.StockIncomeStatement.netincome;
            ViewBag.TotalRevenue = stock.StockIncomeStatement.totalrevenue;
            ViewBag.TotalGrossProfit = stock.StockIncomeStatement.totalgrossprofit;
            ViewBag.RetainedEarnings = stock.StockBalanceSheet.retainedearnings;
            //pie chart
            ViewBag.TotalAssets = stock.StockBalanceSheet.totalassets;
            ViewBag.TotalLiabilities = stock.StockBalanceSheet.totalliabilities;
            ViewBag.TotalEquity = stock.StockBalanceSheet.totalequity;
           
            return View(stock);
        }
        public ActionResult StockBarChart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Stock stock = db.stocks.Include(s => s.StockBasic).Include(s => s.StockIncomeStatement).Include(s => s.StockBalanceSheet).Include(s => s.StockMetrics).SingleOrDefault(s => s.Id == id);
            if (stock == null)
            {
                return HttpNotFound();
            }

            var netIncome = stock.StockIncomeStatement.netincome;
            var retainedEarnings = stock.StockBalanceSheet.retainedearnings;
            var totalRevenue = stock.StockIncomeStatement.totalrevenue;
            var totalGrossProfit = stock.StockIncomeStatement.totalgrossprofit;
            
            List<Datapoint> dataPoints = new List<Datapoint>();

            dataPoints.Add(new Datapoint("Net Income", netIncome));
            dataPoints.Add(new Datapoint("Retained Earnings", retainedEarnings));
            dataPoints.Add(new Datapoint("Total Revenue", totalRevenue));
            dataPoints.Add(new Datapoint("Total Gross Profit", totalGrossProfit));
            

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View(stock);
           
        }
        public ActionResult StockPieChart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Stock stock = db.stocks.Include(s => s.StockBasic).Include(s => s.StockIncomeStatement).Include(s => s.StockBalanceSheet).Include(s => s.StockMetrics).SingleOrDefault(s => s.Id == id);
            if (stock == null)
            {
                return HttpNotFound();
            }

            var totalAssets = stock.StockBalanceSheet.totalassets;
            var totalLiabilities = stock.StockBalanceSheet.totalliabilities;
            var totalEquity = stock.StockBalanceSheet.totalequity;
            

            List<Datapoint> dataPoints = new List<Datapoint>();

            dataPoints.Add(new Datapoint("Total Assets", totalAssets));
            dataPoints.Add(new Datapoint("Total Liabilities", totalLiabilities));
            dataPoints.Add(new Datapoint("Total Equity", totalEquity));
           


            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View(stock);

        }

        public ActionResult StockToWatchList(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.stocks.Include(s => s.StockBasic).Include(s => s.StockIncomeStatement).Include(s => s.StockBalanceSheet).Include(s => s.StockMetrics).SingleOrDefault(s => s.Id == id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        [HttpPost]
        public ActionResult StockToWatchList(int id, ApplicationUser applicationUser)
        {
            
            WatchList watchList = new WatchList { StockId = id, ApplicationUserId = User.Identity.GetUserId() };
            var userId = User.Identity.GetUserId();
            var userCurrent =
                    (from u in db.Users
                     where u.Id == userId
                     select u).First();
            
             
           
            var stockToAdd =
                  (from e in db.stocks
                   where e.Id == id
                   select e).First();

            var basicsToAdd =
            (from s in db.stockBasics
             where s.Id == stockToAdd.StockBasicId
             select s).First();

           

            try
            {
                var juniorToUpdate =
                    (from e in db.juniorUserAccounts
                     where e.ApplicationUserId == userCurrent.Id
                     select e).First();
                if (juniorToUpdate.MyWatchList.Count<6)
                {
                    juniorToUpdate.MyWatchList.Add(stockToAdd);
                    stockToAdd.StockBasic = basicsToAdd;
                    db.watchLists.Add(watchList);
                    db.SaveChanges();
                    return RedirectToAction("WatchList");
                }
                else
                {
                    return RedirectToAction("WatchList");
                }
            }
            catch
            {
                var seniorToUpdate =
                     (from e in db.seniorUserAccounts
                      where e.ApplicationUserId == userCurrent.Id
                      select e).First();
                seniorToUpdate.MyWatchList.Add(stockToAdd);
                db.watchLists.Add(watchList);
                stockToAdd.StockBasic = basicsToAdd;
                db.SaveChanges();
                return RedirectToAction("WatchList");
            }
            
          
        }
        
        public ActionResult WatchList()
        {

            var userId = User.Identity.GetUserId();
            try
            {
                var userCurrent =
                    (from u in db.Users
                     where u.Id == userId
                     select u).First();
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            

           
            try
            {
                var userCurrent =
                   (from u in db.Users
                    where u.Id == userId
                    select u).First();
                var juniorToUpdate =
                     (from e in db.juniorUserAccounts
                      where e.ApplicationUserId == userCurrent.Id
                      select e).First();
                var watchlistToDisplay =
                    (from x in db.watchLists
                     where x.ApplicationUserId == userCurrent.Id
                     select x).ToList();


                

                List<int> stockIds = new List<int>();
                foreach(var item in watchlistToDisplay)
                {
                    stockIds.Add(item.StockId);
                }

                List<Stock> myStocks = new List<Stock>();

                myStocks = db.stocks.Where(s => stockIds.Contains(s.Id)).Include(s => s.StockBasic).ToList();

                
                return View(myStocks);
            }
            catch
            {
                var userCurrent =
                   (from u in db.Users
                    where u.Id == userId
                    select u).First();
                var seniorToUpdate =
                     (from e in db.seniorUserAccounts
                      where e.ApplicationUserId == userCurrent.Id
                      select e).First();
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


                return View(myStocks);
            }
            

           
        }
      

        // GET: Stocks/Create
        public ActionResult Create()
        {
            ViewBag.StockBalanceSheetId = new SelectList(db.stockBalanceSheets, "Id", "ticker");
            ViewBag.StockBasicId = new SelectList(db.stockBasics, "Id", "ticker");
            ViewBag.StockIncomeStatementId = new SelectList(db.stockIncomeStatements, "Id", "ticker");
            ViewBag.StockMetricsId = new SelectList(db.stockMetrics, "Id", "ticker");
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StockBasicId,StockIncomeStatementId,StockBalanceSheetId,StockMetricsId")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.stocks.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StockBalanceSheetId = new SelectList(db.stockBalanceSheets, "Id", "ticker", stock.StockBalanceSheetId);
            ViewBag.StockBasicId = new SelectList(db.stockBasics, "Id", "ticker", stock.StockBasicId);
            ViewBag.StockIncomeStatementId = new SelectList(db.stockIncomeStatements, "Id", "ticker", stock.StockIncomeStatementId);
            ViewBag.StockMetricsId = new SelectList(db.stockMetrics, "Id", "ticker", stock.StockMetricsId);
            return View(stock);
        }

        // GET: Stocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            ViewBag.StockBalanceSheetId = new SelectList(db.stockBalanceSheets, "Id", "ticker", stock.StockBalanceSheetId);
            ViewBag.StockBasicId = new SelectList(db.stockBasics, "Id", "ticker", stock.StockBasicId);
            ViewBag.StockIncomeStatementId = new SelectList(db.stockIncomeStatements, "Id", "ticker", stock.StockIncomeStatementId);
            ViewBag.StockMetricsId = new SelectList(db.stockMetrics, "Id", "ticker", stock.StockMetricsId);
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StockBasicId,StockIncomeStatementId,StockBalanceSheetId,StockMetricsId")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StockBalanceSheetId = new SelectList(db.stockBalanceSheets, "Id", "ticker", stock.StockBalanceSheetId);
            ViewBag.StockBasicId = new SelectList(db.stockBasics, "Id", "ticker", stock.StockBasicId);
            ViewBag.StockIncomeStatementId = new SelectList(db.stockIncomeStatements, "Id", "ticker", stock.StockIncomeStatementId);
            ViewBag.StockMetricsId = new SelectList(db.stockMetrics, "Id", "ticker", stock.StockMetricsId);
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Stock stock = db.stocks.Include(s => s.StockBasic).Include(s => s.StockIncomeStatement).Include(s => s.StockBalanceSheet).Include(s => s.StockMetrics).SingleOrDefault(s => s.Id == id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Stock stock, WatchList watchList)
        {
            var userId = User.Identity.GetUserId();
            var userCurrent =
                    (from u in db.Users
                     where u.Id == userId
                     select u).First();
            try
            {
                var juniorToUpdate =
                    (from e in db.juniorUserAccounts
                     where e.ApplicationUserId == userCurrent.Id
                     select e).First();

                var thisStock = db.stocks.FirstOrDefault(e => e.Id == stock.Id);

                var watchListToRemove = (from s in db.watchLists
                                         where s.StockId == thisStock.Id
                                         select s).FirstOrDefault();

                
                
                    juniorToUpdate.MyWatchList.Remove(thisStock);
                    db.watchLists.Remove(watchListToRemove);
                    db.SaveChanges();
                    return RedirectToAction("WatchList");
                    
               
                
                   
            }
            catch
            {
                var seniorToUpdate =
                     (from e in db.seniorUserAccounts
                      where e.ApplicationUserId == userCurrent.Id
                      select e).First();
                var thisStock = db.stocks.FirstOrDefault(e => e.Id == stock.Id);

                var watchListToRemove = (from s in db.watchLists
                                         where s.StockId == thisStock.Id
                                         select s).FirstOrDefault();

                seniorToUpdate.MyWatchList.Remove(thisStock);
                db.watchLists.Remove(watchListToRemove);
                db.SaveChanges();
                return RedirectToAction("WatchList");
            }

           
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
