using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using BasicWebCrawler.Object;

namespace BasicWebCrawler
{
    class HostData
    {

        private string getJsonString(string url)
        {
            var json = new WebClient().DownloadString(url);
            return json.ToString();
        }

        // dsd
        private dynamic parseJson(int typeIndex) {

            string json = getJsonString(@"https://www.zacks.com/includes/classes/z2_class_calendarfunctions_data.php?calltype=eventscal&type="+typeIndex);
  
            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic d = js.Deserialize<dynamic>(json);
            return d["data"];
        }

        public void GetData() {

            List<Earnings> lstEarnings = new List<Earnings>();
            List<Sales> lstSales = new List<Sales>();
            List<Guidance> lstGuidance = new List<Guidance>();
            List<Revisions> lstRevisions = new List<Revisions>();
            List<Dividends> lstDividends = new List<Dividends>();
            List<Splits> lstSplits = new List<Splits>();
            List<Transcripts> lstTranscripts = new List<Transcripts>();

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
            }

        }

    }
}
