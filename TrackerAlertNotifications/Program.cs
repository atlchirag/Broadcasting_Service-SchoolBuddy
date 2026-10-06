using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrackerAlertNotifications
{
    class Program : ServiceBase
    {
        public Program()
        {
            this.ServiceName = "BroadcastNotificationService";
        }

        static void Main(string[] args)
        {

            //leaverequest request = new leaverequest();
     
            // ✅ Local Debug Mode (only when running directly without args)
            if (Environment.UserInteractive && args.Length == 0)
            {
                Console.WriteLine("Debug Mode: Running service as console app...");
                StartServiceDebug();
                return;
            }

            // 🛠 Install/Uninstall Mode
            if (args.Length > 0)
            {
                switch (args[0].ToUpper())
                {
                    case "/I":
                        InstallService();
                        return;
                    case "/U":
                        UninstallService();
                        return;
                }
            }

            // 🚀 Run as actual Windows service
            ServiceBase.Run(new Program());
        }

        // ✅ Debug Method (local test mode)
        private static void StartServiceDebug()
        {
            Thread t = new Thread(() => BroadcastService.BroadcastNotification.Start());
            t.Start();
            Console.WriteLine("BroadcastNotification Service Running in Console Mode... Press any key to exit.");
            Console.ReadKey();
        }

        protected override void OnStart(string[] args)
        {
            Thread t = new Thread(() => BroadcastService.BroadcastNotification.Start());
            t.Start();
            Thread.Sleep(500);
            General.WriteToLogFile("Broadcast Service Started.", AppDomain.CurrentDomain.BaseDirectory, "serviceLog.txt");
        }

        protected override void OnStop()
        {
            General.WriteToLogFile("Broadcast Service Stopped.", AppDomain.CurrentDomain.BaseDirectory, "serviceLog.txt");
        }

        private static void InstallService()
        {
            if (IsServiceInstalled())
            {
                UninstallService();
            }
            ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
        }

        private static void UninstallService()
        {
            ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
        }

        private static bool IsServiceInstalled()
        {
            return ServiceController.GetServices().Any(s => s.ServiceName == "BroadcastNotificationService");
        }
    }
}
