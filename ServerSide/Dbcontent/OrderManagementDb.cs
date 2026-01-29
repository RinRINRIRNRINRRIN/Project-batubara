
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
using System.Web.UI;

namespace ServerSide.Dbcontent
{
    internal class OrderManagementDb
    {
        public OrderManagementDb()
        {
            con = ConnectDb.con;
        }


        private readonly SqlConnection con;
        public string ERR { get; set; }
        private string sql;


        List<OrderManageModel> defineModelList(DataTable tb)
        {
            List<OrderManageModel> lists = new List<OrderManageModel>();
            try
            {
                foreach (DataRow dr in tb.Rows)
                {
                    OrderManageModel model = new OrderManageModel
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        OrderNumber = dr["OrderNumber"].ToString(),
                        JobNumber = dr["JobNumber"].ToString(),
                        WeightType = dr["WeightType"].ToString(),
                        PoBuy = dr["POBuy"].ToString(),
                        PoSale = dr["POSale"].ToString(),
                        SuppireName = dr["SuppireName"].ToString(),
                        CustomerName = dr["CustomerName"].ToString(),
                        ProductNamez = dr["ProductName"].ToString(),
                        RawMatName = dr["RawMatName"].ToString(),
                        StartStationName = dr["StartStationName"].ToString(),
                        StartStationType = dr["StartStationType"].ToString(),
                        EndStationName = dr["EndStationName"].ToString(),
                        EndStationType = dr["EndStationType"].ToString(),
                        TransportName = dr["TransportName"].ToString(),
                        LIcensePlate = dr["LicensePlate"].ToString(),
                        DriverName = dr["DriverName"].ToString(),
                        DateCreate = DateTime.Parse(dr["DateCreate"].ToString()).ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("TH-th")),
                        Status = dr["Status"].ToString(),
                        VerifyStatus = dr["VerifyStatus"].ToString(),
                        EmployeeCreate = dr["EmployeeCreate"].ToString(),
                        ReferenceNumber = dr["ReferenceNumber"].ToString(),
                        OutSideFirstWeight = int.Parse(dr["OutSideFirstWeight"].ToString()),
                        OutSideSecondWeight = int.Parse(dr["OutSideSecondWeight"].ToString()),
                        OutSideNetWeight = int.Parse(dr["OutSideNetWeight"].ToString()),
                        EmplpoyeeVerify = dr["EmployeeVerify"].ToString(),
                        Remark = dr["Remark"].ToString(),
                        QcCode = dr["QC_code"].ToString()
                    };

                    lists.Add(model);
                }
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("OrderManagementDb,definModelList : " + ERR);
                return null;
            }
            return lists;
        }


        /// <summary>
        /// สำหรับคืนค่าให้เมื่อ  UpdateOrderAndGetOrderId  สำเร็จ
        /// </summary>
        public string getOrderNumber { get; set; }


        /// <summary>
        /// สำหรับอัพเดทเลขที่ออเดอร์หลังจากกดบันทึก Firstweight
        /// </summary>
        /// <param name="id"></param>
        /// <returns>เลขที่ order string </returns>

        public bool UpdateOrderAndGetOrderId(int id, string remark)
        {
            string orderNumber = "";
            try
            {
                Log.Information("== อัพเดทเลขที่ ordernumber หลังจากมีการชั่ง first weight");
                string yy = DateTime.Now.ToString("yy", System.Globalization.CultureInfo.CreateSpecificCulture("EN-en"));
                string MM = DateTime.Now.ToString("MM", System.Globalization.CultureInfo.CreateSpecificCulture("EN-en"));
                string dd = DateTime.Now.ToString("dd", System.Globalization.CultureInfo.CreateSpecificCulture("EN-en"));

                sql = $"SELECT * FROM Order_Management WHERE OrderNumber LIKE 'ORW{yy}{MM}{dd}%'";
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    DataTable tb = new DataTable();
                    int seq = 0;
                    da.Fill(tb);
                    seq = tb.Rows.Count + 1;
                    if (tb.Rows.Count == 0)
                        orderNumber = $"ORW{yy}{MM}{dd}-1";
                    else
                        orderNumber = $"ORW{yy}{MM}{dd}-{seq}";
                }

                Log.Information("gen orderNumber : " + orderNumber);
                Log.Information("Id PK : " + id);
                sql = "UPDATE Order_Management " +
                    "SET OrderNumber = @OrderNumber," +
                    "Remark = @Remark " +
                    "WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@OrderNumber", orderNumber));
                    cmd.Parameters.Add(new SqlParameter("@Remark", remark));
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    cmd.ExecuteNonQuery();
                }

                getOrderNumber = orderNumber;
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("OrderManagementDb,UpdateOrderAndGetOrderId : " + ERR);
                return false;
            }
            return true;
        }


        /// <summary>
        /// update สถานะการชั่ง
        /// </summary>
        /// <param name="id">เลขที่ order id(AI)</param>
        /// <param name="status">Planning,Cancel,Process,Success</param>
        /// <returns></returns>
        public bool UpdateStatus(int id, string status)
        {
            try
            {
                string sql = "UPDATE Order_Management " +
                    "SET Status = @Status " +
                    "WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add(new SqlParameter("Status", status));
                    cmd.Parameters.Add(new SqlParameter("Id", id));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("OrderManagementDb,UpdateStatus : " + ERR);
                return false;
            }
            return true;
        }

        /// <summary>
        /// สำหรับอัพเดทสถานะ verify หลังจากมีการยืนยันแล้ว
        /// </summary>
        /// <param name="id">id order</param>
        /// <param name="status">สถานะ Wait , Not Check,Approve</param>
        /// <returns></returns>
        public bool UpdateVerifyStatus(int id, string status, string employeeVerify)
        {
            try
            {
                string sql = "UPDATE Order_Management " +
                    "SET VerifyStatus = @VerifyStatus," +
                    "EmployeeVerify = @EmployeeVerify " +
                    "WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add(new SqlParameter("VerifyStatus", status));
                    cmd.Parameters.Add(new SqlParameter("EmployeeVerify", employeeVerify));
                    cmd.Parameters.Add(new SqlParameter("Id", id));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("OrderManagementDb,UpdateVerifyStatus : " + ERR);
                return false;
            }
            return true;
        }

        /// <summary>
        /// update เลขที่การชั่งจากแท่นภายนอก
        /// </summary>
        /// <param name="id">เลขที่ order id(AI)</param>
        /// <param name="referenceNumber">เลขที่ชั่งจากแท่นภายนอก</param>
        /// <returns></returns>
        public bool UpdateReferenceNumber(int id, string referenceNumber, int outFirst, int outSecond, int outNet)
        {
            try
            {
                string sql = "UPDATE Order_Management " +
                   "SET ReferenceNumber = @ReferenceNumber," +
                   "OutSideFirstWeight = @OutSideFirstWeight, " +
                   "OutSideSecondWeight = @OutSideSecondWeight, " +
                   "OutSideNetWeight = @OutSideNetWeight  " +
                   "WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add(new SqlParameter("ReferenceNumber", referenceNumber));
                    cmd.Parameters.Add(new SqlParameter("OutSideFirstWeight", outFirst));
                    cmd.Parameters.Add(new SqlParameter("OutSideSecondWeight", outSecond));
                    cmd.Parameters.Add(new SqlParameter("OutSideNetWeight", outNet));
                    cmd.Parameters.Add(new SqlParameter("Id", id));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("OrderManagementDb,UpdateReferenceNumber : " + ERR);
                return false;
            }
            return true;
        }

        /// <summary>
        /// อัพเดทน้ำหนักสุทธิหลังจากชั่งเสร็จ
        /// </summary>
        /// <param name="id">เลขที่ order id(AI)</param>
        /// <param name="netWeight">น้ำหนัก</param>
        /// <returns></returns>
        public bool UpdateNetWeight(int id, int netWeight)
        {
            try
            {
                string sql = "UPDATE Order_Management " +
                    "SET NetWeight = @NetWeight " +
                    "WHERE Id =  @Id";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@NetWeight", netWeight));
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("OrderManagementDb,UpdateNetWeight : " + ERR);
                return false;
            }
            return true;
        }

        /// <summary>
        /// แสดงรายการที่เป็น Planning or Process
        /// </summary>
        /// <returns></returns>
        public List<OrderManageModel> GetAllDatePlanning()
        {
            List<OrderManageModel> list = new List<OrderManageModel>();
            try
            {
                Log.Information("== get jobnumber status is planning");
                sql = "SELECT * FROM Order_Management WHERE Status = 'Planning' or Status = 'Process'";
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    DataTable tb = new DataTable();
                    da.Fill(tb);
                    if (tb != null && tb.Rows.Count > 0)
                        list = defineModelList(tb);
                }
                string _json = JsonConvert.SerializeObject(list);
                Log.Information(_json);
                Log.Information("get success");
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("OrderManagementDb,GetAllDatePlanning : " + ERR);
                return null;
            }
            return list;
        }

        /// <summary>
        /// แสดงข้อมูล order ที่ยังไม่ผ่านการ Verify
        /// </summary>
        /// <returns></returns>
        public List<OrderManageModel> getOrderVerifyIsNotSuccess()
        {
            List<OrderManageModel> lists = new List<OrderManageModel>();
            try
            {
                Log.Information("== get order is not verify yet");
                string sql = "SELECT * FROM Order_Management " +
                    "WHERE OrderNumber != 'NULL' and Status = 'Success' and VerifyStatus ='Wait'  and StartStationType = 'OUTSIDE' or EndStationType = 'OUTSIDE'";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    DataTable tb = new DataTable();
                    da.Fill(tb);
                    if (tb != null && tb.Rows.Count > 0)
                        lists = defineModelList(tb);
                }
                Log.Information("success");
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("OrderManagementDb,getOrderVerifyIsNotSuccess : " + ERR);
                return null;
            }
            return lists;
        }


        /// <summary>
        /// สำหรับเพิ่มข้อมูลใหม่จาก Excel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddNewOrderNumberForPlanning(ImportExcelModel model)
        {
            try
            {
                Log.Information("== addNewOrderForPlanning");
                Log.Information("JobNumber : " + model.JobNumber);
                Log.Information("LicensePlate : " + model.LIcensePlate);
                Log.Information("Weight Type : " + model.WeightType);
                Log.Information("RawMatName : " + model.RawMatName);
                sql = "INSERT INTO Order_Management (JobNumber,WeightType,POBuy,POSale,SuppireName,CustomerName,ProductName,RawMatName,StartStationName,StartStationType,EndStationName,EndStationType,TransportName,LicensePlate,DriverName,DateCreate,Status,EmployeeCreate,ReferenceNumber,OutSideFirstWeight,OutSideSecondWeight,OutSideNetWeight,NetWeight,VerifyStatus,Remark,QC_code) " +
                    "VALUES(@JobNumber,@WeightType,@POBuy,@POSale,@SuppireName,@CustomerName,@ProductName,@RawMatName,@StartStationName,@StartStationType,@EndStationName,@EndStationType,@TransportName,@LicensePlate,@DriverName,@DateCreate,@Status,@EmployeeCreate,@ReferenceNumber,@OutSideFirstWeight,@OutSideSecondWeight,@OutSideNetWeight,@NetWeight,@VerifyStatus,@Remark,@QC_code)";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@JobNumber", model.JobNumber));
                    cmd.Parameters.Add(new SqlParameter("@WeightType", model.WeightType));
                    cmd.Parameters.Add(new SqlParameter("@POBuy", model.PoBuy));
                    cmd.Parameters.Add(new SqlParameter("@POSale", model.PoSale));
                    cmd.Parameters.Add(new SqlParameter("@SuppireName", model.SuppireName));
                    cmd.Parameters.Add(new SqlParameter("@CustomerName", model.CustomerName));
                    cmd.Parameters.Add(new SqlParameter("@ProductName", model.Productname));
                    cmd.Parameters.Add(new SqlParameter("@RawMatName", model.RawMatName));
                    cmd.Parameters.Add(new SqlParameter("@StartStationName", model.StartStationName));
                    cmd.Parameters.Add(new SqlParameter("@StartStationType", model.StartStationType));
                    cmd.Parameters.Add(new SqlParameter("@EndStationName", model.EndStationName));
                    cmd.Parameters.Add(new SqlParameter("@EndStationType", model.EndStationType));
                    cmd.Parameters.Add(new SqlParameter("@TransportName", model.TransportName));
                    cmd.Parameters.Add(new SqlParameter("@LicensePlate", model.LIcensePlate));
                    cmd.Parameters.Add(new SqlParameter("@DriverName", model.DriverName));
                    cmd.Parameters.Add(new SqlParameter("@DateCreate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("EN-en"))));
                    cmd.Parameters.Add(new SqlParameter("@Status", "Planning"));
                    if (model.StartStationType == "OUTSIDE" || model.EndStationType == "OUTSIDE")
                        cmd.Parameters.Add(new SqlParameter("@VerifyStatus", "Wait"));
                    else
                        cmd.Parameters.Add(new SqlParameter("@VerifyStatus", "Not Check"));
                    cmd.Parameters.Add(new SqlParameter("@EmployeeCreate", model.EmployeeCreate));
                    cmd.Parameters.Add(new SqlParameter("@ReferenceNumber", ""));
                    cmd.Parameters.Add(new SqlParameter("@OutSideFirstWeight", "0"));
                    cmd.Parameters.Add(new SqlParameter("@OutSideSecondWeight", "0"));
                    cmd.Parameters.Add(new SqlParameter("@OutSideNetWeight", "0"));
                    cmd.Parameters.Add(new SqlParameter("@NetWeight", "0"));
                    cmd.Parameters.Add(new SqlParameter("@Remark", ""));
                    cmd.Parameters.Add(new SqlParameter("@QC_code", model.QC_code));
                    cmd.ExecuteNonQuery();
                }
                Log.Information("success");
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("OrderManagementDb,AddNewOrderNumberForPlanning : " + ERR);
                return false;
            }
            return true;
        }

        /// <summary>
        /// ดึงข้อมูลการชั่งใช้เวลา
        /// </summary>
        /// <param name="dateStrat">รับ ค.ศ (yyyy-MM-dd HH:mm:ss)</param>
        /// <param name="dateEnd">รับ ค.ศ (yyyy-MM-dd HH:mm:ss)</param>
        /// <param name="status">สถานะ (Planning,Success,Process,All)</param>
        /// <returns></returns>
        public List<OrderManageModel> GetOrderDate(string dateStrat, string dateEnd, string status)
        {
            List<OrderManageModel> list = new List<OrderManageModel>();
            try
            {
                string sql = "";
                if (status == "All")
                    sql = $"SELECT * FROM Order_Management WHERE DateCreate BETWEEN '{dateStrat}' and '{dateEnd}'";
                else
                    sql = $"SELECT * FROM Order_Management WHERE DateCreate BETWEEN '{dateStrat}' and '{dateEnd}' and Status = '{status}'";

                DataTable tb = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    da.Fill(tb);
                }
                if (tb != null && tb.Rows.Count > 0)
                    list = defineModelList(tb);
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("OrderManagementDb,GetOrderDate : " + ERR);
                return null;
            }
            return list;
        }


        /// <summary>
        /// สำหรับค้นหาข้อมูลที่หน้าประวัติการชั่งเท่านั้น
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<OrderManageModel> GetOrderAnyQuery(string dateStrat, string dateEnd, string status, string licensePlate, string orderNumber, string jobNumber)
        {
            List<OrderManageModel> list = new List<OrderManageModel>();
            try
            {
                string sql = $"SELECT * FROM Order_Management WHERE DateCreate BETWEEN '{dateStrat}' and '{dateEnd}' ";
                if (status != "-- ไม่เลือก --")
                    sql += $"and Status = '{status}'";
                if (orderNumber != "")
                    sql += $"and OrderNumber = '{orderNumber}'";
                if (jobNumber != "")
                    sql += $"and JobNumber = '{jobNumber}'";
                if (licensePlate != "")
                    sql += $"and LicensePlate LIKE '%{licensePlate}%'";

                DataTable tb = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    da.Fill(tb);
                }
                if (tb != null && tb.Rows.Count > 0)
                    list = defineModelList(tb);
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("OrderManagementDb,GetOrderAnyQuery : " + ERR);
                return null;
            }
            return list;
        }

        /// <summary>
        /// สำหรับใช้ export xlsx เท่านั้น
        /// </summary>
        /// <param name="dateStrat"></param>
        /// <param name="dateEnd"></param>
        /// <param name="status"></param>
        /// <param name="licensePlate"></param>
        /// <param name="orderNumber"></param>
        /// <param name="jobNumber"></param>
        /// <returns></returns>
        public DataTable getOrderByQuery(string dateStrat, string dateEnd, string status, string licensePlate, string orderNumber, string jobNumber)
        {
            DataTable tb = new DataTable();
            try
            {

                string sql = "SELECT " +
                     " a.OrderNumber as 'เลขที่ชั่ง'," +
                     " a.JobNumber as 'เลขที่งาน'," +
                     " a.WeightType as 'ประเภทชั่ง'," +
                     " a.POBuy as 'เลขที่ใบสั่งซื้อ'," +
                     " a.POSale as 'เลขที่ใบสั่งขาย'," +
                     " a.SuppireName as 'ชื่อซัพพลายเออร์'," +
                     " a.CustomerName as 'ชื่อลูกค้า', " +
                     " a.ProductName as 'ชื่อสินค้า'," +
                     " a.RawMatName as 'ชื่อวัตถุดิบ', " +
                     " a.StartStationName as 'สถานีชั่งครั้งที่หนึ่ง'," +
                     " a.StartStationType as 'ประเภทแท่นชั่งครั้งที่หนึ่ง', " +
                     " a.EndStationName as 'สถานีชั่งครั้งที่สอง', " +
                     " a.EndStationType as 'ประเภทแท่นชั่งครั้งที่สอง', " +
                     " a.TransportName as 'ชื่อขนส่ง', " +
                     " a.LicensePlate as 'ทะเบียนรถ', " +
                     " a.DriverName as 'ชื่อคนขับ', " +
                     " a.DateCreate as 'วันที่สร้างรายการ', " +
                     " a.Status as 'สถานะรายการชั่ง', " +
                     " a.VerifyStatus as 'สถานะตรวจสอบ', " +
                     " a.EmployeeCreate as 'พนักงานสร้างรายการ', " +
                     " a.EmployeeVerify as 'พนักงานตรวจสอบ', " +
                     " a.ReferenceNumber as 'หมายเลขอ้างอิงจากแท่นอื่น', " +
                     " a.OutSideFirstWeight as 'น้ำหนักชั่งครั้งที่หนึ่งแท่นภายนอก', " +
                     " a.OutSideSecondWeight as 'น้ำหนักชั่งครั้งที่สองแท่นภายนอก', " +
                     " a.OutSideNetWeight as 'น้ำหนักสุทธิแท่นภายนอก', " +
                     " ( " +
                     " SELECT TOP 1 FORMAT(b.DateWeighing, 'yyyy-MM-dd') " +
                     " FROM Weighing_Detail b WHERE b.OrderId = a.Id ORDER BY b.DateWeighing ASC" +
                     " ) AS 'วันที่ชั่งเข้า'," +
                     " ( " +
                     " SELECT TOP 1 FORMAT(b.DateWeighing, 'HH:mm:ss') " +
                     " FROM Weighing_Detail b WHERE b.OrderId = a.Id ORDER BY b.DateWeighing ASC" +
                     " ) AS 'เวลาชั่งเข้า'," +
                     " (" +
                     " SELECT FORMAT(b.DateWeighing, 'yyyy-MM-dd')" +
                     " FROM Weighing_Detail b WHERE b.OrderId = a.Id ORDER BY b.DateWeighing ASC " +
                     " OFFSET 1 ROWS FETCH NEXT 1 ROW ONLY" +
                     " ) AS 'วันที่ชั่งออก'," +
                     " (" +
                     " SELECT FORMAT(b.DateWeighing, 'HH:mm:ss')" +
                     " FROM Weighing_Detail b WHERE b.OrderId = a.Id ORDER BY b.DateWeighing ASC " +
                     " OFFSET 1 ROWS FETCH NEXT 1 ROW ONLY" +
                     " ) AS 'เวลาชั่งออก'," +
                     " (" +
                     " SELECT TOP 1 b.NetWeight " +
                     " FROM Weighing_Detail b WHERE b.OrderId = a.Id ORDER BY b.DateWeighing ASC" +
                     " ) AS 'น้ำหนักชั่งเข้า'," +
                     " (" +
                     " SELECT b.NetWeight" +
                     " FROM Weighing_Detail b " +
                     " WHERE b.OrderId = a.Id ORDER BY b.DateWeighing ASC" +
                     " OFFSET 1 ROWS FETCH NEXT 1 ROW ONLY" +
                     " ) AS 'น้ำหนักชั่งออก'," +
                     " a.NetWeight as 'น้ำหนักสุทธิ'" +
                     " FROM Order_Management a" +
                     $" WHERE a.DateCreate BETWEEN '{dateStrat}' and '{dateEnd}'";

                if (status != "-- ไม่เลือก --")
                    sql += $"and a.Status = '{status}'";
                if (orderNumber != "")
                    sql += $"and a.OrderNumber = '{orderNumber}'";
                if (jobNumber != "")
                    sql += $"and a.JobNumber = '{jobNumber}'";
                if (licensePlate != "")
                    sql += $"and a.LicensePlate LIKE '%{licensePlate}%'";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    da.Fill(tb);
                    if (tb.Rows == null || tb.Rows.Count == 0)
                        return null;
                }
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("OrderManagermentDb,getOrderByQuery : " + ERR);
                return null;
            }
            return tb;
        }

        /// <summary>
        /// สำหรับข้อมูล order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderManageModel GetOrderById(int id)
        {
            Log.Information("== getOrderById");
            OrderManageModel orderManageModel = new OrderManageModel();
            try
            {
                string sql = $"SELECT * FROM Order_Management WHERE Id = {id}";
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
                        orderManageModel = new OrderManageModel
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            OrderNumber = dr["OrderNumber"].ToString(),
                            JobNumber = dr["JobNumber"].ToString(),
                            WeightType = dr["WeightType"].ToString(),
                            PoBuy = dr["POBuy"].ToString(),
                            PoSale = dr["POSale"].ToString(),
                            SuppireName = dr["SuppireName"].ToString(),
                            CustomerName = dr["CustomerName"].ToString(),
                            ProductNamez = dr["ProductName"].ToString(),
                            RawMatName = dr["RawMatName"].ToString(),
                            StartStationName = dr["StartStationName"].ToString(),
                            StartStationType = dr["StartStationType"].ToString(),
                            EndStationName = dr["EndStationName"].ToString(),
                            EndStationType = dr["EndStationType"].ToString(),
                            TransportName = dr["TransportName"].ToString(),
                            LIcensePlate = dr["LicensePlate"].ToString(),
                            DriverName = dr["DriverName"].ToString(),
                            DateCreate = DateTime.Parse(dr["DateCreate"].ToString()).ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("TH-th")),
                            Status = dr["Status"].ToString(),
                            VerifyStatus = dr["VerifyStatus"].ToString(),
                            EmployeeCreate = dr["EmployeeCreate"].ToString(),
                            EmplpoyeeVerify = dr["EmployeeVerify"].ToString(),
                            ReferenceNumber = dr["ReferenceNumber"].ToString(),
                            OutSideFirstWeight = int.Parse(dr["OutSideFirstWeight"].ToString()),
                            OutSideSecondWeight = int.Parse(dr["OutSideSecondWeight"].ToString()),
                            OutSideNetWeight = int.Parse(dr["OutSideNetWeight"].ToString()),
                            NetWeight = int.Parse(dr["NetWeight"].ToString()),
                            Remark = dr["Remark"].ToString(),
                            QcCode = dr["QC_code"].ToString()
                        };
                        string _json = JsonConvert.SerializeObject(orderManageModel);
                        Log.Information(_json);
                        break;
                    }
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("OrderManagementDb,GetOrderById : " + ERR);
                return null;
            }
            return orderManageModel;
        }
    }
}
