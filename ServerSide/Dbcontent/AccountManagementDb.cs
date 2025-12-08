using Microsoft.Win32;
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
    internal class AccountManagementDb
    {

        public AccountManagementDb()
        {
            con = ConnectDb.con;
        }

        private readonly SqlConnection con;
        public string ERR { get; set; }
        private string sql;


        public List<AccountManagementModel> GetAllAccount()
        {
            List<AccountManagementModel> list = new List<AccountManagementModel>();
            try
            {
                sql = "SELECT * FROM Account_Management";
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    DataTable tb = new DataTable();
                    da.Fill(tb);
                    if (tb.Rows.Count > 0)
                        foreach (DataRow rw in tb.Rows)
                        {
                            AccountManagementModel model = new AccountManagementModel
                            {
                                Id = int.Parse(rw["Id"].ToString()),
                                Username = rw["Username"].ToString(),
                                Password = rw["Password"].ToString(),
                                FullName = rw["FullName"].ToString(),
                                Status = rw["Status"].ToString(),
                                Position = rw["Position"].ToString()
                            };
                            list.Add(model);
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

        public AccountManagementModel GetAccountByUsernameAndPassword(string _user, string _pass)
        {
            AccountManagementModel account = new AccountManagementModel(); ;
            try
            {
                sql = $"SELECT * FROM Account_Management WHERE Username = '{_user}' and Password = '{_pass}'";
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    DataTable tb = new DataTable();
                    da.Fill(tb);
                    if (tb.Rows.Count == 0)
                        return null;
                    foreach (DataRow rw in tb.Rows)
                    {
                        account.Id = int.Parse(rw["Id"].ToString());
                        account.Username = rw["Username"].ToString();
                        account.Password = rw["Password"].ToString();
                        account.FullName = rw["FullName"].ToString();
                        account.Status = rw["Status"].ToString();
                        account.Position = rw["Position"].ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                return null;
            }
            return account;
        }



        public bool AddNew(AccountManagementModel model)
        {
            try
            {
                sql = "INSERT INTO Account_Management (Username,Password,FullName,Status,Position) " +
                    "VALUES (@Username,@Password,@FullName,@Status,@Position)";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@Username", model.Username));
                    cmd.Parameters.Add(new SqlParameter("@Password", model.Password));
                    cmd.Parameters.Add(new SqlParameter("@FullName", model.FullName));
                    cmd.Parameters.Add(new SqlParameter("@Status", model.Status));
                    cmd.Parameters.Add(new SqlParameter("@Position", model.Position));
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


        public bool UpdateAccount(AccountManagementModel model)
        {
            try
            {
                sql = "UPDATE Account_Management " +
                    "SET Username = @Username, " +
                    "Password = @Password, " +
                    "FullName = @FullName," +
                    "Status = @Status, " +
                    "Position = @Position " +
                    "WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@Username", model.Username));
                    cmd.Parameters.Add(new SqlParameter("@Password", model.Password));
                    cmd.Parameters.Add(new SqlParameter("@FullName", model.FullName));
                    cmd.Parameters.Add(new SqlParameter("@Status", model.Status));
                    cmd.Parameters.Add(new SqlParameter("@Position", model.Position));
                    cmd.Parameters.Add(new SqlParameter("@Id", model.Id));
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


        public bool Delete(int id)
        {
            try
            {
                sql = $"DELETE FROM Account_Management WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
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
