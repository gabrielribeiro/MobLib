using System;
using System.IO;
using System.Security.Cryptography;

namespace MobLib.Crypt
{
    public static class AESCrypter
    {
        static int Rfc2898KeygenIterations = 100;
        static int AesKeySizeInBits = 128;
        static String Password = "n3@RB&& 4u7h3nt!C@t!0n Bec4us3 Siz3 M@tt3r$";
        static byte[] Salt = { 77, 128, 77, 65, 16, 12, 61, 77, 126, 77, 16, 32, 12, 41, 64, 255 };

        public static string Encrypt(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            byte[] rawPlaintext = System.Text.Encoding.Unicode.GetBytes(text);
            byte[] cipherText = null;
            using (Aes aes = new AesManaged())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.KeySize = AesKeySizeInBits;
                int KeyStrengthInBytes = aes.KeySize / 8;
                System.Security.Cryptography.Rfc2898DeriveBytes rfc2898 =
                    new System.Security.Cryptography.Rfc2898DeriveBytes(Password, Salt, Rfc2898KeygenIterations);
                aes.Key = rfc2898.GetBytes(KeyStrengthInBytes);
                aes.IV = rfc2898.GetBytes(KeyStrengthInBytes);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(rawPlaintext, 0, rawPlaintext.Length);
                    }
                    cipherText = ms.ToArray();
                }
                return ByteArrayToString(cipherText);
            }
        }

        public static string Decrypt(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] cipherText = StringToByteArray(text);
                    byte[] plainText;
                    using (Aes aes = new AesManaged())
                    {
                        aes.Padding = PaddingMode.PKCS7;
                        aes.KeySize = AesKeySizeInBits;
                        int KeyStrengthInBytes = aes.KeySize / 8;
                        System.Security.Cryptography.Rfc2898DeriveBytes rfc2898 =
                            new System.Security.Cryptography.Rfc2898DeriveBytes(Password, Salt, Rfc2898KeygenIterations);
                        aes.Key = rfc2898.GetBytes(KeyStrengthInBytes);
                        aes.IV = rfc2898.GetBytes(KeyStrengthInBytes);
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherText, 0, cipherText.Length);
                        }
                        plainText = ms.ToArray();
                        return System.Text.Encoding.Unicode.GetString(plainText);
                    }
                }
            }
            //hides the decryptation exception, usually are not valids hexa caracters
            catch(Exception)
            {
                return null;
            }
        }

        public static byte[] EncryptBytes(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            byte[] rawPlaintext = System.Text.Encoding.Unicode.GetBytes(text);
            byte[] cipherText = null;
            using (Aes aes = new AesManaged())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.KeySize = AesKeySizeInBits;
                int KeyStrengthInBytes = aes.KeySize / 8;
                System.Security.Cryptography.Rfc2898DeriveBytes rfc2898 =
                    new System.Security.Cryptography.Rfc2898DeriveBytes(Password, Salt, Rfc2898KeygenIterations);
                aes.Key = rfc2898.GetBytes(KeyStrengthInBytes);
                aes.IV = rfc2898.GetBytes(KeyStrengthInBytes);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(rawPlaintext, 0, rawPlaintext.Length);
                    }
                    cipherText = ms.ToArray();
                }
                return cipherText;
            }
        }

        public static string DecryptBytes(this byte[] text)
        {
            if (text == null)
            {
                return null;
            }
            try
            {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] cipherText = text;
                byte[] plainText;
                using (Aes aes = new AesManaged())
                {
                    aes.Padding = PaddingMode.PKCS7;
                    aes.KeySize = AesKeySizeInBits;
                    int KeyStrengthInBytes = aes.KeySize / 8;
                    System.Security.Cryptography.Rfc2898DeriveBytes rfc2898 =
                        new System.Security.Cryptography.Rfc2898DeriveBytes(Password, Salt, Rfc2898KeygenIterations);
                    aes.Key = rfc2898.GetBytes(KeyStrengthInBytes);
                    aes.IV = rfc2898.GetBytes(KeyStrengthInBytes);
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherText, 0, cipherText.Length);
                    }
                    plainText = ms.ToArray();
                    return System.Text.Encoding.Unicode.GetString(plainText);
                }
            }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static byte[] StringToByteArray(this string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static string ByteArrayToString(this byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }
    }
}
