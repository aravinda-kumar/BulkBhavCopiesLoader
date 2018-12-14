using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TALib = TicTacTec.TA.Library;

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
        private List<double> GetOpenPrices(List<BhavCopy> bhavCopies)
        {
            var openPricesList = from bc in bhavCopies
                                    select new
                                    {
                                        bc.O
                                    };
            List<double> openPrices = new List<double>();
            foreach (var item in openPricesList)
            {
                openPrices.Add(item.O);
            }
            return openPrices;
        }
        private List<double> GetHighPrices(List<BhavCopy> bhavCopies)
        {
            var highPricesList = from bc in bhavCopies
                                 select new
                                 {
                                     bc.H
                                 };
            List<double> highPrices = new List<double>();
            foreach (var item in highPricesList)
            {
                highPrices.Add(item.H);
            }
            return highPrices;
        }
        private List<double> GetLowPrices(List<BhavCopy> bhavCopies)
        {
            var lowPricesList = from bc in bhavCopies
                                 select new
                                 {
                                     bc.L
                                 };
            List<double> lowPrices = new List<double>();
            foreach (var item in lowPricesList)
            {
                lowPrices.Add(item.L);
            }
            return lowPrices;
        }
        private List<double> GetVolumes(List<BhavCopy> bhavCopies)
        {
            var volumesList = from bc in bhavCopies
                                    select new
                                    {
                                        bc.Volume
                                    };
            List<double> volumes = new List<double>();
            foreach (var item in volumesList)
            {
                volumes.Add(item.Volume);
            }
            return volumes;
        }

        public void PopulateIndicators(Ticker t)
        {
            List<BhavCopy> bhavCopies = dbAccessLayer.GetQuotes(t.Ticker1);
            List<double> volumes = GetVolumes(bhavCopies);

            int outBegIndx;
            int outNBelement;
            double[] result = new double[volumes.Count];
            TALib.Core.Sma(0, volumes.Count - 1, volumes.ToArray(), 10, out outBegIndx, out outNBelement, result);

            List<Indicator> indicators = new List<Indicator>();
            int maIndex = 0;
            for (int i = 9; i < bhavCopies.Count; i++, maIndex++)
            {

                Indicator ind = new Indicator();
                ind.Date = bhavCopies[i].Date;
                ind.FK_Ticker_Id = t.Id;
                ind.Indicator_1 = result[maIndex];
                indicators.Add(ind);
            }
            dbAccessLayer.UpdateIndicator("MAVolume", indicators);
        }

        public void ScanForCandleStickPatterns(List<BhavCopy> bhavCopies)
        {


            // THERE IS CONFUSION IN HOW TO USE TALIB API. LEAVING THIS TOPIC HERE 8/12/2018
            List<double> open = GetOpenPrices(bhavCopies);
            List<double> close = GetClosingPrices(bhavCopies);
            List<double> high = GetHighPrices(bhavCopies);
            List<double> low = GetLowPrices(bhavCopies);

            int outBegIndx = 0;
            int outOutNBElement = 0;
            int[] outInteger = new int[bhavCopies.Count];

            //double[] open = new double[1];
            //double[] close = new double[1];
            //double[] high = new double[1];
            //double[] low = new double[1];

            //List<BhavCopy> bullishHammers = new List<BhavCopy>();
            //List<BhavCopy> bearishHammers = new List<BhavCopy>();
            //foreach (BhavCopy bc in bhavCopies)
            //{
            //    open[0] = bc.O;
            //    close[0] = bc.C;
            //    high[0] = bc.H;
            //    low[0] = bc.L;

            //    TALib.Core.CdlHammer(0, 0, open, high, low, close, out outBegIndx, out outOutNBElement, outInteger);
            //    if(outInteger[0] == 100)
            //    {
            //        bullishHammers.Add(bc);
            //    }
            //    else if(outInteger[0] == -100)
            //    {
            //        bearishHammers.Add(bc); 
            //    }

            //}
            TALib.Core.CdlHammer(0, bhavCopies.Count-1, open.ToArray(), high.ToArray(), low.ToArray(), close.ToArray(), out outBegIndx, out outOutNBElement, outInteger);
            

        }
    }
}
