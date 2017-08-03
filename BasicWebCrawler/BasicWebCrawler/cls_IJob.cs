using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Quartz;
using Quartz.Impl;

namespace BasicWebCrawler
{
    public class cls_IJob
    {
        public static void GetData(string _text)
        {
            try
            {
                // Grab the Scheduler instance from the Factory 
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                // and start it off
                scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .UsingJobData("Name", _text)
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

        //Write Logs files on Debug folder
        static void WriteLogs(string _text)
        {
            StreamWriter _sw = new StreamWriter(Application.StartupPath + @"\logs_test.txt", true);

            _sw.WriteLine(_text);
            _sw.Close();
        }

        //Create class inheritance
        class HelloJob : IJob
        {
            public void Execute(IJobExecutionContext context)
            {
                var _name = context.JobDetail.JobDataMap.GetString("Name");
                WriteLogs(DateTime.Now.ToString() + ": " + _name);
            }
        }
    }
}
