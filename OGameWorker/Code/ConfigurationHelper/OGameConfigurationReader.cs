using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OGameWorker.Code.ConfigurationHelper
{
    internal static class OGameConfigurationReader
    {
        private static readonly string _encryptionKey = "Ikaro$";

        public static string ReadValue(string key)
        {
            var configurationValue = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings.Settings[key].Value;
            return string.IsNullOrEmpty(configurationValue) ? string.Empty : Decrypt(configurationValue);
        }

        public static void SaveValue(string key, string value)
        {
            var encryptedValue = Encrypt(value);
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = encryptedValue;
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private static byte[] GetMd5Hash() => new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(_encryptionKey));

        private static TripleDESCryptoServiceProvider GetCryptoProvider() => new TripleDESCryptoServiceProvider
                                                                                            {
                                                                                                Key = GetMd5Hash(),
                                                                                                Mode = CipherMode.ECB,
                                                                                                Padding = PaddingMode.PKCS7
                                                                                            };

        private static ICryptoTransform GetEncryptor() => GetCryptoProvider().CreateEncryptor();
        private static ICryptoTransform GetDecryptor() => GetCryptoProvider().CreateDecryptor();

        private static string Encrypt(string value)
        {
            var byteData = Encoding.UTF8.GetBytes(value);
            var result = GetEncryptor().TransformFinalBlock(byteData, 0, byteData.Length);
            return Convert.ToBase64String(result, 0, result.Length);
        }

        private static string Decrypt(string value)
        {
            var byteData = Convert.FromBase64String(value);
            var result = GetDecryptor().TransformFinalBlock(byteData, 0, byteData.Length);
            return Encoding.UTF8.GetString(result);
        }
    }
}
