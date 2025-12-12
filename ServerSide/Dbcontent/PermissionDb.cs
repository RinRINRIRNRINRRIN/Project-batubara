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
    internal class PermissionDb
    {
        public PermissionDb()
        {
            con = ConnectDb.con;
        }

        private readonly SqlConnection con;
        public string ERR { get; set; }
        private string sql;


        public List<PermissionModel> GetAllPermissionByAccountId(int id)
        {
            Log.Information("== get permissionById");
            Log.Information("ID : " + id);
            List<PermissionModel> list = new List<PermissionModel>();
            try
            {
                sql = $"SELECT * FROM Permission WHERE AccountId = {id}";
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    DataTable tb = new DataTable();
                    da.Fill(tb);
                    if (tb.Rows.Count > 0)
                    {
                        foreach (DataRow item in tb.Rows)
                        {
                            PermissionModel permissionModel = new PermissionModel
                            {
                                Id = int.Parse(item["Id"].ToString()),
                                AccountId = int.Parse(item["AccountId"].ToString()),
                                Menuname = item["MenuName"].ToString(),
                                MenuType = item["MenuType"].ToString()
                            };
                            string _json = JsonConvert.SerializeObject(permissionModel);
                            Log.Information(_json);
                            list.Add(permissionModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("PermissionDb,GetAllPermissionByAccountId : " + ERR);
                return null;
            }
            return list;
        }

        public bool AddNew(List<PermissionModel> models)
        {
            try
            {
                Log.Information("== add permission new");
                string _json = JsonConvert.SerializeObject(models);
                Log.Information(_json);
                foreach (var item in models)
                {
                    sql = "INSERT INTO Permission (AccountId,MenuName,MenuType) " +
                        "VALUES(@AccountId,@MenuName,@MenuType)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@AccountId", item.AccountId));
                        cmd.Parameters.Add(new SqlParameter("@MenuName", item.Menuname));
                        cmd.Parameters.Add(new SqlParameter("@MenuType", item.MenuType));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("PermissionDb,AddNew : " + ERR);
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                Log.Information("== delete permission ");
                sql = $"DELETE FROM Permission WHERE AccountId = {id}";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
                Log.Information("success");
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("PermissionDb,Delete : " + ERR);
                return false;
            }
            return true;
        }
    }
}
