﻿using CSBusiness;
using CSBusiness.Preference;
using CSBusiness.Web;
using CSWeb.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSWeb
{
    public partial class index : CSWebBase.SiteBasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

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
                if (OrderHelper.IsMobileBrowser() && (Request.QueryString["mobile"] == null || (Request.QueryString["mobile"] != null && Request.QueryString["mobile"] != "false")))
                    Response.Redirect("/mobile/?" + Request.QueryString.ToString().Replace("SID=DISPLAY", "SID=DISPLAY_MOB"));

                if (CSBasePage.GetClientDeviceType() == CSBusiness.Enum.DeviceType.Tablet) // device mobile but version not mobile
                    Response.Redirect("/tablet_big3/" + ((Request.QueryString.ToString().Length > 0) ? "?" : "") + Request.QueryString.ToString().Replace("SID=DISPLAY", "SID=DISPLAY_TAB"));

                NavigationControl.RouteTo("big1");

                base.Page_Load(sender, e);
            }

        }
    }
}