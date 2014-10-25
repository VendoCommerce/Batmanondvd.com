using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSWeb.Mobile
{
    public partial class _500 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           ClearCart();
        }


        private void ClearCart()
        {
            //reset entire Context object
            //Session["ClientOrderData"] = null;
            Session.Clear();
        }
    }
}