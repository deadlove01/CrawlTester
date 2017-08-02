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
using System.IO;
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

        void WriteLogs(string _text)
        {
            StreamWriter _sw = new StreamWriter(Application.StartupPath + @"\logs_test.txt", true);

            _sw.WriteLine(_text);
            _sw.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            //data.GetData();
            //btnGetData.Enabled = false;
            //backgroundWorker.RunWorkerAsync("RunLoadData");
            try
            {
                // Grab the Scheduler instance from the Factory 
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                // and start it off
                scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .UsingJobData("Name", txt_Respond.Text)
                    .Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .WithRepeatCount(3))
                    .StartNow()
                    .Build();

                // Tell quartz to schedule the job using our trigger
                scheduler.ScheduleJob(job, trigger);

                // some sleep to show what's happening
                Thread.Sleep(TimeSpan.FromSeconds(30));

                // and last shut down the scheduler when you are ready to close your program
                scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
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

        public class HelloJob : Form1, IJob
        {
            public void Execute(IJobExecutionContext context)
            {
                var name = context.JobDetail.JobDataMap.GetString("Name");
                WriteLogs(DateTime.Now.ToString() + ": " + name);
            }
        }
    }
}
