using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    /// <summary>
    /// The Carrier class models the carrier table found within the TMS database.
    ///
    /// It contains a variety of properties to provide programmers an easy to use interface
    /// with the carrier data.
    /// </summary>
    public class Carrier
    {
        public uint CarrierID { get; set; }
        public City DepotCity { get; set; }

        public int FtlAvailability { get; set; }
        public int LtlAvailability { get; set; }
        public float FTLRate { get; set; }
        public float LTLRate { get; set; }
        public float ReeferCharge { get; set; }
    }
}
