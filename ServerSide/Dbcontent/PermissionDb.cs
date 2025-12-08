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
                            list.Add(permissionModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                return null;
            }
            return list;
        }

        public bool AddNew(List<PermissionModel> models)
        {
            try
            {
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
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                sql = $"DELETE FROM Permission WHERE AccountId = {id}";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
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
