using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Dbcontent
{
    internal class ConnectDb
    {
        public static SqlConnection con = new SqlConnection();
        public static string ERR { get; set; }
        public static bool Connect()
        {
            try
            {
                Log.Information("==Try to connect database");
                string host = ConfigurationManager.AppSettings["HOST"];
                string port = ConfigurationManager.AppSettings["PORT"];
                string db = ConfigurationManager.AppSettings["DB"];
                string user = ConfigurationManager.AppSettings["USER"];
                string pass = ConfigurationManager.AppSettings["PASS"];
                if (port == "" || port == null)
                {
                    con = new SqlConnection($"Server={host};Database={db};User Id={user};Password={pass};");
                }
                else
                {
                    con = new SqlConnection($"Server={host},{port};Database={db};User Id={user};Password={pass};");
                }
                Log.Information("Connection string : " + con.ConnectionString);
                con.Open();
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("ConnectDb,Connect : " + ERR);
                return false;
            }
            return true;
        }

    }
}
