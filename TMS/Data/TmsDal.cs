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
using TMS.Utils;

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
        
        
        // private readonly string connectionString = ConfigurationManager.ConnectionStrings["TMSConnectionString"].ConnectionString;

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
                "INSERT INTO `User` VALUES (NULL, @username, @password, @email, @firstname, @lastname, @userType);";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@username", user.Username);
                query.Parameters.AddWithValue("@password", user.Password);
                query.Parameters.AddWithValue("@email", user.Email);
                query.Parameters.AddWithValue("@firstname", user.FirstName);
                query.Parameters.AddWithValue("@lastname", user.LastName);
                query.Parameters.AddWithValue("@userType", (int) user.Type);

                if (query.ExecuteNonQuery() != 1)
                {
                    Logger.Error(LogOrigin.Database, "(CreateUser) Could not insert new user into database");
                    throw new CouldNotInsertException();
                }

                // Update the passed in user object to include the it's new ID
                user.UserID = GetLastInsertId(conn);

                conn.Close();
            }

            Logger.Info(LogOrigin.Database, "(CreateUser) User '" + user.Username + "' has been created");

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
                    Logger.Warn(LogOrigin.Database, "Could not find user '" + username + "'");
                    throw new UserNotExistsException("There is no account associated with username '" + username + "'");
                }

                conn.Close();

                UserID = (uint) (int) table.Rows[0]["UserID"];
            }

            Logger.Info(LogOrigin.Database, "(GetUserID) User '" + username + "' fetched from database");

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
                "INSERT INTO `Carrier` VALUES (NULL, @name, @depotCity, @ftlAvailability, @ltlAvailability);";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@name", carrier.Name);
                query.Parameters.AddWithValue("@depotCity", carrier.DepotCity.ToString());
                query.Parameters.AddWithValue("@ftlAvailability", carrier.FtlAvailability);
                query.Parameters.AddWithValue("@ltlAvailability", carrier.LtlAvailability);

                if (query.ExecuteNonQuery() != 1)
                {
                    Logger.Error(LogOrigin.Database, "(CreateCarrier) Could not insert new carrier into database");
                    throw new CouldNotInsertException();
                }

                // Update the passed in carrier object to include the it's new ID
                carrier.CarrierID = GetLastInsertId(conn);

                conn.Close();
            }

            Logger.Info(LogOrigin.Database, "(CreateCarrier) Carrier '" + carrier.Name + "' has been created");

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

            Logger.Info(LogOrigin.Database, "(GetCarriers) Fetched " + carriers.Count + " carriers from the database");

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
                    Logger.Warn(LogOrigin.Database, "(GetCarrier) Could not find a carrier with an ID of " + carrierId);
                    throw new CarrierNotExistsException("No carrier by that ID could be found");
                }

                PopulateCarrier(ref carrier, table.Rows[0]);

                conn.Close();
            }

            Logger.Info(LogOrigin.Database, "(GetCarrier) Fetched carrier '" + carrier.Name + "' from the database");

            return carrier;
        }

        /// <summary>
        /// This method finds a list of carriers based on their depot city
        /// </summary>
        /// <param name="city">City</param>
        /// <returns>List<Carrier></returns>
        public List<Carrier> GetCarriersByCity(City city)
        {
            List<Carrier> carriers = new List<Carrier>();

            const string queryString = "SELECT * FROM `Carrier` WHERE `Carrier`.`DepotCity` = @city;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@city", city.ToString());
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
        /// This method takes in a carrier's ID, and deletes it.
        /// </summary>
        /// <param name="carrierId">uint</param>
        public void DeleteCarrier(uint carrierId)
        {

            const string deleteLtlRateQueryString = "DELETE FROM `LTLRate` WHERE `LTLRate`.`CarrierID` = @carrierId";
            const string deleteFtlRateQueryString = "DELETE FROM `FTLRate` WHERE `FTLRate`.`CarrierID` = @carrierId";
            const string deleteReeferChargeQueryString = "DELETE FROM `ReeferCharge` WHERE `ReeferCharge`.`CarrierID` = @carrierId";
            const string deleteCarrierQueryString = "DELETE FROM `Carrier` WHERE `Carrier`.`CarrierID` = @carrierId";
            
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(deleteLtlRateQueryString, conn);
                query.Parameters.AddWithValue("@carrierId", carrierId);
                query.ExecuteNonQuery();

                query = new MySqlCommand(deleteFtlRateQueryString, conn);
                query.Parameters.AddWithValue("@carrierId", carrierId);
                query.ExecuteNonQuery();

                query = new MySqlCommand(deleteReeferChargeQueryString, conn);
                query.Parameters.AddWithValue("@carrierId", carrierId);
                query.ExecuteNonQuery();

                query = new MySqlCommand(deleteCarrierQueryString, conn);
                query.Parameters.AddWithValue("@carrierId", carrierId);
                if (query.ExecuteNonQuery() == 0)
                {
                    Logger.Warn(LogOrigin.Database, "(DeleteCarrier) Could not delete carrier with an ID of " + carrierId);
                    throw new CouldNotDeleteException("Carrier with ID " + carrierId + " could not be deleted");
                }

                conn.Close();
            }

            Logger.Info(LogOrigin.Database, "(DeleteCarrier) Deleted carrier with ID " + carrierId + " from the database");
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
                                        `Carrier`.`Name` = @name,
                                        `Carrier`.`DepotCity` = @depotCity,
                                        `Carrier`.`FtlAvailability` = @ftlAvailability,
                                        `Carrier`.`LtlAvailability` = @ltlAvailability
                                        WHERE `Carrier`.`CarrierID` = @carrierId;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@name", carrier.Name);
                query.Parameters.AddWithValue("@depotCity", carrier.DepotCity.ToString());
                query.Parameters.AddWithValue("@ftlAvailability", carrier.FtlAvailability);
                query.Parameters.AddWithValue("@ltlAvailability", carrier.LtlAvailability);
                query.Parameters.AddWithValue("@carrierId", carrierId);

                if (query.ExecuteNonQuery() == 0)
                {
                    Logger.Warn(LogOrigin.Database, "(UpdateCarrier) Could not update carrier ID " + carrierId);
                    throw new CouldNotUpdateException();
                }

                conn.Close();
            }

            Logger.Info(LogOrigin.Database, "(UpdateCarrier) Updated carrier ID " + carrierId);

            return carrier;
        }

        /// <summary>
        /// This method takes 2 queries and returns any carrier that is like them
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="depotCity">string</param>
        /// <returns>List<Carrier></returns>
        public List<Carrier> SearchCarriers(string name, string depotCity)
        {
            List<Carrier> carriers = new List<Carrier>();

            const string queryString = @"SELECT * FROM `Carrier` c WHERE
                (c.Name LIKE CONCAT(@name, '%') OR @name = '') AND
                (c.DepotCity LIKE CONCAT(@depotCity, '%') OR @deoitCity = '');";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@name", name);
                query.Parameters.AddWithValue("@depotCity", depotCity);
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

            Logger.Info(LogOrigin.Database, "(SearchCarriers) Fetched " + carriers.Count + " carriers");

            return carriers;
        }

        /// <summary>
        /// This method gets a carrier's FTL Rate
        /// </summary>
        /// <param name="carrierId">uint</param>
        /// <returns>float</returns>
        public float GetFtlRate(uint carrierId)
        {
            const string queryString = "SELECT Rate FROM FTLRate WHERE `FTLRate`.`CarrierID` = @carrierId;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@carrierId", carrierId);

                object rate = query.ExecuteScalar();

                if (rate == null)
                {
                    throw new CouldNotFindRateException();
                }

                conn.Close();

                return (float)rate;
            }
        }

        /// <summary>
        /// SetFtlRate takes a CarrierID and a ftlRate, and either creates a new row
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
                        Logger.Error(LogOrigin.Database, "(SetFtlRate) Could not insert FTL rate for carrier " + carrierId);
                        throw new CouldNotInsertException();
                    }

                    Logger.Error(LogOrigin.Database, "(SetFtlRate) Could not update FTL rate for carrier " + carrierId);
                    throw new CouldNotUpdateException();
                }

                conn.Close();
            }

            Logger.Info(LogOrigin.Database, "(SetFtlRate) FTLRate of carrier " + carrierId + " has been set to " + ftlRate);
        }

        /// <summary>
        /// This method gets a carrier's LTL Rate
        /// </summary>
        /// <param name="carrierId">uint</param>
        /// <returns>float</returns>
        public float GetLtlRate(uint carrierId)
        {
            const string queryString = "SELECT Rate FROM LTLRate WHERE `LTLRate`.`CarrierID` = @carrierId;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@carrierId", carrierId);

                object rate = query.ExecuteScalar();

                if (rate == null)
                {
                    throw new CouldNotFindRateException();
                }

                conn.Close();

                return (float) rate;
            }
        }

        /// <summary>
        /// SetLtlRate takes a CarrierID and a ltlRate, and either creates a new row
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
                        Logger.Error(LogOrigin.Database, "(SetLtlRate) Could not insert LTL rate for carrier " + carrierId);
                        throw new CouldNotInsertException();
                    }

                    Logger.Error(LogOrigin.Database, "(SetLtlRate) Could not update LTL rate for carrier " + carrierId);
                    throw new CouldNotUpdateException();
                }

                conn.Close();
            }

            Logger.Info(LogOrigin.Database, "(SetLtlRate) LTLRate of carrier " + carrierId + " has been set to " + ltlRate);
        }

        /// <summary>
        /// This method gets a carrier's Reefer Charge
        /// </summary>
        /// <param name="carrierId">uint</param>
        /// <returns>float</returns>
        public float GetReeferCharge(uint carrierId)
        {
            const string queryString = "SELECT Charge FROM ReeferCharge WHERE `ReeferCharge`.`CarrierID` = @carrierId;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@carrierId", carrierId);

                object rate = query.ExecuteScalar();

                if (rate == null)
                {
                    throw new CouldNotFindRateException();
                }

                conn.Close();

                return (float)rate;
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
                        Logger.Error(LogOrigin.Database, "(SetReeferCharge) Could not insert reefer charge for carrier " + carrierId);
                        throw new CouldNotInsertException();
                    }

                    Logger.Error(LogOrigin.Database, "(SetReeferCharge) Could not update reefer charge for carrier " + carrierId);
                    throw new CouldNotUpdateException();
                }

                conn.Close();
            }

            Logger.Info(LogOrigin.Database, "(SetReeferCharge) ReeferCharge of carrier " + carrierId + " has been set to " + reeferCharge);
        }

        /// <summary>
        /// This method takes a customer object with the wanted attributes already set and creates it in the database
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>Customer</returns>
        public Customer CreateCustomer(Customer customer)
        {
            const string queryString = "INSERT INTO `Customer` VALUES (NULL, @customerName);";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@customerName", customer.Name);

                if (query.ExecuteNonQuery() == 0)
                {
                    Logger.Error(LogOrigin.Database, "(CreateCustomer) Could not create new customer");
                    throw new CouldNotInsertException();
                }

                // Update the customer's ID
                customer.CustomerID = GetLastInsertId(conn);

                conn.Close();
            }

            Logger.Info(LogOrigin.Database, "(CreateCustomer) Created customer '" + customer.Name + "'");

            return customer;
        }

        /// <summary>
        /// This method takes a customerId and deletes the matching customer from the database
        /// </summary>
        /// <param name="customerId">uint</param>
        public void DeleteCustomer(uint customerId)
        {
            const string queryString = "DELETE FROM `TMS`.`Customer` WHERE `Customer`.`CustomerID` = @customerId;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@customerId", customerId);

                if (query.ExecuteNonQuery() == 0)
                {
                    Logger.Warn(LogOrigin.Database, "(DeleteCustomer) Could not delete customer with ID " + customerId);
                    throw new CouldNotDeleteException();
                }

                conn.Close();
            }

            Logger.Info(LogOrigin.Database, "(DeleteCustomer) Deleted customer with ID " + customerId);
        }

        /// <summary>
        /// This method returns a List of all Customers in the database
        /// </summary>
        /// <returns>List<Customer></returns>
        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            const string queryString = "SELECT * FROM `Customer`;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                MySqlDataReader reader = query.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                foreach (DataRow row in table.Rows)
                {
                    Customer customer = new Customer();

                    customer.CustomerID = (uint) (int) row["CustomerID"];
                    customer.Name = (string) row["CustomerName"];

                    customers.Add(customer);
                }

                conn.Close();
            }

            Logger.Info(LogOrigin.Database, "(GetCustomers) Fetched " + customers.Count + " customers");

            return customers;
        }

        /// <summary>
        /// This method finds and returns a customer by name
        /// </summary>
        /// <param name="customerName">string</param>
        /// <returns>Customer</returns>
        public Customer GetCustomer(string customerName)
        {
            Customer customer = new Customer();

            const string queryString = "SELECT * FROM `Customer` WHERE `Customer`.`CustomerName` = @customerName LIMIT 1;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@customerName", customerName);

                MySqlDataReader reader = query.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                if (table.Rows.Count == 0)
                {
                    Logger.Warn(LogOrigin.Database, "(GetCustomer) A customer with the name " + customerName + " does not exist");
                    throw new CustomerNotExistsException();
                }

                customer.CustomerID = (uint) (int) table.Rows[0]["CustomerID"];
                customer.Name = (string) table.Rows[0]["CustomerName"];

                conn.Close();
            }

            Logger.Info(LogOrigin.Database, "(GetCustomer) Fetched customer '" + customer.Name + "'");

            return customer;
        }

        /// <summary>
        /// This method finds and returns a customer by ID
        /// </summary>
        /// <param name="customerId">uint</param>
        /// <returns>Customer</returns>
        public Customer GetCustomerById(uint customerId)
        {
            Customer customer = new Customer();

            const string queryString = "SELECT * FROM `Customer` WHERE `Customer`.`CustomerID` = @customerID LIMIT 1;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@customerId", customerId);

                MySqlDataReader reader = query.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                if (table.Rows.Count == 0)
                {
                    Logger.Warn(LogOrigin.Database, "(GetCustomer) A customer with the ID " + customerId + " does not exist");
                    throw new CustomerNotExistsException();
                }

                customer.CustomerID = (uint)(int)table.Rows[0]["CustomerID"];
                customer.Name = (string)table.Rows[0]["CustomerName"];

                conn.Close();
            }

            Logger.Info(LogOrigin.Database, "(GetCustomer) Fetched customer '" + customer.Name + "'");

            return customer;
        }

        /// <summary>
        /// This method searches through customers in the database and returns a list matching
        /// the query provided
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>List<Customer></returns>
        public List<Customer> SearchCustomers(string name)
        {
            List<Customer> customers = new List<Customer>();

            const string queryString = @"SELECT * FROM `Customer` c WHERE
                (c.FirstName LIKE CONCAT(@name, '%') OR @name = '');";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@name", name);
                MySqlDataReader reader = query.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                foreach (DataRow row in table.Rows)
                {
                    Customer customer = new Customer();

                    customer.CustomerID = (uint) (int) row["CustomerID"];
                    customer.Name = (string) row["CustomerName"];

                    customers.Add(customer);
                }

                conn.Close();
            }

            Logger.Info(LogOrigin.Database, "(SearchCustomers) Fetched " + customers.Count + " customers");

            return customers;
        }

        /// <summary>
        /// This method registers a new contract in the database
        /// </summary>
        /// <param name="contract">Contract</param>
        /// <returns>Contract</returns>
        public Contract CreateContract(Contract contract)
        {
            const string queryString =
                "INSERT INTO Contract VALUES (NULL, @carrierId, @customerId, @status, @quantity, @loadType, @vanType, @originCity, @destCity);";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@customerId", contract.Customer.CustomerID);
                query.Parameters.AddWithValue("@status", contract.Status);
                query.Parameters.AddWithValue("@quantity", contract.Quantity);
                query.Parameters.AddWithValue("@loadType", contract.JobType);
                query.Parameters.AddWithValue("@vanType", contract.VanType);
                query.Parameters.AddWithValue("@originCity", contract.Origin.ToString());
                query.Parameters.AddWithValue("@destCity", contract.Destination.ToString());
                if (contract.Carrier == null)
                {
                    query.Parameters.AddWithValue("@carrierId", null);
                }
                else
                {
                    query.Parameters.AddWithValue("@carrierId", contract.Carrier.CarrierID);
                }

                if (query.ExecuteNonQuery() == 0)
                {
                    throw new CouldNotInsertException();
                }

                contract.ContractID = GetLastInsertId(conn);

                conn.Close();
            }

            return contract;
        }

        /// <summary>
        /// This method takes a contract ID and a status enum and updates the status
        /// </summary>
        /// <param name="contractId">uint</param>
        /// <param name="status">status</param>
        public void SetContractStatus(uint contractId, Status status)
        {
            const string queryString =
                "UPDATE Contract SET `Contract`.`Status` = @status WHERE `Contract`.`ContractID` = @contractId;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@status", (int) status);
                query.Parameters.AddWithValue("@contractId", contractId);

                if (query.ExecuteNonQuery() == 0)
                {
                    throw new CouldNotUpdateException();
                }

                conn.Close();
            }
        }

        /// <summary>
        /// This method takes a contract ID and a carier ID and updates the carrier ID
        /// </summary>
        /// <param name="contractId">uint</param>
        /// <param name="carrierId">uint</param>
        public void SetContractCarrier(uint contractId, uint carrierId)
        {
            const string queryString =
                "UPDATE Contract SET `Contract`.`CarrierID` = @carrierId WHERE `Contract`.`ContractID` = @contractId;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@carrierId", carrierId);
                query.Parameters.AddWithValue("@contractId", contractId);

                if (query.ExecuteNonQuery() == 0)
                {
                    throw new CouldNotUpdateException();
                }

                conn.Close();
            }
        }

        /// <summary>
        /// This method returns contracts based on their status property
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List<Contract></returns>
        public List<Contract> GetContractsByStatus(Status status)
        {
            List<Contract> contracts = new List<Contract>();

            const string queryString = "SELECT * FROM `Contract` WHERE `Contract`.`Status` = @status;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@status", (int) status);
                MySqlDataReader reader = query.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                foreach (DataRow row in table.Rows)
                {
                    Contract contract = new Contract();

                    PopulateContract(ref contract, row);

                    contracts.Add(contract);
                }

                conn.Close();
            }

            return contracts;
        }

        /// <summary>
        /// This method fetches a list of all contracts
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

                foreach (DataRow row in table.Rows)
                {
                    Contract contract = new Contract();

                    PopulateContract(ref contract, row);

                    contracts.Add(contract);
                }
                
                conn.Close();
            }

            return contracts;
        }

        /// <summary>
        /// This method retrieves the list of trips from the database
        /// </summary>
        /// <returns>List<Trip></returns>
        public List<Trip> GetTrips()
        {
            List<Trip> trips = new List<Trip>();

            const string queryString = "SELECT * FROM Trip;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                MySqlDataReader reader = query.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                foreach (DataRow row in table.Rows)
                {
                    Trip trip = new Trip();

                    PopulateTrip(ref trip, row);

                    trips.Add(trip);
                }

                conn.Close();
            }

            return trips;
        }

        /// <summary>
        /// This method retrieves all trips with a certain destination set
        /// </summary>
        /// <param name="destination">City</param>
        /// <returns>List<Trip></returns>
        public List<Trip> GetTripByDestination(City destination)
        {
            List<Trip> trips = new List<Trip>();

            const string queryString = "SELECT * FROM Trip WHERE `Trip`.`Destination` = @destination;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand query = new MySqlCommand(queryString, conn);
                MySqlDataReader reader = query.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                foreach (DataRow row in table.Rows)
                {
                    Trip trip = new Trip();

                    PopulateTrip(ref trip, row);

                    trips.Add(trip);
                }

                conn.Close();
            }

            return trips;
        }

        /// <summary>
        /// PopulateTrip() takes a reference to a trip object and a data row and parses
        /// the data row into the trip object.
        /// </summary>
        /// <param name="trip">ref Trip</param>
        /// <param name="row">DataRow</param>
        private void PopulateTrip(ref Trip trip, DataRow row)
        {
            trip.TripID = (uint)(int)row["TripID"];
            City.TryParse((string)row["Destination"], out City destination);
            City.TryParse((string)row["West"], out City west);
            City.TryParse((string)row["East"], out City east);
            trip.TravelTime = (int)row["Time"];
            trip.Distance = (int)row["Distance"];
        }

        /// <summary>
        /// PopulateCarrier() takes a reference to a carrier object and a data row and parses
        /// the data row into the carrier object.
        /// </summary>
        /// <param name="carrier">ref Carrier</param>
        /// <param name="row">DataRow</param>
        private void PopulateCarrier(ref Carrier carrier, DataRow row)
        {
            carrier.CarrierID = (uint) (int) row["CarrierID"];
            carrier.Name = (string) row["Name"];
            carrier.FtlAvailability = (int) row["FtlAvailability"];
            carrier.LtlAvailability = (int) row["LtlAvailability"];

            City depot;
            Enum.TryParse((string) row["DepotCity"], out depot);
            carrier.DepotCity = depot;
        }

        /// <summary>
        /// PopulateContract() takes a reference to a contract object and a data row and parses
        /// the data row into the contract object.
        /// </summary>
        /// <param name="carrier">ref Contract</param>
        /// <param name="row">DataRow</param>
        private void PopulateContract(ref Contract contract, DataRow row)
        {
            if (row["CarrierID"] != null)
            {
                contract.Carrier = GetCarrier((uint)row["CarrierID"]);
            }

            contract.Customer = GetCustomerById((uint)row["CustomerID"]);
            contract.Status = (Status)(sbyte)row["Status"];
            contract.Quantity = (int)row["Quantity"];
            contract.JobType = (JobType)(sbyte)row["LoadType"];
            contract.VanType = (VanType)(sbyte)row["VanType"];

            City origin;
            City.TryParse((string)row["OriginCity"], out origin);
            contract.Origin = origin;

            City destination;
            City.TryParse((string)row["DestCity"], out destination);
            contract.Destination = destination;
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
