using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StockScreenerLibrary
{
    
    public class HousebreakInfo
    {
        public  BhavCopy MotherCandle { get; set; }
        public int numberOfCandles { get; set; }

        public BhavCopy DayOfBreak { get; set; }
    }
    public partial class HouseBreakReport
    {
        [Key]
        public long Id { get; set; }
        public double MotherCandleHigh { get; set; }
        public double MotherCandleLow { get; set; }
         
        public string Ticker { get; set; }
        public System.DateTime MotherCandleDate { get; set; }
        public System.DateTime BreakOutCandleDate { get; set; }
        public string BullOrBear { get; set; }
        public int NumberofCandles { get; set; }

    }
    public static class HousebreakScanner
    {
        public static List<HouseBreakReport> GenerateHousebreakReport(List<BhavCopy> bhavList,DateTime givenDate)
        {
            List<HouseBreakReport> housebreaksReport = new List<HouseBreakReport>();
            if (bhavList.Count < 40)
                return new List<HouseBreakReport>();
            for (int i = bhavList.Count - 30; i < bhavList.Count; i++)
            {
                BhavCopy motherCandle = bhavList[i - 1];
                BhavCopy insideDayCandle = bhavList[i];
                // Check for inside day
                if (IsInsideDay(motherCandle, insideDayCandle))
                {
                    // move till the mother candle high or low breaks
                    for (int j = i + 1; j < bhavList.Count; j++)
                    {
                        if (IsCurrentCandleBreakOutOfMotherCandle(motherCandle, bhavList[j]))
                        {
                            
                            HouseBreakReport hbInfo = new HouseBreakReport();
                            hbInfo.MotherCandleHigh = motherCandle.H;
                            hbInfo.MotherCandleLow = motherCandle.L;
                            hbInfo.MotherCandleDate = motherCandle.Date;
                            hbInfo.NumberofCandles = j - i;
                            hbInfo.Ticker = motherCandle.Ticker;
                            hbInfo.BreakOutCandleDate = bhavList[j].Date;
                            if (bhavList[j].C > motherCandle.H)
                                hbInfo.BullOrBear = "Bullish";
                            else if (bhavList[j].C <= motherCandle.L)
                                hbInfo.BullOrBear = "Bearish";
                            else
                                hbInfo.BullOrBear = "";

                            housebreaksReport.Add(hbInfo);
                            //Move the indices
                            i = j;
                            break;
                        }
                        else
                        {

                        }
                    }
                }
            }
            return housebreaksReport;
        }

        public static List<HousebreakInfo> ScanForHousebreaks(List<BhavCopy> bhavList)
        {
            List<HousebreakInfo> housebreaksHistory = new List<HousebreakInfo>();
            if (bhavList.Count < 40)
                return new List<HousebreakInfo>();
            for(int i= bhavList.Count-30; i < bhavList.Count;i++)
            {
                BhavCopy motherCandle = bhavList[i - 1];
                BhavCopy insideDayCandle = bhavList[i];
                // Check for inside day
                if (IsInsideDay(motherCandle, insideDayCandle))
                {
                    // move till the mother candle high or low breaks
                    for(int j=i+1;j < bhavList.Count;j++)
                    {
                        if(IsCurrentCandleBreakOutOfMotherCandle(motherCandle, bhavList[j]))
                        {
                            HousebreakInfo hbInfo = new HousebreakInfo();
                            hbInfo.MotherCandle = motherCandle;
                            hbInfo.DayOfBreak = bhavList[j];
                            hbInfo.numberOfCandles = j - i;
                            housebreaksHistory.Add(hbInfo);
                            //Move the indices
                            i = j;
                            break;
                        }
                        else
                        {

                        }
                    }
                }
            }
            return housebreaksHistory;
        }

        public static bool IsCurrentCandleBreakOutOfMotherCandle(BhavCopy mc, BhavCopy currentCandle)
        {
            double mcHigh, mcLow;
            mcHigh = mc.H;
            mcLow = mc.L;

            if (currentCandle.C > mcHigh || currentCandle.C < mcLow)
            {
                // Mother candle range is broken
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsInsideDay(BhavCopy mc, BhavCopy insideDay)
        {
            double mcHigh, mcLow;
            mcHigh = mc.H;
            mcLow = mc.L;

            if(insideDay.H <= mcHigh && insideDay.L >= mcLow)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
