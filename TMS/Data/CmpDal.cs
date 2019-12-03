using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using TMS.Exceptions;
using TMS.Utils;

namespace TMS.Data
{
    /// <summary>
    /// CmpDal provides an interface for the rest of the application to interact with the CMP Database.
    /// CmpDal allows the application to access the contract marketplace database.
    /// </summary>
    public class CmpDal
    {
        /// <summary>
        /// This string is our CMP DB connection string drawn from App.config
        /// </summary>
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["CMPConnectionString"].ConnectionString;

        /// <summary>
        /// GetContracts() returns a list of contracts currently on the contract marketplace
        /// </summary>
        /// <returns>List<Contract></returns>
        public List<Contract> GetContracts()
        {
            List<Contract> contracts = new List<Contract>();

            const string queryString = "SELECT * FROM Contract;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                MySqlDataReader reader = query.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                TmsDal dal = new TmsDal();

                foreach (DataRow row in table.Rows)
                {
                    Contract contract = new Contract();

                    Customer customer = new Customer();
                    customer.Name = (string) row["Client_Name"];

                    try
                    {
                        customer = dal.GetCustomer(customer.Name);
                    }
                    catch (CustomerNotExistsException)
                    {
                        try
                        {
                            customer = dal.CreateCustomer(customer);
                        }
                        catch (CouldNotInsertException)
                        {
                            Logger.Error(LogOrigin.DATABASE, "Could not create customer");
                        }
                    }

                    // TODO: Customer needs to be created here from the client_name field.
                    // TODO: Customer table takes an ID and a Name and must be created if it
                    // TODO: doesn't already exist.

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
