﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSWeb.Admin
{
    public partial class _400 : CSWebBase.SiteBasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            //Exception ex = Server.GetLastError();
            //if (ex != null && ex.InnerException != null)
            //{
            //    CSCore.CSLogger.Instance.LogException(ex.InnerException.Message, ex.InnerException);
            //}
            //else
            //{
            //    CSCore.CSLogger.Instance.LogException(Request.Url.ToString(), null);
            //}

        }
    }
}