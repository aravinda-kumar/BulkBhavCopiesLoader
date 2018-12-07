using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockScreenerLibrary;

namespace BulkBhavCopiesLoader
{
    public partial class BulkBhavCopiesUploader : Form
    {
        BackgroundWorker bwUploadBhavCopyWorker = new BackgroundWorker();
        BackgroundWorker bwHousebreakPopulator = new BackgroundWorker();
        IBhavCopyDBAccessLayer dbAccessLayer = new BhavCopyDBAccessLayer();
        public BulkBhavCopiesUploader()
        {
            InitializeComponent();
            InitializeBackgroundWorker();
            bwUploadBhavCopyWorker.WorkerReportsProgress = true;
            bwUploadBhavCopyWorker.WorkerSupportsCancellation = true;

            bwHousebreakPopulator.WorkerReportsProgress = true;
            bwHousebreakPopulator.WorkerSupportsCancellation = true;
        }

        private void InitializeBackgroundWorker()
        {
            bwUploadBhavCopyWorker.DoWork +=
                new DoWorkEventHandler(backgroundWorker1_DoWork);
            bwUploadBhavCopyWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker1_RunWorkerCompleted);
            bwUploadBhavCopyWorker.ProgressChanged +=
                new ProgressChangedEventHandler(
            backgroundWorker1_ProgressChanged);

