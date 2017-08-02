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
using Quartz;
using System.Threading;
using Quartz.Impl;

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
            try
            {
                // Grab the Scheduler instance from the Factory 
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                // and start it off
                scheduler.Start();

                // some sleep to show what's happening
                Thread.Sleep(TimeSpan.FromSeconds(60));

                // and last shut down the scheduler when you are ready to close your program
                scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD

            //data.GetData();
            btnGetData.Enabled = false;
            backgroundWorker.RunWorkerAsync("RunLoadData");
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument.ToString().Equals("RunLoadData"))
            {
                
                data.GetData();
                
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

=======
            HostData data = new HostData();
            data.GetData();
>>>>>>> 4993ab68b78cae98f3deeee243272b419abe4d53
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnGetData.Enabled = true;
            MessageBox.Show("updated");
        }
    }
}
