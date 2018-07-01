using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpCommon
{
    public static class HttpHelper
    {
        public static HttpWebResponse POSTRequest(string url, string body)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.CreateHttp(url);
            request.Method = Constants.POST_Method;
            request.ContentType = Constants.Content_Type;
            request.Accept = Constants.Accept_Language;

            byte[] data = Encoding.UTF8.GetBytes(body);
            request.ContentLength = data.Length;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }

        public static string ReadHttpWebResponse(HttpWebResponse response)
        {
            string result = string.Empty;
            Stream stream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}
