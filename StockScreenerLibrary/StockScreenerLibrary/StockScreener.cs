using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockScreenerLibrary
{
    public class StockScreener : IStockScreener
    {
        IBhavCopyDBAccessLayer dbAccessLayer = new BhavCopyDBAccessLayer();
        public List<HousebreakInfo> GenerateHousebreakInfo(List<BhavCopy> BhavCopies)
        {
            return HousebreakScanner.ScanForHousebreaks(BhavCopies);
        }

        public List<HouseBreakReport> GenerateHousebreakReport(DateTime givenDate)
        {
            IStockScreener stockScreener = new StockScreener();
            List<string> tickerNames = dbAccessLayer.GetTickerNames();
            List<HouseBreakReport> AllStocksHouseBreakReport = new List<HouseBreakReport>();
            foreach (string ticker in tickerNames)
            {
                List<BhavCopy> quotesList = dbAccessLayer.GetQuotes(ticker);
                List<HouseBreakReport> houseBreaks = HousebreakScanner.GenerateHousebreakReport(quotesList,givenDate);
                if (houseBreaks.Count > 0)
                {
                    HouseBreakReport hb = houseBreaks[houseBreaks.Count - 1];
                    AllStocksHouseBreakReport.Add(hb);
                }
            }
            
            return AllStocksHouseBreakReport;
        }

        private List<double> GetClosingPrices(List<BhavCopy> bhavCopies)
        {
            var closingPricesList = from bc in bhavCopies
                                select new
                                {
                                    bc.C
                                };
            List<double> closingPrices = new List<double>();
            foreach(var item in closingPricesList)
            {
                closingPrices.Add(item.C);
            }
            return closingPrices;
        }

        public void PopulateIndicators()
        {
            List<BhavCopy> bhavCopies = dbAccessLayer.GetQuotes("Nifty 50");
            

        }
    }
}
