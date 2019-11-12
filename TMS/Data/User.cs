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

    class User
    {
        private const int SaltLength = 16;
        private const int HashLength = 20;

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

        private UserType Type { get; set; }
    }
}