using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockScreenerLibrary;

namespace MyStockScreener.Controllers
{
    public class IndicesController : Controller
    {
        IBhavCopyDBAccessLayer dbAccessLayer = new BhavCopyDBAccessLayer();
        // GET: Indices
        public ActionResult Index()
        {
            List<string> IndexNames = dbAccessLayer.GetIndexList();
            return View(IndexNames);
        }

        public ActionResult Details(string Id)
        {
            List<Housebreak> HousebreaksInIndexStocks = dbAccessLayer.GetQuickHousebreakReportOfIndex(Id);

            return View(HousebreaksInIndexStocks);
        }
    }
}