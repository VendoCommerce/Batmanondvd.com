using System;
using System.Web;

namespace CSWeb.Admin
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
            for (int i = 0; i < Request.Cookies.Count; i++)
            {
                HttpCookie cookie = Request.Cookies[i];
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Set(cookie);
            }

            Response.Redirect("Login.aspx");
        }
    }
}