using System.IO;
using System.Net;
using System.Text;

namespace Authorize.NET_API.Code
{
    internal static class Rest
    {
        public static string Request(string urlEndpoint, string requestMethod, string contentType)
        {
            return Rest.Request(urlEndpoint, requestMethod, contentType, "");
        }

        public static string Request(
          string urlEndpoint,
          string requestMethod,
          string contentType,
          string body)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(urlEndpoint);
            httpWebRequest.Method = requestMethod;
            httpWebRequest.ContentType = contentType;
            byte[] bytes = Encoding.ASCII.GetBytes(body);
            using (Stream requestStream = httpWebRequest.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                using (HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse())
                {
                    Stream responseStream = response.GetResponseStream();
                    return response.StatusCode == HttpStatusCode.OK ? Rest.ToString(responseStream) : response.StatusDescription;
                }
            }
        }

        private static string ToString(Stream stream) => new StreamReader(stream).ReadToEnd();
    }
}