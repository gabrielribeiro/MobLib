using SHA3;
using System;
using System.Text;

namespace MobLib.Security.Criptography
{
    /// <summary>
    /// This class generates and compares hashes using SHA3 hashing algorithm.
    /// Before computing a hash, it appends a randomly generated salt to the plain text, 
    /// and stores this salt appended to the result. 
    /// To verify another plain text value against the given hash,
    /// this class will retrieve the salt value from the hash string and use it
    /// when computing a new hash of the plain text. Appending a salt value to
    /// the hash may not be the most efficient approach, so when using hashes in
    /// a real-life application, you may choose to store them separately. You may
    /// also opt to keep results as byte arrays instead of converting them into
    /// base64-encoded strings.
    /// </summary>
    public static class SHA3Hasher
    {
        /// <summary>
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, a random salt
        /// is generated and appended to the plain text. This salt is stored at
        /// the end of the hash value, so it can be used later for hash
        /// verification.
        /// </summary>
        /// <param name="plainText">
        /// Plaintext value to be hashed. The function does not check whether
        /// this parameter is null.
        /// </param>
        /// <returns>
        /// Hash value formatted as a base64-encoded string.
        /// </returns>
        public static string Hash(string plainText)
        {
            try
            {
                /*
                 * Initialize SHA-3 hash class and set up size for it.
                 * Accepted values are 224, 256, 384 and 512 bits.
                 */
                var hash = new SHA3Managed(256);
                var encoding = new ASCIIEncoding();

                //Keep message bytes
                byte[] messageBytes = encoding.GetBytes(plainText);

                //Compute message hash
                byte[] computeHashBytes = hash.ComputeHash(messageBytes);

                //Convert to hexa and return all this shit
                var x = new StringBuilder();
                foreach (var item in computeHashBytes)
                {
                    x.Append(item.ToString("x2"));
                }

                return x.ToString();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
