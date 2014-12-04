using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSWeb.BIG1.Store
{
	public partial class CheckoutSessionExpired : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            Session.Clear();
		}
	}
}