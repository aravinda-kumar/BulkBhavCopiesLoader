using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockScreenerLibrary
{
    public interface IStockScreener
    {
        List<HousebreakInfo> GenerateHousebreakInfo(List<BhavCopy> BhavCopies);
        List<HouseBreakReport> GenerateHousebreakReport(DateTime givenDate);

        void PopulateIndicators();

    }
}