            bwHousebreakPopulator.DoWork +=
                new DoWorkEventHandler(bwHousebreakPopulator_DoWork);
            bwHousebreakPopulator.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            bwHousebreakPopulator_RunWorkerCompleted);
            bwHousebreakPopulator.ProgressChanged +=
                new ProgressChangedEventHandler(
            bwHousebreakPopulator_ProgressChanged);
        }


        // This event handler is where the actual,
        // potentially time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender,
            DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            CsvFilesEnumerator csvFiles = new CsvFilesEnumerator(textBox_Folder.Text);
            List<string> filesList = csvFiles.EnumerateCSVFiles();
            int fileCount = 1;
            List<BhavCopy> batchBhavs = new List<BhavCopy>();
            List<BhavCopy> bhavCopyObjectList = new List<BhavCopy>();
            string progressMessage = "";
            
            foreach (string file in filesList)
            {
                string bhavCopyText = BhavCopyFileReader.ReadCompleteBhavCopy(file);
                bhavCopyObjectList = BhavCopyParser.ParseQuotesFromBhavCopy(bhavCopyText);

                
                

                int bhavCount = 0;

                try
                {
                    
                    progressMessage = "";

                    bhavCount++;
                    progressMessage = "";


                }
                catch(Exception exp)
                {
                    worker.ReportProgress(bhavCount, exp.Message);
                    continue;
                }
                
                
                if (fileCount % 5 == 0)
                {
                    batchBhavs.AddRange(bhavCopyObjectList);
                    dbAccessLayer.UploadBhavCopys(batchBhavs);
                    progressMessage = $"{fileCount} Bhav Copies of {filesList.Count} in Progress.";
                    int percentage = (int)((float)(100.0 * fileCount) / filesList.Count);
                    worker.ReportProgress(percentage, progressMessage);
                    batchBhavs.Clear();
                }
                else
                    batchBhavs.AddRange(bhavCopyObjectList);
                fileCount++;

            }
            if(batchBhavs.Count > 0)
            {
                batchBhavs.AddRange(bhavCopyObjectList);
                dbAccessLayer.UploadBhavCopys(batchBhavs);
                fileCount--; // just for percentage calculation
                progressMessage = $"{fileCount} Bhav Copies of {filesList.Count} in Progress.";
                int percentage = (int)((float)(100.0 * fileCount) / filesList.Count);
                worker.ReportProgress(percentage, progressMessage);
                batchBhavs.Clear();
            }

            //foreach (BhavCopy bc in bhavCopyObjectList)
            //{
            //    try
            //    {
            //        dbAccessLayer.UploadBhavCopy(bc);
            //        progressMessage = "";

            //        bhavCount++;
            //        int percentage = (int)((float)(100.0 * bhavCount) / bhavCopyObjectList.Count);
            //        progressMessage = $"{fileCount} Bhav Copies of {filesList.Count} in Progress.";
            //        worker.ReportProgress(percentage, progressMessage);
            //    }
            //    catch (Exception exp)
            //    {
            //        worker.ReportProgress(bhavCount, exp.Message);
            //        continue;
            //    }

            //}

        }

        // This event handler deals with the results of the
        // background operation.
        private void backgroundWorker1_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                resultLabel.Text = "Canceled";
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if (e.Result!=null)
                    resultLabel.Text = "Complete";
            }

                        // Enable the Start button.
            button_StartUpload.Enabled = true;

            // Disable the Cancel button.
            button_StopUpload.Enabled = false;
        }

        // This event handler updates the progress bar.
        private void backgroundWorker1_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            resultLabel.Text = (string)e.UserState;
        }

        private void bwHousebreakPopulator_DoWork(object sender,
            DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            List<Ticker> tickers = dbAccessLayer.GetTickerObjects();

            

            IStockScreener stockScreener = new StockScreener();
            
            List<HouseBreakReport> AllStocksHouseBreakReport = new List<HouseBreakReport>();
            int tickerCount = 1;
            foreach (Ticker ticker in tickers)
            {
                List<BhavCopy> quotesList = dbAccessLayer.GetQuotes(ticker.Ticker1);
                List<HouseBreakReport> houseBreaks = HousebreakScanner.GenerateHousebreakReport(quotesList, new DateTime(2018,1,1));
                if (houseBreaks.Count > 0)
                {
                    dbAccessLayer.PopulateHousebreaks(ticker, houseBreaks);
                    tickerCount++;
                    int percentage = (100 * tickerCount) / tickers.Count;
                    string progressMessage = "";
                    progressMessage = $"{tickerCount} Bhav Copies of {tickers.Count} in Progress.";
                    worker.ReportProgress(percentage, progressMessage);
                }
            }
        }

        private void bwHousebreakPopulator_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                resultLabel.Text = "Canceled";
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if (e.Result != null)
                    resultLabel.Text = "Complete";
            }

            button3.Enabled = true;
        }

        private void bwHousebreakPopulator_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            resultLabel.Text = (string)e.UserState;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderSelectionDlg = new FolderBrowserDialog();
            if(folderSelectionDlg.ShowDialog() == DialogResult.OK)
            {
                
                textBox_Folder.Text = folderSelectionDlg.SelectedPath;

                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button_StartUpload_Click(object sender, EventArgs e)
        {
            if (bwUploadBhavCopyWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                bwUploadBhavCopyWorker.RunWorkerAsync();
            }

        }
        IStockScreener stockScreener = new StockScreener();
        private void button_ScanHousebreaks_Click(object sender, EventArgs e)
        {
            
            List<string> tickerNames = dbAccessLayer.GetTickerNames();

            StreamWriter houseBreaksReport = new StreamWriter("D:\\housebreaks.txt");

            foreach(string ticker in tickerNames)
            {
                List<BhavCopy> quotesList = dbAccessLayer.GetQuotes(ticker);
                List<HousebreakInfo> houseBreaks =  stockScreener.GenerateHousebreakInfo(quotesList);
                if(houseBreaks.Count > 0)
                {
                    int test = 0;
                    HousebreakInfo hb = houseBreaks[houseBreaks.Count - 1];
                    houseBreaksReport.WriteLine($"{ticker},{hb.DayOfBreak.Date},{hb.MotherCandle.Date},{hb.MotherCandle.H},{hb.MotherCandle.L},{hb.numberOfCandles}");
                }
            }
            houseBreaksReport.Close();
            MessageBox.Show("Done");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<HouseBreakReport> quotesList = stockScreener.GenerateHousebreakReport(new DateTime(2018, 11, 30));
            StreamWriter houseBreaksReport = new StreamWriter("D:\\housebreaksreport.txt");
            foreach(HouseBreakReport hbr in quotesList)
            {
                if(hbr.BreakOutCandleDate == new DateTime(2018,12,3))
                    houseBreaksReport.WriteLine($"{hbr.Ticker},{hbr.BreakOutCandleDate},{hbr.MotherCandleDate},{hbr.MotherCandleHigh},{hbr.MotherCandleLow},{hbr.NumberofCandles}");
            }
            houseBreaksReport.Close();
            MessageBox.Show("Done");

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            button3.Enabled = false;
            if (bwHousebreakPopulator.IsBusy != true)
            {
                // Start the asynchronous operation.
                bwHousebreakPopulator.RunWorkerAsync();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<Housebreak> housebreakReport = dbAccessLayer.GetQuickHousebreakReport(new DateTime(2018, 12, 4));
            StreamWriter houseBreaksReport = new StreamWriter("D:\\quickhousebreaksreport.txt");
            foreach (Housebreak hbr in housebreakReport)
            {
                houseBreaksReport.WriteLine($"{hbr.Ticker.Ticker1},{hbr.BreakoutCandleDate},{hbr.MotherCandleDate},{hbr.MotherCandleHigh},{hbr.MotherCandleLow},{hbr.NumberOfCandles}");
            }
            houseBreaksReport.Close();
            MessageBox.Show("Done");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<string> IndexNames = dbAccessLayer.GetIndexList();

            dbAccessLayer.GetQuickHousebreakReportOfIndex("Nifty_50");
        }
    }
}
