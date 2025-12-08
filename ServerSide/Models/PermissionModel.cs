using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Models
{
    internal class PermissionModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Menuname { get; set; }
        public string MenuType { get; set; }

    }
}
