﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSWeb.COMPLETE.Store
{
	public partial class CheckoutExpired : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            Response.Redirect("CheckoutSessionExpired.aspx");
		}
	}
}