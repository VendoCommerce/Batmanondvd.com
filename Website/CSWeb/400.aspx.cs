﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSWeb
{
    public partial class _400 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
            string url =ResolveUrl( Request.QueryString[0].Substring(4) + ".aspx");// ResolveUrl("~/MyProject/JavaScripts/dir/test.js");
            System.Uri uri = new Uri(url);
            //Page.ClientScript.RegisterClientScriptInclude("myfile", jSFile);
            if (System.IO.File.Exists(Server.MapPath(uri.AbsolutePath)))
                Server.Transfer(uri.AbsolutePath, true);
            //else

        }
    }
}