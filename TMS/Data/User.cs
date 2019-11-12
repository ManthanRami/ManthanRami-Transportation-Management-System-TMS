using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    public enum UserType
    {
        Planner = 0,
        Buyer = 1,
        Admin = 2
    }

    class User
    {
        private int UserID { get; set; }

        private string Username { get; set; }

        private string Password { get; set; }

        private UserType Type { get; set; }
    }
}
