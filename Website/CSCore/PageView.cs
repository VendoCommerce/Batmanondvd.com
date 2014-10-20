using System;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Collections.Generic;


/// <summary>
/// Summary description for Customer
/// </summary>
/// 
public class PageView
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
            DailyCookieID.Domain = context.Request.Url.Host.ToString().Replace("www.","");
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

    public static void InsertPageEntry(HttpContext context)
    {
       
        string sessionId = GetSessionId(context); 
        string ipAddress = GetIpAddress(context);
        string version = context.Request.Url.AbsolutePath.ToString().Replace("/store", "");
        version = version.Substring(0, version.LastIndexOf('/'));
        version = version.Substring(version.LastIndexOf('/')+1 , (version.Length - (version.LastIndexOf('/')+1)));
        
        string sql = "";
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["client_db"].ToString()))
        {
            conn.Open();
            sql = "INSERT INTO [PageViews] ([CustomerSessionId],[IPAddress],[URL],[VersionId],[CreateDate])";
            sql = sql + " VALUES ('" + sessionId + "','" + ipAddress + "',@url,'" + version + "','" + DateTime.Now + "') ";
            //Put url into a param to avoid sql injection
            SqlParameter urlParam = new SqlParameter("@url", System.Data.SqlDbType.NVarChar);
            urlParam.Value = context.Request.Url.ToString();

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(urlParam);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}


