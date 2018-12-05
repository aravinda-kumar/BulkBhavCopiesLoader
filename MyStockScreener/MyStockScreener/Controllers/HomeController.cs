using StockScreenerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStockScreener.Controllers
{
    public class HomeController : Controller
    {
        IBhavCopyDBAccessLayer dbAccessLayer = new BhavCopyDBAccessLayer();
        IStockScreener stockScreener = new StockScreener();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DisplayQuotes()
        {
            List<BhavCopy> quotesList = dbAccessLayer.GetQuotes("Nifty 50");
            return View(quotesList);
        }

        public ActionResult GenerateHousebreakReport()
        {
            List<HouseBreakReport> quotesList = stockScreener.GenerateHousebreakReport(new DateTime(2018,12,03));
            List<HouseBreakReport> currentDayBreakOutList = new List<HouseBreakReport>();
            foreach (HouseBreakReport hbr in quotesList)
            {
                if (hbr.BreakOutCandleDate == new DateTime(2018, 12, 3))
                    currentDayBreakOutList.Add(hbr);


            }
            currentDayBreakOutList.Sort(delegate (HouseBreakReport x, HouseBreakReport y)
            {
                return y.NumberofCandles.CompareTo(x.NumberofCandles);
            });

            return View(currentDayBreakOutList);
        }

        public ActionResult QuickHousebreakReport()
        {
            List<Housebreak> housebreakReport = dbAccessLayer.GetQuickHousebreakReport(new DateTime(2018,12,4));
            return View(housebreakReport);
        }
    }
}