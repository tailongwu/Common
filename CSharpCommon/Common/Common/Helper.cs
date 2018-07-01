using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

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
    }
}