using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.IO;

namespace CSCore
{
    public class HttpPost
    {
        public enum Method
        {
            GET,
            POST,
            PUT
        }

        public const string defaultContentType = "application/x-www-form-urlencoded";

        public static string PostData(string url, Method method, string parameters, bool encodeParametersIfGet, string contentType, Encoding encoding)
        {
            string response = null;
            string useUrl = url;

            if (method == Method.GET)
            {
                useUrl = UrlHelper.UpdateQuerystring(url, parameters, encodeParametersIfGet);                
            }
            
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(useUrl);

            webRequest.ContentType = contentType;
            webRequest.Method = GetMethodString(method);

            if (method == Method.POST)
            {
                byte[] bytes = encoding.GetBytes(parameters);
                webRequest.ContentLength = bytes.Length;
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Flush();
                    requestStream.Close();
                }                
            }
            
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                response = reader.ReadToEnd();
                reader.Close();
            }

            return response;
        }

        public static string PostData(string url, Method method, string parameters)
        {
            return PostData(url, method, parameters, true, defaultContentType, System.Text.Encoding.UTF8);
        }

        public static string PostData(string url, Method method, Dictionary<string, string> parameters, bool encodeParameters, string contentType, Encoding encoding, out string requestData)
        {            
            requestData = UrlHelper.GetQueryString(parameters, encodeParameters);

            return PostData(url, method, requestData, encodeParameters, contentType, encoding);
        }
        
        public static string PostData(string url, Method method, Dictionary<string, string> parameters, bool encodeParameters, string contentType, Encoding encoding)
        {            
            return PostData(url, method, UrlHelper.GetQueryString(parameters, encodeParameters), encodeParameters, contentType, encoding);
        }

        public static string PostData(string url, Method method, Dictionary<string, string> parameters, bool encodeParameters, out string requestData)
        {            
            return PostData(url, method, parameters, encodeParameters, defaultContentType, System.Text.Encoding.UTF8, out requestData);
        }

        public static string PostData(string url, Method method, Dictionary<string, string> parameters, bool encodeParameters)
        {
            return PostData(url, method, parameters, encodeParameters, defaultContentType, System.Text.Encoding.UTF8);
        }

        public static string PostData(string url, Method method, Dictionary<string, string> parameters, out string requestData)
        {
            return PostData(url, method, parameters, true, defaultContentType, System.Text.Encoding.UTF8, out requestData);
        }

        public static string PostData(string url, Method method, Dictionary<string, string> parameters)
        {
            return PostData(url, method, parameters, true, defaultContentType, System.Text.Encoding.UTF8);
        }
        
        public static string GetMethodString(Method method)
        {
            switch(method)
            {
                case Method.GET:
                    return "GET";
                case Method.POST:
                    return "POST";
                case Method.PUT:
                    return "PUT";
            }

            return null;
        }
    }
}
