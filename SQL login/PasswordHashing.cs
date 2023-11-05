using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SQL_login
{
    

    class PasswordHashing
    {
        public const int SaltBytes = 32;
        public const int HashBytes = 32;
        public const int pbkdf2Iterations = 600000;
        public const int IterationIndex = 1;
        public const int HashSizeIndex = 2;
        public const int SaltIndex = 3;
        public const int pbkdf2Index = 4;

        public static string CreateHash(string password)
        {
            // Generate a random salt
            byte[]? salt = new byte[SaltBytes];
            try
            {
                using (RandomNumberGenerator CSPRNG = RandomNumberGenerator.Create())   // Cryptographically Secure Pseudo Random Number Generator
                {
                    CSPRNG.GetBytes(salt);
                }
            }
            catch (CryptographicException ex)
            {
                throw new Exception("", ex);
            }


            byte[]? hash = PBKDF2(password, salt, pbkdf2Iterations, HashBytes);    // PBKDF is a Password based Key Derivation Function

            //            Algorithm  :     iterations      :  hashSize(bytes)  :             salt                   :                hash
            string parts = "sha256:" + pbkdf2Iterations + ":" + hash.Length + ":" + Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            return parts;
        }

        public static bool PasswordVerification(string password, string goodHash)
        {
            char[] delimiter = { ':' };
            string[] split = goodHash.Split(delimiter);

            int iterations = 0;
            iterations = Int32.Parse(split[IterationIndex]);

            byte[]? salt = null;
            salt = Convert.FromBase64String(split[SaltIndex]);

            byte[]? hash = null;
            hash = Convert.FromBase64String(split[pbkdf2Index]);

            int storedHashSize = 0;
            storedHashSize = Int32.Parse(split[HashSizeIndex]);

            byte[]? testHash = PBKDF2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }


        // uint = 32 bit unsigned integer, can only store non negative integers, Ensures the result is not negative.
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint LengthDifference = (uint)a.Length ^ (uint)b.Length;             // XOR operator used calculates length difference between the arrays
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                LengthDifference |= (uint)(a[i] ^ b[i]);    // Results in 0 if the two corresponding elements match, non zero if they dont match
            }
            return LengthDifference == 0;  // Returns true if they match / false if they're different
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))   // I changed the hashing algorithm from SHA1 to SHA256 as it is more secure
            {
                return pbkdf2.GetBytes(outputBytes);
            }
        }
    }
}
