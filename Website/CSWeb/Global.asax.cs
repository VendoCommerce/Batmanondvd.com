using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using CSBusiness.Resolver;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Routing;



namespace CSWeb
{
    public class Global : CSBusiness.Web.CSBaseGlobal
    {
        public override void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            base.Application_Start(sender,e);
        }

        //public void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    string uri = Request.Url.AbsolutePath.Replace(".aspx", "");
        //    if (!Request.Url.AbsolutePath.Contains("400.aspx") && Request.Url.AbsolutePath.Contains(".aspx"))
        //        Response.Redirect(uri, true);
        //}

        public override void Session_Start(object sender, EventArgs e)
        {
            UserSessions.InsertSessionEntry(Context);
        }

        public override void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex.InnerException != null)
            {
                CSCore.CSLogger.Instance.LogException(ex.InnerException.Message, ex.InnerException);
                //LogError(ex.InnerException.Message);
            }
            else if (ex != null)
            {
                CSCore.CSLogger.Instance.LogException(Request.Url.ToString(), null);
                //LogError(ex.Message);
            }

            Handle404Error(ex);
        }

        private void Handle404Error(Exception ex)
        {
            HttpException httpEx = ex as HttpException;
            if (httpEx != null && httpEx.GetHttpCode() == 404)
            {
                //Ignore if a file is requsted.
                Regex regex = new Regex(@".(txt|gif|pdf|doc|docx|jpg|pdf|js|png|mp4|html|htm|css|scss|less|eot|svg|ttf|woff|otf|xml)$");//aspx|asp|
                if (!regex.IsMatch(Request.Url.AbsoluteUri))
                    Response.Redirect("400.aspx");
            }
        }
        private void LogError(string message)
        {
            try
            {
                CSCore.EmailHelper.SendEmail("info@conversionsystems.com", "ravi@conversionsystems.com", "BatmanOnDvd.Com Error", message, false);
            }
            catch (Exception)
            {
            }
        }
    }

}
