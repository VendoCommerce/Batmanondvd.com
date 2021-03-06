﻿using CSBusiness;
using CSBusiness.Preference;
using CSBusiness.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSWeb.PS
{
    public partial class index : CSBasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                base.Page_Load(sender, e);
                SitePreference sitePrefCache = CSFactory.GetCacheSitePref();

                if (Request.Headers["X-HTTPS"] != null)
                {
                    if (Request.Headers["X-HTTPS"].ToLower().Equals("no"))
                    {
                        if (Request.Url.ToString().Contains("www"))
                        {
                            Response.Redirect((Request.Url.ToString().Replace("http:/", "https:/").Replace("index.aspx", "")));
                        }
                        else
                        {
                            Response.Redirect((Request.Url.ToString().Replace("http:/", "https:/").Replace("https://", "https://www.").Replace("index.aspx", "")));
                        }
                    }
                }
                if (OrderHelper.IsMobileBrowser() && (Request.QueryString["mobile"] == null ||(Request.QueryString["mobile"] != null && Request.QueryString["mobile"] != "false")))
                    Response.Redirect("/mobile/?" + Request.QueryString.ToString());

            }

        }

        

    }
}