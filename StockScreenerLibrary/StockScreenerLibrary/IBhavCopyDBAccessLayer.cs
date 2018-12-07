using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockScreenerLibrary
{
    public interface IBhavCopyDBAccessLayer
    {
        void UploadBhavCopys(List<BhavCopy> bhavCopyObject);
        void UploadBhavCopy(BhavCopy bhavCopyObject);
        void AddBhavCopyUpdateLog(DateTime date, int stocksUpdated,string status);

        List<string> GetTickerNames();

        List<BhavCopy> GetQuotes(string ticker);
        List<Ticker> GetTickerObjects();

        void PopulateHousebreaks(Ticker ticker, List<HouseBreakReport> housebreakReport);
        List<Housebreak> GetQuickHousebreakReport(DateTime breakOutDate);

        List<string> GetIndexList();

        List<Housebreak> GetQuickHousebreakReportOfIndex(string indexName);

        List<double> GetClosingPrices(string Ticker);
    }
}
