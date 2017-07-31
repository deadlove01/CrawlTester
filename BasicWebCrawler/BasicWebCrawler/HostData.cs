using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace BasicWebCrawler
{
    class HostData
    {
        public string getJsonString(string url)
        {
            var json = new WebClient().DownloadString(url);
            return json.ToString();
        }

    }
}
