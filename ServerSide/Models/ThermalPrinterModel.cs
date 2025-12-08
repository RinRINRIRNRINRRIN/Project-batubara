using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Models
{
    internal class ThermalPrinterModel
    {
        public Image Logo { get; set; }
        public string LicensePLate { get; set; }
        public string OrderNumber { get; set; }
        public string JobNumber { get; set; }
        public string TransportName { get; set; }
        public string DriverName { get; set; }
        public string StartStationName { get; set; }
        public string EndStationName { get; set; }
        public string Employee { get; set; }

        public string ProductNamez { get; set; }
    }
}
