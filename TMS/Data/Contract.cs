using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    public enum City
    {
        Windsor = 0,
        London = 1,
        Hamilton = 2,
        Toronto = 3,
        Oshawa = 4,
        Belleville = 5,
        Kingston = 6,
        Ottawa = 7
    }

    public enum JobType
    {
        FTL = 0,
        LTL = 1
    }

    public enum VanType
    {
        Dry = 0,
        Reefer = 1
    }

    class Contract
    {
        public uint ContractID { get; set; }
        public uint CarrierID { get; set; }
        public String Client { get; set; }

        public JobType JobType { get; set; }
        public VanType VanType { get; set; }

        public uint Quantity { get; set; }

        public City origin;
        public City destination;
    }
}
