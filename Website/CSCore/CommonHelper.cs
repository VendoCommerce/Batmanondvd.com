using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Security.Cryptography;
using System.Collections;
using System.Net;
using CSCore.DataHelper;
using System.Globalization;
using CSCore.Encryption;

namespace CSCore.Utils
{
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public  class CommonHelper
    {
        #region Methods

        /// <summary>
        /// Verifies that a string is in valid e-mail format
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
        public static bool IsValidEmail(string email)
        {
            bool result = false;
            if (String.IsNullOrEmpty(email))
                return result;
            email = email.Trim();
            result = Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return result;
        }

        public static bool IsValidPhone(string phone)
        {
            bool result = false;
            if (String.IsNullOrEmpty(phone))
                return result;
            phone = phone.Trim();      
            result = Regex.IsMatch(phone, @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$");
            return result;
        }

        public static bool IsValidZipCodeUS(string zipcode)
        {
            bool result = false;
            if (String.IsNullOrEmpty(zipcode))
                return result;
            zipcode = zipcode.ToUpper().Trim();
            result = Regex.IsMatch(zipcode, @"^[0-9]{5}-[0-9]{4}$|^[0-9]{5}$");
            return result;
        }

        public static bool IsValidZipCode(string zipcode)
        {
            bool result = false;
            if (String.IsNullOrEmpty(zipcode))
                return result;
            zipcode = zipcode.ToUpper().Trim();
            result = Regex.IsMatch(zipcode, @"^[0-9]{5}-[0-9]{4}$|^[0-9]{5}$|^[A-Z][0-9][A-Z][ ]?[0-9][A-Z][0-9]$");
            return result;
        }
        
        public static bool IsValidZipCodeCanadian(string zipcode)
        {
            bool result = false;
            if (String.IsNullOrEmpty(zipcode))
                return result;
            zipcode = zipcode.ToUpper().Trim();
            result = Regex.IsMatch(zipcode, @"^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]{1}\d{1}[A-Za-z]{1}[ ]{0,1}\d{1}[A-Za-z]{1}\d{1}$");
            return result;
        }

        public static bool IsValidZipCodeAustralian(string zipcode)
        {
            bool result = false;
            if (String.IsNullOrEmpty(zipcode) || zipcode.Length > 4)
                return result;
            zipcode = zipcode.ToUpper().Trim();
            result = Regex.IsMatch(zipcode, @"^(0[289][0-9]{2})|([1345689][0-9]{3})|(2[0-8][0-9]{2})|(290[0-9])|(291[0-4])|(7[0-4][0-9]{2})|(7[8-9][0-9]{2})$");
            return result;
        }

        public static bool IsValidZipCodeUK(string postcode)
        {
            string patternStrict = @"^([A-PR-UWYZ0-9][A-HK-Y0-9][AEHMNPRTVXY0-9]?[ABEHMNPRVWXY0-9]? {1,2}[0-9][ABD-HJLN-UW-Z]{2}|GIR 0AA)$";
            Regex reStrict = new Regex(patternStrict);

            if (!(reStrict.IsMatch(postcode.ToUpper())))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public string GetCleanPhoneNumber(string phone)
        {
            string result = string.Empty;

            int i = 0;
            if (phone.Length > 0)
            {
                i = CountNums(phone);
            }

            int cnum = 0;

            foreach (char c in phone)
            {
                cnum++;
                if (!((i > 10) && (cnum == 1) && (c == '1')))
                {
                    if (char.IsDigit(c))
                        result += c;
                }
            }
            return result;
        }

        public string Encrypt(string clearText, string Password)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
            new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return Convert.ToBase64String(encryptedData);
        }
        public byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearData, 0, clearData.Length);
            cs.Close();
            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }

