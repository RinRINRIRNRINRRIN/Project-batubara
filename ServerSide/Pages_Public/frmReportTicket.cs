using DocumentFormat.OpenXml.Math;
using ServerSide.Dbcontent;
using ServerSide.Models;
using ServerSide.Pages_Client_Site;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide.Pages_Public
{
    public partial class frmReportTicket : Form
    {
        public frmReportTicket(int OrderId, AccountManagementModel _account)
        {
            InitializeComponent();
            this.OrderId = OrderId;
            this.account = _account;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Athiti Semibold", 14, FontStyle.Bold);
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            guna2HtmlLabel1.Font = new Font("Athiti", 12, FontStyle.Regular);

            dgv.Columns["cl_seq"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private int OrderId { get; set; }
        private readonly AccountManagementModel account;

        private OrderManageModel OrderModel { get; set; }
        private FirstWeightModel FirstWeightModel { get; set; }
        private SecondWeightModel SecondWeightModel { get; set; }

        private TicketModel TicketModel { get; set; }
        void DefineModel(int orderId)
        {
            // กำหนดค่าข้อมูลออเดอร์
            OrderManagementDb db = new OrderManagementDb();
            OrderModel = db.GetOrderById(orderId);
            // กำหนดค่ารายละเอียดการชั่ง
            WeigthingDetailDb weigthingDetailDb = new WeigthingDetailDb();
            List<WeightDetailModel> listDetail = weigthingDetailDb.GetDetailByOrderId(orderId);

            if (listDetail != null)
            {
                foreach (var item in listDetail)
                {
                    string[] dateTime = item.DateWeighing.Split(' ');
                    switch (item.WeightType)
                    {
                        case "FIRST WEIGHT": // กำหนดค่าชั่งเข้า first weight
                            FirstWeightModel = new FirstWeightModel();
                            FirstWeightModel.Datez = dateTime[0].Trim();
                            FirstWeightModel.Timez = dateTime[1].Trim();
                            FirstWeightModel.Weight = item.NetWeight;
                            break;
                        case "SECOND WEIGHT": // กำหนดค่าชั่งออก second weight
                            SecondWeightModel = new SecondWeightModel();
                            SecondWeightModel.Datez = dateTime[0].Trim();
                            SecondWeightModel.Timez = dateTime[1].Trim();
                            SecondWeightModel.Weight = item.NetWeight;
                            break;
                    }
                }

                // กำหนดค่าให้กับ report
                TicketModel = new TicketModel
                {
                    OrderId = OrderModel.Id,
                    OrderNumber = OrderModel.OrderNumber,
                    OrderType = OrderModel.WeightType,
                    CarNumber = OrderModel.LIcensePlate,
                    ProductName = OrderModel.ProductNamez,
                    CustomerName = OrderModel.CustomerName,
                    TransportName = OrderModel.TransportName,
                    DCNumber = OrderModel.PoBuy,
                    PONumber = OrderModel.PoSale,
                    DateIn = FirstWeightModel.Datez,
                    TimeIn = FirstWeightModel.Timez,
                    DateOut = SecondWeightModel.Datez,
                    TimeOut = SecondWeightModel.Timez,
                    WeightIn = FirstWeightModel.Weight,
                    WeightOut = SecondWeightModel.Weight,
                    WeightNet = OrderModel.NetWeight
                };
            }
        }
        void GetReportDetail(int orderId)
        {
            PrintDetailDb db = new PrintDetailDb();
            List<PrintDetailModel> reportModels = db.GetDetailOrderId(orderId);
            if (reportModels != null)
            {
                dgv.Rows.Clear();
                foreach (var item in reportModels)
                {
                    dgv.Rows.Add(item.Id, item.Seq, item.DateTimez, item.Employee, item.Note);
                }
            }
        }

        void GetOrderDetail(int orderId)
        {
            OrderManagementDb orderManagementDb = new OrderManagementDb();
            OrderManageModel model = orderManagementDb.GetOrderById(orderId);
            lblCustomer.Text = model.CustomerName;
            lblJobNumber.Text = model.JobNumber;
            lblOrderNumber.Text = model.OrderNumber;
            lblProduct.Text = model.ProductNamez;
            lblDate.Text = model.DateCreate;
        }

        private void frmReportTicket_Load(object sender, EventArgs e)
        {
            // get ticket detail
            GetReportDetail(OrderId);
            // get order information
            GetOrderDetail(OrderId);

            // กำหนดค่า Model
            DefineModel(OrderId);

            guna2HtmlLabel1.Text = $"<a> ผลลัพท์ </a> <a style = 'color:red'> {dgv.Rows.Count} </a> <a> รายการ </a>";
            guna2HtmlLabel1.Font = new Font("Athiti", 12, FontStyle.Regular);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtNote.Text == "")
            {
                // msg.Show("Mess")
                msg.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning;
                msg.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                msg.Show("Please fill note");
                return;
            }

            msg.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNoCancel;
            msg.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
            DialogResult dr = msg.Show("Do you want preview before print ?", "Preview");
            frmReport frmReport;
            if (dr == DialogResult.Yes)  // show preview
            {
                frmReport = new frmReport(TicketModel, txtNote.Text, account.FullName, true);
                frmReport.ShowDialog();
                this.Close();
            }
            else if (dr == DialogResult.No) // don't show preview
            {
                frmReport = new frmReport(TicketModel, txtNote.Text, account.FullName, false);
                frmReport.ShowDialog();
                this.Close();
            }

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
