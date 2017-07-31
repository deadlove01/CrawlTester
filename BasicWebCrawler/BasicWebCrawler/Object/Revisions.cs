using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebCrawler.Object
{
    class Revisions
    {
        public string Symbol { get; set; }
        public string Company { get; set; }
        public string MarketCap { get; set; }
        public string Period { get; set; }
        public string PeriodEnd { get; set; }
        public string Old { get; set; }
        public string New { get; set; }
        public string EstChange { get; set; }
        public string Cons { get; set; }
        public string NewEstAndCons { get; set; }
    }
}
