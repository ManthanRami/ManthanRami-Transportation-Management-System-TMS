using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TMS.Data
{
    class CmpDal
    {
        private string connectionString =
            ConfigurationManager.ConnectionStrings["CMPConnectionString"].ConnectionString;

        public List<Contract> GetContracts()
        {
            List<Contract> contracts = new List<Contract>();

            const string queryString = "SELECT * FROM Contract";

            using (MySqlConnection myConn = new MySqlConnection(connectionString))
            {
                MySqlCommand query = new MySqlCommand(queryString);
                MySqlDataReader reader = query.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                foreach (DataRow row in table.Rows)
                {
                    Contract contract = new Contract();

                    contract.ContractID = (uint) row["ContractID"];
                    contract.CarrierID = (uint) row["CarrierID"];
                    contract.Client = (string) row["Client"];
                    contract.JobType = (JobType) row["JobType"];
                    contract.VanType = (VanType) row["VanType"];
                    contract.Quantity = (uint) row["Quantity"];

                    contracts.Add(contract);
                }
            }

            return contracts;
        }
    }
}
