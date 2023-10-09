using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class PasswordHasher
    {
        private const int SaltSize = 32; // Size of the salt in bytes

        public (string Hash, string Salt) HashPassword(string password)
        {
            // It will generate random salt
            byte[] salt = GenerateSalt();

            // Combine the salt and password
            byte[] saltedPassword = CombineSaltAndPassword(salt, Encoding.UTF8.GetBytes(password));

            // Compute the SHA-256 hash
            byte[] hashedPassword = ComputeSHA256Hash(saltedPassword);

            // Convert the byte arrays to hexadecimal strings for storage
            string hashedPasswordHex = BitConverter.ToString(hashedPassword).Replace("-", "");
            string saltHex = BitConverter.ToString(salt).Replace("-", "");

            return (hashedPasswordHex, saltHex);
        }

        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            // Convert stored salt and password hash from hexadecimal strings to byte arrays
            byte[] salt = HexStringToByteArray(storedSalt);
            byte[] storedHashBytes = HexStringToByteArray(storedHash);

            // Combine the entered password and stored salt
            byte[] saltedPassword = CombineSaltAndPassword(salt, Encoding.UTF8.GetBytes(enteredPassword));

            // Compute the SHA-256 hash
            byte[] enteredHash = ComputeSHA256Hash(saltedPassword);

            // Compare the computed hash with the stored hash
            return enteredHash.SequenceEqual(storedHashBytes);
        }

        private byte[] GenerateSalt()
        {
            // Generate a random salt (typically, you would use a cryptographically secure random number generator)
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private byte[] CombineSaltAndPassword(byte[] salt, byte[] password)
        {
            // Combine the salt and password bytes
            byte[] combinedBytes = new byte[salt.Length + password.Length];
            Buffer.BlockCopy(salt, 0, combinedBytes, 0, salt.Length);
            Buffer.BlockCopy(password, 0, combinedBytes, salt.Length, password.Length);
            return combinedBytes;
        }

        private byte[] ComputeSHA256Hash(byte[] data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(data);
            }
        }

        private byte[] HexStringToByteArray(string hex)
        {
            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
