using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DocumentFormat.OpenXml.Office.Word;
using ServerSide.Dbcontent;
using ServerSide.Models;
using ServerSide.Pages_Test;
using ServerSide.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CDS = CrystalDecisions.Shared;
namespace ServerSide.Pages_Client_Site
{
    public partial class frmReport : Form
    {
        public frmReport(TicketModel reportModel, string note, string employee, bool preview)
        {
            InitializeComponent();
            this._reportModel = reportModel;
            this.Note = note;
            this.Employee = employee;
            this.Preview = preview;
            // get seq print 
            PrintDetailDb printDetailDb = new PrintDetailDb();
            Seq = printDetailDb.GetSeqReport(_reportModel.OrderId);
            lblReportOut.Text = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-EN"));
            DefineReportParameter();
        }

        private readonly TicketModel _reportModel;

        private readonly int Seq;
        private string Note { get; set; }
        private string Employee { get; set; }

        public bool Preview { get; set; } = false;

        void Print()
        {
            // add new detail
            PrintDetailModel model = new PrintDetailModel
            {
                DateTimez = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("en-EN")),
                Note = Note,
                Employee = Employee,
                Seq = Seq,
                OrderId = _reportModel.OrderId
            };

            PrintDetailDb printDetailDb = new PrintDetailDb();
            printDetailDb.AddNewDetail(model);

            ReportDocument report = (ReportDocument)crystalReportViewer1.ReportSource;
            if (report == null)
            {
                MessageBox.Show("ยังไม่ได้โหลด Report เข้า Viewer");
                return;
            }

            // ชื่อฟอร์มกระดาษที่สร้างไว้ใน Windows
            string customFormName = "TSC";

            // ใช้ default printer (หรือระบุชื่อเครื่องพิมพ์เองก็ได้)
            PrintDocument pd = new PrintDocument();
            // pd.PrinterSettings.PrinterName = "ชื่อเครื่องพิมพ์ของคุณ";

            // หา paper size ที่ชื่อ = TSC
            System.Drawing.Printing.PaperSize found = pd.PrinterSettings.PaperSizes
                .Cast<System.Drawing.Printing.PaperSize>()
                .FirstOrDefault(p =>
                    p.PaperName.Equals(customFormName, StringComparison.OrdinalIgnoreCase));

            if (found == null)
            {
                MessageBox.Show($"ไม่พบฟอร์ม '{customFormName}' ในเครื่องพิมพ์ {pd.PrinterSettings.PrinterName}");
                this.Close();
                return;
            }

            // เซ็ตค่าให้รายงาน
            report.PrintOptions.PrinterName = pd.PrinterSettings.PrinterName;
            report.PrintOptions.PaperOrientation = CDS.PaperOrientation.Landscape;  // หรือ Portrait ตามต้องการ
            report.PrintOptions.PaperSize = (CDS.PaperSize)found.RawKind;

            // พิมพ์ (1 ชุด, ไม่ collate, ทุกหน้า)
            report.PrintToPrinter(1, false, 0, 0);

            this.Close();
        }

        void DefineReportParameter()
        {
            //rptTicketTSC rpt = new rptTicketTSC();
            rptTicketCustomer rpt = new rptTicketCustomer();
            rpt.SetParameterValue("rptOrderType", _reportModel.OrderType);
            rpt.SetParameterValue("rptCustomerName", _reportModel.CustomerName);
            rpt.SetParameterValue("rptProductName", _reportModel.ProductName);
            rpt.SetParameterValue("rptOrderNumber", _reportModel.OrderNumber);
            rpt.SetParameterValue("rptTransportName", _reportModel.TransportName);
            rpt.SetParameterValue("rptDCnumber", _reportModel.DCNumber);
            rpt.SetParameterValue("rptPONumber", _reportModel.PONumber);
            rpt.SetParameterValue("rptCarNumber", _reportModel.CarNumber);
            rpt.SetParameterValue("rptDateIn", _reportModel.DateIn);
            rpt.SetParameterValue("rptDateOut", _reportModel.DateOut);
            rpt.SetParameterValue("rptTimeIn", _reportModel.TimeIn);
            rpt.SetParameterValue("rptTimeOut", _reportModel.TimeOut);
            rpt.SetParameterValue("rptWeightIn", _reportModel.WeightIn.ToString("#,###"));
            rpt.SetParameterValue("rptWeightOut", _reportModel.WeightOut.ToString("#,###"));
            rpt.SetParameterValue("rptWeightNet", _reportModel.WeightNet.ToString("#,###"));

            rpt.SetParameterValue("rptPrintSeq", Seq);
            crystalReportViewer1.EnableRefresh = false;
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Zoom(100);

            lblFirstWeight.Text = _reportModel.WeightIn.ToString("#,###");
            lblSecondWeight.Text = _reportModel.WeightOut.ToString("#,###");
            lblNetWeight.Text = _reportModel.WeightNet.ToString("#,###");

            lblOrder.Text = _reportModel.OrderNumber;
            lblJobNumber.Text = _reportModel.ProductName;
            lblEmployee.Text = Employee;
            lblLicensePlate.Text = _reportModel.CarNumber;

            lblSeq.Text = Seq.ToString("#,###");
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            // เช็คว่าต้องการ preview หรือไม่

            if (!Preview)
            {
                this.WindowState = FormWindowState.Minimized;
                Print();
            }
        }

        private void frmReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                Print();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void tmDateTime_Tick(object sender, EventArgs e)
        {
            lblPrintD.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("en-EN"));

        }
    }
}
