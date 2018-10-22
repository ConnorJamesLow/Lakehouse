// Connor Low
// 10-22-18
// CST-326
// Source: https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129
using System;
using System.Security.Cryptography;

namespace Lakehouse.Services
{
    /// <summary>
    /// Provides hash functions for security
    /// </summary>
    public class Hasher
    {
        /// <summary>
        /// Creates a hash from a raw string.
        /// </summary>
        /// <param name="raw"></param>
        /// <returns>The hashed value as a string</returns>
        public static string Hash(string raw)
        {
            // salt value
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // get hash value
            var pbkdf2 = new Rfc2898DeriveBytes(raw, salt, 1234);
            byte[] hash = pbkdf2.GetBytes(20);

            // combine salt and password bytes for later use
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // return hashed value
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Compares a raw string to a hashed value.
        /// </summary>
        /// <param name="raw"></param>
        /// <param name="hashedValue"></param>
        /// <returns>True if the hashed value matches the raw value</returns>
        public static bool Compare(string raw, string hashedValue)
        {
            // Extract the bytes
            byte[] hashBytes = Convert.FromBase64String(hashedValue);

            // Get the salt 
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Compute the hash on the password the user entered 
            var pbkdf2 = new Rfc2898DeriveBytes(raw, salt, 1234);
            byte[] hash = pbkdf2.GetBytes(20);

            // Compare the results 
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
