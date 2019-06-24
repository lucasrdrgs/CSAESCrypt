/*
This AES cryptography wrapper was made by Lucas Rodrigues (lucasrdrgs at github).
Please don't distribute this without crediting the developer.
The developer is not responsible for misuse of the application.
*/

using System;
using System.Text;
using System.Security.Cryptography;

namespace CsAesCryptN
{
    public class CsAesCrypt
    {
        public string Key { get; set; }
        public string IV { get; set; }

        /// <summary>
        /// Creates an instance of CsAesCrypt class.
        /// </summary>
        /// <param name="Key">AES 256 CBC Key. It has to be 32 characters long.</param>
        /// <param name="IV">AES 256 CBC IV. It has to be 16 characters long.</param>
        public CsAesCrypt(string Key, string IV)
        {
			this.Key = (Key.Length == 32) ? Key : string.Empty;
			this.IV = (IV.Length == 16) ? IV : string.Empty;
            if(this.Key == string.Empty) throw new ArgumentException("Parameter \"Key\" is not 32 characters long.", "Key");
			if(this.IV == string.Empty)	throw new ArgumentException("Parameter \"IV\" is not 16 characters long.", "IV");
        }

        public CsAesCrypt()
        {
            Key = GenerateString(32);
            IV = GenerateString(16);
        }

        public static string GenerateString(int length = 32)
        {
			Random r = new Random();
			string possibleChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			string Key = "";
            for (int i = 0; i < length; i++)
            {
                int randomIndex = r.Next(possibleChars.Length);
                Key += possibleChars[randomIndex];
            }
            return Key;
        }

        public byte[] EncodeBytes(byte[] bytesToEncode)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider
            {
                BlockSize = 128,
                KeySize = 256,
                Key = Encoding.UTF8.GetBytes(Key),
                IV = Encoding.UTF8.GetBytes(IV),
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC
            };
            ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
            bytesToEncode = crypto.TransformFinalBlock(bytesToEncode, 0, bytesToEncode.Length);
            crypto.Dispose();
            return bytesToEncode;
        }

        public byte[] DecodeBytes(byte[] encryptedBytes)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider
            {
                BlockSize = 128,
                KeySize = 256,
                Key = Encoding.UTF8.GetBytes(Key),
                IV = Encoding.UTF8.GetBytes(IV),
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC
            };
            ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] decodedBytes = crypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            crypto.Dispose();
            return decodedBytes;
        }
    }
}
