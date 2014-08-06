using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CSBusiness.Web;
using CSBusiness;
using CSBusiness.Resolver;
using System.Configuration;
using CSData;
using System.Data.SqlClient;
using CSCore.DataHelper;
using CSBusiness.Preference;
using CSBusiness.Attributes;
using System.Xml.Linq;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace CSWebBase
{
    public class AbTestingVersionUpdate 
    {
        public ClientCartContext CartContext
        {
            get
            {
                return HttpContext.Current.Session["ClientOrderData"] != null ? HttpContext.Current.Session["ClientOrderData"] as ClientCartContext : null;
            }
            set { HttpContext.Current.Session["ClientOrderData"] = value; }
        }
        public void UpdateVersionNameWhileAbTesting()
        {
            if (HttpContext.Current.Request.Cookies["csexperience"] != null)
            {
                if (CartContext != null && CartContext.OrderAttributeValues != null)
                {
                    if (!CartContext.OrderAttributeValues.ContainsKey("parentversionid"))
                    {
                        CartContext.OrderAttributeValues.Add("parentversionid", new CSBusiness.Attributes.AttributeValue(CartContext.VersionId.ToString()));
                        UpdateVersionId();
                    }
                    else if (CartContext.OrderAttributeValues["parentversionid"].Value.Equals(CartContext.VersionId.ToString()))
                    {
                        UpdateVersionId();
                    }
                }
            }
        }
        public void UpdateVersionId()
        {
            if (HttpContext.Current.Request.Cookies["csexperience"] != null)
            {
                string abTestingVersionName;

                abTestingVersionName = HttpContext.Current.Request.Cookies["csexperience"].Value;

                if (abTestingVersionName != "")
                {
                    List<CSBusiness.Version> list = (CSFactory.GetCacheSitePref()).VersionItems;
                    CSBusiness.Version item = list.Find(x => x.Title.ToLower() == abTestingVersionName.ToLower());
                    if (item != null)
                        CartContext.VersionId = item.VersionId;
                    HttpContext.Current.Session["ClientOrderData"] = CartContext;
                }
            }
        }
        public void LoadScripts(Page page)
        {
            //Adding scripts to header
            if (CartContext != null)
            {
                SitePreference sitePref = CSFactory.GetCacheSitePref();
                if (!sitePref.AttributeValuesLoaded)
                    sitePref.LoadAttributeValues();
                if (sitePref.AttributeValues != null)
                {
                    if (sitePref.ContainsAttribute("scripts") != null)
                    {
                        Literal li = new Literal();
                        li.Text = sitePref.AttributeValues["scripts"].Value;
                        if (page.Header != null)
                            page.Header.Controls.Add(li);
                    }
                }
            }
        }
    }

}
