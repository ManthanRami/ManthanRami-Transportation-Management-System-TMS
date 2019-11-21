using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using TMS.Exceptions;

namespace TMS.Data
{
    public class TmsDal
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["TMSConnectionString"].ConnectionString;

        public void CreateUser(User user)
        {
            const string queryString =
                "INSERT INTO `User` VALUES (NULL, @username, @password, @email, @firstname, @lastname, @usertype);";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@username", user.Username);
                query.Parameters.AddWithValue("@password", user.Password);
                query.Parameters.AddWithValue("@email", user.Email);
                query.Parameters.AddWithValue("@firstname", user.FirstName);
                query.Parameters.AddWithValue("@lastname", user.LastName);
                query.Parameters.AddWithValue("@usertype", (int) user.Type);

                if (query.ExecuteNonQuery() != 1)
                {
                    throw new CouldNotInsertException();
                }

                conn.Close();
            }
        }

        public int GetUserID(string username)
        {
            Trace.WriteLine(connectionString);

            const string queryString = "SELECT UserID FROM `User` WHERE `User`.`Username` = @username LIMIT 1;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@username", username);

                MySqlDataReader reader = query.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                if (table.Rows.Count == 0)
                {
                    throw new UserNotExistsException("There is no account associated with username '" + username + "'");
                }

                conn.Close();

                return (int) table.Rows[0]["UserID"];
            }
        }

        public List<Carrier> GetCarriers()
        {
            List<Carrier> carriers = new List<Carrier>();

            const string queryString = "SELECT * FROM `Carrier`;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                MySqlDataReader reader = query.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                foreach (DataRow row in table.Rows)
                {
                    Carrier carrier = new Carrier();

                    PopulateCarrier(ref carrier, row);

                    carriers.Add(carrier);
                }

                conn.Close();
            }

            return carriers;
        }

        public Carrier GetCarrier(uint carrierId)
        {
            Carrier carrier = new Carrier();

            const string queryString = "SELECT * FROM `Carrier` WHERE `Carrier`.`CarrierID` = @carrierId LIMIT 1";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@carrierId", carrierId);

                MySqlDataReader reader = query.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                if (table.Rows.Count == 0)
                {
                    throw new CarrierNotExistsException("No carrier by that ID could be found");
                }

                PopulateCarrier(ref carrier, table.Rows[0]);

                conn.Close();
            }

            return carrier;
        }

        private void PopulateCarrier(ref Carrier carrier, DataRow row)
        {
            carrier.CarrierID = (uint)row["CarrierID"];
            carrier.FtlAvailability = (int)row["FtlAvailability"];
            carrier.LtlAvailability = (int)row["LtlAvailability"];

            City depot;
            Enum.TryParse((string)row["DepotCity"], out depot);
            carrier.DepotCity = depot;
        }
    }
}
