using ESC_POS_USB_NET.Enums;
using ESC_POS_USB_NET.Printer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("ออก", null, OnExit);
            contextMenu.Items.Add("เปิด", null, OnLoad);

            // สร้างไอคอนใน System Tray
            notifyIcon = new NotifyIcon();
            notifyIcon.Text = "My Desktop App";
            notifyIcon.Icon = new System.Drawing.Icon("D:\\image\\mainback.ico"); // ใช้ .ico เท่านั้น
            notifyIcon.ContextMenuStrip = contextMenu;
            notifyIcon.Visible = true;

            // ไม่แสดงฟอร์มหลัก
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
        }


        private void OnExit(object sender, EventArgs e)
        {

        }

        private void OnLoad(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            this.Visible = true;
        }

        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenu;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font header = new Font("Tahoma", 12, FontStyle.Bold);
            Font detail = new Font("Tahoma", 8, FontStyle.Regular);
            Font SubHeader = new Font("Tahoma", 8, FontStyle.Bold);
            e.Graphics.DrawImage(pictureBox2.Image, 70, 5, 150, 100);
            e.Graphics.DrawString("บัตรขนสินค้าออกขาย", header, Brushes.Black, 10, 120);
            e.Graphics.DrawString("ทะเบียนรถ : ผก-1932", detail, Brushes.Black, 10, 160);
            e.Graphics.DrawString("เลขที่ชั่ง : OR680624-1", detail, Brushes.Black, 10, 180);
            e.Graphics.DrawString("บริษัทขนส่ง : OR680624-1", detail, Brushes.Black, 10, 200);
            e.Graphics.DrawString("ผู้ขับรถ : OR680624-1", detail, Brushes.Black, 10, 220);
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("TH-th"));
            e.Graphics.DrawString($"วันที่พิมพ์ : {date}", detail, Brushes.Black, 10, 240);
            e.Graphics.DrawString($"สินค้า : ถ่ายหินพิเศษ", detail, Brushes.Black, 10, 280);
            e.Graphics.DrawString($"สถานีต้นทาง", SubHeader, Brushes.Black, 10, 320);
            e.Graphics.DrawString($"โกดัง BBE1 ตำแหน่ง B1", detail, Brushes.Black, 10, 340);
            e.Graphics.DrawString($"สถานีปลายทาง", SubHeader, Brushes.Black, 10, 380);
            e.Graphics.DrawString($"บ.ปูนซีเมนต์เอเชีย จำกัด (มหาชน)", detail, Brushes.Black, 10, 400);
            e.Graphics.DrawString($"พนักงานออกบัตร : กรินทร์", detail, Brushes.Black, 10, 440);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

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
            catch (Exception ex)
            {


            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Printer printer = new Printer("EPSON TM-82X", "TIS-620");
            //--------------- Header
            printer.AlignCenter();
            printer.BoldMode("TEST PAGE FOR CUSTOMER");
            printer.AlignLeft();
            printer.DoubleWidth3();
            //--------------- Body
            Bitmap bitmap = new Bitmap(pictureBox2.Image, 2500, 1200);
            printer.Image(bitmap);
            printer.NewLines(2);
            printer.AlignCenter();
            printer.DoubleWidth3();
            printer.Append("บัตรขนสินค้าออกขาย");
            printer.NewLines(3);
            printer.AlignLeft();
            printer.DoubleWidth2();
            printer.Append("ทะเบียนรถ : ผก-1932");
            printer.NewLines(3);
            printer.NormalWidth();
            printer.Append(" เลขที่ชั่ง : ORDER680626-1");
            printer.Append(" เลขที่ใบงาน : JB680626-1");
            printer.Append(" บ.ขนส่ง : ไทยเครื่องชั่ง ทรานสปอตส์");
            printer.Append(" ผู้ขับรถ : ไทยเครื่องชั่ง Driver");
            printer.Append(" วันเวลาที่พิมพ์ : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("TH-th")));
            printer.NewLines(3);
            printer.Append("สินค้า : Loadcell 3 ตัว");
            printer.NewLines(3);
            printer.DoubleWidth2();
            printer.Append("สถานีต้นทาง");
            printer.NormalWidth();
            printer.Append("ไทยเครื่องชั่งสำนักงานใหม่ กกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกก");
            printer.NewLines(2);
            printer.DoubleWidth2();
            printer.Append("สถานีปลายทาง");
            printer.NormalWidth();
            printer.Append("ไทยเครื่องชั่ง สาขา เชียงใหม่ กกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกกก");
            printer.NewLines(3);
            printer.Append("พนักงานออกบัตร : Karin techarattanayuenyong");

            //-------------- Footer
            printer.Separator();
            printer.FullPaperCut();
            printer.PrintDocument();
            //  printer.Append(Encoding.GetEncoding("TIS-620").GetBytes("ทดสอบของลูกค้า"));
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

        }
    }
}
