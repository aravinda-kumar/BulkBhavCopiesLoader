using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using StockScreenerLibrary.ViewModels;

namespace StockScreenerLibrary
{
    enum Indices
    {
        Nifty_50 = 1,
        Nifty_Auto = 2,
        Nifty_Bank = 3,
        Nifty_Finance = 4,
        Nifty_Fmcg = 5,
        Nifty_IT = 6,
        Nifty_Media = 7,
        Nifty_Metal = 8,
        Nifty_Next_50 = 9,
        Nifty_Pharma = 10,
        Nifty_Private_Bank = 11,
        Nifty_PSU_Bank = 12,
        Nifty_Realty=13
    }

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

            hb.Ticker = null; // BUG: if this is assigned ticker object, then it trys to 
                              //insert ticker row into tickers table.  so made it  null to fix

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
         
        public List<HousebreakAboveAvgVolume> GetQuickHousebreakAbvAvgVolume(DateTime breakOutDate)
        {
            using (var bhavDBContext = new BhavCopiesDbEntities())
            {

                var housbreaksSortedByDate = (from hb in bhavDBContext.Housebreaks
                                              orderby hb.BreakoutCandleDate descending
                                              select hb).FirstOrDefault();
                // Eager Loading
                //var houseBreakList = bhavDBContext.Housebreaks
                //    .Include("Ticker")
                //    .Where(hb => hb.BreakoutCandleDate == housbreaksSortedByDate.BreakoutCandleDate)
                //    .OrderByDescending(hb => hb.NumberOfCandles)
                //    .ToList();


                //var hbAbvAvgVolumes = from hb in houseBreakList
                //                      join ind in bhavDBContext.Indicators on hb.BreakoutCandleDate equals ind.Date
                //                      join bc in bhavDBContext.BhavCopies on hb.BreakoutCandleDate equals bc.Date
                //                      where (hb.FK_Ticker_Id == ind.FK_Ticker_Id) && (hb.BreakoutCandleDate == ind.Date) && (bc.Date == ind.Date)
                //                      select new
                //                      {
                //                          bc.Ticker,
                //                          bc.Volume,
                //                          ind.Indicator_1,
                //                          hb.MotherCandleHigh,
                //                          hb.MotherCandleLow,
                //                          hb.MotherCandleDate,
                //                          hb.BreakoutCandleDate
                //                      };

                var hbAbvAvgVolumes = from bc in bhavDBContext.BhavCopies
                                  join t in bhavDBContext.Tickers on bc.Ticker equals t.Ticker1
                                  join hb in bhavDBContext.Housebreaks on bc.Date equals hb.BreakoutCandleDate
                                  join ind in bhavDBContext.Indicators on t.Id equals ind.FK_Ticker_Id
                                  where bc.Date == housbreaksSortedByDate.BreakoutCandleDate && t.Id == hb.FK_Ticker_Id && bc.Date == ind.Date && bc.Volume > ind.Indicator_1
                                  select new
                                  {
                                      bc.Ticker,
                                      bc.Volume,
                                      bc.C,
                                      ind.Indicator_1,
                                      hb.MotherCandleHigh,
                                      hb.MotherCandleLow,
                                      hb.MotherCandleDate,
                                      hb.BreakoutCandleDate,
                                      hb.NumberOfCandles
                                  };


                List < HousebreakAboveAvgVolume > hbAboveAvgVolumes = new List<HousebreakAboveAvgVolume>();
                foreach(var item in hbAbvAvgVolumes)
                {
                    HousebreakAboveAvgVolume hbObject = new HousebreakAboveAvgVolume();

                    hbObject.Ticker = item.Ticker;
                    hbObject.Volume = item.Volume;
                    hbObject.avgVolume = item.Indicator_1;
                    hbObject.MotherCandleHigh = item.MotherCandleHigh;
                    hbObject.MotherCandleLow = item.MotherCandleLow;
                    hbObject.MotherCandleDate = item.MotherCandleDate;
                    hbObject.BreakoutCandleDate = item.BreakoutCandleDate;
                    hbObject.NumberOfCandles = item.NumberOfCandles;
                    hbObject.PercentageVolume = (100 * item.Volume) / hbObject.avgVolume;
                    hbObject.ClosingPrice = item.C;
                    if (item.C > item.MotherCandleHigh)
                        hbObject.Breakout_Type = "Bullish";
                    else
                        hbObject.Breakout_Type = "Bearish";
                    hbAboveAvgVolumes.Add(hbObject);
                }

                // Below code is a lazy loading and navigation property Ticker will not be loaded unless it is queried
                // It will generate an exception in view if not loaded. So this code is converted to eager loading
                // as above

                                      //var bhavCopyfromDB = (from s in bhavDBContext.Housebreaks
                                      //                      orderby s.NumberOfCandles descending
                                      //                      where s.BreakoutCandleDate == breakOutDate
                                      //                      select s).ToList();


                return hbAboveAvgVolumes.OrderByDescending(hb => hb.NumberOfCandles).ToList();
            }
        }
        public List<Housebreak> GetQuickHousebreakReport(DateTime breakOutDate)
        {
            using (var bhavDBContext = new BhavCopiesDbEntities())
            {

                var housbreaksSortedByDate = (from hb in bhavDBContext.Housebreaks
                                              orderby hb.BreakoutCandleDate descending
                                              select hb).FirstOrDefault();
                // Eager Loading
                var houseBreakList = bhavDBContext.Housebreaks
                    .Include("Ticker")
                    .Where(hb => hb.BreakoutCandleDate == housbreaksSortedByDate.BreakoutCandleDate)
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

        public List<string> GetIndexList()
        {
            List<string> IndexNames = Enum.GetNames(typeof(Indices)).ToList();
            return IndexNames;
        }

        
        public List<Housebreak> GetQuickHousebreakReportOfIndex(string indexName)
        {
            using (var bhavDBContext = new BhavCopiesDbEntities())
            {
                var housbreaksSortedByDate = (from hb in bhavDBContext.Housebreaks
                                              orderby hb.BreakoutCandleDate descending
                                              select hb).FirstOrDefault();

                var houseBreakList = bhavDBContext.Housebreaks
                    .Include("Ticker")
                    .Where(hb => hb.BreakoutCandleDate == housbreaksSortedByDate.BreakoutCandleDate)
                    .OrderByDescending(hb => hb.NumberOfCandles)
                    .ToList();

                Indices index;
                Enum.TryParse<Indices>(indexName, out index);

                switch(index)
                {
                    case Indices.Nifty_50:
                        var Nifty_50_List = (from stock in bhavDBContext.Nifty_50
                                               select stock).ToList();
                        var indexStocksHousebreaks = (from hb in houseBreakList
                                                      join IndexStock in Nifty_50_List on hb.Ticker.Ticker1 equals IndexStock.Symbol
                                                      select hb).ToList();
                        return indexStocksHousebreaks;
                    case Indices.Nifty_Auto:
                        var Nifty_Auto_List = (from stock in bhavDBContext.Nifty_Auto
                                               select stock).ToList();

                        indexStocksHousebreaks = (from hb in houseBreakList
                                                      join IndexStock in Nifty_Auto_List on hb.Ticker.Ticker1 equals IndexStock.Symbol
                                                      select hb).ToList();
                        return indexStocksHousebreaks;
                    case Indices.Nifty_Bank:
                        var Nifty_Bank_List = (from stock in bhavDBContext.Nifty_Bank
                                               select stock).ToList();

                        indexStocksHousebreaks = (from hb in houseBreakList
                                                  join IndexStock in Nifty_Bank_List on hb.Ticker.Ticker1 equals IndexStock.Symbol
                                                  select hb).ToList();
                        return indexStocksHousebreaks;
                    case Indices.Nifty_Finance:
                        var Nifty_Finance_List = (from stock in bhavDBContext.Nifty_Finance
                                                  select stock).ToList();

                        indexStocksHousebreaks = (from hb in houseBreakList
                                                  join IndexStock in Nifty_Finance_List on hb.Ticker.Ticker1 equals IndexStock.Symbol
                                                  select hb).ToList();
                        return indexStocksHousebreaks;
                    case Indices.Nifty_Fmcg:
                        var Nifty_Fmcg_List = (from stock in bhavDBContext.Nifty_Fmcg
                                               select stock).ToList();

                        indexStocksHousebreaks = (from hb in houseBreakList
                                                  join IndexStock in Nifty_Fmcg_List on hb.Ticker.Ticker1 equals IndexStock.Symbol
                                                  select hb).ToList();
                        return indexStocksHousebreaks;
                    case Indices.Nifty_IT:
                        var Nifty_IT_List = (from stock in bhavDBContext.Nifty_IT
                                             select stock).ToList();

                        indexStocksHousebreaks = (from hb in houseBreakList
                                                  join IndexStock in Nifty_IT_List on hb.Ticker.Ticker1 equals IndexStock.Symbol
                                                  select hb).ToList();
                        return indexStocksHousebreaks;
                    case Indices.Nifty_Media:
                        var Nifty_Media_List = (from stock in bhavDBContext.Nifty_Media
                                                select stock).ToList();

                        indexStocksHousebreaks = (from hb in houseBreakList
                                                  join IndexStock in Nifty_Media_List on hb.Ticker.Ticker1 equals IndexStock.Symbol
                                                  select hb).ToList();
                        return indexStocksHousebreaks;
                    case Indices.Nifty_Metal:
                        var Nifty_Metal_List = (from stock in bhavDBContext.Nifty_Metal
                                                select stock).ToList();

                        indexStocksHousebreaks = (from hb in houseBreakList
                                                  join IndexStock in Nifty_Metal_List on hb.Ticker.Ticker1 equals IndexStock.Symbol
                                                  select hb).ToList();
                        return indexStocksHousebreaks;
                    case Indices.Nifty_Next_50:
                        var Nifty_Next_50_List = (from stock in bhavDBContext.Nifty_Next_50
                                                  select stock).ToList();

                        indexStocksHousebreaks = (from hb in houseBreakList
                                                  join IndexStock in Nifty_Next_50_List on hb.Ticker.Ticker1 equals IndexStock.Symbol
                                                  select hb).ToList();
                        return indexStocksHousebreaks;
                    case Indices.Nifty_Pharma:
                        var Nifty_Pharma_List = (from stock in bhavDBContext.Nifty_Pharma
                                                    select stock).ToList();

                        indexStocksHousebreaks = (from hb in houseBreakList
                                                  join IndexStock in Nifty_Pharma_List on hb.Ticker.Ticker1 equals IndexStock.Symbol
                                                  select hb).ToList();
                        return indexStocksHousebreaks;
                    case Indices.Nifty_Private_Bank:
                        var Nifty_Private_List = (from stock in bhavDBContext.Nifty_Private_Bank
                                                  select stock).ToList();

                        indexStocksHousebreaks = (from hb in houseBreakList
                                                  join IndexStock in Nifty_Private_List on hb.Ticker.Ticker1 equals IndexStock.Symbol
                                                  select hb).ToList();
                        return indexStocksHousebreaks;
                    case Indices.Nifty_PSU_Bank:
                        var Nifty_PSU_Bank_List = (from stock in bhavDBContext.Nifty_PSU_Bank
                                                  select stock).ToList();

                        indexStocksHousebreaks = (from hb in houseBreakList
                                                  join IndexStock in Nifty_PSU_Bank_List on hb.Ticker.Ticker1 equals IndexStock.Symbol
                                                  select hb).ToList();
                        return indexStocksHousebreaks;
                    case Indices.Nifty_Realty:
                        var Nifty_Realty_List = (from stock in bhavDBContext.Nifty_Realty
                                                 select stock).ToList();

                        indexStocksHousebreaks = (from hb in houseBreakList
                                                  join IndexStock in Nifty_Realty_List on hb.Ticker.Ticker1 equals IndexStock.Symbol
                                                  select hb).ToList();
                        return indexStocksHousebreaks;
                }

                return houseBreakList;
            }
        }

        public List<double> GetClosingPrices(string Ticker)
        {
            using (var bhavDBContext = new BhavCopiesDbEntities())
            {

                var closingPrices = (from bc in bhavDBContext.BhavCopies
                                              orderby bc.Date descending
                                              select new
                                              {
                                                  bc.C
                                              }).ToList();

                List<double> closingPricesList = new List<double>();
                foreach(var item in closingPrices)
                {
                    closingPricesList.Add(item.C);
                }
                return closingPricesList;
            }
        }

        public void UpdateIndicator(string Indicator, List<Indicator> IndicatorValues)
        {
            using (var bhavDBContext = new BhavCopiesDbEntities())
            {
                if (Indicator == "MAVolume")
                {
                    foreach(Indicator i in IndicatorValues)
                    {
                        long ticker_Id = i.FK_Ticker_Id;
                        DateTime t = i.Date;

                        var indValueRow = (from indRow in bhavDBContext.Indicators
                                           where indRow.FK_Ticker_Id == ticker_Id && indRow.Date == t
                                           select indRow)?.First();
                        indValueRow.Indicator_1 = i.Indicator_1;

                    }
                    bhavDBContext.SaveChanges();
                }
            }
        }

        public List<Ticker> GetStocksNamesFromHousebreaks()
        {
            //Get Distinct Tickers from house breaks table
            //select Distinct(t.[Ticker])
            //from Housebreaks as hb
            //INNER JOIN Tickers as t ON hb.[FK_Ticker_Id] = t.[Id]
            //order by t.[Ticker]
            using (var bhavDBContext = new BhavCopiesDbEntities())
            {
                var stockNamesFromHousebreaks = (from t in bhavDBContext.Tickers
                                                 join hb in bhavDBContext.Housebreaks on t.Id equals hb.FK_Ticker_Id
                                                 select t).Distinct().ToList();
                return stockNamesFromHousebreaks;
            }
        }

        public List<Housebreak> GetUnprocessedHousebreaksOfTicker(long ticker_id)
        {
            //select*
            //from Housebreaks as hb
            //where hb.[FK_Ticker_Id] = 256
            using (var bhavDBContext = new BhavCopiesDbEntities())
            {
                var hbOfStock = (from hb in bhavDBContext.Housebreaks
                                 where hb.FK_Ticker_Id == ticker_id && hb.PercentageMoveAfterBreakOut == null
                                 orderby hb.BreakoutCandleDate ascending
                                 select hb).ToList();
                return hbOfStock;
            }
        }

        public void UpdateHousebreaks(List<Housebreak> housebreakList)
        {
            using (var bhavDBContext = new BhavCopiesDbEntities())
            {
                bhavDBContext.Housebreaks.AddRange(housebreakList);
                bhavDBContext.SaveChanges();
            }
        }
    }
}
