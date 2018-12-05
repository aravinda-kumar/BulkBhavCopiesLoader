using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkBhavCopiesLoader
{
    public class BhavCopyFileReader
    {
        public static string ReadCompleteBhavCopy(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            if (sr != null)
            {
                string completeText = sr.ReadToEnd();
                sr.Close();
                return completeText;
            }
            else
            {
                return "";
            }
        }
    }
}
