using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    public class Carrier
    {
        public uint CarrierID { get; set; }
        public City DepotCity { get; set; }

        public int FtlAvailability { get; set; }
        public int LtlAvailability { get; set; }
    }
}
