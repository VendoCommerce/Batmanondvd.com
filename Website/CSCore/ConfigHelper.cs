using System;
using System.Collections.Specialized;
using System.Configuration;

namespace CSCore.DataHelper
{
   

    public class ConfigHelper
    {
        // Constants
        private const string SMTP_SERVER = "SmtpServer";
        private const string SMTP_SERVER_DEFAULT = "10.10.220.225";
        private const string BaseConnectionString = "";
        public const int PAGE_SIZE = 20;
        public const string DefaultCountry = "231";
        public static string SmtpServer;
        
         //ASP.NET Cache Settings
        private const double DefaultExpireMin = 80;
        private const int DefaultPriority = 3;
        public static NameValueCollection cacheValues;


        public ConfigHelper() { }

        //Read connection from web.config file
        public static string GetDBConnection()
        {
       
            string conn = ReadAppSetting("client_db");
            string clientConnection = (conn == null) ? BaseConnectionString : conn;
            return clientConnection;
        }

        public static string GetMasterDBConnection()
        {

            string conn = ReadAppSetting("master_db");
            string masterConnection = (conn == null) ? BaseConnectionString : conn;
            return masterConnection;
        }

        public static string GetASPStateDBConnection()
        {
            string conn = ReadAppSetting("aspstate_db");
            string aspStateConnection = (conn == null) ? BaseConnectionString : conn;
            return aspStateConnection;
        }

        public static int EmailAppSetting(string name)
        {
            int emailId = 0;
            if (ReadAppSetting(name) != null)
                emailId = Convert.ToInt32(ConfigHelper.ReadAppSetting(name));
            return emailId;
        }


        public static string ReadAppSetting(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }

        public static string ReadAppSetting(string name, string defaultValue)
        {
            string val = ReadAppSetting(name);
            return (val == null ? defaultValue : val);
        }

        /// <summary>
        /// Retrieve value for settings specific to ASP.NET Cache
        /// </summary>
        /// <param name="cacheGlobalKey">The global key to identify one type of cache from another
        /// , usually the name of the class</param>
        /// <param name="timeSpan">Return the relative expiration period in minutes</param>
        /// <param name="priority">Return the cache priority</param>
        public static void ReadSpecificCacheSetting(string cacheGlobalKey
            , out TimeSpan timeSpan, out int priority)
        {
            double expireMins = DefaultExpireMin;
            if (cacheValues[cacheGlobalKey + "Expire"] != null)
                expireMins = Convert.ToDouble(cacheValues[cacheGlobalKey + "Expire"].ToString());
            if (expireMins >= 1000)
                timeSpan = TimeSpan.FromMinutes(1000);
            else
                timeSpan = TimeSpan.FromMinutes(expireMins);

            priority = DefaultPriority;
            if (cacheValues[cacheGlobalKey + "Priority"] != null)
                priority = Convert.ToInt32(cacheValues[cacheGlobalKey + "Priority"].ToString());
        }


       




    }
}

