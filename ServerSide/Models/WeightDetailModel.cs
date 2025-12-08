using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Models
{
    internal class WeightDetailModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string DateWeighing { get; set; }
        /// <summary>
        /// ประเภทชั้ง ว่าเข้าหรือออก
        /// </summary>
        public string WeightType { get; set; }
        public string Employee { get; set; }

        public int NetWeight { get; set; }
    }
}
