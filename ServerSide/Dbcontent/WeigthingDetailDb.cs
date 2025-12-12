using Newtonsoft.Json;
using Serilog;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Dbcontent
{
    internal class WeigthingDetailDb
    {

        public WeigthingDetailDb()
        {
            this.con = ConnectDb.con;
        }

        private readonly SqlConnection con;
        public string ERR { get; set; }


        public bool AddNew(WeightDetailModel model)
        {
            try
            {
                Log.Information("==add new Weight detail");
                string jsonLog = JsonConvert.SerializeObject(model);
                Log.Information("Saved Model: " + jsonLog);
                string sql = "INSERT INTO Weighing_Detail (OrderId,DateWeighing,NetWeight,WeightType,Employee) " +
                    "VALUES (@OrderId,@DateWeighing,@NetWeight,@WeightType,@Employee)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@OrderId", model.OrderId));
                    cmd.Parameters.Add(new SqlParameter("@DateWeighing", model.DateWeighing));
                    cmd.Parameters.Add(new SqlParameter("@NetWeight", model.NetWeight));
                    cmd.Parameters.Add(new SqlParameter("@WeightType", model.WeightType));
                    cmd.Parameters.Add(new SqlParameter("@Employee", model.Employee));
                    cmd.ExecuteNonQuery();
                }
                Log.Information("Success");
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("WeighingDetailDb,AddNew : " + ERR);
                return false;
            }
            return true;
        }

        public List<WeightDetailModel> GetDetailByOrderId(int orderId)
        {
            Log.Information("== getDetailByOrderId");
            List<WeightDetailModel> list = new List<WeightDetailModel>();
            try
            {
                string sql = $"SELECT * FROM Weighing_Detail WHERE OrderId  = {orderId}";
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    DataTable tb = new DataTable();
                    da.Fill(tb);
                    if (tb.Rows.Count == 0)
                        return null;
                    else
                        foreach (DataRow rw in tb.Rows)
                        {
                            int _id = int.Parse(rw["Id"].ToString());
                            int _orderid = int.Parse(rw["OrderId"].ToString());
                            DateTime dateTime = DateTime.Parse(rw["DateWeighing"].ToString());
                            string _dateWeight = dateTime.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("TH-th"));
                            string _weightType = rw["WeightType"].ToString();
                            string _employee = rw["Employee"].ToString();
                            int netWeight = int.Parse(rw["NetWeight"].ToString());
                            WeightDetailModel weightDetailModel = new WeightDetailModel
                            {
                                Id = _id,
                                OrderId = _orderid,
                                WeightType = _weightType,
                                Employee = _employee,
                                DateWeighing = _dateWeight,
                                NetWeight = netWeight,
                            };
                            list.Add(weightDetailModel);
                        }
                    Log.Information("Success");
                }
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("WeighingDetailDb,GetDetailByOrderId : " + ERR);
                return null;
            }
            return list;
        }
    }
}
