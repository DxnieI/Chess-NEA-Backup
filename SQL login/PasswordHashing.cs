using System;
using System.Security.Cryptography;
using System.Text;

namespace SQL_login
{
    class PasswordHashing
    {
        public const int SaltBytes = 32;
        public const int HashBytes = 32;
        public const int pbkdf2Iterations = 600000;  // More iteration increases security
        public const int IterationIndex = 1;
        public const int HashSizeIndex = 2;
        public const int SaltIndex = 3;
        public const int pbkdf2Index = 4;
        public const string Pepper = "YouCantHackMe";


        private static byte[] Hashing_SHA256(string Value)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(Value);
                return sha256.ComputeHash(bytes);
            }
        }

        public static string CreateHash(string password)
        {
            string PepperedPassword = Pepper + password;  // I prepended the pepper to the password

            // Generate a random salt
            byte[]? salt = new byte[SaltBytes];
            try
            {
                using (RandomNumberGenerator CSRNG = RandomNumberGenerator.Create())   // Cryptographically Secure Random Number Generator generates salt
                {
                    CSRNG.GetBytes(salt);
                }
            }
            catch (CryptographicException ex)
            {
                throw new Exception("", ex);
            }

            byte[] PreHashedPassword = Hashing_SHA256(PepperedPassword);

            byte[] hash = PBKDF2(Convert.ToBase64String(PreHashedPassword), salt, pbkdf2Iterations, HashBytes);

            //             Algorithm :     iterations      :  hashSize(bytes)  :             salt                   :                hash
            string parts = "sha256:" + pbkdf2Iterations + ":" + hash.Length + ":" + Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            return parts;
        }

        private static byte[] PBKDF2(string PreHashedPassword, byte[] salt, int iterations, int outputBytes)  // PBKDF is a Password based Key Derivation Function algorithm
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(PreHashedPassword, salt, iterations))   // The HMAC alogrithms default hashing algorithm is SHA1 so I implemented a SHA256 algorithm as it is more secure
            {
                return pbkdf2.GetBytes(outputBytes);
            }
        }




        // uint = 32 bit unsigned integer, can only store non negative integers, Ensures the result is not negative.
        private static bool Diff(byte[] array1, byte[] array2)
        {
            uint LengthDifference = (uint)(array1.Length ^ (uint)array2.Length);             // XOR operator used calculates length difference between the arrays


            for (int i = 0; i < array1.Length && i < array2.Length; i++)
            {
                LengthDifference |= (uint)(array1[i] ^ array2[i]);    // Results in 0 if the two corresponding elements match, non zero if they dont match
            }
            return LengthDifference == 0;  // Returns true if they match / false if they're different
        }

        public static bool PasswordVerification(string password, string StoredPasswordHash)
        {
            char[] HashDelimiter = { ':' };
            string[] Split = StoredPasswordHash.Split(HashDelimiter);  // The delimiter value is used to split the stored password hash into parts

            int iterations = 0;
            iterations = Int32.Parse(Split[IterationIndex]); // Parse converts the first and fouth part of the split to an integer

            byte[]? salt = null;
            salt = Convert.FromBase64String(Split[SaltIndex]);

            byte[]? hash = null;
            hash = Convert.FromBase64String(Split[pbkdf2Index]); // FromBase64String converts the second and third part of the split to a byte array

            int storedHashSize = 0;
            storedHashSize = Int32.Parse(Split[HashSizeIndex]);

            string PepperedPassword = Pepper + password;

            byte[] PreHashedPassword = Hashing_SHA256(PepperedPassword);

            byte[]? testHash = PBKDF2(Convert.ToBase64String(PreHashedPassword), salt, iterations, hash.Length);
            return Diff(hash, testHash);
        }
    }
}