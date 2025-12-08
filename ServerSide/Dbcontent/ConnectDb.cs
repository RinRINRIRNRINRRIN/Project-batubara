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
                con.Open();
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                return false;
            }
            return true;
        }

    }
}
