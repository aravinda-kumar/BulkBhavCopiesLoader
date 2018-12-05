using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkBhavCopiesLoader
{
    public class CsvFilesEnumerator
    {
        private string folderName;
        public CsvFilesEnumerator(string folder)
        {
            folderName = folder;
        }

        public List<string> EnumerateCSVFiles()
        {
            DirectoryInfo di = new DirectoryInfo(folderName);
            FileInfo[] files = di.GetFiles();

            List<string> csvFilesList = new List<string>();
            foreach (FileInfo fi in files)
            {
                if (fi.Extension == ".csv")
                {
                    csvFilesList.Add(fi.FullName);
                }
            }
            return csvFilesList;
        }
    }
}
