using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using CSCore.DataHelper;
using CSCore.Utils;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;
using CSBusiness.Preference;
using CSBusiness.Attributes;
using System.Threading;
using System.Globalization;
using System.Web;
using CSData;

namespace CSBusiness.Web.Admin
{
    public class AdminBasePage : System.Web.UI.Page
    {
        public int userTypeId = 0;
        public virtual int SiteId
        {
            get
            {
                SitePreference sitePreference = CSFactory.GetCacheSitePref();

                if (sitePreference.AttributeValues.ContainsAttribute("SiteId"))
                {
                    int siteId = 0;

                    if (int.TryParse(sitePreference.GetAttributeValue("SiteId"), out siteId))
                    {
                        return siteId;
                    }
                }

                throw new Exception("SiteId not set or could not be determined.");
            }
        }

        protected virtual string TnTApiUrl
        {
            get
            {
                return ConfigHelper.ReadAppSetting("TnTApiUrl");
            }
        }

        protected virtual string TnTApiQuery_CampaignView
        {
            get
            {
                return ConfigHelper.ReadAppSetting("TnTApiQuery_CampaignView");
            }
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            BindSettings();

            if (!Page.IsPostBack)
            {
                BaseLoad();
            }
        }

        protected void BaseLoad()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            if (Request.Cookies["CSVal"] == null)
                Response.Redirect("/cssecuresite/login.aspx?targeturl=" + Request.RawUrl);
            else
            {
                HttpCookie cookie = Request.Cookies["CSVal"];
                userTypeId = Convert.ToInt32(cookie.Value);
            }
        }

        public virtual void BindSettings()
        {
            if (!Page.IsPostBack)
            {
                // use the following like to load site information to admin page
                //SitePref PrefObject = CSFactory.GetSitePreference();                
            }
        }

        protected virtual string GetTnTApiUrl(string url, string query, params string[] queryFields)
        {
            // ex url: https://testandtarget.omniture.com/api?client=conversionsystems{0}email=kevin@conversionsystems.com{0}password=Friday12
            // ex query: operation=viewCampaign{0}campaignId={1}{0}version=2
            
            StringBuilder sbFull = new StringBuilder(url.Contains("{0}") ? 
                string.Format(url, "&").TrimEnd('&', '?') : 
                url.TrimEnd('&', '?')); // get host/base with "&". drop trailing "&".

            StringBuilder sbQuery = new StringBuilder(query);
            
            if (query.Contains("{0}"))
                    sbQuery = sbQuery.Replace("{0}", "&");

            for (int i = 0; i < queryFields.Length; i++)
            {
                sbQuery = sbQuery.Replace("{" + (i + 1).ToString() + "}", queryFields[i].ToString());
            }

            return sbFull.Append(!sbFull.ToString().Contains("?") ? "?" : "&").Append(sbQuery).ToString(); // add "?" if needed
        }

        protected virtual string GetTnTApiUrl(Analytics.TestAndTargetApiMethod tntApiMethod, params string[] queryFields)
        {
            string url = TnTApiUrl;

            switch (tntApiMethod)
            {
                case Analytics.TestAndTargetApiMethod.CampaignView:
                    return GetTnTApiUrl(url, TnTApiQuery_CampaignView, queryFields);                
            }

            throw new Exception("Case for supplied TestAndTargetApiMethod not defined.");
        }

        protected virtual Dictionary<int, string> GetTnTExperienceNames(int campaignId, out string campaignName)
        {
            #region Sample XML
            /*
<?xml version="1.0" encoding="UTF-8"?>
<campaign><id>53043</id><name>Test campaign(ttsandbox)</name><state>approved</state><enabled>true</enabled><startDate>1970-01-01T00:00:00Z</startDate><endDate>2100-01-01T10:00:00Z</endDate><displayLocations><location><targetExpression><targetMbox><name>HeadLine</name></targetMbox></targetExpression></location><location><targetExpression><targetMbox><name>OfferDetails</name></targetMbox></targetExpression></location></displayLocations><branches><branch><id>0</id><name>Experience  A</name><targetExpression><targetPercent><percent>25</percent></targetPercent></targetExpression><offers><displayLocation location="/campaign/displayLocations/location[1]"><offerManaged><id>91985</id><name>Headline Test</name></offerManaged></displayLocation><displayLocation location="/campaign/displayLocations/location[2]"><offerManaged><id>91988</id><name>Offer Details 1</name></offerManaged></displayLocation></offers></branch><branch><id>2</id><name>Experience  B :-/</name><targetExpression><targetPercent><percent>25</percent></targetPercent></targetExpression><offers><displayLocation location="/campaign/displayLocations/location[1]"><offerManaged><id>91986</id><name>Headline Test 2</name></offerManaged></displayLocation><displayLocation location="/campaign/displayLocations/location[2]"><offerManaged><id>91989</id><name>Offer Details 2</name></offerManaged></displayLocation></offers></branch><branch><id>3</id><name>Experience  C</name><targetExpression><targetPercent><percent>25</percent></targetPercent></targetExpression><offers><displayLocation location="/campaign/displayLocations/location[1]"><offerManaged><id>91987</id><name>Headline Test 3</name></offerManaged></displayLocation><displayLocation location="/campaign/displayLocations/location[2]"><offerManaged><id>91990</id><name>Offer Terms 3</name></offerManaged></displayLocation></offers></branch><branch><id>4</id><name>Experience  D</name><targetExpression><targetPercent><percent>25</percent></targetPercent></targetExpression><offers><displayLocation location="/campaign/displayLocations/location[1]"><offerManaged><id>92392</id><name>Test Redirect Offer</name></offerManaged></displayLocation><displayLocation location="/campaign/displayLocations/location[2]"><offerManaged><id>92392</id><name>Test Redirect Offer</name></offerManaged></displayLocation></offers></branch></branches><segments/></campaign>                          
             */
            #endregion

            string url = GetTnTApiUrl(Analytics.TestAndTargetApiMethod.CampaignView, Convert.ToString(campaignId));

            string response = CommonHelper.HttpPost(url, string.Empty);

            if (string.IsNullOrEmpty(response))
            {
                campaignName = string.Empty;
                return new Dictionary<int, string>();
            }

            XElement root = XElement.Load(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(response)));

            campaignName = root.XPathSelectElement("name").Value;

            IEnumerable<XElement> branches = root.XPathSelectElements("branches/branch");

            Dictionary<int, string> experiences = new Dictionary<int, string>();
            foreach (XElement branch in branches)
            {
                experiences.Add(Convert.ToInt32(branch.XPathSelectElement("id").Value), branch.XPathSelectElement("name").Value);
            }

            return experiences;
        }
    }
}
