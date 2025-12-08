using DocumentFormat.OpenXml.Wordprocessing;
using ESC_POS_USB_NET.Enums;
using ESC_POS_USB_NET.Printer;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide.Functions
{
    internal class ThermalPrinter
    {
        public ThermalPrinter()
        {
            printerName = ConfigurationManager.AppSettings["PrinterName"];
            encodingPage = "TIS-620";
        }


        private readonly string printerName;
        /// <summary>
        /// ต้องใช้ TIS-620 เพื่อให้ปริ้นเตอร์รองรับภาษาไทย
        /// </summary>
        private readonly string encodingPage;

        public string Error { get; set; }

        /// <summary>
        /// สำหรับปริ้นกระดาษ
        /// </summary>
        /// <param name="_model"></param>
        public bool PrintPage(ThermalPrinterModel _model)
        {
            try
            {
                //--------------- Header
                Printer printer = new Printer(printerName, encodingPage);
                Bitmap bitmap = new Bitmap(_model.Logo, 2500, 1200);
                printer.Image(bitmap);
                printer.NewLines(2);
                printer.AlignCenter();
                printer.DoubleWidth3();
                printer.Append("บัตรขนสินค้าออกขาย");
                printer.NewLines(3);
                printer.AlignLeft();
                printer.DoubleWidth2();
                printer.Append($"ทะเบียนรถ : {_model.LicensePLate}");
                //--------------- Body
                printer.NewLines(3);
                printer.NormalWidth();
                printer.Append($" เลขที่ชั่ง : {_model.OrderNumber}");
                printer.Append($" เลขที่ใบงาน : {_model.JobNumber}");
                printer.Append($" บ.ขนส่ง : {_model.TransportName}");
                printer.Append($" ผู้ขับรถ : {_model.DriverName}");
                printer.Append(" วันเวลาที่พิมพ์ : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("TH-th")));
                printer.NewLines(3);
                printer.Append($"สินค้า : {_model.ProductNamez}");
                printer.NewLines(3);
                printer.DoubleWidth2();
                printer.Append("สถานีต้นทาง");
                printer.NormalWidth();
                printer.Append($"{_model.StartStationName}");
                printer.NewLines(2);
                printer.DoubleWidth2();
                printer.Append("สถานีปลายทาง");
                printer.NormalWidth();
                printer.Append($"{_model.EndStationName}");
                printer.NewLines(3);
                printer.Append($"พนักงานออกบัตร : {_model.Employee}");

                //-------------- Footer
                printer.Separator();
                printer.FullPaperCut();
                printer.PrintDocument();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
            return true;

        }

    }
}
