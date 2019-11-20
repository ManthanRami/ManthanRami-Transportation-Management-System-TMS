using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public const int SaltLength = 16;
        public const int HashLength = 20;
        public const int HashIterations = 10000;

        public int UserID { get; set; }

        public string Username { get; set; }

        private string password;
        public string Password
        {
            get
            {
                // Decode Base64 password
                byte[] hashBytes = Convert.FromBase64String(password);

                // Extract just the password hash bytes while ignoring the salt bytes
                byte[] hash = new byte[HashLength];
                Array.Copy(hashBytes, SaltLength, hash, 0, HashLength);

                // Return the hash without the salt
                return Convert.ToBase64String(hash);
            }
            set
            {
                // Generate a salt
                byte[] salt = new byte[SaltLength];
                new RNGCryptoServiceProvider().GetBytes(salt);

                // Hash the password
                var pbkdf2 = new Rfc2898DeriveBytes(value, salt, HashIterations);
                byte[] hash = pbkdf2.GetBytes(HashLength);

                byte[] hashBytes = new byte[SaltLength + HashLength];
                Array.Copy(salt, 0, hashBytes, 0, SaltLength);
                Array.Copy(hash, 0, hashBytes, SaltLength, HashLength);

                // Encode and set password
                password = Convert.ToBase64String(hashBytes);
            }
        }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType Type { get; set; }

        public bool ComparePassword(string password)
        {
            byte[] hashBytes = Convert.FromBase64String(password);

            // Extract the salt
            byte[] salt = new byte[SaltLength];
            Array.Copy(hashBytes, 0, salt, 0, SaltLength);

            // Hash the password being tested with the same salt used in registration
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, HashIterations);
            byte[] hash = pbkdf2.GetBytes(HashLength);

            // Compare the passwords. Since they were hashed with the same salt, they will be
            // their hash results will be the same as long as the passwords match.
            for (int i = 0; i < HashLength; i++)
            {
                // If the hash results don't match, return false as the user is not authorized
                if (hashBytes[i + SaltLength] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
