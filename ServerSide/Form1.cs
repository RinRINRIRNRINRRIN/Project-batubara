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

namespace ServerSide
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font fontHeader = new Font("Tahoma", 10, System.Drawing.FontStyle.Bold);

            int y = 0;
            for (int i = 0; i < 50; i++)
            {
                #region Header
                e.Graphics.DrawString("FUTURE FLEX CO.,LTD", fontHeader, Brushes.Black, new System.Drawing.Point(0, y));
                y = y + 20;
                #endregion
            }
            e.Graphics.DrawString("\n", fontHeader, Brushes.Black, new System.Drawing.Point(0, y));
            e.Graphics.DrawString("\n", fontHeader, Brushes.Black, new System.Drawing.Point(0, y));
            e.Graphics.DrawString("", fontHeader, Brushes.Black, new System.Drawing.Point(0, y));
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
