using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Models
{
    public class TicketModel
    {
        public int OrderId { get; set; }
        public string OrderType { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public string TransportName { get; set; }
        public string DCNumber { get; set; }
        public string PONumber { get; set; }
        public string CarNumber { get; set; }
        public string Remark { get; set; }
        public string DateIn { get; set; }
        public string DateOut { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }

        public int WeightIn { get; set; }
        public int WeightOut { get; set; }
        public int WeightNet { get; set; }

    }
}
