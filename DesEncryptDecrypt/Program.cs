using System;
using System.Security.Cryptography;
using System.Text;

namespace DesEncryptDecrypt
{
    internal class Program
    {
        private static void Main()
        {
            var key = new byte[] { 36, 101, 99, 114, 51, 116, 33, 46 };

            const string message = "Quick brown dogs jump over the lazy fox. " +
                                   "The jay, pig, fox, zebra, and my wolves quack! " +
                                   "Blowzy red vixens fight for a quick jump";

            var messageBytes = Encoding.ASCII.GetBytes(message);

            var encryptedMessage = Encrypt(messageBytes, key);
            var decryptedMessage = Decrypt(encryptedMessage, key);

            Console.WriteLine("Encrypted message");
            Console.WriteLine("-----------------");
            Console.WriteLine(Encoding.ASCII.GetString(encryptedMessage));
            Console.WriteLine();
            Console.WriteLine("Decrypted message");
            Console.WriteLine("-----------------");
            Console.WriteLine(Encoding.ASCII.GetString(decryptedMessage));

            Console.ReadLine();
        }
        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            var tdes = new DESCryptoServiceProvider
            {
                Key = key,
                IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                Mode = CipherMode.CBC,
                Padding = PaddingMode.None
            };

            var cTransform = tdes.CreateEncryptor();

            var result = cTransform.TransformFinalBlock(data, 0, data.Length);
            tdes.Clear();

            return result;
        }

        public static byte[] Decrypt(byte[] data, byte[] key)
        {
            var tdes = new DESCryptoServiceProvider
            {
                Key = key,
                IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                Mode = CipherMode.CBC,
                Padding = PaddingMode.None
            };

            var cTransform = tdes.CreateDecryptor();
            var result = cTransform.TransformFinalBlock(data, 0, data.Length);
            tdes.Clear();

            return result;
        }
    }
}
