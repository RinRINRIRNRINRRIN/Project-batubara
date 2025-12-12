using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using Serilog;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Dbcontent
{
    public class PrintDetailDb
    {


        public PrintDetailDb()
        {
            this.con = ConnectDb.con;
        }

        private readonly SqlConnection con;
        public string ERR { get; set; }


        public List<PrintDetailModel> GetDetailOrderId(int orderId)
        {
            List<PrintDetailModel> list = new List<PrintDetailModel>();
            try
            {
                Log.Information("== getDetailOrderId");
                string sql = $"SELECT * FROM PrintDetail WHERE OrderId = {orderId}";
                DataTable tb = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    da.Fill(tb);
                }

                if (tb.Rows.Count == 0)
                    return null;
                else
                    foreach (DataRow dr in tb.Rows)
                    {
                        int id = int.Parse(dr["Id"].ToString());
                        int OrderId = int.Parse(dr["OrderId"].ToString());
                        int seq = int.Parse(dr["Seq"].ToString());
                        DateTime dateTime = DateTime.Parse(dr["Datez"].ToString());
                        string emp = dr["Employee"].ToString();
                        string note = dr["Note"].ToString();

                        PrintDetailModel model = new PrintDetailModel
                        {
                            Id = id,
                            OrderId = orderId,
                            Seq = seq,
                            Employee = emp,
                            Note = note,
                            DateTimez = dateTime.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("en-EN"))
                        };
                        string _json = JsonConvert.SerializeObject(model);
                        Log.Information(_json);
                        list.Add(model);
                    }

            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("PrintDetailDb,GetDetailOrderId : " + ERR);
                return null;
            }
            return list;
        }
        public int GetSeqReport(int OrderId)
        {
            int seq = 0;
            try
            {
                string sql = $"SELECT COUNT(*) as Seq FROM PrintDetail WHERE OrderId = {OrderId}";
                DataTable tb = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    da.Fill(tb);
                }
                foreach (DataRow rw in tb.Rows)
                {
                    seq = int.Parse(rw["Seq"].ToString()) + 1;
                    break;
                }
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("PrintDetailDb,GetSeqReport : " + ERR);
                return 0;
            }
            return seq;
        }
        public bool AddNewDetail(PrintDetailModel model)
        {
            try
            {
                Log.Information("== add new detail print detail");
                string _json = JsonConvert.SerializeObject(model);
                string sql = "INSERT INTO PrintDetail (OrderId,Seq,Datez,Employee,Note) " +
                    " VALUES(@OrderId,@Seq,@Datez,@Employee,@Note)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@OrderId", model.OrderId));
                    cmd.Parameters.Add(new SqlParameter("@Seq", model.Seq));
                    cmd.Parameters.Add(new SqlParameter("@Datez", model.DateTimez));
                    cmd.Parameters.Add(new SqlParameter("@Employee", model.Employee));
                    cmd.Parameters.Add(new SqlParameter("@Note", model.Note));
                    cmd.ExecuteNonQuery();
                }
                Log.Information("success");
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("PrintDetailDb,AddNew Detail : " + ERR);
                return false;
            }
            return true;
        }
    }
}
