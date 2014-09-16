using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CSCore;
using System.Web.Script.Serialization;
using CSCore.Utils;

namespace CSBusiness.Web
{
    public class AjaxTotalsHelper
    {
        public const int tokenLifeHours = 24;

        public static AjaxTotalsToken GetAuthenticationToken(HttpContext httpContext)
        {
            AjaxTotalsToken token = new AjaxTotalsToken() { 
                expireDateTime = DateTime.Now.AddHours(tokenLifeHours).ToString(),
                userIP = httpContext.Request.UserHostAddress, 
                random = new Random().Next() 
            };

            return token;
        }

        public static string EncryptToken(AjaxTotalsToken token)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return CommonHelper.Encrypt(serializer.Serialize(token));
        }

        public static AjaxTotalsToken DecryptToken(string token)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            
            try
            {
                return serializer.Deserialize<AjaxTotalsToken>(CommonHelper.Decrypt(token));
            }
            catch
            {
            }

            return null;
        }
    }
}
