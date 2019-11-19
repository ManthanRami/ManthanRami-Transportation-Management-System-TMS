using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using static TMS.Data.City;

namespace TMS.Data
{
    public class CmpDal
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["CMPConnectionString"].ConnectionString;

        public List<Contract> GetContracts()
        {
            List<Contract> contracts = new List<Contract>();

            const string queryString = "SELECT * FROM Contract";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                MySqlDataReader reader = query.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                foreach (DataRow row in table.Rows)
                {
                    Contract contract = new Contract();

                    contract.Client = (string) row["Client_Name"];
                    contract.JobType = (JobType) row["Job_Type"];
                    contract.VanType = (VanType) row["Van_Type"];
                    contract.Quantity = (int) row["Quantity"];

                    City origin;
                    City destination;

                    Enum.TryParse((string) row["Origin"], out origin);
                    Enum.TryParse((string) row["Destination"], out destination);

                    contract.Origin = origin;
                    contract.Destination = destination;

                    contracts.Add(contract);
                }

                conn.Close();
            }

            return contracts;
        }
    }
}
