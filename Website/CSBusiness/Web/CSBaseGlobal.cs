using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSBusiness.Resolver;
using System.Configuration;
using System.Web;
using System.Web.Configuration;

namespace CSBusiness.Web
{
    public class CSBaseGlobal : System.Web.HttpApplication
    {
        public virtual void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            CSResolve.InitializeWith(new DependencyResolverFactory());
            int visitorscount = 0;
            Application["visitorscount"] = visitorscount;
        }

        public virtual void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        public virtual void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex.InnerException != null)
            {
                //Exception baseEx = Server.GetLastError().GetBaseException();
                //CSCore.CSLogger.Instance.LogException(baseEx.Message, baseEx);
                CSCore.CSLogger.Instance.LogException(ex.InnerException.Message, ex.InnerException);
            }

            HttpException httpEx = ex as HttpException;

            // show "redirect" message is file does not exist.
            if (httpEx != null)
            {
                if (httpEx.GetHttpCode() == 404)
                {
                    Configuration configuration = WebConfigurationManager.OpenWebConfiguration("/");
                    CustomErrorsSection customErrors = (CustomErrorsSection)configuration.GetSection("system.web/customErrors");

                    if (string.IsNullOrEmpty(customErrors.Errors["404"].Redirect)) // ... no redirect page specified in code
                    {

                    }
                }
            }
        }

        public virtual void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            if (ConfigurationManager.AppSettings["SessionTTL"] != null)
                Session.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["SessionTTL"]);
        }

        public virtual void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }
    }
}
