using System;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace CSWeb
{
    /// <summary>
    /// Summary description for Customer
    /// </summary>
    /// 
    public class UserSessions
    {

        public static string GetIpAddress(HttpContext context)
        {
            string strIpAddress;
            strIpAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIpAddress == null)
            {
                strIpAddress = context.Request.ServerVariables["REMOTE_ADDR"];
            }
            return strIpAddress;
        }

        public static string GetSessionId(HttpContext context)
        {
            string sessionId = String.Empty;
            string DailyCookie = String.Empty;
            if (context.Request.Cookies["DailyCookie"] != null)
            {
                DailyCookie = context.Request.Cookies["DailyCookie"].Value;
            }
            else
            {
                if (context.Request.Cookies["ASP.NET_SessionId"] != null)
                {
                    DailyCookie = context.Request.Cookies["ASP.NET_SessionId"].Value;
                }
                HttpCookie DailyCookieID = new HttpCookie("DailyCookie");
                DateTime date = DateTime.Now;
                DailyCookieID.Expires = EndOfDay(date);
                DailyCookieID.Value = DailyCookie;
                DailyCookieID.Domain = context.Request.Url.Host.ToString().Replace("www.", "");
                context.Response.Cookies.Add(DailyCookieID);
            }
            sessionId = DailyCookie;
            return sessionId;
        }

        public static DateTime EndOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        public static DateTime StartOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }

        private static string GetBoolValue(bool value)
        {
            if (value)
                return "1";
            return "0";
        }

        private static string GetCleanUrl(HttpContext context)
        {
            string url = context.Request.Url.AbsoluteUri;
            if (url.Contains("?404"))
            {
                url = url.Substring(url.IndexOf("?404") + 5);
                url = url.Substring(url.IndexOf("http"));
            }
            return context.Server.UrlDecode(url);
        }

        private static string GetDeviceType(HttpContext context)
        {
            try
            {
                string ua = context.Request.UserAgent.ToLower();
                if (ua != null && (ua.Contains("iphone") || ua.Contains("blackberry") || ua.Contains("android")))
                {
                    if (ua.Contains("ipad") || ua.Contains("galaxy"))
                        return "Mobile Tablet";
                    return "Mobile";
                }
            }
            catch { }
            return "Desktop";
        }

        private static string GetQueryValue(HttpContext context, string value)
        {
            if (context.Request.QueryString[value] != null)
                return context.Request.QueryString[value];
            else return null;
        }

        private static string GetTrafficSource(HttpContext context)
        {
            //TODO: Also account for DISPLAY and ORGANIC
            if (context.Request.QueryString["SID"] != null && context.Request.QueryString["SID"].Length > 0)
            {
                if (context.Request.QueryString["SID"] == "DISPLAY")
                    return "DISPLAY";
                else
                    return "SEM";
            }
            //if (context.Request.RawUrl.ToLower().Contains("sid="))
                //return context.Request.QueryString["SID"];
            return "DIRECT";
        }

        private static bool GetOrderFlag(HttpContext context)
        {
            if (context.Request.RawUrl.ToLower().Contains("postsale.aspx"))
                return true;
            return false;
        }
        public static void InsertSessionEntry(HttpContext context)
        {
            InsertSessionEntry(context, null, null, null, null, null,null);
        }

        public static void InsertSessionEntry(HttpContext context, bool mobileCallFlag, bool mobileEmailFlag)
        {
            InsertSessionEntry(context, null, null, mobileCallFlag, mobileEmailFlag, null,null);
        }

        public static void InsertSessionEntry(HttpContext context, bool orderFlag, decimal orderValue, int userId, int? orderId)
        {
            InsertSessionEntry(context, orderFlag, orderValue, null, null, userId,orderId);
        }
         
        public static void InsertSessionEntry(HttpContext context, bool? orderFlag, decimal? orderValue, bool? mobileCallFlag, bool? mobileEmailFlag, int? userId, int? orderId)
        {
            try
            {

                string sessionId = null;
                string ipAddress = null;
                string trafficSource = null;
                string deviceType = null;
                string url = null;
                string version = null;

                if (context != null)
                {
                    sessionId = GetSessionId(context);
                    ipAddress = GetIpAddress(context);
                    trafficSource = GetTrafficSource(context);
                    deviceType = GetDeviceType(context);
                    url = GetCleanUrl(context);
                    version = context.Request.Url.AbsolutePath.ToString().Replace("/store", "");
                    version = version.Substring(0, version.LastIndexOf('/'));
                    version = version.Substring(version.LastIndexOf('/') + 1, (version.Length - (version.LastIndexOf('/') + 1)));
                }
                string sql = "";
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["client_db"].ToString()))
                {
                    sql = "pr_set_userSession";

                    //Setup values
                    SqlParameter CustomerSessionIdParam = new SqlParameter("@CustomerSessionId", System.Data.SqlDbType.NVarChar);
                    SqlParameter OrderIdParam = new SqlParameter("@OrderId", System.Data.SqlDbType.Int);
                    SqlParameter URLParam = new SqlParameter("@URL", System.Data.SqlDbType.NVarChar);
                    SqlParameter IPAddressParam = new SqlParameter("@IPAddress", System.Data.SqlDbType.NVarChar);
                    SqlParameter VersionIdParam = new SqlParameter("@VersionId", System.Data.SqlDbType.NVarChar);
                    SqlParameter CreateDateParam = new SqlParameter("@CreateDate", System.Data.SqlDbType.DateTime);
                    SqlParameter UserIdParam = new SqlParameter("@UserId", System.Data.SqlDbType.Int);
                    SqlParameter TrafficSourceParam = new SqlParameter("@TrafficSource", System.Data.SqlDbType.NVarChar);
                    SqlParameter DeviceTypeParam = new SqlParameter("@DeviceType", System.Data.SqlDbType.NVarChar);
                    SqlParameter MobileCallFlagParam = new SqlParameter("@MobileCallFlag", System.Data.SqlDbType.Bit);
                    SqlParameter MobileEmailFlagParam = new SqlParameter("@MobileEmailFlag", System.Data.SqlDbType.Bit);
                    SqlParameter OrderFlagParam = new SqlParameter("@OrderFlag", System.Data.SqlDbType.Bit);
                    SqlParameter OrderValueParam = new SqlParameter("@OrderValue", System.Data.SqlDbType.Money);
                    SqlParameter UtmSourceParam = new SqlParameter("@UtmSource", System.Data.SqlDbType.NVarChar);
                    SqlParameter UtmCampaignParam = new SqlParameter("@UtmCampaign", System.Data.SqlDbType.NVarChar);
                    SqlParameter UtmMediumParam = new SqlParameter("@UtmMedium", System.Data.SqlDbType.NVarChar);
                    SqlParameter UtmContentParam = new SqlParameter("@UtmContent", System.Data.SqlDbType.NVarChar);

                    URLParam.Value = url;
                    OrderIdParam.Value = orderId;
                    IPAddressParam.Value = ipAddress;
                    VersionIdParam.Value = version;
                    CreateDateParam.Value = DateTime.Now;
                    UserIdParam.Value = userId;
                    TrafficSourceParam.Value = trafficSource;
                    DeviceTypeParam.Value = deviceType;
                    MobileCallFlagParam.Value = mobileCallFlag;
                    MobileEmailFlagParam.Value = mobileEmailFlag;
                    OrderFlagParam.Value = orderFlag;
                    OrderValueParam.Value = orderValue;
                    CustomerSessionIdParam.Value = sessionId;
                    UtmSourceParam.Value = GetQueryValue(context, "utm_source");
                    UtmCampaignParam.Value = GetQueryValue(context, "utm_campaign");
                    UtmMediumParam.Value = GetQueryValue(context, "utm_medium");
                    UtmContentParam.Value = GetQueryValue(context, "utm_content");


                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(OrderIdParam);
                    cmd.Parameters.Add(URLParam);
                    cmd.Parameters.Add(IPAddressParam);
                    cmd.Parameters.Add(VersionIdParam);
                    cmd.Parameters.Add(CreateDateParam);
                    cmd.Parameters.Add(UserIdParam);
                    cmd.Parameters.Add(TrafficSourceParam);
                    cmd.Parameters.Add(DeviceTypeParam);
                    cmd.Parameters.Add(MobileCallFlagParam);
                    cmd.Parameters.Add(MobileEmailFlagParam);
                    cmd.Parameters.Add(OrderFlagParam);
                    cmd.Parameters.Add(OrderValueParam);
                    cmd.Parameters.Add(CustomerSessionIdParam);
                    cmd.Parameters.Add(UtmSourceParam);
                    cmd.Parameters.Add(UtmCampaignParam);
                    cmd.Parameters.Add(UtmMediumParam);
                    cmd.Parameters.Add(UtmContentParam);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception)
            {
            }

        }
    }
}