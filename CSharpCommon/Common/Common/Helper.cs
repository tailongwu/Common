using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Security.Cryptography;

namespace Common
{
    public static class Helper
    {
        public static string ReadFile(string filePath)
        {
            string content = string.Empty;
            using (StreamReader reader = new StreamReader(filePath))
            {
                content = reader.ReadToEnd();
            }
            return content;
        }

        public static XmlDocument LoadXML(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            return doc;
        }

        public static string GetDestinationFromSource(string input, string parttern)
        {
            List<string> result = new List<string>();
            Regex regex = new Regex(parttern);
            Match match = regex.Match(input);
            if (match == null)
            {
                return string.Empty;
            }
            return match.Value;
        }

        public static string GetMd5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] inputBytes = Encoding.Default.GetBytes(input);
            byte[] data = md5Hasher.ComputeHash(inputBytes);

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        static bool VerifyMd5Hash(string input, string hash)
        {
            string hashOfInput = GetMd5Hash(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return 0 == comparer.Compare(hashOfInput, hash);
        }

        public static string EncryptBase64(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes);
        }

        public static string DecryptBase64(string input)
        {
            byte[] inputBytes = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(inputBytes);
        }
    }
}