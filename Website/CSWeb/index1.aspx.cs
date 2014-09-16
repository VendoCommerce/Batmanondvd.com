using System;
using System.Text;
using CSCore;
using CSCore.Utils;
using CSBusiness.Preference;
using CSBusiness;
using System.Web;
using CSBusiness.Web;

namespace CSWeb.Root.Store
{
    public partial class index : CSBasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
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
                
            }
           
        }

        protected void lbSimple_Click(object sender, EventArgs e)
        {
            //string SKU =ddls ddlSingle.SelectedValue;
            //Response.Redirect("AddProduct.aspx?PId=" + SKU + "&CId=2");

        }
    }
}