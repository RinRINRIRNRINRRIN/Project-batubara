using DocumentFormat.OpenXml.Wordprocessing;
using ESC_POS_USB_NET.Enums;
using ESC_POS_USB_NET.Printer;
using Newtonsoft.Json;
using Serilog;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide.Functions
{
    internal class ThermalPrinter
    {
        public ThermalPrinter(string _printerName)
        {
            printerName = _printerName;
            encodingPage = "TIS-620";
            // 1. กำหนดชื่อเครื่องพิมพ์ที่เลือกจาก ComboBox ให้กับ PrintDocument
            PrintDocument.PrinterSettings.PrinterName = printerName;
            PrintDocument.PrintPage += PrintDocument_PrintPage;
        }
        PictureBox pictureBox2 = new PictureBox();

        PrintDocument PrintDocument = new PrintDocument();

        private readonly string printerName;
        /// <summary>
        /// ต้องใช้ TIS-620 เพื่อให้ปริ้นเตอร์รองรับภาษาไทย
        /// </summary>
        private readonly string encodingPage;

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int xColon = 110;
            System.Drawing.Font header = new System.Drawing.Font("Tahoma", 12, FontStyle.Bold);
            System.Drawing.Font detail = new System.Drawing.Font("Tahoma", 12, FontStyle.Regular);
            System.Drawing.Font SubHeader = new System.Drawing.Font("Tahoma", 8, FontStyle.Bold);
            e.Graphics.DrawImage(pictureBox2.Image, 90, 5, 100, 80);
            //e.Graphics.DrawString("บัตรขนสินค้าออกขาย", header, Brushes.Black, 10, 120);
            e.Graphics.DrawString("ทะเบียนรถ", detail, Brushes.Black, 10, 120);
            e.Graphics.DrawString(": ผก-1932", detail, Brushes.Black, xColon, 120);
            e.Graphics.DrawString("เลขที่ชั่ง", detail, Brushes.Black, 10, 140);
            e.Graphics.DrawString(": OR680624-1", detail, Brushes.Black, xColon, 140);
            e.Graphics.DrawString("เลขที่ใบงาน", detail, Brushes.Black, 10, 160);
            e.Graphics.DrawString(": IMR23425", detail, Brushes.Black, xColon, 160);
            e.Graphics.DrawString("บริษัท(ลูกค้า)", detail, Brushes.Black, 10, 180);
            e.Graphics.DrawString(": ไทยเครื่องช่ัง", detail, Brushes.Black, xColon, 180);
            //  e.Graphics.DrawString("ผู้ขับรถ : OR680624-1", detail, Brushes.Black, 10, 220);
            string date = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("TH-th"));
            string time = DateTime.Now.ToString("HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("TH-th"));
            e.Graphics.DrawString($"วันที่พิมพ์", detail, Brushes.Black, 10, 220);
            e.Graphics.DrawString($": {date}", detail, Brushes.Black, xColon, 220);
            e.Graphics.DrawString($": {time}", detail, Brushes.Black, xColon, 240);
            //    e.Graphics.DrawString($"สินค้า : ถ่ายหินพิเศษ", detail, Brushes.Black, 10, 280);
            e.Graphics.DrawString($"QC Code ", detail, Brushes.Black, 10, 280);
            e.Graphics.DrawString($"123/32-22/2/32/222/2323232", detail, Brushes.Black, 10, 300);
            //   e.Graphics.DrawString($"สถานีต้นทาง", SubHeader, Brushes.Black, 10, 320);
            e.Graphics.DrawString($"พนักงานออกบัตร :", detail, Brushes.Black, 10, 340);
            e.Graphics.DrawString($"กรินทร์ เตชะรัตนยืนยง", detail, Brushes.Black, 10, 360);
            e.Graphics.DrawString($"-----------------------------------------------------", detail, Brushes.Black, 10, 400);
            //  e.Graphics.DrawString($"บ.ปูนซีเมนต์เอเชีย จำกัด (มหาชน)", detail, Brushes.Black, 10, 400);
            //  e.Graphics.DrawString($"พนักงานออกบัตร : กรินทร์", detail, Brushes.Black, 10, 440);
        }

        public string Error { get; set; }


        public bool PrintPage(ThermalPrinterModel _model)
        {
            pictureBox2.Image = _model.Logo;
            printerModel = _model;

            PrintDocument.Print();
            return true;
        }
    }
}
