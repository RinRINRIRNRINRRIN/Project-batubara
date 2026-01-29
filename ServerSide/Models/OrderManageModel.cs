using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Models
{
    public class OrderManageModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string JobNumber { get; set; }


        /// <summary>
        /// รูปแบบการชั่ง MIX,BUY,SALE
        /// </summary>
        public string WeightType { get; set; }
        public string PoBuy { get; set; }
        public string PoSale { get; set; }
        public string SuppireName { get; set; }
        public string ProductNamez { get; set; }
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

        /// <summary>
        /// สถานะการชั่ง Planning,Process,Cancel,Success
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// สถานะการตรวจสอบ Wait,Not Check,Approve
        /// </summary>
        public string VerifyStatus { get; set; }
        public string EmployeeCreate { get; set; }

        /// <summary>
        /// สำหรับเก็บข้อมูลผู้ที่ตรวจสอบรายการ
        /// </summary>
        public string EmplpoyeeVerify { get; set; }

        //public string CarNumber { get; set; }
        /// <summary>
        /// เลขที่ชั่งจากแท่นภายนอก
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// ใส่กรณีที่รับน้ำหนักจากแท่นภายนอก
        /// </summary>
        public int OutSideFirstWeight { get; set; }
        /// <summary>
        /// ใส่กรณีที่รับน้ำหนักจากแท่นภายนอก
        /// </summary>
        public int OutSideSecondWeight { get; set; }
        /// <summary>
        /// ใส่กรณีที่รับน้ำหนักจากแท่นภายนอก
        /// </summary>
        public int OutSideNetWeight { get; set; }

        /// <summary>
        /// น้ำหนักสุทธิของการชั่ง
        /// </summary>
        public int NetWeight { get; set; }

        /// <summary>
        /// สำหรับกำหนดค่า remark
        /// </summary>
        public string Remark { get; set; }
    }
}
