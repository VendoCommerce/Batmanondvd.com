using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CSCore.Extensions;

namespace CSCore
{
    public class UrlHelper
    {
        /// <summary>
        /// Gets domain given Request object. If excludeSubdomains is true, then only top level domain will be returned, e.g.: 
        /// 
        /// www.zquiet.com -> zquiet.com, 
        /// secure.zquiet.com -> zquiet.com, 
        /// secure.miracleskinnow.co.uk -> miracleskinnow.co.uk, 
        /// localhost -> localhost, 
        /// zquiet.com -> zquiet.com. 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="excludeSubdomains"></param>
        /// <returns></returns>
        public static string GetDomain(HttpRequest request, bool excludeSubdomains)
        {
            if (excludeSubdomains)
            {                
                List<string> hostParts = new List<string>(request.Url.Host.ToLower().Split('.'));

                if (hostParts.Count <= 2) // "localhost", "zquiet.com"
                    return request.Url.Host;
                
                List<string> multiLevelDomainSuffixes = string.IsNullOrEmpty(Resource.MultiLevelDomainSuffix) ? 
                    new List<string>() : 
                    new List<string>(Resource.MultiLevelDomainSuffix.ToLower().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                hostParts.Reverse();
                string domainSuffix = null;

                if (multiLevelDomainSuffixes.Count > 0)
                {   
                    // check if domain is two level suffix
                    int multiLevelSuffix = 0;
                    List<string> suffixParts;
                    multiLevelDomainSuffixes.FirstOrDefault<string>(x =>
                        {
                            suffixParts = new List<string>(x.Trim().Split('.'));
                            suffixParts.Reverse();
                            multiLevelSuffix = 0;
                            for (int i = 0; i < suffixParts.Count; i++)
                            {
                                if (suffixParts[i] == hostParts[i] && i == suffixParts.Count - 1)
                                {
                                    multiLevelSuffix = i; // 1 when "uk.co.miracleskinnow", "uk.co.miracleskinnow.www", "uk.co.miracleskinnow.secure"
                                    return true;
                                }
                            }

                            return false;
                        });

                    domainSuffix = string.Join(".", hostParts.Take(multiLevelSuffix + 1).Reverse().ToList().ToArray());

                    hostParts.RemoveRange(0, multiLevelSuffix + 1);
                }
                else
                {
                    domainSuffix = hostParts[0];
                    hostParts.RemoveAt(0);
                }

                // "miracleskinnow", "miracleskinnow.www", "miracleskinnow.secure"
                return hostParts[0] + "." + domainSuffix; //"miracleskinnow.co.uk", "miracleskinnow.co.uk", "miracleskinnow.co.uk"
            }
            else
            {
                return request.Url.Host;
            }
        }

        /// <summary>
        /// Updated querystring in provided url with provided querystring collection, maintaining original querystring values, then return that url containing new querystring.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <param name="urlEncodeValues"></param>
        /// <returns></returns>
        public static string UpdateQuerystring(string url, Dictionary<string, string> queryString, bool urlEncodeValues)
        {
            if (queryString == null || queryString.Count == 0)
                return url;

            StringBuilder retUrl;
            string[] keyValue;
            int questionIndex = url.IndexOf('?');
            string query = null;
            string extraPart = string.Empty;
            string trailingAmp = string.Empty;

            Dictionary<string, string> originalQuery = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);

            // from the query that's already in the URL, add only those key/values that are not in passed in querystring collection.
            if (questionIndex > 0)
            {
                query = url.Substring(questionIndex + 1);
                retUrl = new StringBuilder(url.Substring(0, questionIndex + 1)); // include "?" in return url

                if (query.Contains("="))
                {
                    string[] parts = query.Split('&');

                    foreach (string part in parts)
                    {
                        keyValue = part.Split('=');
                        if (!originalQuery.ContainsKey(keyValue[0]))
                            originalQuery.Add(keyValue[0], keyValue[1]);
                    }

                    trailingAmp = "&";
                }
                else // just a word after "?", like "site.com?abc"
                {
                    extraPart = query;

                    if (!string.IsNullOrEmpty(extraPart))
                        trailingAmp = "&";
                }
            }
            else
                retUrl = new StringBuilder(url + "?");

            // update original query if needed
            string key;
            for (int i = 0; i < queryString.Count; i++)
            {
                key = queryString.Keys.ElementAt(i);
                if (originalQuery.ContainsKey(key))
                {
                    originalQuery[key] = urlEncodeValues ? HttpUtility.UrlEncode(queryString[key]) : queryString[key];
                    queryString.Remove(key);
                    i--;
                }
            }

            retUrl = retUrl
                .Append(extraPart)
                .Append(GetQueryString(originalQuery, false))
                .Append(trailingAmp)
                .Append(GetQueryString(queryString, urlEncodeValues));

            return retUrl.ToString();
        }

        public static string UpdateQuerystring(string url, string queryString, bool urlEncodeValues)
        {
            return UpdateQuerystring(url, GetCollectionFromQuerystring(queryString), urlEncodeValues);
        }

        public static string GetQueryString(Dictionary<string, string> queryString, bool urlEncodeValues)
        {
            if (queryString == null || queryString.Count == 0)
                return null;

            StringBuilder query = new StringBuilder();

            foreach (string key in queryString.Keys)
            {
                query.AppendFormat("{0}={1}&",
                    key,
                    urlEncodeValues ? HttpUtility.UrlEncode(queryString[key]) : queryString[key]);
            }

            return query.ToString().Substring(0, query.Length - 1);
        }

        public static Dictionary<string, string> GetCollectionFromQuerystring(string queryString)
        {   
            Dictionary<string, string> collection = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);

            if (!string.IsNullOrEmpty(queryString))
                return collection;

            string[] keyValue;
            string[] parts = queryString.Split('&');

            foreach (string part in parts)
            {
                keyValue = part.Split('=');
                if (!collection.ContainsKey(keyValue[0]))
                    collection.Add(keyValue[0], keyValue[1]);
            }

            return collection;
        }
    }
}
