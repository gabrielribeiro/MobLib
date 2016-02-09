using System;
using System.IO;
using System.Security.Cryptography;

namespace MobLib.Crypt
{
    public static class AESCrypter
    {
        private const string SharedSecret = "n3@RB&& 4u7h3nt!C@t!0n Bec4us3 Siz3 M@tt3r$";
        static readonly byte[] Salt = { 77, 128, 77, 65, 16, 12, 61, 77, 126, 77, 16, 32, 12, 41, 64, 255 };


        /// <summary>
        /// Encrypt the given string using AES.  The string can be decrypted using 
        /// DecryptStringAES().  The SharedSecret parameters must match.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        public static byte[] Encrypt(this string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException("plainText");

            //string outStr = null;                       // Encrypted string to return
            byte[] outBytes = null;                       // Encrypted byte array to return
            RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data.
            CryptoStream csEncrypt = null;
            StreamWriter swEncrypt = null;
            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(SharedSecret, Salt);

                // Create a RijndaelManaged object
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // prepend the IV
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

                    swEncrypt = new StreamWriter(csEncrypt);

                    //Write all data to the stream.
                    swEncrypt.Write(plainText);

                    //outStr = Convert.ToBase64String(msEncrypt.ToArray());
                    outBytes = msEncrypt.ToArray();
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();

                if (csEncrypt != null)
                    csEncrypt.Dispose();

                if (swEncrypt != null)
                    swEncrypt.Dispose();
            }

            // Return the encrypted bytes from the memory stream.
            return outBytes;
        }

        /// <summary>
        /// Decrypt the given string.  Assumes the string was encrypted using 
        /// EncryptStringAES(), using an identical SharedSecret.
        /// </summary>
        /// <param name="cipherText">The text to decrypt.</param>
        public static string Decrypt(this string cipherText)
        {
            byte[] bytes = Convert.FromBase64String(cipherText);

            string plaintext = Decrypt(bytes);

            return plaintext;
        }

        /// <summary>
        /// Decrypt the given string.  Assumes the string was encrypted using 
        /// EncryptStringAES(), using an identical SharedSecret.
        /// </summary>
        /// <param name="cipherBytes">The byte array to decrypt.</param>
        public static string Decrypt(this byte[] cipherBytes)
        {
            if (cipherBytes == null)
                throw new ArgumentNullException("cipherBytes");

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;
            CryptoStream csDecrypt = null;
            StreamReader srDecrypt = null;
            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(SharedSecret, Salt);

                // Create the streams used for decryption.                \
                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                {
                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    // Get the initialization vector from the encrypted stream
                    aesAlg.IV = ReadByteArray(msDecrypt);
                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

                    srDecrypt = new StreamReader(csDecrypt);

                    // Read the decrypted bytes from the decrypting stream
                    // and place them in a string.
                    plaintext = srDecrypt.ReadToEnd();
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();

                if (csDecrypt != null)
                    csDecrypt.Dispose();

                if (srDecrypt != null)
                    srDecrypt.Dispose();
            }

            return plaintext;
        }

        private static byte[] ReadByteArray(Stream s)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }
    }
}
