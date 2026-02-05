using ESC_POS_USB_NET.Enums;
using ESC_POS_USB_NET.Printer;
using Newtonsoft.Json;
using Serilog;
using ServerSide.Functions;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide.Pages_Test
{
    public partial class frmPrinterThermal : Form
    {
        public frmPrinterThermal()
        {
            InitializeComponent();

            printerName = ConfigurationManager.AppSettings["PrinterName"];
            encodingPage = "TIS-620";

            // ไม่แสดงฟอร์มหลัก
            //this.ShowInTaskbar = false;
            //this.WindowState = FormWindowState.Minimized;
            //this.Visible = false;
        }

        private readonly string printerName;
        /// <summary>
        /// ต้องใช้ TIS-620 เพื่อให้ปริ้นเตอร์รองรับภาษาไทย
        /// </summary>
        private readonly string encodingPage;


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Printer printer = new Printer("EPSON TM-T82X Receipt", "TIS-620");
                printer.Append("NORMAL - 48 COLUMNS");
                printer.Append("1...5...10...15...20...25...30...35...40...45.48");
                printer.Separator();
                printer.Append("Text Normal");
                printer.BoldMode("Bold Text");
                printer.UnderlineMode("Underlined text");
                printer.Separator();
                printer.ExpandedMode(PrinterModeState.On);
                printer.Append("Expanded - 23 COLUMNS");
                printer.Append("1...5...10...15...20..23");
                printer.ExpandedMode(PrinterModeState.Off);
                printer.Separator();
                printer.CondensedMode(PrinterModeState.On);
                printer.Append("Condensed - 64 COLUMNS");
                printer.Append("1...5...10...15...20...25...30...35...40...45...50...55...60..64");
                printer.CondensedMode(PrinterModeState.Off);
                printer.Separator();
                printer.DoubleWidth2();
                printer.Append("Font Width 2");
                printer.DoubleWidth3();
                printer.Append("Font Width 3");
                printer.NormalWidth();
                printer.Append("Normal width");
                printer.Separator();
                printer.AlignRight();
                printer.Append("Right aligned text");
                printer.AlignCenter();
                printer.Append("Center-aligned text");
                printer.AlignLeft();
                printer.Append("Left aligned text");
                printer.Separator();
                printer.Font("Font A", Fonts.FontA);
                printer.Font("Font B", Fonts.FontB);
                printer.Font("Font C", Fonts.FontC);
                printer.Font("Font D", Fonts.FontD);
                printer.Font("Font E", Fonts.FontE);
                printer.Font("Font Special A", Fonts.SpecialFontA);
                printer.Font("Font Special B", Fonts.SpecialFontB);
                printer.Separator();
                printer.InitializePrint();
                printer.SetLineHeight(24);
                printer.Append("This is first line with line height of 30 dots");
                printer.SetLineHeight(40);
                printer.Append("This is second line with line height of 24 dots");
                printer.Append("This is third line with line height of 40 dots");
                printer.NewLines(3);
                printer.Append("End of Test :)");
                printer.Separator();
                printer.FullPaperCut();
                printer.PrintDocument();
            }
            catch
            {


            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            ThermalPrinterModel _model = new ThermalPrinterModel
            {
                Logo = pictureBox2.Image,
                LicensePLate = "นฐ71-7017",
                OrderNumber = "ORW260131-1",
                JobNumber = "IM2511105022",
                TransportName = "ลูกค้ามารับเอง",
                Employee = "กรินทร์ เตชะรัตนยืนยง",
                QcCode = "QC12312414"
            };
            ThermalPrinter thermalPrinter = new ThermalPrinter(comboBox1.Text);
            thermalPrinter.PrintPage(_model);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Printer printer = new Printer("EPSON TM-82X", "TIS-620");
            printer.AutoTest();
            printer.Separator();
            printer.FullPaperCut();
            printer.PrintDocument();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            switch (cbPreview.Checked)
            {
                case true:
                    PrinterSettings printerSettings = new PrinterSettings();
                    printerSettings.PrinterName = "EPSON TM-82X";
                    // สำหรับเครื่อง Zebra
                    int widthInHundredthsOfInch = (int)(80 / 25.4 * 100);
                    int heightInHundredthsOfInch = (int)(120 / 25.4 * 100);
                    PaperSize paperSize = new PaperSize("POS", widthInHundredthsOfInch, heightInHundredthsOfInch);
                    printerSettings.DefaultPageSettings.PaperSize = paperSize;

                    PageSettings pageSettings = new PageSettings(printerSettings);
                    printDocument1.PrinterSettings = printerSettings;
                    printPreviewDialog1.ShowDialog();
                    break;
                case false:
                    printDocument1.Print();
                    break;
            }
        }

        private void frmPrinterThermal_Load(object sender, EventArgs e)
        {
            // ล้างข้อมูลเก่าออกก่อน
            comboBox1.Items.Clear();

            // วนลูปดึงชื่อเครื่องพิมพ์ทั้งหมดใน Windows
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(printer);
            }

            // เลือกเครื่องพิมพ์เริ่มต้น (Default Printer) ให้โดยอัตโนมัติ
            System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
            comboBox1.Text = settings.PrinterName;
        }
    }
}
