using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Models
{
    public class AccountManagementModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        /// <summary>
        /// Status (Active,Inactive)
        /// </summary>
        public string Status { get; set; }

        public string Position { get; set; }

    }
}
