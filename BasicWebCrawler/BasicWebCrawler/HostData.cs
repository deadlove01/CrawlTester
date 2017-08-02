using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using System.Web.Script.Serialization;
using BasicWebCrawler.Object;
using EXcel = Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System.IO;

namespace BasicWebCrawler
{
    class HostData
    {

        List<Earnings> lstEarnings = new List<Earnings>();
        List<Sales> lstSales = new List<Sales>();
        List<Guidance> lstGuidance = new List<Guidance>();
        List<Revisions> lstRevisions = new List<Revisions>();
        List<Dividends> lstDividends = new List<Dividends>();
        List<Splits> lstSplits = new List<Splits>();
        List<Transcripts> lstTranscripts = new List<Transcripts>();

        private string getJsonString(string url)
        {
            var json = new WebClient().DownloadString(url);
            return json.ToString();
        }

        private dynamic parseJson(int typeIndex) {

            string json = getJsonString(@"https://www.zacks.com/includes/classes/z2_class_calendarfunctions_data.php?calltype=eventscal&type="+typeIndex);
  
            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic d = js.Deserialize<dynamic>(json);
            return d["data"];
        }

        public void GetData() {

            
            ////////////////////////////////////////////////////////////////
            // add data from json string to object
            //Earning = 1
            dynamic[] dataEarning = parseJson(1);
            foreach (dynamic[] objects in dataEarning)
            {
                Earnings earnings = new Earnings();
                earnings.Symbol = Regex.Replace(objects[0], "<.*?>", String.Empty);
                earnings.Company = Regex.Replace(objects[1], "<.*?>", String.Empty);
                earnings.MarketCap = Regex.Replace(objects[2], "<.*?>", String.Empty);
                earnings.Time = Regex.Replace(objects[3], "<.*?>", String.Empty);
                earnings.Estimate = Regex.Replace(objects[4], "<.*?>", String.Empty);
                earnings.Reported = Regex.Replace(objects[5], "<.*?>", String.Empty);
                earnings.Surprise = Regex.Replace(objects[6], "<.*?>", String.Empty);
                earnings.Surp = Regex.Replace(objects[7], "<.*?>", String.Empty);
                earnings.PriceChange = Regex.Replace(objects[8], "<.*?>", String.Empty);
                earnings.Report = Regex.Replace(objects[9], "<.*?>", String.Empty);
                lstEarnings.Add(earnings);

                exportExcel(1);
            }

            //Sales = 9
            dynamic[] dataSales = parseJson(9);
            foreach (dynamic[] objects in dataSales)
            {
                Sales sales = new Sales();
                sales.Symbol = Regex.Replace(objects[0], "<.*?>", String.Empty);
                sales.Company = Regex.Replace(objects[1], "<.*?>", String.Empty);
                sales.MarketCap = Regex.Replace(objects[2], "<.*?>", String.Empty);
                sales.Time = Regex.Replace(objects[3], "<.*?>", String.Empty);
                sales.Estimate = Regex.Replace(objects[4], "<.*?>", String.Empty);
                sales.Reported = Regex.Replace(objects[5], "<.*?>", String.Empty);
                sales.Surprise = Regex.Replace(objects[6], "<.*?>", String.Empty);
                sales.Surp = Regex.Replace(objects[7], "<.*?>", String.Empty);
                sales.PriceChange = Regex.Replace(objects[8], "<.*?>", String.Empty);
                sales.Report = Regex.Replace(objects[9], "<.*?>", String.Empty);
                lstSales.Add(sales);

                exportExcel(9);
            }

            //Guidance = 6
            dynamic[] dataGuidance = parseJson(6);
            foreach (dynamic[] objects in dataGuidance)
            {
                Guidance guidance = new Guidance();
                guidance.Symbol = Regex.Replace(objects[0], "<.*?>", String.Empty);
                guidance.Company = Regex.Replace(objects[1], "<.*?>", String.Empty);
                guidance.MarketCap = Regex.Replace(objects[2], "<.*?>", String.Empty);
                guidance.Period = Regex.Replace(objects[3], "<.*?>", String.Empty);
                guidance.PeriodEnd = Regex.Replace(objects[4], "<.*?>", String.Empty);
                guidance.GuidRange = Regex.Replace(objects[5], "<.*?>", String.Empty);
                guidance.MidGuid = Regex.Replace(objects[6], "<.*?>", String.Empty);
                guidance.Cons = Regex.Replace(objects[7], "<.*?>", String.Empty);
                guidance.ToHighPoint = Regex.Replace(objects[8], "<.*?>", String.Empty);
                lstGuidance.Add(guidance);

                exportExcel(6);
            }

            //Revisions = 3

            dynamic[] dataRevisions = parseJson(3);
            foreach (dynamic[] objects in dataRevisions)
            {
                Revisions revisions = new Revisions();
                revisions.Symbol = Regex.Replace(objects[0], "<.*?>", String.Empty);
                revisions.Company = Regex.Replace(objects[1], "<.*?>", String.Empty);
                revisions.MarketCap = Regex.Replace(objects[2], "<.*?>", String.Empty);
                revisions.Period = Regex.Replace(objects[3], "<.*?>", String.Empty);
                revisions.PeriodEnd = Regex.Replace(objects[4], "<.*?>", String.Empty);
                revisions.Old = Regex.Replace(objects[5], "<.*?>", String.Empty);
                revisions.New = Regex.Replace(objects[6], "<.*?>", String.Empty);
                revisions.EstChange = Regex.Replace(objects[7], "<.*?>", String.Empty);
                revisions.Cons = Regex.Replace(objects[8], "<.*?>", String.Empty);
                revisions.NewEstAndCons = Regex.Replace(objects[9], "<.*?>", String.Empty);
                lstRevisions.Add(revisions);

                exportExcel(3);
            }

            //Dividends = 5
            dynamic[] dataDividends = parseJson(5);
            foreach (dynamic[] objects in dataDividends)
            {
                Dividends dividends = new Dividends();
                dividends.Symbol = Regex.Replace(objects[0], "<.*?>", String.Empty);
                dividends.Company = Regex.Replace(objects[1], "<.*?>", String.Empty);
                dividends.MarketCap = Regex.Replace(objects[2], "<.*?>", String.Empty);
                dividends.Amount = Regex.Replace(objects[3], "<.*?>", String.Empty);
                dividends.yield = Regex.Replace(objects[4], "<.*?>", String.Empty);
                dividends.ExDivDate = Regex.Replace(objects[5], "<.*?>", String.Empty);
                dividends.CurrentPrice = Regex.Replace(objects[6], "<.*?>", String.Empty);
                dividends.PayableDate = Regex.Replace(objects[7], "<.*?>", String.Empty);
                lstDividends.Add(dividends);

                exportExcel(5);
            }

            //Splits = 4
            dynamic[] dataSplits = parseJson(4);
            foreach (dynamic[] objects in dataSplits)
            {
                Splits splits = new Splits();
                splits.Symbol = Regex.Replace(objects[0], "<.*?>", String.Empty);
                splits.Company = Regex.Replace(objects[1], "<.*?>", String.Empty);
                splits.MarketCap = Regex.Replace(objects[2], "<.*?>", String.Empty);
                splits.Price = Regex.Replace(objects[3], "<.*?>", String.Empty); ;
                splits.SplitFactor = Regex.Replace(objects[4], "<.*?>", String.Empty);
                lstSplits.Add(splits);

                exportExcel(4);
            }

            //Transcripts = 8
            dynamic[] dataTranscripts = parseJson(8);
            foreach (dynamic[] objects in dataTranscripts)
            {
                Transcripts transcripts = new Transcripts();
                transcripts.Symbol = Regex.Replace(objects[0], "<.*?>", String.Empty);
                transcripts.Company = Regex.Replace(objects[1], "<.*?>", String.Empty);
                transcripts.MarketCap = Regex.Replace(objects[2], "<.*?>", String.Empty);
                transcripts.Event = Regex.Replace(objects[3], "<.*?>", String.Empty); ;
                transcripts.Transcript = Regex.Replace(objects[4], "<.*?>", String.Empty);
                lstTranscripts.Add(transcripts);

                exportExcel(8);
            }
            /////////////////////////////////////////////////////////


                
            
        }


