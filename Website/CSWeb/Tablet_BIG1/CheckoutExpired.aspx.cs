﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSWeb.Tablet_BIG1.Store
{
	public partial class CheckoutExpired : CSWebBase.SiteBasePage
	{
        protected override void Page_Load(object sender, EventArgs e)
		{
            Response.Redirect("CheckoutSessionExpired.aspx");
		}
	}
}