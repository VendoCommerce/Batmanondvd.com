using System.Web;
using CSCore.Utils;
using System;

namespace CSWeb
{
    public class SiteHelper
    {
        public static void  ProcessSiteUrl()
        {
            int i = 0;
            String value = String.Empty, referrer = String.Empty;

            string sidValue = CommonHelper.GetCookieString("sid", false);
            if (sidValue != String.Empty)
            {
                TimeSpan ts = new TimeSpan(720, 0, 0);
                CommonHelper.SetCookie("sid", sidValue, ts);
            }

            string ua = HttpContext.Current.Request.UserAgent.ToLower();
            if (ua != null && (ua.Contains("iPhone") || ua.Contains("ipad") || ua.Contains("blackberry") || ua.Contains("android")))
            {
                HttpContext.Current.Response.Redirect("/mobile");
                HttpContext.Current.Response.End();
            }



            if (HttpContext.Current.Request.ServerVariables["HTTP_HOST"] != null)
            {
                referrer = HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
            }
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                referrer = HttpContext.Current.Request.UrlReferrer.ToString();

            }

              i = Convert.ToInt32(HttpContext.Current.Application["visitorscount"].ToString());
              i++;
              HttpContext.Current.Application["visitorscount"] = i;

              string cookieValue  = CommonHelper.GetCookieString("ZQuietSite", false);
              if (cookieValue == string.Empty)
              {

                  if (i % 3 == 0)
                  {
                      cookieValue = "A";
                  }
                  else if (i % 3 == 1)
                  {
                      cookieValue = "B";
                  }
                  else if (i % 3 == 2)
                  {
                      cookieValue = "C";
                  }

                  TimeSpan ts = new TimeSpan(72, 0, 0);
                  CommonHelper.SetCookie("ZQuietSite", cookieValue, ts);
              }

              HttpContext.Current.Response.Redirect("default.aspx?" + HttpContext.Current.Request.QueryString + "&referrer=" + referrer);

              if (cookieValue == "A")
              {
                  HttpContext.Current.Response.Redirect("/CsStore/V2/?" + HttpContext.Current.Request.QueryString + "&referrer=" + referrer);
              }
              else if (cookieValue == "B")
              {
                  HttpContext.Current.Response.Redirect("/CsStore/V3?" + HttpContext.Current.Request.QueryString + "&referrer=" + referrer);
              }
              else if (cookieValue == "C")
              {
                  HttpContext.Current.Response.Redirect("default.aspx?" + HttpContext.Current.Request.QueryString + "&referrer=" + referrer);
              }

        }
    }
}
