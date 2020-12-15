using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0vaServiceSettings
{
    [RunInstaller(true)]
    public partial class Service1 : ServiceBase
    {
        int BackTime = Convert.ToInt32(ConfigurationSettings.AppSettings["Threadinglapse"]);
        public Thread Labor = null;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                ThreadStart StartOn = new ThreadStart(Scripting);
                Labor = new Thread(StartOn);
                Labor.Start();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void Scripting()
        {
            while(true)
            {
                string DigitPath = "C:\\sample.txt";

                using (StreamWriter streamWriter = new StreamWriter(DigitPath, true))
                {
                    streamWriter.WriteLine(string.Format("Service accessed on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + ""));
                    streamWriter.Close();
                }

                Thread.Sleep(BackTime * 60 * 1000);
            }
        }

        protected override void OnStop()
        {
            try
            {
                if(Labor.IsAlive &( Labor!= null))
                {
                    Labor.Abort();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
