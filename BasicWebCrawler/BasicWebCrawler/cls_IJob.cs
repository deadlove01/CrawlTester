using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Quartz;
using Quartz.Impl;

namespace BasicWebCrawler
{
    public class cls_IJob : IJob
    {
        //Get Scheduler
        public static void GetData(string _text)
        {
            try
            {
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                scheduler.Start();

                IJobDetail job = JobBuilder.Create<cls_IJob>()
                    .WithIdentity("job1", "group1")
                    .UsingJobData("Name", _text)
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .WithRepeatCount(3))
                    .StartNow()
                    .Build();

                scheduler.ScheduleJob(job, trigger);
                Thread.Sleep(TimeSpan.FromSeconds(30));
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
        public void Execute(IJobExecutionContext context)
        {
            var _name = context.JobDetail.JobDataMap.GetString("Name");
            WriteLogs(DateTime.Now.ToString() + ": " + _name);
        }
    }
}
