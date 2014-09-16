using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace CSCore.TestAndTarget
{
    public static class TnTHelper
    {
        public const int tokenLifeHours = 24;

        public static string GetEncryptedAjaxToken(HttpContext httpContext, string operation, int versionId)
        {
            return EncryptToken(GetAjaxToken(httpContext, operation, versionId));
        }

        public static AjaxToken GetAjaxToken(HttpContext httpContext, string operation, int versionId)
        {
            return new AjaxToken()
            {
                versionId = versionId,
                operation = operation,
                expireDateTime = DateTime.Now.AddHours(tokenLifeHours).ToString(),
                userIP = httpContext.Request.UserHostAddress,
                random = new Random().Next()
            };
        }

        public static string EncryptToken(AjaxToken token)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return CSCore.Utils.CommonHelper.Encrypt(serializer.Serialize(token));
        }

        public static AjaxToken DecryptToken(string token)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                return serializer.Deserialize<AjaxToken>(CSCore.Utils.CommonHelper.Decrypt(token));
            }
            catch
            {
            }

            return null;
        }
    }
}
