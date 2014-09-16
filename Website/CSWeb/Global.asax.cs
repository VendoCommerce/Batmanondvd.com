using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using CSBusiness.Resolver;
using System.Configuration;



namespace CSWeb
{
    public class Global : CSBusiness.Web.CSBaseGlobal
    {

        public override void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex.InnerException != null)
            {
                CSCore.CSLogger.Instance.LogException(ex.InnerException.Message, ex.InnerException);
            }
        }
    }
}
