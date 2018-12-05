using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StockScreenerLibrary
{
    public class BhavCopyDBAccessLayer : IBhavCopyDBAccessLayer
    {
        public void AddBhavCopyUpdateLog(DateTime date, int stocksUpdated,string Status)
        {
            using (var bhavDBContext = new BhavCopiesDbEntities())
            {
                BhavCopyUploadLog logObject = new BhavCopyUploadLog();
                logObject.Date = date;
                logObject.NumberOfStocksUpdated = stocksUpdated;
                logObject.Status = Status;
                
                bhavDBContext.BhavCopyUploadLogs.Add(logObject);
                bhavDBContext.SaveChanges();
                

            }
        }

        public List<BhavCopy> GetQuotes(string ticker)
        {
            using (var bhavDBContext = new BhavCopiesDbEntities())
            {
                var bhavCopyfromDB = (from s in bhavDBContext.BhavCopies
                                      orderby s.Date ascending
                                      where s.Ticker == ticker
                                      select s).ToList();
                return bhavCopyfromDB;
            }
        }

        public List<string> GetTickerNames()
        {
            using (var bhavDBContext = new BhavCopiesDbEntities())
            {
                var bhavCopyfromDB = (from s in bhavDBContext.Tickers
                                      select s.Ticker1).Distinct();
                return bhavCopyfromDB.ToList();
            }
        }

        public List<Ticker> GetTickerObjects()
        {
            using (var bhavDBContext = new BhavCopiesDbEntities())
            {
                var bhavCopyfromDB = (from s in bhavDBContext.Tickers
                                      select s).Distinct();
                return bhavCopyfromDB.ToList();
            }
        }

        private Housebreak CreateHousebreakObjectFromHBR(HouseBreakReport hbr, Ticker ticker)
        {
            Housebreak hb = new Housebreak();
            hb.FK_Ticker_Id = ticker.Id;
            hb.MotherCandleHigh = hbr.MotherCandleHigh;
            hb.MotherCandleLow = hbr.MotherCandleLow;
            hb.MotherCandleDate = hbr.MotherCandleDate;

            hb.NumberOfCandles = hbr.NumberofCandles;
            hb.BreakoutCandleDate = hbr.BreakOutCandleDate;
            hb.Ticker = ticker;
            return hb;
        }
        public void PopulateHousebreaks(Ticker ticker, List<HouseBreakReport> housebreakReport)
        {
            using (var bhavDBContext = new BhavCopiesDbEntities())
            {
                foreach(HouseBreakReport hbr in housebreakReport)
                {
                    Housebreak hb = CreateHousebreakObjectFromHBR(hbr, ticker);
                    bhavDBContext.Housebreaks.Add(hb);
                }
                bhavDBContext.SaveChanges();
            }
        }

        public void UploadBhavCopy(BhavCopy bhavCopyObject)
        {
            using (var bhavDBContext = new BhavCopiesDbEntities())
            {
                var bhavCopyfromDB = (from s in bhavDBContext.BhavCopies
                                where s.Date == bhavCopyObject.Date && s.Ticker == bhavCopyObject.Ticker
                                      select s).ToList();
                if(bhavCopyfromDB.Count == 0)
                {
                    // Bhav copy of date is not uploaded yet
                    bhavDBContext.BhavCopies.Add(bhavCopyObject);
                    bhavDBContext.SaveChanges();
                }
                else
                    throw new Exception($"BhavCopy of {bhavCopyObject.Date.ToShortDateString()} , {bhavCopyObject.Ticker} already exists");
                   
            }
        }

        public void UploadBhavCopys(List<BhavCopy> bhavCopyObjectList)
        {
            using (var bhavDBContext = new BhavCopiesDbEntities())
            {
                foreach(BhavCopy bc in bhavCopyObjectList)
                {
                    //var bhavCopyfromDB = (from s in bhavDBContext.BhavCopies
                    //                      where s.Date == bc.Date && s.Ticker == bc.Ticker
                    //                      select s).ToList();
                    //if (bhavCopyfromDB.Count == 0)
                    {
                        // Bhav copy of date is not uploaded yet
                        bhavDBContext.BhavCopies.Add(bc);
                        //bhavDBContext.SaveChanges();
                    }
                }
                bhavDBContext.SaveChanges();

            }
        }

        public List<Housebreak> GetQuickHousebreakReport(DateTime breakOutDate)
        {
            using (var bhavDBContext = new BhavCopiesDbEntities())
            {

                // Eager Loading
                var houseBreakList = bhavDBContext.Housebreaks
                    .Include("Ticker")
                    .Where(hb => hb.BreakoutCandleDate == breakOutDate)
                    .OrderByDescending(hb => hb.NumberOfCandles)
                    .ToList();
                    

                

                // Below code is a lazy loading and navigation property Ticker will not be loaded unless it is queried
                // It will generate an exception in view if not loaded. So this code is converted to eager loading
                // as above

                //var bhavCopyfromDB = (from s in bhavDBContext.Housebreaks
                //                      orderby s.NumberOfCandles descending
                //                      where s.BreakoutCandleDate == breakOutDate
                //                      select s).ToList();


                return houseBreakList;
            }
        }
    }
}
