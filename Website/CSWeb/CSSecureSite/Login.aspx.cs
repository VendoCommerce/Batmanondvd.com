using System;
using System.Web.UI.WebControls;
using CSBusiness;
using CSBusiness.Resolver;
using CSCore.Utils;
using CSCore.DataHelper;
using CSData;
using CSBusiness.Web;
using System.Text.RegularExpressions;

namespace CSWeb.Account
{
    public class Login : CSBasePage
    {
        public System.Web.UI.WebControls.TextBox UserName, Password;
        protected Literal liErrorMessage;
        protected Image imgLogo;
        public static string siteName = string.Empty;
        public static string siteUrl = string.Empty;
        public static string siteTitle = string.Empty;
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                base.Page_Load(sender, e);

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

            }
            
            if (!Page.IsPostBack)
            {
                SitePref PrefObject = CSFactory.GetSitePreference();
                siteTitle = PrefObject.SiteHeader;
                siteName = PrefObject.SiteName;
                siteUrl = PrefObject.SiteUrl;
                imgLogo.ImageUrl = PrefObject.LogoPath;
            }
        }

        protected void btnAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            //After username and password validation
            if (Regex.IsMatch(UserName.Text, @"^[a-zA-Z0-9_]+$"))
            {
                liErrorMessage.Text = "Suspicious user name detected, try again!";
                return;
            }

            int Valcode = CSResolve.Resolve<ICustomerService>().Validate(CommonHelper.CheckSQLInjection(CommonHelper.EnsureNotNull(UserName.Text)), CommonHelper.EnsureNotNull(Password.Text));
            if (Valcode > 0)
            {
                int mins = Int32.Parse(ConfigHelper.ReadAppSetting("AdminCookieSetting"));
                TimeSpan ts = new TimeSpan(0, mins, 0);
                CommonHelper.SetCookie("CSVal", Valcode.ToString());
                switch (Valcode)
                {
                    case 2:
                        try
                        {
                            string redirectUrl = ResourceHelper.GetResoureValue("ClientAdminRedirect");
                            if (redirectUrl.Length > 2)
                            {
                                Response.Redirect(redirectUrl, false);
                            }
                            else
                            {
                                Response.Redirect("versionreport.aspx");
                            }
                        }
                        catch
                        {
                            Response.Redirect("versionreport.aspx");
                        }
                        
                        
                        break;

                    default:
                        if (Request["targeturl"] != null)
                        {
                            Response.Redirect(Request["targeturl"].ToString());
                        }
                        else
                            Response.Redirect("Main.aspx");
                        break;
                }

            }
            else
            {
                liErrorMessage.Text = "Your login attempt was not successful. Please try again.";

            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
    }
}
