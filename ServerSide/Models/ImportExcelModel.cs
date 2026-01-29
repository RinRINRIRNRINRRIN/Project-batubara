using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Models
{
    internal class ImportExcelModel
    {
        public string JobNumber { get; set; }


        /// <summary>
        /// รูปแบบการชั่ง MIX,BUY,SALE
        /// </summary>
        public string WeightType { get; set; }
        public string PoBuy { get; set; }
        public string PoSale { get; set; }
        public string SuppireName { get; set; }
        public string Productname { get; set; }
        public string CustomerName { get; set; }
        public string RawMatName { get; set; }
        public string StartStationName { get; set; }
        public string EndStationName { get; set; }

        /// <summary>
        /// รูปแบบสถานี  INSIDE,OUTSIDE
        /// </summary>
        public string StartStationType { get; set; }
        /// <summary>
        /// รูปแบบสถานี  INSIDE,OUTSIDE
        /// </summary>
        public string EndStationType { get; set; }
        public string TransportName { get; set; }
        public string LIcensePlate { get; set; }
        public string DriverName { get; set; }
        public string DateCreate { get; set; }
        public string Status { get; set; }
        public string EmployeeCreate { get; set; }

        public string QC_code { get; set; }

    }
}
