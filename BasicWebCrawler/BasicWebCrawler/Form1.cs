using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasicWebCrawler.Object;

using BasicWebCrawler.Object;

namespace BasicWebCrawler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HostData data = new HostData();
            string json = data.getJsonString(@"https://www.zacks.com/includes/classes/z2_class_calendarfunctions_data.php?calltype=eventscal&type=8");
            //Console.WriteLine(json);
            
            data.parseJson(8);
        }
    }
}
