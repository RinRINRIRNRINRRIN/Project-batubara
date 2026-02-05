using DocumentFormat.OpenXml.Office2013.Drawing.Chart;
using Serilog;
using ServerSide.Dbcontent;
using ServerSide.Functions;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.WebSockets;
using System.Windows.Forms;

namespace ServerSide.Pages_Client_Site
{
    public partial class frmWeighing : Form
    {
        public frmWeighing(OrderManageModel model, AccountManagementModel accountModel, FirstWeightModel firstWeightModel)
        {
            InitializeComponent();
            this._orderModel = model;
            this._accountModel = accountModel;
            this.firstWeightModel = firstWeightModel;
            int x = (this.Width - pnLoader.Width) / 2;
            int y = (this.Height - pnLoader.Height) / 2;
            pnLoader.Location = new Point(x, y);
        }
        private static string scaleName = ConfigurationManager.AppSettings["SCALENAME"];
        private static string printerName = ConfigurationManager.AppSettings["PrinterName"];
        private readonly OrderManageModel _orderModel;
        private readonly AccountManagementModel _accountModel;
        private readonly FirstWeightModel firstWeightModel;
        private WeightDetailModel model;
        private SecondWeightModel secondWeightModel;
        private Comport comport = new Comport();
        private System.Timers.Timer watchdogTimer;

        int newWeight = 0;
        int lastWeight = 0;
        /// <summary>
        /// กำหนดค่าโปรแกรม
        /// </summary>
        void DefineParameter()
        {
            lblCustomer.Text = _orderModel.CustomerName;
            lblProduct.Text = _orderModel.ProductNamez;
            lblDriverName.Text = _orderModel.DriverName;
            lblEndStationName.Text = _orderModel.EndStationName;
            lblEndStationName.Text = _orderModel.EndStationName;
            lblStartStationName.Text = _orderModel.StartStationName;
            lblStartStationType.Text = _orderModel.StartStationType;
            lblWeightType.Text = _orderModel.WeightType;
            lblMainOrdernumber.Text = "ชั่งรายการที่ : " + _orderModel.JobNumber;
            lblJobNumber.Text = _orderModel.JobNumber;
            lblOrdernumber.Text = _orderModel.OrderNumber;
            lblPoBuy.Text = _orderModel.PoBuy;
            lblPoSale.Text = _orderModel.PoSale;
            lblRawMat.Text = _orderModel.RawMatName;
            lblTransportName.Text = _orderModel.TransportName;
            lblEndStationType.Text = _orderModel.EndStationType;
            lblLicense.Text = _orderModel.LIcensePlate;
            btnScaleName.Text = comport.ScaleName;
            btnDevicePort.Text = comport.Port;
            txtNote.Text = _orderModel.Remark;
            if (_orderModel.Status == "Process")
            {
                txtNote.Enabled = true;
                lblStatus.Text = "ประเภทชั่ง : ชั่งออก";
            }
            else if (_orderModel.Status == "Planning")
            {
                txtNote.Enabled = false;
                lblStatus.Text = "ประเภทชั่ง : ชั่งเข้า";
            }
            watchdogTimer = new System.Timers.Timer();
            watchdogTimer.Interval = 2500;
            watchdogTimer.Elapsed += WatchdogTimer_Elapsed;
            watchdogTimer.AutoReset = false;

        }

