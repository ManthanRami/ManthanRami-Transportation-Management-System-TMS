using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data;

namespace TMS.Utils
{
    public class Invoice
    {
        public static void GenerateInvoice(Contract contract)
        {
            if (!Directory.Exists("invoices"))
            {
                Directory.CreateDirectory("invoices");
            }

            string dateString = DateTime.Now.ToString("MM-dd-yyyy");
            string filePath = "invoices\\" + contract.Customer.Name + "-" + dateString + ".txt";

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("------------ INVOICE ------------");
                writer.WriteLine("Order Date: " + dateString);
                writer.WriteLine("Customer: " + contract.Customer.Name);
                writer.WriteLine("");
                writer.WriteLine("--- Job Details ---");
                writer.WriteLine("Job Type: " + contract.JobType);
                writer.WriteLine("Van Type: " + contract.VanType);
                writer.WriteLine("Palettes: " + contract.Quantity);
                writer.WriteLine("");
                writer.WriteLine("--- Shipping Details ---");
                writer.WriteLine("Origin: " + contract.Origin);
                writer.WriteLine("Destination: " + contract.Destination);

                /*CitiesData cd = new CitiesData();
                TripLogic tl = new TripLogic(contract.Quantity, (int) contract.Origin, (int) contract.Destination);

                writer.WriteLine("ETA: " + "<eta goes here>");*/
            }
        }
    }
}
