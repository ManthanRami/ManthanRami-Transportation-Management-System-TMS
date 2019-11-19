using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    public enum UserType
    {
        Planner = 0,
        Buyer = 1,
        Admin = 2
    }

    public class User
    {
        private const int SaltLength = 16;
        private const int HashLength = 20;
        private UserType Type { get; set; }
        private int UserID { get; set; }

        private string Username { get; set; }

        private string Password
        {
            get
            {
                // Decode Base64 password
                byte[] hashBytes = Convert.FromBase64String(Password);

                // Extract just the password hash bytes while ignoring the salt bytes
                byte[] hash = new byte[HashLength];
                Array.Copy(hashBytes, SaltLength, hash, 0, HashLength);

                // Return the hash without the salt
                return Convert.ToString(hash);
            }
            set
            {
                // Generate a salt
                byte[] salt = new byte[SaltLength];
                new RNGCryptoServiceProvider().GetBytes(salt);

                // Hash the password
                var pbkdf2 = new Rfc2898DeriveBytes(value, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(HashLength);

                byte[] hashBytes = new byte[SaltLength + HashLength];
                Array.Copy(salt, 0, hashBytes, 0, SaltLength);
                Array.Copy(hash, 0, hashBytes, SaltLength, HashLength);

                // Encode and set password
                Password = Convert.ToBase64String(hashBytes);
            }

        }
        /*====================================================================================================================================
        /// <summary>
        /// This function will verify user account in database if it is exist or given info is wrong 
        /// </summary>
        /// <param name="username">string     username : Username provided by the TMS application userto login in to thhe application.</param>
        /// <param name="password">string     password : Password provided by the TMS application user to login in to the application.</param>
        /// <returns>bool       loginOk  : True  if the user provided right information of the account otherwise False.</returns>
        =======================================================================================================================================*/
        public User VerifyAccount(string username, string password)
        {                  
            // code to communicate with the database,account table to verify

            return this;
        }
    
    }
       
}
