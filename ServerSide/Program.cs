using Serilog;
using ServerSide.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using ServerSide.Pages_Client_Site;
using ServerSide.Pages_Test;
using ServerSide.Models;
using ServerSide.Pages_Public;
namespace ServerSide
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Log.Logger = new LoggerConfiguration()
.WriteTo.File(Application.StartupPath + "\\Logs\\log-.txt", rollingInterval: RollingInterval.Day)
.WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
.CreateLogger();

            string programType = ConfigurationManager.AppSettings["ProgramType"];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            switch (programType)
            {
                case "SERVER":
                    Application.Run(new frmLoginSS());
                    //Application.Run(new frmOrderDetail());
                    break;
                case "CLIENT":
                    Application.Run(new frmLoginCS());
                    break;
            }
        }
    }
}
