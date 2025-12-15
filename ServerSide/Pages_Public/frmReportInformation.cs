using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Guna.UI2.WinForms;
using Serilog;
using ServerSide.Dbcontent;
using ServerSide.Models;
using ServerSide.Pages_Public;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide.Pages_Client_Site
{
    public partial class frmReportInformation : Form
    {
        public frmReportInformation(AccountManagementModel account)
        {
            InitializeComponent();
            this.accountManagementModel = account;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Athiti Semibold", 14, FontStyle.Bold);
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            guna2HtmlLabel1.Font = new Font("Athiti", 12, FontStyle.Regular);
        }

        string OrderNumber { get; set; }
        string JobNumber { get; set; }
        int Id { get; set; }
        int indexDgv { get; set; }
        string Status { get; set; }

        private readonly AccountManagementModel accountManagementModel;

        /// <summary>
        /// สำหรับเก็บข้อมูลเมื่อมีการกดค้นหารายการ
        /// </summary>
        private DataTable DataTableForSearch { get; set; }

        /// <summary>
        ///  สำหรับแสดงรายการวันนี้
        /// </summary>
        void GetReportToday()
        {
            OrderManagementDb db = new OrderManagementDb();
            string dateToDay = DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.CreateSpecificCulture("en-EN"));
            List<OrderManageModel> list = db.GetOrderDate(dateToDay + " 00:00:00", dateToDay + " 23:59:59", "All");
            UpdateDatagrid(list);
        }


        /// <summary>
        /// สำหรับกดค้นหา
        /// </summary>
        void GetReportSearch()
        {
            OrderManagementDb orderManagementDb = new OrderManagementDb();
            string dateStrt = dtpStart.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.CreateSpecificCulture("en-EN")) + " 00:00:00";
            string dateEnd = dtpEnd.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.CreateSpecificCulture("en-EN")) + " 23:59:59";

            List<OrderManageModel> list = orderManagementDb.GetOrderAnyQuery(dateStrt, dateEnd, cbbOrderStatus.Text, txtLicensePlate.Text, txtOrderNumber.Text, txtJobNumber.Text);
            DataTable tb = orderManagementDb.getOrderByQuery(dateStrt, dateEnd, cbbOrderStatus.Text, txtLicensePlate.Text, txtOrderNumber.Text, txtJobNumber.Text);
            if (list != null)
            {
                UpdateDatagrid(list);
                DataTableForSearch = tb;
            }
        }

        void updateUiDgv()
        {
            foreach (DataGridViewRow rw in dgv.Rows)
            {
                string status = rw.Cells["cl_status"].Value.ToString();
                rw.Cells["cl_status"].Style.Font = new Font("Athiti SemiBold", 12);
                rw.Cells["cl_status"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                switch (status)
                {
                    case "Planning":
                        rw.Cells["cl_status"].Style.ForeColor = Color.Goldenrod;
                        //  rw.DefaultCellStyle.BackColor = Color.FromArgb(243, 248, 206);
                        break;
                    case "Process":
                        rw.Cells["cl_status"].Style.ForeColor = Color.Blue;
                        break;
                    case "Success":
                        rw.Cells["cl_status"].Style.ForeColor = Color.Green;
                        rw.DefaultCellStyle.BackColor = Color.FromArgb(236, 250, 229);
                        break;
                    case "Cancel":
                        rw.Cells["cl_status"].Style.ForeColor = Color.Red;
                        rw.DefaultCellStyle.BackColor = Color.FromArgb(247, 202, 201);
                        break;
                }

            }
        }


        void UpdateDatagrid(List<OrderManageModel> lists)
        {

            if (lists != null)
            {
                dgv.Rows.Clear();
                //DataTableForSearch = new DataTable();

                //foreach (var item in lists)
                //{
                //    // create column datatable
                //    foreach (var orderModel in item.GetType().GetProperties())
                //    {
                //        DataTableForSearch.Columns.Add(orderModel.Name);
                //    }
                //    break;
                //}
                foreach (var item in lists)
                {
                    // create rows 
                    //DataTableForSearch.Rows.Add(item.Id, item.OrderNumber, item.JobNumber, item.WeightType, item.PoBuy, item.PoSale, item.SuppireName, item.CustomerName, item.ProductNamez, item.RawMatName, item.StartStationName, item.StartStationType, item.EndStationName, item.EndStationType, item.TransportName, item.LIcensePlate, item.DriverName, item.DateCreate, item.VerifyStatus, item.EmployeeCreate, item.EmplpoyeeVerify, item.ReferenceNumber, item.OutSideFirstWeight, item.OutSideSecondWeight, item.OutSideNetWeight, item.NetWeight);
                    dgv.Rows.Add(item.Id, item.OrderNumber, item.WeightType, item.JobNumber, item.ProductNamez, item.RawMatName, item.CustomerName, item.TransportName, item.LIcensePlate, item.Status);
                }

                updateUiDgv();
                dgv.GridColor = Color.FromArgb(188, 185, 188);
                guna2HtmlLabel1.Text = $"<a> ผลลัพท์ </a> <a style = 'color:red'> {lists.Count} </a> <a> รายการ </a>";
                guna2HtmlLabel1.Font = new Font("Athiti", 12, FontStyle.Regular);
            }
        }
        void DefineParameter()
        {
            dtpEnd.Value = DateTime.Now;
            dtpStart.Value = DateTime.Now;
        }

        private void ExportToExcel(DataTable dt)
        {
            // ตรวจสอบว่ามีข้อมูลหรือไม่
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("ไม่พบข้อมูลสำหรับ Export", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // สร้าง Dialog ให้ผู้ใช้เลือกที่เซฟไฟล์
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Workbook|*.xlsx";
                sfd.FileName = "ExportData_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"); // ตั้งชื่อไฟล์เริ่มต้น

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // เริ่มใช้งาน ClosedXML
                        using (var workbook = new XLWorkbook())
                        {
                            // เพิ่ม Worksheet
                            var worksheet = workbook.Worksheets.Add("DataSheet");

                            // *** ไฮไลท์: คำสั่งเดียวจบ นำ DataTable ลง Excel ***
                            // คำสั่งนี้จะเอาชื่อ Column มาเป็น Header ให้ด้วยอัตโนมัติ
                            worksheet.Cell(1, 1).InsertTable(dt);

                            // ปรับขนาดคอลัมน์ให้อัตโนมัติ (Optional)
                            worksheet.Columns().AdjustToContents();

                            // บันทึกไฟล์ตาม Path ที่ผู้ใช้เลือก
                            workbook.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Export ข้อมูลสำเร็จเรียบร้อย!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void frmReportInformation_Load(object sender, EventArgs e)
        {
            DefineParameter();
            GetReportToday();
            cbbOrderStatus.Items.Add("-- ไม่เลือก --");
            cbbOrderStatus.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetReportSearch();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string button = dgv.Columns[e.ColumnIndex].Name;
                OrderNumber = dgv.Rows[e.RowIndex].Cells["cl_ordernumber"].Value.ToString();
                JobNumber = dgv.Rows[e.RowIndex].Cells["cl_jobnumber"].Value.ToString();
                Id = int.Parse(dgv.Rows[e.RowIndex].Cells["cl_id"].Value.ToString());

                Status = dgv.Rows[e.RowIndex].Cells["cl_status"].Value.ToString();
                switch (button)
                {
                    case "cl_print":
                        if (Status != "Success")
                        {
                            msg.Icon = MessageDialogIcon.Warning;
                            msg.Buttons = MessageDialogButtons.OK;
                            msg.Show($"The order isn't complete", "Print");
                            return;
                        }

                        frmReportTicket frmReportTicket = new frmReportTicket(Id, accountManagementModel);
                        frmReportTicket.ShowDialog();

                        break;
                    case "cl_detail":
                        if (Status != "Success")
                        {
                            msg.Icon = MessageDialogIcon.Warning;
                            msg.Buttons = MessageDialogButtons.OK;
                            msg.Show($"The order isn't complete", "Order detail");
                            return;
                        }
                        frmOrderDetail frmOrderDetail = new frmOrderDetail(Id, accountManagementModel);
                        this.Hide();
                        frmOrderDetail.ShowDialog();
                        this.Show();
                        break;
                }
            }
            catch
            {


            }
        }

        private void cbbOrderStatus_DropDown(object sender, EventArgs e)
        {
            cbbOrderStatus.Items.Clear();
            cbbOrderStatus.Items.Add("-- ไม่เลือก --");
            cbbOrderStatus.Items.Add("Planning");
            cbbOrderStatus.Items.Add("Process");
            cbbOrderStatus.Items.Add("Success");
            cbbOrderStatus.Items.Add("Cancel");
        }

        private void cbbOrderStatus_DropDownClosed(object sender, EventArgs e)
        {
            if (cbbOrderStatus.Text == "")
            {
                cbbOrderStatus.Items.Clear();
                cbbOrderStatus.Items.Add("-- ไม่เลือก --");
                cbbOrderStatus.SelectedIndex = 0;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Guna2TextBox item in guna2Panel2.Controls.OfType<Guna2TextBox>())
            {
                item.Clear();
                cbbOrderStatus.Items.Clear();
                cbbOrderStatus.Items.Add("-- ไม่เลือก --");
                cbbOrderStatus.SelectedIndex = 0;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ExportToExcel(DataTableForSearch);
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // เช็คสิทธิ์
                if (accountManagementModel.Position == "Super" || accountManagementModel.Position == "Admin")
                {
                    // เช็คสถานะถ้า success ไม่สามารถยกเลิกได้
                    if (Status == "Success")
                    {
                        msg.Icon = MessageDialogIcon.Warning;
                        msg.Buttons = MessageDialogButtons.OK;
                        msg.Show("Can't cancel the order cause order is successfully ", "Order successfully");
                        return;
                    }

                    msg.Icon = MessageDialogIcon.Question;
                    msg.Buttons = MessageDialogButtons.YesNo;
                    DialogResult result = msg.Show($"OrderNumber : {OrderNumber}\n" +
                        $"JobNumber : {JobNumber} \n\nwill not be removed from the system but will be invalidated and cannot be reused.", "Cancel order");
                    // if press yes
                    OrderManagementDb orderManagementDb = new OrderManagementDb();
                    if (orderManagementDb.UpdateStatus(Id, "Cancel"))
                    {
                        msg.Icon = MessageDialogIcon.Information;
                        msg.Buttons = MessageDialogButtons.OK;
                        msg.Show("Delete success", "Success");
                        // update ui สถานะรายการนั้น 
                        dgv.Rows[indexDgv].Cells["cl_status"].Value = "Cancel";
                        updateUiDgv();
                    }
                    else
                    {
                        msg.Icon = MessageDialogIcon.Error;
                        msg.Buttons = MessageDialogButtons.OK;
                        msg.Show("Delete error \n\n" +
                            $"Message : {orderManagementDb.ERR}", "Error");
                    }
                }
                else
                {
                    msg.Icon = MessageDialogIcon.Warning;
                    msg.Buttons = MessageDialogButtons.OK;
                    msg.Show("Your account is Permission denied for cancel order", "Permiss denied");
                }

            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if(dgv.Rows.Count == 0)
            //{
            //    MessageBox.Show("ไม่พบรายการที่ต้องการเอาข้อมูลออก", "ไม่พบรายการ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //OrderNumber = dgv.Rows[e.RowIndex].Cells["cl_ordernumber"].Value.ToString();
            //JobNumber = dgv.Rows[e.RowIndex].Cells["cl_jobnumber"].Value.ToString();
            //Id = int.Parse(dgv.Rows[e.RowIndex].Cells["cl_id"].Value.ToString());
            //Status = dgv.Rows[e.RowIndex].Cells["cl_status"].Value.ToString();
            //indexDgv = dgv.Rows[e.RowIndex].Index;
        }
    }
}
