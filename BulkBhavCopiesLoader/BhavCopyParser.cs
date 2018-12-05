using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using StockScreenerLibrary;

namespace BulkBhavCopiesLoader
{
    public class BhavCopyParser
    {
        public static List<BhavCopy> ParseQuotesFromBhavCopy(string BhavCopyText)
        {
            List<BhavCopy> bhavCopyList = new List<BhavCopy>();
            char[] delimiterChars = {'\n' };
            char[] paramsep = { ',' };
            string[] lines = BhavCopyText.Split(delimiterChars);

            foreach(string line in lines)
            {
                if (line == "")
                    continue;
                string[] csvReader = line.Trim().Split(paramsep);
                BhavCopy bc = new BhavCopy();
                var Ticker = csvReader[0];
                bc.Ticker = Ticker.ToString();

                var date = csvReader[1];
                bc.Date = DateTime.ParseExact(date.ToString(), "MMddyyyy", CultureInfo.InvariantCulture);

                var open = csvReader[2];
                if (open == "")
                    bc.O = 0;
                else
                    bc.O = Convert.ToDouble(open);

                var High = csvReader[3];
                if (High == "")
                    bc.H = 0;
                else
                    bc.H = Convert.ToDouble(High);

                var Low = csvReader[4];
                if (Low == "")
                    bc.L = 0;
                else
                    bc.L = Convert.ToDouble(Low);

                var Close = csvReader[5];
                if (Close == "")
                    bc.C = 0;
                else
                    bc.C = Convert.ToDouble(Close);

                var Volume = csvReader[6];
                if (Volume == "")
                    bc.Volume = 0;
                else
                    bc.Volume = Convert.ToDouble(Volume);

                bhavCopyList.Add(bc);
            }
            return bhavCopyList;
        }
        
    }
}
