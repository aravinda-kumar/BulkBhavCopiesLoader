using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockScreenerLibrary.ViewModels
{
    public class HousebreakAboveAvgVolume
    {
        public string Ticker { get; set; }
        [Display(Name = "Traded Volume")]
        public double Volume { get; set; }
        [Display(Name = "CMP")]
        public double ClosingPrice { get; set; }
        [Display(Name = "10 Day Average Volume")]
        public double avgVolume { get; set; }
        public double MotherCandleHigh { get; set; }
        public double MotherCandleLow { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime MotherCandleDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> BreakoutCandleDate { get; set; }
        [Display(Name = "Number of Candles")]
        public int NumberOfCandles { get; set; }
        [Display(Name = "% Volume Change")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
        public double PercentageVolume { get; set; }
        [Display(Name = "Breakout Type")]
        public string Breakout_Type { get; set; }

    }
}