        /// <summary>
        /// Gets query string value by name
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <returns>Query string value</returns>
        public static string QueryString(string name)
        {
            string result = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request.QueryString[name] != null)
                result = HttpContext.Current.Request.QueryString[name].ToString();
            return result;
        }

        /// <summary>
        /// Gets boolean value from query string 
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <returns>Query string value</returns>
        public static bool QueryStringBool(string name)
        {
            string resultStr = QueryString(name).ToUpperInvariant();
            return (resultStr == "YES" || resultStr == "TRUE" || resultStr == "1");
        }

        /// <summary>
        /// Gets integer value from query string 
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <returns>Query string value</returns>
        public static int QueryStringInt(string name)
        {
            string resultStr = QueryString(name).ToUpperInvariant();
            int result;
            Int32.TryParse(resultStr, out result);
            return result;
        }

        /// <summary>
        /// Gets integer value from query string 
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Query string value</returns>
        public static int QueryStringInt(string name, int defaultValue)
        {
            string resultStr = QueryString(name).ToUpperInvariant();
            if (resultStr.Length > 0)
            {
                return Int32.Parse(resultStr);
            }
            return defaultValue;
        }

        /// <summary>
        /// Gets GUID value from query string 
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <returns>Query string value</returns>
        public static Guid? QueryStringGuid(string name)
        {
            string resultStr = QueryString(name).ToUpperInvariant();
            Guid? result = null;
            try
            {
                result = new Guid(resultStr);
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// Gets Form String
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Result</returns>
        public static string GetFormString(string name)
        {
            string result = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request[name] != null)
                result = HttpContext.Current.Request[name].ToString();
            return result;
        }

        /// <summary>
        /// Set meta http equiv (eg. redirects)
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="httpEquiv">Http Equiv</param>
        /// <param name="content">Content</param>
        public static void SetMetaHttpEquiv(Page page, string httpEquiv, string content)
        {
            if (page.Header == null)
                return;

            HtmlMeta meta = new HtmlMeta();
            if (page.Header.FindControl("meta" + httpEquiv) != null)
            {
                meta = (HtmlMeta)page.Header.FindControl("meta" + httpEquiv);
                meta.Content = content;
            }
            else
            {
                meta.ID = "meta" + httpEquiv;
                meta.HttpEquiv = httpEquiv;
                meta.Content = content;
                page.Header.Controls.Add(meta);
            }
        }

        /// <summary>
        /// Finds a control recursive
        /// </summary>
        /// <typeparam name="T">Control class</typeparam>
        /// <param name="controls">Input control collection</param>
        /// <returns>Found control</returns>
        public static T FindControlRecursive<T>(ControlCollection controls) where T : class
        {
            T found = default(T);

            if (controls != null && controls.Count > 0)
            {
                for (int i = 0; i < controls.Count; i++)
                {
                    if (controls[i] is T)
                    {
                        found = controls[i] as T;
                        break;
                    }
                    else
                    {
                        found = FindControlRecursive<T>(controls[i].Controls);
                        if (found != null)
                            break;
                    }
                }
            }

            return found;
        }

        /// <summary>
        /// Selects item
        /// </summary>
        /// <param name="list">List</param>
        /// <param name="value">Value to select</param>
        public static void SelectListItem(DropDownList list, object value)
        {
            if (list.Items.Count != 0)
            {
                var selectedItem = list.SelectedItem;
                if (selectedItem != null)
                    selectedItem.Selected = false;
                if (value != null)
                {
                    selectedItem = list.Items.FindByValue(value.ToString());
                    if (selectedItem != null)
                        selectedItem.Selected = true;
                }
            }
        }

        /// <summary>
        /// Gets server variable by name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Server variable</returns>
        public static string ServerVariables(string name)
        {
            string tmpS = string.Empty;
            try
            {
                if (HttpContext.Current.Request.ServerVariables[name] != null)
                {
                    tmpS = HttpContext.Current.Request.ServerVariables[name].ToString();
                }
            }
            catch
            {
                tmpS = string.Empty;
            }
            return tmpS;
        }

        /// <summary>
        /// Bind jQuery
        /// </summary>
        /// <param name="page">Page</param>
        public static void BindJQuery(Page page)
        {
            BindJQuery(page, false);
        }

        /// <summary>
        /// Bind jQuery
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="useHosted">Use hosted jQuery</param>
        public static void BindJQuery(Page page, bool useHosted)
        {
            if (page == null)
                throw new ArgumentNullException("page");

            //update version if required (e.g. 1.4)
            string jQueryUrl = string.Empty;
            if (useHosted)
            {
                jQueryUrl = "http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js";
                if (CommonHelper.IsCurrentConnectionSecured())
                {
                    jQueryUrl = jQueryUrl.Replace("http://", "https://");
                }
            }
            else
            {
               // jQueryUrl = CommonHelper.GetStoreLocation() + "Scripts/jquery-1.4.min.js";
            }

            jQueryUrl = string.Format("<script type=\"text/javascript\" src=\"{0}\" ></script>", jQueryUrl);

            if (page.Header != null)
            {
                //we have a header
                if (HttpContext.Current.Items["JQueryRegistered"] == null ||
                    !Convert.ToBoolean(HttpContext.Current.Items["JQueryRegistered"]))
                {
                    Literal script = new Literal() { Text = jQueryUrl };
                    Control control = page.Header.FindControl("SCRIPTS");
                    if (control != null)
                        control.Controls.AddAt(0, script);
                    else
                        page.Header.Controls.AddAt(0, script);
                }
                HttpContext.Current.Items["JQueryRegistered"] = true;
            }
            else
            {
                //no header found
                page.ClientScript.RegisterClientScriptInclude(jQueryUrl, jQueryUrl);
            }
        }

        /// <summary>
        /// Disable browser cache
        /// </summary>
        public static void DisableBrowserCache()
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Cache.SetExpires(new DateTime(1995, 5, 6, 12, 0, 0, DateTimeKind.Utc));
                HttpContext.Current.Response.Cache.SetNoStore();
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                HttpContext.Current.Response.Cache.AppendCacheExtension("post-check=0,pre-check=0");

            }
        }

      

        /// <summary>
        /// Gets a value indicating whether current connection is secured
        /// </summary>
        /// <returns>true - secured, false - not secured</returns>
        public static bool IsCurrentConnectionSecured()
        {
            bool useSSL = false;
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                useSSL = HttpContext.Current.Request.IsSecureConnection;
                //when your hosting uses a load balancer on their server then the Request.IsSecureConnection is never got set to true, use the statement below
                //just uncomment it
                //useSSL = HttpContext.Current.Request.ServerVariables["HTTP_CLUSTER_HTTPS"] == "on" ? true : false;
            }

            return useSSL;
        }

        /// <summary>
        /// Gets this page name
        /// </summary>
        /// <returns></returns>
        public static string GetThisPageUrl(bool includeQueryString)
        {
            string URL = string.Empty;
            if (HttpContext.Current == null)
                return URL;

            if (includeQueryString)
            {
                bool useSSL = IsCurrentConnectionSecured();
                string storeHost = GetStoreHost(useSSL);
                if (storeHost.EndsWith("/"))
                    storeHost = storeHost.Substring(0, storeHost.Length - 1);
                URL = storeHost + HttpContext.Current.Request.RawUrl;
            }
            else
            {
                URL = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            }
            URL = URL.ToLowerInvariant();
            return URL;
        }



        /// <summary>
        /// Gets store host location
        /// </summary>
        /// <param name="useSsl">Use SSL</param>
        /// <returns>Store host location</returns>
        public static string GetStoreHost(bool useSsl)
        {
            string result = "http://" + ServerVariables("HTTP_HOST");
            if (!result.EndsWith("/"))
                result += "/";
            if (useSsl)
            {
                //shared SSL certificate URL
                string sharedSslUrl = string.Empty;
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SharedSSLUrl"]))
                    sharedSslUrl = ConfigurationManager.AppSettings["SharedSSLUrl"].Trim();

                if (!String.IsNullOrEmpty(sharedSslUrl))
                {
                    //shared SSL
                    result = sharedSslUrl;
                }
                else
                {
                    //standard SSL
                    result = result.Replace("http:/", "https:/");
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["UseSSL"])
                    && Convert.ToBoolean(ConfigurationManager.AppSettings["UseSSL"]))
                {
                    //SSL is enabled

                    //get shared SSL certificate URL
                    string sharedSslUrl = string.Empty;
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SharedSSLUrl"]))
                        sharedSslUrl = ConfigurationManager.AppSettings["SharedSSLUrl"].Trim();
                    if (!String.IsNullOrEmpty(sharedSslUrl))
                    {
                        //shared SSL

                        /* we need to set a store URL here (IoC.Resolve<ISettingManager>().StoreUrl property)
                         * but we cannot reference Nop.BusinessLogic.dll assembly.
                         * So we are using one more app config settings - <add key="NonSharedSSLUrl" value="http://www.yourStore.com" />
                         */
                        string nonSharedSslUrl = string.Empty;
                        if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["NonSharedSSLUrl"]))
                            nonSharedSslUrl = ConfigurationManager.AppSettings["NonSharedSSLUrl"].Trim();
                        if (string.IsNullOrEmpty(nonSharedSslUrl))
                            throw new Exception("NonSharedSSLUrl app config setting is not empty");
                        result = nonSharedSslUrl;
                    }
                }
            }

            if (!result.EndsWith("/"))
                result += "/";

            return result.ToLowerInvariant();
        }

        /// <summary>
        /// Reloads current page
        /// </summary>
        public static void ReloadCurrentPage()
        {
            bool useSSL = IsCurrentConnectionSecured();
            ReloadCurrentPage(useSSL);
        }

        /// <summary>
        /// Reloads current page
        /// </summary>
        /// <param name="useSsl">Use SSL</param>
        public static void ReloadCurrentPage(bool useSsl)
        {
            string storeHost = GetStoreHost(useSsl);
            if (storeHost.EndsWith("/"))
                storeHost = storeHost.Substring(0, storeHost.Length - 1);
            string url = storeHost + HttpContext.Current.Request.RawUrl;
            url = url.ToLowerInvariant();
            HttpContext.Current.Response.Redirect(url);
        }

        /// <summary>
        /// Modifies query string
        /// </summary>
        /// <param name="url">Url to modify</param>
        /// <param name="queryStringModification">Query string modification</param>
        /// <param name="targetLocationModification">Target location modification</param>
        /// <returns>New url</returns>
        public static string ModifyQueryString(string url, string queryStringModification, string targetLocationModification)
        {
            if (url == null)
                url = string.Empty;
            url = url.ToLowerInvariant();

            if (queryStringModification == null)
                queryStringModification = string.Empty;
            queryStringModification = queryStringModification.ToLowerInvariant();

            if (targetLocationModification == null)
                targetLocationModification = string.Empty;
            targetLocationModification = targetLocationModification.ToLowerInvariant();


            string str = string.Empty;
            string str2 = string.Empty;
            if (url.Contains("#"))
            {
                str2 = url.Substring(url.IndexOf("#") + 1);
                url = url.Substring(0, url.IndexOf("#"));
            }
            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?") + 1);
                url = url.Substring(0, url.IndexOf("?"));
            }
            if (!string.IsNullOrEmpty(queryStringModification))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    var dictionary = new Dictionary<string, string>();
                    foreach (string str3 in str.Split(new char[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str3))
                        {
                            string[] strArray = str3.Split(new char[] { '=' });
                            if (strArray.Length == 2)
                            {
                                dictionary[strArray[0]] = strArray[1];
                            }
                            else
                            {
                                dictionary[str3] = null;
                            }
                        }
                    }
                    foreach (string str4 in queryStringModification.Split(new char[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str4))
                        {
                            string[] strArray2 = str4.Split(new char[] { '=' });
                            if (strArray2.Length == 2)
                            {
                                dictionary[strArray2[0]] = strArray2[1];
                            }
                            else
                            {
                                dictionary[str4] = null;
                            }
                        }
                    }
                    var builder = new StringBuilder();
                    foreach (string str5 in dictionary.Keys)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("&");
                        }
                        builder.Append(str5);
                        if (dictionary[str5] != null)
                        {
                            builder.Append("=");
                            builder.Append(dictionary[str5]);
                        }
                    }
                    str = builder.ToString();
                }
                else
                {
                    str = queryStringModification;
                }
            }
            if (!string.IsNullOrEmpty(targetLocationModification))
            {
                str2 = targetLocationModification;
            }
            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)) + (string.IsNullOrEmpty(str2) ? "" : ("#" + str2))).ToLowerInvariant();
        }

        /// <summary>
        /// Remove query string from url
        /// </summary>
        /// <param name="url">Url to modify</param>
        /// <param name="queryString">Query string to remove</param>
        /// <returns>New url</returns>
        public static string RemoveQueryString(string url, string queryString)
        {
            if (url == null)
                url = string.Empty;
            url = url.ToLowerInvariant();

            if (queryString == null)
                queryString = string.Empty;
            queryString = queryString.ToLowerInvariant();


            string str = string.Empty;
            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?") + 1);
                url = url.Substring(0, url.IndexOf("?"));
            }
            if (!string.IsNullOrEmpty(queryString))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    foreach (string str3 in str.Split(new char[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str3))
                        {
                            string[] strArray = str3.Split(new char[] { '=' });
                            if (strArray.Length == 2)
                            {
                                dictionary[strArray[0]] = strArray[1];
                            }
                            else
                            {
                                dictionary[str3] = null;
                            }
                        }
                    }
                    dictionary.Remove(queryString);

                    var builder = new StringBuilder();
                    foreach (string str5 in dictionary.Keys)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("&");
                        }
                        builder.Append(str5);
                        if (dictionary[str5] != null)
                        {
                            builder.Append("=");
                            builder.Append(dictionary[str5]);
                        }
                    }
                    str = builder.ToString();
                }
            }
            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)));
        }

        /// <summary>
        /// Ensures that requested page is secured (https://)
        /// </summary>
        public static void EnsureSsl()
        {
            if (!IsCurrentConnectionSecured())
            {
                bool useSSL = false;
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["UseSSL"]))
                    useSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSSL"]);
                if (useSSL)
                {
                    //if (!HttpContext.Current.Request.Url.IsLoopback)
                    //{
                    ReloadCurrentPage(true);
                    //}
                }
            }
        }

        /// <summary>
        /// Ensures that requested page is not secured (http://)
        /// </summary>
        public static void EnsureNonSsl()
        {
            if (IsCurrentConnectionSecured())
            {
                ReloadCurrentPage(false);
            }
        }

        /// <summary>
        /// Sets cookie
        /// </summary>
        /// <param name="cookieName">Cookie name</param>
        /// <param name="cookieValue">Cookie value</param>
        /// <param name="ts">Timespan</param>
        public static void SetCookie(string cookieName, string cookieValue)
        {
            try
            {
                HttpCookie cookie = new HttpCookie(cookieName);
                cookie.Value = HttpContext.Current.Server.UrlEncode(cookieValue);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }

        /// <summary>
        /// Sets cookie
        /// </summary>
        /// <param name="cookieName">Cookie name</param>
        /// <param name="cookieValue">Cookie value</param>
        /// <param name="ts">Timespan</param>
        public static void SetCookie(string cookieName, string cookieValue, TimeSpan ts)
        {
            try
            {
                HttpCookie cookie = new HttpCookie(cookieName);
                cookie.Value = HttpContext.Current.Server.UrlEncode(cookieValue);
                DateTime dt = DateTime.Now;
                cookie.Expires = dt.Add(ts);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }

        /// <summary>
        /// Gets cookie string
        /// </summary>
        /// <param name="cookieName">Cookie name</param>
        /// <param name="decode">Decode cookie</param>
        /// <returns>Cookie string</returns>
        public static string GetCookieString(string cookieName, bool decode)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] == null)
            {
                return string.Empty;
            }
            try
            {
                string tmp = HttpContext.Current.Request.Cookies[cookieName].Value.ToString();
                if (decode)
                    tmp = HttpContext.Current.Server.UrlDecode(tmp);
                return tmp;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets boolean value from cookie
        /// </summary>
        /// <param name="cookieName">Cookie name</param>
        /// <returns>Result</returns>
        public static bool GetCookieBool(string cookieName)
        {
            string str1 = GetCookieString(cookieName, true).ToUpperInvariant();
            return (str1 == "TRUE" || str1 == "YES" || str1 == "1");
        }

        /// <summary>
        /// Gets integer value from cookie
        /// </summary>
        /// <param name="cookieName">Cookie name</param>
        /// <returns>Result</returns>
        public static int GetCookieInt(string cookieName)
        {
            string str1 = GetCookieString(cookieName, true);
            if (!String.IsNullOrEmpty(str1))
                return Convert.ToInt32(str1);
            else
                return 0;
        }

        /// <summary>
        /// Gets boolean value from NameValue collection
        /// </summary>
        /// <param name="config">NameValue collection</param>
        /// <param name="valueName">Name</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Result</returns>
        public static bool ConfigGetBooleanValue(NameValueCollection config,
            string valueName, bool defaultValue)
        {
            bool result;
            string str1 = config[valueName];
            if (str1 == null)
                return defaultValue;
            if (!bool.TryParse(str1, out result))
                throw new Exception(string.Format("Value must be boolean {0}", valueName));
            return result;
        }

        /// <summary>
        /// Gets integer value from NameValue collection
        /// </summary>
        /// <param name="config">NameValue collection</param>
        /// <param name="valueName">Name</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="zeroAllowed">Zero allowed</param>
        /// <param name="maxValueAllowed">Max value allowed</param>
        /// <returns>Result</returns>
        public static int ConfigGetIntValue(NameValueCollection config,
            string valueName, int defaultValue, bool zeroAllowed, int maxValueAllowed)
        {
            int result;
            string str1 = config[valueName];
            if (str1 == null)
                return defaultValue;
            if (!int.TryParse(str1, out result))
            {
                if (zeroAllowed)
                {
                    throw new Exception(string.Format("Value must be non negative integer {0}", valueName));
                }
                throw new Exception(string.Format("Value must be positive integer {0}", valueName));
            }
            if (zeroAllowed && (result < 0))
                throw new Exception(string.Format("Value must be non negative integer {0}", valueName));
            if (!zeroAllowed && (result <= 0))
                throw new Exception(string.Format("Value must be positive integer {0}", valueName));
            if ((maxValueAllowed > 0) && (result > maxValueAllowed))
                throw new Exception(string.Format("Value too big {0}", valueName));
            return result;
        }

        /// <summary>
        /// Write XML to response
        /// </summary>
        /// <param name="xml">XML</param>
        /// <param name="fileName">Filename</param>
        public static void WriteResponseXml(string xml, string fileName)
        {
            if (!String.IsNullOrEmpty(xml))
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                XmlDeclaration decl = document.FirstChild as XmlDeclaration;
                if (decl != null)
                {
                    decl.Encoding = "utf-8";
                }
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.Charset = "utf-8";
                response.ContentType = "text/xml";
                response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
                response.BinaryWrite(Encoding.UTF8.GetBytes(document.InnerXml));
                response.End();
            }
        }

        /// <summary>
        /// Write text to response
        /// </summary>
        /// <param name="txt">text</param>
        /// <param name="fileName">Filename</param>
        public static void WriteResponseTxt(string txt, string fileName)
        {
            if (!String.IsNullOrEmpty(txt))
            {
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.Charset = "utf-8";
                response.ContentType = "text/plain";
                response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
                response.BinaryWrite(Encoding.UTF8.GetBytes(txt));
                response.End();
            }
        }

        /// <summary>
        /// Write XLS file to response
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="targetFileName">Target file name</param>
        public static void WriteResponseXls(string filePath, string targetFileName)
        {
            if (!String.IsNullOrEmpty(filePath))
            {
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.Charset = "utf-8";
                response.ContentType = "text/xls";
                response.AddHeader("content-disposition", string.Format("attachment; filename={0}", targetFileName));
                response.BinaryWrite(File.ReadAllBytes(filePath));
                response.End();
            }
        }

        /// <summary>
        /// Write PDF file to response
        /// </summary>
        /// <param name="filePath">File napathme</param>
        /// <param name="targetFileName">Target file name</param>
        /// <remarks>For BeatyStore project</remarks>
        public static void WriteResponsePdf(string filePath, string targetFileName)
        {
            if (!String.IsNullOrEmpty(filePath))
            {
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.Charset = "utf-8";
                response.ContentType = "text/pdf";
                response.AddHeader("content-disposition", string.Format("attachment; filename={0}", targetFileName));
                response.BinaryWrite(File.ReadAllBytes(filePath));
                response.End();
            }
        }

        /// <summary>
        /// Generate random digit code
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>Result string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = String.Concat(str, random.Next(10).ToString());
            return str;
        }

        /// <summary>
        /// Convert enum for front-end
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Converted string</returns>
        public static string ConvertEnum(string str)
        {
            string result = string.Empty;
            char[] letters = str.ToCharArray();
            foreach (char c in letters)
                if (c.ToString() != c.ToString().ToLower())
                    result += " " + c.ToString();
                else
                    result += c.ToString();
            return result;
        }

        /// <summary>
        /// Fills drop down list with values of enumaration
        /// </summary>
        /// <param name="list">Dropdownlist</param>
        /// <param name="enumType">Enumeration</param>
        public static void FillDropDownWithEnum(DropDownList list, Type enumType)
        {
            FillDropDownWithEnum(list, enumType, true);
        }

        /// <summary>
        /// Fills drop down list with values of enumaration
        /// </summary>
        /// <param name="list">Dropdownlist</param>
        /// <param name="enumType">Enumeration</param>
        /// <param name="clearListItems">Clear list of exsisting items</param>
        public static void FillDropDownWithEnum(DropDownList list, Type enumType, bool clearListItems)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (enumType == null)
            {
                throw new ArgumentNullException("enumType");
            }
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("enumType must be enum type");
            }

            if (clearListItems)
            {
                list.Items.Clear();
            }
            string[] strArray = Enum.GetNames(enumType);
            foreach (string str2 in strArray)
            {
                int enumValue = (int)Enum.Parse(enumType, str2, true);
                ListItem ddlItem = new ListItem(CommonHelper.ConvertEnum(str2), enumValue.ToString());
                list.Items.Add(ddlItem);
            }
        }

    

        /// <summary>
        /// Set response NoCache
        /// </summary>
        /// <param name="response">Response</param>
        public static void SetResponseNoCache(HttpResponse response)
        {
            if (response == null)
                throw new ArgumentNullException("response");

            //response.Cache.SetCacheability(HttpCacheability.NoCache) 

            response.CacheControl = "private";
            response.Expires = 0;
            response.AddHeader("pragma", "no-cache");
        }

        /// <summary>
        /// Ensure that a string doesn't exceed maximum allowed length
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="maxLength">Maximum length</param>
        /// <returns>Input string if its lengh is OK; otherwise, truncated input string</returns>
        public static string EnsureMaximumLength(string str, int maxLength)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
                return str.Substring(0, maxLength);
            else
                return str;
        }

        /// <summary>
        /// Ensures that a string only contains numeric values
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Input string with only numeric values, empty string if input is null/empty</returns>
        public static string EnsureNumericOnly(string str)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;

            var result = new StringBuilder();
            foreach (char c in str)
            {
                if (Char.IsDigit(c))
                    result.Append(c);
            }
            return result.ToString();

            // Loop is faster than RegEx
            //return Regex.Replace(str, "\\D", "");
        }

        /// <summary>
        /// Ensure that a string is not null
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Result</returns>
        public static string EnsureNotNull(string str)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;

            return str;
        }

        /// <summary>
        /// Get a value indicating whether content page is requested
        /// </summary>
        /// <returns>Result</returns>
        public static bool IsContentPageRequested()
        {
            HttpContext context = HttpContext.Current;
            HttpRequest request = context.Request;

            if (!request.Url.LocalPath.ToLower().EndsWith(".aspx") &&
                !request.Url.LocalPath.ToLower().EndsWith(".asmx") &&
                !request.Url.LocalPath.ToLower().EndsWith(".ashx"))
            {
                return false;
            }

            return true;
        }

        #endregion

        #region CreditCard

        public static bool ValidateCSC(string cardNumber, string cardType, string cvv)
        {
            if (CommonHelper.EnsureNotNull(cvv) == String.Empty)
            {
                return false;
            }
            else
            {
                if (CommonHelper.onlynums(cvv) == false)
                {
                    return false;
                }
                if ((CommonHelper.CountNums(cvv) != 3) && (CommonHelper.CountNums(cvv) != 4))
                {
                    return false;
                }

                if ((cardNumber[0].ToString() == "5") && (cardType.ToLower() != "mastercard"))
                {
                    return false;
                }
                else if ((cardNumber[0].ToString() == "4") && (cardType.ToLower() != "visa"))
                {
                    return false;

                }
                else if ((cardNumber[0].ToString() == "6") && (cardType.ToLower() != "discover"))
                {
                    return false;

                }
                else if ((cardNumber[0].ToString() == "3") && (cardType.ToLower() != "americanexpress"))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ValidateCardNumber(string cardNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(cardNumber))
                    return false;
                // Array to contain individual numbers
                System.Collections.ArrayList CheckNumbers = new ArrayList();
                // So, get length of card
                int CardLength = cardNumber.Length;

                // Double the value of alternate digits, starting with the second digit
                // from the right, i.e. back to front.
                // Loop through starting at the end
                for (int i = CardLength - 2; i >= 0; i = i - 2)
                {
                    // Now read the contents at each index, this
                    // can then be stored as an array of integers

                    // Double the number returned
                    CheckNumbers.Add(Int32.Parse(cardNumber[i].ToString()) * 2);
                }

                int CheckSum = 0;    // Will hold the total sum of all checksum digits

                // Second stage, add separate digits of all products
                for (int iCount = 0; iCount <= CheckNumbers.Count - 1; iCount++)
                {
                    int _count = 0;    // will hold the sum of the digits

                    // determine if current number has more than one digit
                    if ((int)CheckNumbers[iCount] > 9)
                    {
                        int _numLength = ((int)CheckNumbers[iCount]).ToString().Length;
                        // add count to each digit
                        for (int x = 0; x < _numLength; x++)
                        {
                            _count = _count + Int32.Parse(
                                  ((int)CheckNumbers[iCount]).ToString()[x].ToString());
                        }
                    }
                    else
                    {
                        // single digit, just add it by itself
                        _count = (int)CheckNumbers[iCount];
                    }
                    CheckSum = CheckSum + _count;    // add sum to the total sum
                }
                // Stage 3, add the unaffected digits
                // Add all the digits that we didn't double still starting from the
                // right but this time we'll start from the rightmost number with 
                // alternating digits
                int OriginalSum = 0;
                for (int y = CardLength - 1; y >= 0; y = y - 2)
                {
                    OriginalSum = OriginalSum + Int32.Parse(cardNumber[y].ToString());
                }

                // Perform the final calculation, if the sum Mod 10 results in 0 then
                // it's valid, otherwise its false.
                return (((OriginalSum + CheckSum) % 10) == 0);
            }
            catch
            {
                return false;
            }
        }
       //public static string Encrypt(string clearText, string Password)
       // {
       //     byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
       //     PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
       //     new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
       //     byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
       //     return Convert.ToBase64String(encryptedData);
       // }
       //                                                                                                                                                                                                                                                     public static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
       // {
       //     MemoryStream ms = new MemoryStream();
       //     Rijndael alg = Rijndael.Create();
       //     alg.Key = Key;
       //     alg.IV = IV;
       //     CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
       //     cs.Write(clearData, 0, clearData.Length);
       //     cs.Close();
       //     byte[] encryptedData = ms.ToArray();
       //     return encryptedData;
       //     }
         
        public static int CountNums(string s)
        {
            string s1 = s;

            int i;
            int j = 0;
            for (i = 0; i < s1.Length; i++)
            {
                if (isnum(s1[i]) == true) { j++; }
            }

            return j;

        }

                                                                                                                                                                                                                                                            public static bool isnum(char c)
        {
            bool b = false;
            if ((((int)c) >= 48) && (((int)c) <= 57)) b = true;
            return b;
        }

                                                                                                                                                                                                                                                            public static bool onlynums(string s)
        {
            string s1 = s;

            int i;
            bool b = true;

            for (i = 0; i < s1.Length; i++)
            {
                if (isnum(s1[i]) != true) { b = false; }
            }

            return b;

        }

         public static bool onlynumsperdash(string s)
        {
            string s1 = s;

            int i;
            bool b = true;

            for (i = 0; i < s1.Length; i++)
            {
                if ((isnum(s1[i]) != true) && (s1[i].ToString() != "-") && (s1[i].ToString() != "(") && (s1[i].ToString() != ")")) { b = false; }

            }

            return b;

        }


        public static string Left(string param, int length)
        {
            string result = "";
            if (param.Length > length)
            {
                result = param.Substring(0, length);
            }
            else
            {
                result = param;
            }
            return result;
        }

        public static string fixquotes(string s)
        {
            return  s.Replace("'", "''");
     
        }

        public static string fixquotesAccents(string s)
        {
            string s1;
            s1 = s.Replace("'", "''");
            return  ClearAccents(s1);
            
        }

        public static string ClearAccents(string text)
        {
            //url = Regex.Replace(url, @"\s+", "-");
            string stFormD = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb.Replace("'", "''");
            return (sb.ToString());
        }



        public static string zeropad(string s, int i)
        {
            string s1 = s;
            while (s1.Length < i)
            {
                s1 = "0" + s1;
            }
            return s1;
        }
        public static string HttpPost(string uri, string parameters)
        {
            // parameters: name1=value1&name2=value2	
            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(parameters);
            Stream os = null;
            try
            { // send the Post
                webRequest.ContentLength = bytes.Length;   //Count bytes to send
                os = webRequest.GetRequestStream();
                os.Write(bytes, 0, bytes.Length);         //Send it
            }
            catch (WebException ex)
            {
                Console.WriteLine("HttpPost: request error" + ex.Message);
            }
            finally
            {
                if (os != null)
                {
                    os.Close();
                }
            }

            try
            { // get the response
                WebResponse webResponse = webRequest.GetResponse();
                if (webResponse == null)
                { return null; }
                StreamReader sr = new StreamReader(webResponse.GetResponseStream());
                return sr.ReadToEnd().Trim();
            }
            catch (WebException ex)
            {
                Console.WriteLine("HttpPost: Response error" + ex.Message);
            }
            return null;
        }

        public static string SoapRequest(String data, string URL, int timeOut)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            // Create the web request  
            HttpWebRequest request = WebRequest.Create(URL) as HttpWebRequest;
            // Add authentication to request  
            //request.Credentials = new NetworkCredential("foo@mydomain.com", "bar");
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.Timeout = timeOut;
            request.Headers.Add("SOAPAction: " + URL);
            //Create Stream and Complete Request
            StreamWriter streamWriter = new StreamWriter(request.GetRequestStream());
            streamWriter.Write(data);
            streamWriter.Close();

            // Get response  
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream  
                StreamReader reader = new StreamReader(response.GetResponseStream());
                // Console application output  
                //Console.WriteLine(reader.ReadToEnd());
                return reader.ReadToEnd();
            }
        }

        public static string Right(string param, int length)
        {
            string result = param.Substring(param.Length - length, length);
            return result;
        }

        public static Hashtable BindToEnum(Type enumType)
        {
            // get the names from the enumeration
            string[] names = Enum.GetNames(enumType);
            // get the values from the enumeration
            Array values = Enum.GetValues(enumType);
            // turn it into a hash table
            Hashtable ht = new Hashtable();
            for (int i = 0; i < names.Length; i++)
                // note the cast to integer here is important
                // otherwise we'll just get the enum string back again
                ht.Add(names[i], (int)values.GetValue(i));
            // return the dictionary to be bound to
            return ht;
        }

        public static string Encrypt(string strPlainText)
        {
            string encryptionPrivateKey =BCEncryptor.GetEncryptionKey();
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            UnicodeEncoding encoding = new UnicodeEncoding();
            ASCIIEncoding encoding2 = new ASCIIEncoding();
            byte[] bytes = encoding.GetBytes(strPlainText);
            MemoryStream stream = new MemoryStream();
            provider.Key = encoding2.GetBytes(encryptionPrivateKey.Substring(0, 16));
            provider.IV = encoding2.GetBytes(encryptionPrivateKey.Substring(8, 8));
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            return Convert.ToBase64String(stream.ToArray());
        }

        public static string Decrypt(string strCipherText)
        {
            string encryptionPrivateKey = BCEncryptor.GetEncryptionKey();
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            UnicodeEncoding encoding = new UnicodeEncoding();
            ASCIIEncoding encoding2 = new ASCIIEncoding();
            byte[] buffer = Convert.FromBase64String(strCipherText);
            MemoryStream stream = new MemoryStream();
            MemoryStream stream2 = new MemoryStream(buffer);
            provider.Key = encoding2.GetBytes(encryptionPrivateKey.Substring(0, 16));
            provider.IV = encoding2.GetBytes(encryptionPrivateKey.Substring(8, 8));
            CryptoStream stream3 = new CryptoStream(stream2, provider.CreateDecryptor(), CryptoStreamMode.Read);
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(new StreamReader(stream3).ReadToEnd());
            writer.Flush();
            stream3.Clear();
            provider.Clear();
            return encoding.GetString(stream.ToArray());
        }

        public static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {

            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();

            alg.Key = Key;
            alg.IV = IV;

            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return decryptedData;

        }

     

        public static string Decrypt(string cipherText, string Password)
        {

            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            return System.Text.Encoding.Unicode.GetString(decryptedData);

        }
     

        public static  Dictionary<string, string> QueryCollection(string queryParam)
        {
            NameValueCollection queryparams = HttpUtility.ParseQueryString(queryParam);
            Dictionary<string, string> qDict = new Dictionary<string, string>();
            for (int i = 0; i < queryparams.Count; i++) // <-- No duplicates returned.
            {
                
                qDict.Add(queryparams.GetKey(i).ToLower(), queryparams.Get(i));

            }

            return qDict;
        }

          public static string GetQueryString(string Url)
          {
              string queryString = String.Empty;
              int startPos = Url.IndexOf('?');
              if (startPos > 0)
                  queryString = Url.Substring(startPos, (Url.Length - startPos));
            
              return queryString;
          }

          public static string GetSeparator(string url)
          {
              string separator;
              if (!url.Contains("?"))
                  separator = "?";
              else
                  separator = "&";

              return separator;
          }

          public static string IpAddress(HttpContext context)
          {
              string strIpAddress;
              strIpAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
              if (strIpAddress == null)
              {
                  strIpAddress = context.Request.ServerVariables["REMOTE_ADDR"];
              }
              return strIpAddress;
          }
          public static bool IsHttps(HttpContext context)
          {
              bool isHttps = false;
              //if (context.Request.Url.ToString().ToLower().Contains("csstaging") || context.Request.Url.ToString().ToLower().Contains("localhost"))
              //{
              //    //This means this is test server so we return true. This is temporary fix until RS finds permanent solution for x-https variable
              //    return true;
              //}
              //if (context.Request.Url != null)
              //{
              //    if (context.Request.Url.ToString().ToLower().Contains(":81"))
              //    {
              //        isHttps = true;
              //    }
              //    else
              //    {
              //        isHttps = false;
              //    }
              //}


              if (context.Request.Headers["X-HTTPS"] != null)
              {
                  if (context.Request.Headers["X-HTTPS"].ToLower().Equals("no"))
                  {
                      isHttps = false;
                  }
                  else
                  {
                      isHttps = true;
                  }
              }
              else
              {
                  isHttps = true; // this will avoid redirects on dev and local
              }

              return isHttps;
          }
          public string decoderesponse(string s)
          {
              string TransactionStatus1 = "";
              string[] tok = new string[500];
              int i;
              for (i = 0; i < 500; i++)
              {
                  tok[i] = "";
              }
              int numtok = 1;

              for (i = 0; i < s.Length; i++)
              {
                  if (s[i].ToString() == "|")
                  {
                      numtok++;
                  }
                  else
                  {
                      tok[numtok] += s[i];
                  }
              }

              string s1 = "";
              s1 += "MessageFormat:" + tok[1] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "ePayAccountNum:" + tok[2] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "TransactionCode:" + tok[3] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "SequenceNum:" + tok[4] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "MailOrderIdentifier:" + tok[5] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "AccountNum:" + tok[6] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "ExpirationDate:" + tok[7] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "AuthorizedAmount:" + tok[8] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "AuthorizationDate:" + tok[9] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "AuthorizationTime:" + tok[10] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "TransactionStatus:" + tok[11] + ((char)(13)).ToString() + ((char)(10)).ToString();
              TransactionStatus1 = tok[11];

              s1 += "CustomerNum:" + tok[12] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "Order Num:" + tok[13] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "ReferenceNum:" + tok[14] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "Authorization ResponseCode:" + tok[15] + ((char)(13)).ToString() + ((char)(10)).ToString();
             // Session["authcode2"] = tok[15];
              s1 += "AuthorizationSource:" + tok[16] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "Authorization Characteristic Indicator:" + tok[17] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "TransactionID:" + tok[18] + ((char)(13)).ToString() + ((char)(10)).ToString();
             // Session["transactionid2"] = tok[18];
              s1 += "ValidationCode:" + tok[19] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "SIC/CatCode:" + tok[20] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "CountryCode:" + tok[21] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "AVSResponseCode:" + tok[22] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "MerchantStoreNum:" + tok[23] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "CVV2ResponseCode:" + tok[24] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "CAVVCODE:" + tok[25] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "CrossReferenceNum:" + tok[26] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "ExtendedTransaction Status:" + tok[27] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "CAVVResponseCode:" + tok[28] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "XID:" + tok[29] + ((char)(13)).ToString() + ((char)(10)).ToString();
              s1 += "ECIValue:" + tok[30] + ((char)(13)).ToString() + ((char)(10)).ToString();

              return s1;

          }
          public static string GetGeoTargetLocation(string ipAddress)
          {
              string ApiUrl = "http://geoip.maxmind.com/a";
              string ApiParameters = "l=qfUqNIdaA1Ur&i=" + ipAddress;
              string ApiRawResponse = "";
              //string[] ApiResponse;
              try
              {
                  ApiRawResponse = HttpPost(ApiUrl, ApiParameters);
                  //ApiResponse = ApiRawResponse.Split('|');
                  return ApiRawResponse.ToLower();
              }
              catch (Exception)
              {
                  
              }
              return "";
          }
        #endregion

          #region Methods

          /// <summary>
          /// Gets Randon number
          /// </summary>
          /// <param name="email">Order Date</param>
          /// <returns>string with randon order number</returns>
          public static string GetRandonOrderNumber(DateTime orderDate)
          {
              string result = "";
              result = orderDate.ToString("yyyyMMddhhmmss") + new Random().Next(10, 1000);
              return result;
          }
          /// <summary>
          /// Gets SOAP Envelope
          /// </summary>
          /// <param name="email"></param>
          /// <returns>string with soap envelope</returns>        
          public static string GetSoapEnvelope()
          {
              StringBuilder soapRequest = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
              soapRequest.Append("<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding\" ");
              soapRequest.Append("xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" ");
              soapRequest.Append("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" ");
              soapRequest.Append("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
              soapRequest.Append("xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" ");
              soapRequest.Append("xmlns:si=\"http://soapinterop.org/xsd\">");
              soapRequest.Append("<SOAP-ENV:Body>");
              soapRequest.Append("{0}");
              soapRequest.Append("</SOAP-ENV:Body></SOAP-ENV:Envelope>");
              return soapRequest.ToString();
          }
          /// <summary>
          /// Validates International Phone Number
          /// </summary>
          /// <param name="phone"></param>
          /// <param name="international"></param>
          /// <returns>bool if the phone number is in valid format</returns> 
          public static bool IsValidInternationalPhone(string phone, bool international)
          {
              bool result = false;
              if (String.IsNullOrEmpty(phone))
                  return result;
              phone = phone.Trim();
              if (international)
                  //  result = Regex.IsMatch(phone, @"\+(9[976]\d|8[987530]\d|6[987]\d|5[90]\d|42\d|3[875]\d|2[98654321]\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|4[987654310]|3[9643210]|2[70]|7|1)\d{1,14}$");
                  result = Regex.IsMatch(phone, @"^[+]?([0-9]*[\.\s\-\(\)]|[0-9]+){3,24}$");

              else
                  result = IsValidPhone(phone);
              return result;
          }

          public static string ReplaceString(string source, string replacechar, string replacewith)
          {
              string result = "";

              if (String.IsNullOrEmpty(source) || String.IsNullOrEmpty(replacechar))
              {
                  return source;
              }

              result = source.Replace(replacechar, replacewith);
              return result;
          } 
            
          #endregion

    }
}
