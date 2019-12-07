using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    public static class DataMap
    {
        private static List<Contract> contracts = new List<Contract>();

        public static void AddContract(Contract contract)
        {
            contracts.Add(contract);
        }

        public static void RemoveContract(Contract contract)
        {
            contracts.Remove(contract);
        }
    }
}
