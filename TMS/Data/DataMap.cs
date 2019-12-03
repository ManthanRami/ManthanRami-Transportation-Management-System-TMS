using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    public static class DataMap
    {
        private static List<Customer> customers = new List<Customer>();

        public static void RegisterCustomer(Customer customer)
        {
            customers.Add(customer);
        }
    }
}