        //====================================================
        //export excel file
        public void exportExcel(int index)
        {

            string dic = @"D:\ZACKS\"+ getShortDateString(true);
            string tag;

            if (!System.IO.Directory.Exists(dic)) {

                System.IO.Directory.CreateDirectory(dic);
            }

            switch (index)
            {
                case 1:
                    tag = "Earnings";

                    System.IO.FileInfo f = new System.IO.FileInfo(dic+@"\"+tag+".xls");
                    if (f.Exists) f.Delete();

                    using (ExcelPackage ep = new ExcelPackage(f))
                    {

                        ExcelWorksheet sheet = ep.Workbook.Worksheets.Add(tag);
                        for (int i = 1; i <= 10; i++)
                        {
                            switch (i)
                            {
                                case 1:
                                    sheet.Cells[1, i].Value = "Symbol";
                                    break;
                                case 2:
                                    sheet.Cells[1, i].Value = "Company";
                                    break;
                                case 3:
                                    sheet.Cells[1, i].Value = "Market Cap";
                                    break;
                                case 4:
                                    sheet.Cells[1, i].Value = "Time";
                                    break;
                                case 5:
                                    sheet.Cells[1, i].Value = "Estimate";
                                    break;
                                case 6:
                                    sheet.Cells[1, i].Value = "Reported";
                                    break;
                                case 7:
                                    sheet.Cells[1, i].Value = "Surprise";
                                    break;
                                case 8:
                                    sheet.Cells[1, i].Value = "%Surp";
                                    break;
                                case 9:
                                    sheet.Cells[1, i].Value = "Price Change";
                                    break;
                                case 10:
                                    sheet.Cells[1, i].Value = "Report";
                                    break;
                            }

                            for (int j = 0; j < lstEarnings.Count; j++)
                            {

                                Earnings item = lstEarnings[j];
                                sheet.Cells[j + 2, 1].Value = item.Symbol;
                                sheet.Cells[j + 2, 2].Value = item.Company;
                                sheet.Cells[j + 2, 3].Value = item.MarketCap;
                                sheet.Cells[j + 2, 4].Value = item.Time;
                                sheet.Cells[j + 2, 5].Value = item.Estimate;
                                sheet.Cells[j + 2, 6].Value = item.Reported;
                                sheet.Cells[j + 2, 7].Value = item.Surprise;
                                sheet.Cells[j + 2, 8].Value = item.Surp;
                                sheet.Cells[j + 2, 9].Value = item.PriceChange;
                                sheet.Cells[j + 2, 10].Value = item.Report;
                                
                            }


                            ep.Save();

                        }

                    }

                    writeLog(tag,"updated");
                    
                    break;
                case 9:
                    tag = "Sales";

                    f = new System.IO.FileInfo(dic + @"\" + tag + ".xls");
                    if (f.Exists) f.Delete();

                    using (ExcelPackage ep = new ExcelPackage(f))
                    {

                        ExcelWorksheet sheet = ep.Workbook.Worksheets.Add(tag);
                        for (int i = 1; i <= 10; i++)
                        {
                            switch (i)
                            {
                                case 1:
                                    sheet.Cells[1, i].Value = "Symbol";
                                    break;
                                case 2:
                                    sheet.Cells[1, i].Value = "Company";
                                    break;
                                case 3:
                                    sheet.Cells[1, i].Value = "Market Cap";
                                    break;
                                case 4:
                                    sheet.Cells[1, i].Value = "Time";
                                    break;
                                case 5:
                                    sheet.Cells[1, i].Value = "Estimate";
                                    break;
                                case 6:
                                    sheet.Cells[1, i].Value = "Reported";
                                    break;
                                case 7:
                                    sheet.Cells[1, i].Value = "Surprise";
                                    break;
                                case 8:
                                    sheet.Cells[1, i].Value = "%Surp";
                                    break;
                                case 9:
                                    sheet.Cells[1, i].Value = "Price Change";
                                    break;
                                case 10:
                                    sheet.Cells[1, i].Value = "Report";
                                    break;
                            }

                            for (int j = 0; j < lstSales.Count; j++)
                            {

                                Sales item = lstSales[j];
                                sheet.Cells[j + 2, 1].Value = item.Symbol;
                                sheet.Cells[j + 2, 2].Value = item.Company;
                                sheet.Cells[j + 2, 3].Value = item.MarketCap;
                                sheet.Cells[j + 2, 4].Value = item.Time;
                                sheet.Cells[j + 2, 5].Value = item.Estimate;
                                sheet.Cells[j + 2, 6].Value = item.Reported;
                                sheet.Cells[j + 2, 7].Value = item.Surprise;
                                sheet.Cells[j + 2, 8].Value = item.Surp;
                                sheet.Cells[j + 2, 9].Value = item.PriceChange;
                                sheet.Cells[j + 2, 10].Value = item.Report;

                            }


                            ep.Save();

                        }

                    }
                    writeLog(tag, "updated");
                    break;
                case 6:
                    tag = "Guidance";

                    f = new System.IO.FileInfo(dic + @"\" + tag + ".xls");
                    if (f.Exists) f.Delete();

                    using (ExcelPackage ep = new ExcelPackage(f))
                    {

                        ExcelWorksheet sheet = ep.Workbook.Worksheets.Add(tag);
                        for (int i = 1; i <= 9; i++)
                        {
                            switch (i)
                            {
                                case 1:
                                    sheet.Cells[1, i].Value = "Symbol";
                                    break;
                                case 2:
                                    sheet.Cells[1, i].Value = "Company";
                                    break;
                                case 3:
                                    sheet.Cells[1, i].Value = "Market Cap";
                                    break;
                                case 4:
                                    sheet.Cells[1, i].Value = "Period";
                                    break;
                                case 5:
                                    sheet.Cells[1, i].Value = "Period End";
                                    break;
                                case 6:
                                    sheet.Cells[1, i].Value = "Guid Range";
                                    break;
                                case 7:
                                    sheet.Cells[1, i].Value = "MidGuid Cons";
                                    break;
                                case 8:
                                    sheet.Cells[1, i].Value = "Cons";
                                    break;
                                case 9:
                                    sheet.Cells[1, i].Value = "%to High Point";
                                    break;

                            }

                            for (int j = 0; j < lstGuidance.Count; j++)
                            {

                                Guidance item = lstGuidance[j];
                                sheet.Cells[j + 2, 1].Value = item.Symbol;
                                sheet.Cells[j + 2, 2].Value = item.Company;
                                sheet.Cells[j + 2, 3].Value = item.MarketCap;
                                sheet.Cells[j + 2, 4].Value = item.Period;
                                sheet.Cells[j + 2, 5].Value = item.PeriodEnd;
                                sheet.Cells[j + 2, 6].Value = item.GuidRange;
                                sheet.Cells[j + 2, 7].Value = item.MidGuid;
                                sheet.Cells[j + 2, 8].Value = item.Cons;
                                sheet.Cells[j + 2, 9].Value = item.ToHighPoint;


                            }


                            ep.Save();

                        }

                    }
                    writeLog(tag, "updated");
                    break;
                case 3:
                    tag = "Revisions";

                    f = new System.IO.FileInfo(dic + @"\" + tag + ".xls");
                    if (f.Exists) f.Delete();

                    using (ExcelPackage ep = new ExcelPackage(f))
                    {

                        ExcelWorksheet sheet = ep.Workbook.Worksheets.Add(tag);
                        for (int i = 1; i <= 10; i++)
                        {
                            switch (i)
                            {
                                case 1:
                                    sheet.Cells[1, i].Value = "Symbol";
                                    break;
                                case 2:
                                    sheet.Cells[1, i].Value = "Company";
                                    break;
                                case 3:
                                    sheet.Cells[1, i].Value = "Market Cap";
                                    break;
                                case 4:
                                    sheet.Cells[1, i].Value = "Period";
                                    break;
                                case 5:
                                    sheet.Cells[1, i].Value = "Period End";
                                    break;
                                case 6:
                                    sheet.Cells[1, i].Value = "Old";
                                    break;
                                case 7:
                                    sheet.Cells[1, i].Value = "New";
                                    break;
                                case 8:
                                    sheet.Cells[1, i].Value = "Est Change";
                                    break;
                                case 9:
                                    sheet.Cells[1, i].Value = "Cons";
                                    break;
                                case 10:
                                    sheet.Cells[1, i].Value = "New Est vs Cons";
                                    break;
                            }

                            for (int j = 0; j < lstRevisions.Count; j++)
                            {

                                Revisions item = lstRevisions[j];
                                sheet.Cells[j + 2, 1].Value = item.Symbol;
                                sheet.Cells[j + 2, 2].Value = item.Company;
                                sheet.Cells[j + 2, 3].Value = item.MarketCap;
                                sheet.Cells[j + 2, 4].Value = item.Period;
                                sheet.Cells[j + 2, 5].Value = item.PeriodEnd;
                                sheet.Cells[j + 2, 6].Value = item.Old;
                                sheet.Cells[j + 2, 7].Value = item.New;
                                sheet.Cells[j + 2, 8].Value = item.EstChange;
                                sheet.Cells[j + 2, 9].Value = item.Cons;
                                sheet.Cells[j + 2, 10].Value = item.NewEstAndCons;

                            }


                            ep.Save();

                        }

                    }
                    writeLog(tag, "updated");
                    break;
                case 5:
                    tag = "Dividends";

                    f = new System.IO.FileInfo(dic + @"\" + tag + ".xls");
                    if (f.Exists) f.Delete();

                    using (ExcelPackage ep = new ExcelPackage(f))
                    {

                        ExcelWorksheet sheet = ep.Workbook.Worksheets.Add(tag);
                        for (int i = 1; i <= 8; i++)
                        {
                            switch (i)
                            {
                                case 1:
                                    sheet.Cells[1, i].Value = "Symbol";
                                    break;
                                case 2:
                                    sheet.Cells[1, i].Value = "Company";
                                    break;
                                case 3:
                                    sheet.Cells[1, i].Value = "Market Cap";
                                    break;
                                case 4:
                                    sheet.Cells[1, i].Value = "Amount";
                                    break;
                                case 5:
                                    sheet.Cells[1, i].Value = "Yield";
                                    break;
                                case 6:
                                    sheet.Cells[1, i].Value = "Ex-Div Date";
                                    break;
                                case 7:
                                    sheet.Cells[1, i].Value = "Current Price";
                                    break;
                                case 8:
                                    sheet.Cells[1, i].Value = "PayaBle";
                                    break;
                               

                            }

                            for (int j = 0; j < lstDividends.Count; j++)
                            {

                                Dividends item = lstDividends[j];
                                sheet.Cells[j + 2, 1].Value = item.Symbol;
                                sheet.Cells[j + 2, 2].Value = item.Company;
                                sheet.Cells[j + 2, 3].Value = item.MarketCap;
                                sheet.Cells[j + 2, 4].Value = item.Amount;
                                sheet.Cells[j + 2, 5].Value = item.yield;
                                sheet.Cells[j + 2, 6].Value = item.ExDivDate;
                                sheet.Cells[j + 2, 7].Value = item.CurrentPrice;
                                sheet.Cells[j + 2, 8].Value = item.PayableDate;

                            }


                            ep.Save();

                        }

                    }

                    writeLog(tag, "updated");
                    break;
                case 4:
                    tag = "Splits";

                    f = new System.IO.FileInfo(dic + @"\" + tag + ".xls");
                    if (f.Exists) f.Delete();

                    using (ExcelPackage ep = new ExcelPackage(f))
                    {

                        ExcelWorksheet sheet = ep.Workbook.Worksheets.Add(tag);
                        for (int i = 1; i <= 5; i++)
                        {
                            switch (i)
                            {
                                case 1:
                                    sheet.Cells[1, i].Value = "Symbol";
                                    break;
                                case 2:
                                    sheet.Cells[1, i].Value = "Company";
                                    break;
                                case 3:
                                    sheet.Cells[1, i].Value = "Market Cap";
                                    break;
                                case 4:
                                    sheet.Cells[1, i].Value = "Price";
                                    break;
                                case 5:
                                    sheet.Cells[1, i].Value = "Split Factor";
                                    break;

                            }

                            for (int j = 0; j < lstSplits.Count; j++)
                            {

                                Splits item = lstSplits[j];
                                sheet.Cells[j + 2, 1].Value = item.Symbol;
                                sheet.Cells[j + 2, 2].Value = item.Company;
                                sheet.Cells[j + 2, 3].Value = item.MarketCap;
                                sheet.Cells[j + 2, 4].Value = item.Price;
                                sheet.Cells[j + 2, 5].Value = item.SplitFactor;


                            }


                            ep.Save();

                        }

                    }

                    writeLog(tag, "updated");
                    break;
                case 8:
                    tag = "Transcripts";


                    f = new System.IO.FileInfo(dic + @"\" + tag + ".xls");
                    if (f.Exists) f.Delete();

                    using (ExcelPackage ep = new ExcelPackage(f))
                    {

                        ExcelWorksheet sheet = ep.Workbook.Worksheets.Add(tag);
                        for (int i = 1; i <= 5; i++)
                        {
                            switch (i)
                            {
                                case 1:
                                    sheet.Cells[1, i].Value = "Symbol";
                                    break;
                                case 2:
                                    sheet.Cells[1, i].Value = "Company";
                                    break;
                                case 3:
                                    sheet.Cells[1, i].Value = "Market Cap";
                                    break;
                                case 4:
                                    sheet.Cells[1, i].Value = "Event";
                                    break;
                                case 5:
                                    sheet.Cells[1, i].Value = "Transcript";
                                    break;
                                
                            }

                            for (int j = 0; j < lstTranscripts.Count; j++)
                            {

                                Transcripts item = lstTranscripts[j];
                                sheet.Cells[j + 2, 1].Value = item.Symbol;
                                sheet.Cells[j + 2, 2].Value = item.Company;
                                sheet.Cells[j + 2, 3].Value = item.MarketCap;
                                sheet.Cells[j + 2, 4].Value = item.Event;
                                sheet.Cells[j + 2, 5].Value = item.Transcript;
                                
                            }


                            ep.Save();

                        }

                    }

                    writeLog(tag, "updated");
                    break;
            }




        }
        //=====================================================
        // get string date
        public string getShortDateString(bool flag)
        {
            if(flag == true )
            {
                return DateTime.Now.ToString("ddMMyyyy");
            }else
            {
                return DateTime.Now.ToString("dd-MM-yyyy-HH:mm:ss");
            }
        }

        //====================================================
        // write logfile
        public void writeLog(string objects, string status)
        {
            string dic = @"D:\ZACKS\log";

            if (!Directory.Exists(dic))
            {
                Directory.CreateDirectory(dic);
            }
            StreamWriter file = new StreamWriter(dic+@"\logs.txt",true);

            file.WriteLine(getShortDateString(false)+": "+objects+" => "+status);
            file.Close();
        }

    }

}
