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
    /// <summary>
    /// TmsDal provides an interface for the rest of the application to interact with the TMS database.
    /// It contains methods to do a variety of CRUD actions on a variety of tables.
    /// </summary>
    public class TmsDal
    {
        /// <summary>
        /// This string is our TMS DB connection string drawn from App.config
        /// </summary>
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["TMSConnectionString"].ConnectionString;

        /// <summary>
        /// CreateUser() takes in a User object with it's desired values set, and inserts it into the TMS database.
        ///
        /// It then grabs that new User's ID and updates the User object before returning the now fully useable
        /// User object.
        /// </summary>
        /// <param name="user">User user</param>
        /// <returns>User user</returns>
        public User CreateUser(User user)
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

                // Update the passed in user object to include the it's new ID
                user.UserID = GetLastInsertId(conn);

                conn.Close();
            }

            return user;
        }

        /// <summary>
        /// GetUserID() takes a username as a parameter, and attempts to find a user with that username.
        ///
        /// Once found, it returns their UserID.
        /// </summary>
        /// <param name="username">string username</param>
        /// <returns>uint userid</returns>
        public uint GetUserID(string username)
        {
            uint UserID;

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

                UserID = (uint) (int) table.Rows[0]["UserID"];
            }

            return UserID;
        }

        /// <summary>
        /// CreateCarrier inserts a carrier into the TMS database. It takes a Carrier object, inserts it
        /// and then updates the passed in Carrier object with it's new CarrierID before returning the
        /// now fully useable Carrier object.
        /// </summary>
        /// <param name="carrier">Carrier carrier</param>
        /// <returns>Carrier</returns>
        public Carrier CreateCarrier(Carrier carrier)
        {
            const string queryString =
                "INSERT INTO `Carrier` VALUES (NULL, @depotCity, @ftlAvailability, @ltlAvailability);";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@depotCity", carrier.DepotCity.ToString());
                query.Parameters.AddWithValue("@ftlAvailability", carrier.FtlAvailability);
                query.Parameters.AddWithValue("@ltlAvailability", carrier.LtlAvailability);

                if (query.ExecuteNonQuery() != 1)
                {
                    throw new CouldNotInsertException();
                }

                // Update the passed in carrier object to include the it's new ID
                carrier.CarrierID = GetLastInsertId(conn);

                conn.Close();
            }

            return carrier;
        }

        /// <summary>
        /// GetCarriers() returns a list of carriers in the TMS database.
        /// </summary>
        /// <returns>List<Carrier></returns>
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

        /// <summary>
        /// GetCarrier() takes in a CarrierID, checks the database for a matching carrier and if found
        /// it returns a Carrier object representing the found carrier.
        /// </summary>
        /// <param name="carrierId">uint carrierId</param>
        /// <returns>Carrier</returns>
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

        /// <summary>
        /// UpdateCarrier takes a CarrierID and a carrier object with the needed changes and updates
        /// the carrier matching that ID in the database.
        /// </summary>
        /// <param name="carrierId">uint carrierId</param>
        /// <param name="carrier">Carrier carrier</param>
        /// <returns>Carrier</returns>
        public Carrier UpdateCarrier(uint carrierId, Carrier carrier)
        {
            const string queryString = @"UPDATE `Carrier` SET 
                                        `Carrier`.`DepotCity` = @depotCity,
                                        `Carrier`.`FtlAvailability` = @ftlAvailability,
                                        `Carrier`.`LtlAvailability` = @ltlAvailability
                                        WHERE `Carrier`.`CarrierID` = @carrierId;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@depotCity", carrier.DepotCity.ToString());
                query.Parameters.AddWithValue("@ftlAvailability", carrier.FtlAvailability);
                query.Parameters.AddWithValue("@ltlAvailability", carrier.LtlAvailability);
                query.Parameters.AddWithValue("@carrierId", carrierId);

                if (query.ExecuteNonQuery() == 0)
                {
                    throw new CouldNotUpdateException();
                }

                conn.Close();
            }

            return carrier;
        }

        /// <summary>
        /// SetReeferCharge takes a CarrierID and a ftlRate, and either creates a new row
        /// in the FTLRate table or updates an existing one.
        /// </summary>
        /// <param name="carrierId">uint carrierId</param>
        /// <param name="ftlRate">float ftlRate</param>
        public void SetFtlRate(uint carrierId, float ftlRate)
        {
            const string existsQueryString =
                "SELECT COUNT(CarrierID) FROM `FTLRate` WHERE `FTLRate`.`CarrierID` = @carrierId";
            const string insertQueryString = "INSERT INTO `FTLRate` VALUES (@carrierId, @ftlRate);";
            const string updateQueryString = "UPDATE `FTLRate` SET `FTLRate`.`Rate` = @ftlRate WHERE `FTLRate`.`CarrierID` = @carrierId;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(existsQueryString, conn);
                query.Parameters.AddWithValue("@carrierId", carrierId);

                int result = (int) (long) query.ExecuteScalar();

                // Determine which string we're going to use. If the rate already exists in the table, then
                // we want to update it. Otherwise, we're going to insert.
                string settingQuery = result == 0 ? insertQueryString : updateQueryString;


                query = new MySqlCommand(settingQuery, conn);
                query.Parameters.AddWithValue("@carrierId", carrierId);
                query.Parameters.AddWithValue("@ftlRate", ftlRate);

                if (query.ExecuteNonQuery() == 0)
                {
                    if (settingQuery == insertQueryString)
                    {
                        throw new CouldNotInsertException();
                    }
                    else
                    {
                        throw new CouldNotUpdateException();
                    }
                }

                conn.Close();
            }
        }

        /// <summary>
        /// SetReeferCharge takes a CarrierID and a ltlRate, and either creates a new row
        /// in the LTLRate table or updates an existing one.
        /// </summary>
        /// <param name="carrierId">uint carrierId</param>
        /// <param name="ltlRate">float ltlRate</param>
        public void SetLtlRate(uint carrierId, float ltlRate)
        {
            const string existsQueryString =
                "SELECT COUNT(CarrierID) FROM `LTLRate` WHERE `LTLRate`.`CarrierID` = @carrierId";
            const string insertQueryString = "INSERT INTO `LTLRate` VALUES (@carrierId, @ltlRate);";
            const string updateQueryString = "UPDATE `LTLRate` SET `LTLRate`.`Rate` = @ltlRate WHERE `LTLRate`.`CarrierID` = @carrierId;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(existsQueryString, conn);
                query.Parameters.AddWithValue("@carrierId", carrierId);

                int result = (int) (long) query.ExecuteScalar();

                // Determine which string we're going to use. If the rate already exists in the table, then
                // we want to update it. Otherwise, we're going to insert.
                string settingQuery = result == 0 ? insertQueryString : updateQueryString;


                query = new MySqlCommand(settingQuery, conn);
                query.Parameters.AddWithValue("@carrierId", carrierId);
                query.Parameters.AddWithValue("@ltlRate", ltlRate);

                if (query.ExecuteNonQuery() == 0)
                {
                    if (settingQuery == insertQueryString)
                    {
                        throw new CouldNotInsertException();
                    }
                    else
                    {
                        throw new CouldNotUpdateException();
                    }
                }

                conn.Close();
            }
        }

        /// <summary>
        /// SetReeferCharge takes a CarrierID and a reeferCharge, and either creates a new row
        /// in the ReeferCharge table or updates an existing one.
        /// </summary>
        /// <param name="carrierId">uint carrierId</param>
        /// <param name="reeferCharge">float reeferCharge</param>
        public void SetReeferCharge(uint carrierId, float reeferCharge)
        {
            const string existsQueryString =
                "SELECT COUNT(CarrierID) FROM `ReeferCharge` WHERE `ReeferCharge`.`CarrierID` = @carrierId";
            const string insertQueryString = "INSERT INTO `ReeferCharge` VALUES (@carrierId, @reeferCharge);";
            const string updateQueryString = "UPDATE `ReeferCharge` SET `ReeferCharge`.`Charge` = @reeferCharge WHERE `ReeferCharge`.`CarrierID` = @carrierId;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(existsQueryString, conn);
                query.Parameters.AddWithValue("@carrierId", carrierId);

                int result = (int)(long)query.ExecuteScalar();

                // Determine which string we're going to use. If the rate already exists in the table, then
                // we want to update it. Otherwise, we're going to insert.
                string settingQuery = result == 0 ? insertQueryString : updateQueryString;


                query = new MySqlCommand(settingQuery, conn);
                query.Parameters.AddWithValue("@carrierId", carrierId);
                query.Parameters.AddWithValue("@reeferCharge", reeferCharge);

                if (query.ExecuteNonQuery() == 0)
                {
                    if (settingQuery == insertQueryString)
                    {
                        throw new CouldNotInsertException();
                    }
                    else
                    {
                        throw new CouldNotUpdateException();
                    }
                }

                conn.Close();
            }
        }

        /// <summary>
        /// PopulateCarrier() takes a reference to a carrier object and a data row and parses
        /// the data row into the carrier object.
        /// </summary>
        /// <param name="carrier">ref Carrier carrier</param>
        /// <param name="row">DataRow row</param>
        private void PopulateCarrier(ref Carrier carrier, DataRow row)
        {
            carrier.CarrierID = (uint) (int) row["CarrierID"];
            carrier.FtlAvailability = (int) row["FtlAvailability"];
            carrier.LtlAvailability = (int) row["LtlAvailability"];

            City depot;
            Enum.TryParse((string) row["DepotCity"], out depot);
            carrier.DepotCity = depot;
        }

        /// <summary>
        /// This method returns the result of SELECT LAST_INSERT_ID(). This method MUST be provided an
        /// open connection. DO NOT close the connection until AFTER this command was executed.
        /// </summary>
        /// <param name="conn">MySqlConnection conn</param>
        /// <returns>uint</returns>
        private uint GetLastInsertId(MySqlConnection conn)
        {
            const string queryString = "SELECT LAST_INSERT_ID();";

            MySqlCommand query = new MySqlCommand(queryString, conn);

            object scalar = query.ExecuteScalar();

            if (scalar == null)
            {
                throw new CouldNotGetIdException();
            }

            ulong id = (ulong) scalar;

            return (uint) id;
        }
    }
}
