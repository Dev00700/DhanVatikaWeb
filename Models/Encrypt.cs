using System.Security.Cryptography;
using System.Text;
namespace DhanVatikaWeb.Models
{
    public static class CryptoHelper
    {
        private static readonly string Key = "MySecretKey@1234"; // 16/24/32 chars

        public static string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = new byte[16];

            var encryptor = aes.CreateEncryptor();
            var bytes = Encoding.UTF8.GetBytes(plainText);

            return Convert.ToBase64String(encryptor.TransformFinalBlock(bytes, 0, bytes.Length));
        }

        public static string Decrypt(string cipherText)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = new byte[16];

            var decryptor = aes.CreateDecryptor();
            var bytes = Convert.FromBase64String(cipherText);

            return Encoding.UTF8.GetString(decryptor.TransformFinalBlock(bytes, 0, bytes.Length));
        }
    }
    }
