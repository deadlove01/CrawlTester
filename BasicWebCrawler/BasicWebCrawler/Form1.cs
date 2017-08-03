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

namespace BasicWebCrawler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        HostData data = new HostData();

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            //data.GetData();
            //btnGetData.Enabled = false;
            //backgroundWorker.RunWorkerAsync("RunLoadData");
            cls_IJob.GetData(txt_Respond.Text);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //if (e.Argument.ToString().Equals("RunLoadData"))
            //{
            //    data.GetData();
            //}
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //HostData data = new HostData();
            //data.GetData();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //btnGetData.Enabled = true;
            //MessageBox.Show("updated");
        }
    }
}
