using DocumentFormat.OpenXml.Office2010.Excel;
using Guna.UI2.WinForms;
using ServerSide.Dbcontent;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide.Pages_Client_Site
{
    public partial class frmJobManagementCS : Form
    {
        public frmJobManagementCS(AccountManagementModel accountManagementModel)
        {
            InitializeComponent();
            dgv.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            _accountManagementModel = accountManagementModel;
        }

        private readonly AccountManagementModel _accountManagementModel;
        private OrderManageModel order = new OrderManageModel();
        private FirstWeightModel firstWeight = new FirstWeightModel();

        /// <summary>
        /// แสดงข้อมูลที่กำลังชั่งและเตรียมชั่ง
        /// </summary>
        void GetDataPlanning()
        {
            OrderManagementDb orderManagementDb = new OrderManagementDb();
            List<OrderManageModel> orderManageModels = orderManagementDb.GetAllDatePlanning();
            dgv.Rows.Clear();
            if (orderManageModels != null)
                foreach (var item in orderManageModels)
                {
                    dgv.Rows.Add("", item.Id, item.OrderNumber, item.WeightType, item.JobNumber, item.ProductNamez, item.RawMatName, item.CustomerName, item.TransportName, item.LIcensePlate, item.StartStationName, item.EndStationName, item.StartStationType, item.EndStationType, item.PoBuy, item.PoSale, item.DriverName, item.Status, item.ReferenceNumber);
                }
        }

        async Task GetWeightDetail(int id, string status)
        {
            btnStart.Enabled = false;
            lblNotFoundFirstWeight.Visible = false;
            pnFirstWeight.Visible = false;
            pbLoadFirstWeight.Visible = true;
            gbReferenceNumber.Enabled = false;
            await Task.Delay(2000);
            // get weight detail
            WeigthingDetailDb weigthingDetailDb = new WeigthingDetailDb();
            // get First weight
            List<WeightDetailModel> listModel = weigthingDetailDb.GetDetailByOrderId(id);
            if (listModel != null)
            {
                if (listModel.Count > 0)
                {
                    foreach (var model in listModel)
                    {
                        if (model.WeightType == "FIRST WEIGHT")
                        {
                            lblEmployeeFirstWeight.Text = model.Employee;
                            lblDateTimeFirstWeight.Text = model.DateWeighing;
                            lblWeightFirstWeight.Text = model.NetWeight.ToString();
                            pnFirstWeight.Visible = true;

                            // keep parameter first weight
                            DateTime dateTime = DateTime.Parse(model.DateWeighing);
                            firstWeight = new FirstWeightModel
                            {
                                Weight = model.NetWeight,
                                Datez = dateTime.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("th-TH")),
                                Timez = dateTime.ToString("HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("th-TH"))
                            };
                            gbReferenceNumber.Enabled = false;
                            break;
                        }
                    }
                }
            }

            else
            {
                lblNotFoundFirstWeight.Visible = true;
                gbReferenceNumber.Enabled = true;

            }

            pbLoadFirstWeight.Visible = false;
            await Task.Delay(1000);
            btnStart.Enabled = true;
        }

        private void frmJobManagementCS_Load(object sender, EventArgs e)
        {

            // แสดงข้อมูลที่กำลังชั่ง
            GetDataPlanning();
        }

        private async void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string clName = dgv.Columns[e.ColumnIndex].Name;
                if (clName == "cl_select")
                {
                    foreach (Guna2TextBox item in gbReferenceNumber.Controls.OfType<Guna2TextBox>())
                    {
                        item.Clear();
                    }
                    // get data
                    int id = int.Parse(dgv.Rows[e.RowIndex].Cells["cl_Id"].Value.ToString());
                    string ordernumber = dgv.Rows[e.RowIndex].Cells["cl_ordernumber"].Value.ToString();

                    OrderManagementDb orderManagementDb = new OrderManagementDb();
                    OrderManageModel model = orderManagementDb.GetOrderById(id);
                    if (model == null)
                    {
                        MessageBox.Show("เกิดข้อผิดผลาดในการดึงข้อมูล \n\n" +
                            "Error : " + orderManagementDb.ERR, "Order : " + order, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // check referancenumber
                    if (model.StartStationType.Trim() == "OUTSIDE" || model.EndStationType.Trim() == "OUTSIDE")
                        gbReferenceNumber.Visible = true;
                    else
                        gbReferenceNumber.Visible = false;

                    // define parameter
                    lblLicense.Text = model.LIcensePlate;
                    lblProduct.Text = model.ProductNamez;
                    lblCustomer.Text = model.CustomerName;
                    lblWeightType.Text = model.WeightType;
                    lblJobNumber.Text = model.JobNumber;
                    lblStartStationName.Text = model.StartStationName;
                    lblEndStationName.Text = model.EndStationName;
                    lblOrdernumber.Text = ordernumber;
                    lblId.Text = id.ToString();
                    lblRawMat.Text = model.RawMatName;
                    lblTransportName.Text = model.TransportName;
                    lblDriverName.Text = model.DriverName;
                    lblPoBuy.Text = model.PoBuy;
                    lblPoSale.Text = model.PoSale;
                    lblStartStationType.Text = model.StartStationType;
                    lblEndStationType.Text = model.EndStationType;
                    txtReferanceNumber.Text = model.ReferenceNumber;
                    txtOutSideFirstWeight.Text = model.OutSideFirstWeight.ToString();
                    txtOutSideNetWeight.Text = model.OutSideNetWeight.ToString();
                    txtOutSideSecondWeight.Text = model.OutSideSecondWeight.ToString();
                    order = model;
                    dgv.Enabled = false;
                    await GetWeightDetail(id, model.Status);
                    dgv.Enabled = true;
                }

            }
            catch
            {


            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            foreach (Label lbl in gbInformation.Controls.OfType<Label>())
            {
                if (lbl.Tag?.ToString() == "DataWeighing" && lbl.Text == "...")
                {
                    MessageBox.Show("กรุณาเลือกเลขที่ใบงานที่ต้องการจะชั่งก่อน", "เลือกใบงาน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            // เช็คว่าถ้าเป็นงานขนย้าย หรือ งานซื้อ หรืองานที่มีการชั่งแท่นภายนอก
            if (order.StartStationType == "OUTSIDE" || order.EndStationType == "OUTSIDE")
            {
                // บังคับบให้คีย์เลขที่อ้างอิง , นน.ชั่งเข้า ,นน.ชั่งออก , นน.สุทธิ
                if (txtReferanceNumber.Text == "" || txtOutSideFirstWeight.Text == "" || txtOutSideNetWeight.Text == "" || txtOutSideSecondWeight.Text == "")
                {
                    MessageBox.Show("เนื่องจากมีการชั่งรถจากแท่นภายนอกกรุณากรอกข้อมูล\n" +
                        "เลขที่อ้างอิง\n" +
                        "นน.ชั่งเข้า\n" +
                        "นน.ชั่งออก\n" +
                        "นน.สุทธิ\n", "กรอกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            // กำหนดค่า
            order.ReferenceNumber = txtReferanceNumber.Text;
            order.OutSideFirstWeight = int.Parse(txtOutSideFirstWeight.Text);
            order.OutSideSecondWeight = int.Parse(txtOutSideSecondWeight.Text);
            order.OutSideNetWeight = int.Parse(txtOutSideNetWeight.Text);
            // open weighing form
            frmWeighing frmWeighing = new frmWeighing(order, _accountManagementModel, firstWeight);
            this.Hide();
            frmWeighing.ShowDialog();
            lblNotFoundFirstWeight.Visible = false;
            pnFirstWeight.Visible = false;
            foreach (Guna2TextBox item in gbReferenceNumber.Controls.OfType<Guna2TextBox>())
            {
                item.Clear();
            }
            // แสดงข้อมูลที่กำลังชั่ง
            GetDataPlanning();
            this.Show();
        }

        private void frmJobManagementCS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                // แสดงข้อมูลที่กำลังชั่ง
                GetDataPlanning();
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ตรวจสอบว่า:
            // 1. ไม่ใช่ปุ่ม Control (เช่น Backspace)
            // 2. และ ไม่ใช่ตัวเลข (0-9)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // e.Handled = true หมายถึง "จัดการแล้ว" (ไม่ต้องทำต่อ) 
                // ผลลัพธ์คือตัวอักษรนั้นจะไม่ถูกพิมพ์ลงใน TextBox
                e.Handled = true;
            }
        }
    }
}