        private void WatchdogTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate ()
            {
                lblWeight.Text = "ERROR";
            }));
        }

        bool SaveFirstWeight()
        {
            Log.Information("==Save first weight");
            // insert weight detial
            WeightDetailModel model = new WeightDetailModel
            {
                OrderId = _orderModel.Id,
                NetWeight = int.Parse(lblWeight.Text),
                WeightType = "FIRST WEIGHT",
                Employee = _accountModel.FullName,
                DateWeighing = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("EN-en"))
            };

            WeigthingDetailDb weigthing = new WeigthingDetailDb();
            if (!weigthing.AddNew(model))
            {
                MessageBox.Show("ไม่สามารถเพิ่มข้อมูลการชั่งได้\n" +
                    "\n" +
                    $"ERROR : {weigthing.ERR}", $"เลขที่ใบงาน : {_orderModel.JobNumber}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // update create ordernumber
            OrderManagementDb orderManagementDb = new OrderManagementDb();
            if (!orderManagementDb.UpdateOrderAndGetOrderId(_orderModel.Id, txtNote.Text))
                return false;
            _orderModel.OrderNumber = orderManagementDb.getOrderNumber;


            // update status
            if (!orderManagementDb.UpdateStatus(_orderModel.Id, "Process"))
                return false;

            // update reference number
            if (!orderManagementDb.UpdateReferenceNumber(_orderModel.Id, _orderModel.ReferenceNumber, _orderModel.OutSideFirstWeight, _orderModel.OutSideSecondWeight, _orderModel.OutSideNetWeight))
                return false;

            MessageBox.Show("บันทึกรายการสำเร็จ");
            return true;
        }

        bool SaveSecondWeight()
        {
            Log.Information("==Save second weight");
            model = new WeightDetailModel
            {
                OrderId = _orderModel.Id,
                NetWeight = int.Parse(lblWeight.Text),
                WeightType = "SECOND WEIGHT",
                Employee = _accountModel.FullName,
                DateWeighing = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("EN-en"))
            };
            DateTime dateTime = DateTime.Parse(model.DateWeighing);
            secondWeightModel = new SecondWeightModel
            {
                Weight = model.NetWeight,
                Datez = dateTime.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("th-TH")),
                Timez = dateTime.ToString("HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("th-TH"))
            };

            WeigthingDetailDb weigthing = new WeigthingDetailDb();
            if (!weigthing.AddNew(model))
            {
                MessageBox.Show("ไม่สามารถเพิ่มข้อมูลการชั่งได้\n" +
                    "\n" +
                    $"ERROR : {weigthing.ERR}", $"เลขที่ใบงาน : {_orderModel.JobNumber}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // oupdate netweight
            OrderManagementDb orderManagementDb = new OrderManagementDb();
            int fristW = firstWeightModel.Weight;
            int secondW = int.Parse(lblWeight.Text);
            int netW = 0;
            if (fristW > secondW)
            {
                netW = fristW - secondW;
            }
            else
            {
                netW = secondW - fristW;
            }
            if (!orderManagementDb.UpdateNetWeight(_orderModel.Id, netW))
                return false;
            _orderModel.NetWeight = netW;
            // update status order 
            if (!orderManagementDb.UpdateStatus(_orderModel.Id, "Success"))
                return false;

            MessageBox.Show("บันทึกรายการสำเร็จ");
            // พิมพ์ตั๋ว
            return true;
        }

        /// <summary>
        /// สำหรับปริ้นกระดาษ Thermal
        /// </summary>
        void PrintThermal(OrderManageModel orderManageModel, AccountManagementModel accountManagementModel, Image image)
        {
            ThermalPrinterModel printerModel = new ThermalPrinterModel
            {
                Logo = image,
                LicensePLate = orderManageModel.LIcensePlate,
                OrderNumber = orderManageModel.OrderNumber,
                JobNumber = orderManageModel.JobNumber,
                TransportName = orderManageModel.TransportName,
                DriverName = orderManageModel.DriverName,
                EndStationName = orderManageModel.EndStationName,
                StartStationName = orderManageModel.StartStationName,
                Employee = accountManagementModel.FullName,
                ProductNamez = orderManageModel.ProductNamez,
                QcCode = orderManageModel.QcCode
            };
            ThermalPrinter printer = new ThermalPrinter(printerName);
            if (!printer.PrintPage(printerModel))
                MessageBox.Show("Error print thermal : " + printer.Error + "\n Please print again", "Printer thermal");
        }

        void PrintTicket(OrderManageModel orderManageModel, FirstWeightModel firstWeight, SecondWeightModel secondWeightModel)
        {
            TicketModel reportModel = new TicketModel
            {
                OrderId = orderManageModel.Id,
                OrderType = orderManageModel.WeightType,
                OrderNumber = orderManageModel.OrderNumber,
                CustomerName = orderManageModel.CustomerName,
                ProductName = orderManageModel.ProductNamez,
                TransportName = orderManageModel.TransportName,
                DCNumber = orderManageModel.PoBuy,
                PONumber = orderManageModel.PoSale,
                DateIn = firstWeight.Datez,
                TimeIn = firstWeight.Timez,
                DateOut = secondWeightModel.Datez,
                TimeOut = secondWeightModel.Timez,
                WeightIn = firstWeight.Weight,
                WeightOut = secondWeightModel.Weight,
                WeightNet = orderManageModel.NetWeight,
                CarNumber = orderManageModel.LIcensePlate,
                Remark = orderManageModel.Remark
            };


            frmReport frmReport = new frmReport(reportModel, "พิมพ์หลังจากชั่ง Second weight เสร็จ", _accountModel.FullName, cbPreview.Checked);
            frmReport.ShowDialog();
        }
        private async void frmWeighing_Load(object sender, EventArgs e)
        {
            guna2ControlBox1.Visible = false;
            await Task.Delay(500);
            lblMessageLoader.Text = "เตรียมข้อมูลชั่งรถ";
            // กำหนดค่าข้อมูล
            DefineParameter();
            await Task.Delay(2000);
            lblMessageLoader.Text = "เชื่อมต่อเครื่องชั่ง";
            // เชื่อมต่อเครื่องชั่ง
            if (!comport.Connect(serialPort1))
            {
                MessageBox.Show("ไม่สามารถเปิดพอร์ตเครื่องชั่งได้\n" +
                    $"Error : {comport.ERR}", "พอร์ตเครื่องชั่ง", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            watchdogTimer.Start();
            await Task.Delay(2000);
            lblMessageLoader.Text = "สำเร็จ";
            pnLoader.Visible = false;
            pnMain.Visible = true;
            guna2ControlBox1.Visible = true;
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                watchdogTimer.Stop();
                watchdogTimer.Start();
                string res = "";
                res = comport.ReceiveWeight(serialPort1);
                int _weight = 0;
                if (!int.TryParse(res, out _weight))
                {
                    BeginInvoke(new MethodInvoker(delegate ()
                    {
                        lblWeight.Font = new Font("Athiti", 20, FontStyle.Bold);
                        lblWeight.Text = "SCALE NOT MATCH";
                    }));
                    return;
                }
                newWeight = _weight;
                BeginInvoke(new MethodInvoker(delegate ()
                {
                    lblWeight.Font = new Font("Athiti", 72, FontStyle.Bold);
                    lblWeight.Text = _weight.ToString();
                }));

                // ถ้าน้ำหนักแตกต่างจากน้ำหนักล่าสุด
                if (newWeight != lastWeight)
                {
                    lastWeight = newWeight;
                    BeginInvoke(new MethodInvoker(delegate ()
                    {
                        tmCheckWeight.Stop();
                        tmCheckWeight.Start();
                        btnStart.Enabled = false;
                        btnStausWeight.ForeColor = Color.FromArgb(243, 128, 0);
                        btnStausWeight.BorderColor = Color.FromArgb(243, 128, 0);
                        btnStausWeight.FillColor = Color.FromArgb(239, 250, 240);
                        btnStausWeight.Text = "Weighing.....".ToUpper();
                    }));
                }
            }
            catch
            {


            }

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                // check weight

                int tryInt = 0;
                if (!int.TryParse(lblWeight.Text, out tryInt))
                {
                    MessageBox.Show("ไม่สามารถบันทึกน้ำหนักได้ กรุณาตรวจสอบน้ำหนักอีกครั้ง", "น้ำหนัก Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // เช็คช่วงน้ำหนัก
                if (tryInt <= 100)
                {
                    MessageBox.Show("น้ำหนักบนแท่นต้องมากกว่า 100 Kg", "น้ำหนักน้อย", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                switch (_orderModel.Status)
                {
                    case "Planning": // fristweight
                       
                        txtNote.BorderColor = Color.Gray;
                        txtNote.BorderThickness = 1;
                        // Add new record weight detail
                        if (!SaveFirstWeight())
                            return;
                        // print receive thermal
                        PrintThermal(_orderModel, _accountModel, pictureBox2.Image);
                        break;
                    case "Process": // second weight
                                    // check remark empty
                        if (txtNote.Text == "")
                        {
                            txtNote.BorderColor = Color.Red;
                            txtNote.BorderThickness = 2;
                            MessageBox.Show("remark ว่างกรุณากรอกข้อมูลก่อนการบันทึกหากไม่มีข้อมูลให้ใช้ - \n" +
                                "จะไม่สามารถกลับมาแก้ไขข้อมูลได้อีก", "Remark ว่าง", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (!SaveSecondWeight())
                            return;

                        // Print ticket
                        PrintTicket(_orderModel, firstWeightModel, secondWeightModel);
                        break;
                }

                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private async void frmWeighing_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            guna2ControlBox1.Visible = false;
            watchdogTimer.Stop();
            lblMessageLoader.Text = "ยกเลิกเชื่อมต่อเครื่องชั่ง";
            pnMain.Visible = false;
            pnLoader.Visible = true;
            serialPort1.Close();
            await Task.Delay(4000);
            lblMessageLoader.Text = "สำเร็จ";
            this.FormClosing -= frmWeighing_FormClosing; // ป้องกัน loop
            this.Close();
        }

        private void tmCheckWeight_Tick(object sender, EventArgs e)
        {
            tmCheckWeight.Stop(); // หยุด Timer

            btnStart.Enabled = true;
            btnStausWeight.ForeColor = Color.FromArgb(46, 125, 50);
            btnStausWeight.FillColor = Color.FromArgb(239, 250, 240);
            btnStausWeight.BorderColor = Color.FromArgb(46, 125, 50);
            btnStausWeight.Text = "STABLE";
        }
    }
}
