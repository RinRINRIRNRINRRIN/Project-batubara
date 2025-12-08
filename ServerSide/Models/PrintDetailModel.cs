using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Models
{
    public class PrintDetailModel
    {

        public int Id { get; set; }
        public int Seq { get; set; }
        public int OrderId { get; set; }
        public string Employee { get; set; }
        public string DateTimez { get; set; }
        public string Note { get; set; }
    }
}
