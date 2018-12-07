using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStockScreener.Controllers
{
    public class IndexHousebreaksController : Controller
    {
        // GET: IndexHousebreaks
        public string Details(string Id)
        {
            return Id;
        }
    }
}