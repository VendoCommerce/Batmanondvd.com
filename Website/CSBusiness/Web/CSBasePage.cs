using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using CSCore.Utils;
using CSBusiness.Attributes;
using CSBusiness.Web.Scripts;
using CSBusiness.Preference;
using CSBusiness.Enum;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace CSBusiness.Web
{
    public class CSBasePage : System.Web.UI.Page
    {
        public enum TrackingField
        {
            TnTCampaignId,
            TnTExperienceId,
        }

        /// <summary>
        /// Set to return true if the page is a landing page, or false otherwise.
        /// </summary>
        protected virtual bool IsLandingPage
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// The referrer URL to the site.
        /// </summary>
        public string ReferrerURL
        {
            get
            {
                return Convert.ToString(Session["CSBasePage.ReferrerURL"] ?? string.Empty);
            }
            protected set
            {
                Session["CSBasePage.ReferrerURL"] = value;
            }
        }

        /// <summary>
        /// The landing page URL.
        /// </summary>
        public string LandingPageURL
        {
            get
            {
                return Convert.ToString(Session["CSBasePage.LandingPageURL"] ?? string.Empty);
            }
            protected set
            {
                Session["CSBasePage.LandingPageURL"] = value;
            }
        }

        /// <summary>
        /// The ClientCartContext of the site.
        /// </summary>
        public ClientCartContext ClientOrderData
        {
            get
            {
                return (ClientCartContext)Session["ClientOrderData"];
            }
            set
            {
                Session["ClientOrderData"] = value;
            }
        }

        protected virtual string TestAndTargetPostPage
        {
            get
            {
                return "/TnTPost.aspx";
            }            
        }

        protected virtual bool SkipCartInitialization
        {
            get
            {
                return false;
            }
        }

        public DeviceType ClientDeviceType
        {
            get
            {
                if (Session["CSBasePage.ClientDeviceType"] == null)
                    Session["CSBasePage.ClientDeviceType"] = CSBasePage.GetClientDeviceType();

                return (DeviceType)Session["CSBasePage.ClientDeviceType"];
            }
        }

        protected virtual bool DisableBrowserCache
        {
            get
            {
                return false;
            }
        }
        
        protected virtual void Page_Load(object sender, EventArgs e)
        {            
            if (!Page.IsPostBack)
            {
                // capture referrer url
                ReferrerURL = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : string.Empty;

                // captuire landing page url
                LandingPageURL = Request.Url.ToString();

                // set new client cart context
                if (!SkipCartInitialization)
                {
                    ClientOrderData = GetSiteInitializedCart(GetInitializedCart());                    
                }
            }

            // Read in TnT related vars. Keep outside of IsPostback block since we need to read in submit values.
            LoadTnTVars();

            WriteCSBaseJS();

            TrackPageView();

            // Write Test&Target related scripts
            DropTestAndTargetScripts();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            FixXsrfAttacks();
            //Page.PreInit += BasePage_Init;
            SetPageHeaders();
        }

        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void FixXsrfAttacks()
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }
        }

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected ClientCartContext GetInitializedCart()
        {
            ClientCartContext cartContext = null;
            if (Session["ClientOrderData"] != null)
            {
                cartContext = (ClientCartContext)Session["ClientOrderData"];
            }
            if (cartContext != null && cartContext.OrderId > 0)
            {
                // This means order is already placed and user re-visited the home page again. So we should reset the data 
                cartContext.ResetData();
                cartContext.OrderId = 0;
                SetCart(out cartContext);
            }
            else if (cartContext != null)
            {
                // This means that cart is set in root and redirected to some version. So if versionid is different then we should set the new landing page and new version.
                if (cartContext.VersionId != CSBasePage.GetVersion())
                {
                    if (cartContext.OrderAttributeValues != null && cartContext.OrderAttributeValues.ContainsKey("landing_url"))
                    {
                        cartContext.OrderAttributeValues.Remove("landing_url");
                        cartContext.OrderAttributeValues.Add("landing_url", new AttributeValue(LandingPageURL));
                    }
                    else
                    {
                        SetCart(out cartContext);
                    }
                    cartContext.VersionId = CSBasePage.GetVersion();
                }
            }
            else
            {
                SetCart(out cartContext);
            }

            Session["ClientOrderData"] = cartContext;
            return cartContext;
        }

        private void TrackPageView()
        {
            try
            {
                SitePreference sitePreference = CSFactory.GetCacheSitePref();
                sitePreference.LoadAttributeValues();
                if (sitePreference.AttributeValues["pageviewinsert"].Value.ToString().Equals("1"))
                {
                    PageView.InsertPageEntry(HttpContext.Current);
                }
            }
            catch
            {
            }
        }

        private void SetCart(out ClientCartContext cartContext)
        {
            cartContext = new ClientCartContext();
            cartContext.RequestParam = CommonHelper.GetQueryString(Request.RawUrl);
            cartContext.IpAddress = CommonHelper.IpAddress(HttpContext.Current);
            cartContext.OrderAttributeValues = new System.Collections.Generic.Dictionary<string, CSBusiness.Attributes.AttributeValue>();
            cartContext.OrderAttributeValues.Add("ref_url", new AttributeValue(ReferrerURL));
            cartContext.OrderAttributeValues.Add("landing_url", new AttributeValue(LandingPageURL));
            cartContext.OrderAttributeValues.Add("servername", new AttributeValue(ServerName));
            cartContext.VersionId = CSBasePage.GetVersion();

            if (cartContext.CartInfo == null)
            {
                cartContext.CartInfo = new CSBusiness.ShoppingManagement.Cart();
                cartContext.CartInfo.ShippingAddress = new CSBusiness.CustomerManagement.Address();
            }

        }

        /// <summary>
        /// Override this method to make any custom initialzations needed by site. The cart object passed to this method has already been initialized by CSBasePage.
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        protected virtual ClientCartContext GetSiteInitializedCart(ClientCartContext cart)
        {
            return cart;
        }

        public static int GetVersion()
        {
            int versionId = 1;
            //string version = HttpContext.Current.Request.Url.AbsolutePath.ToLower();
            string version = VersionManager.OriginaUri.AbsolutePath.ToLower();
            version = version.Substring(0, version.LastIndexOf('/'));
            version = version.Substring(version.LastIndexOf('/') + 1, (version.Length - (version.LastIndexOf('/') + 1)));
            if (version == "")
            {
                version = "control";
            }

            List<CSBusiness.Version> list = (CSFactory.GetCacheSitePref()).VersionItems;
            CSBusiness.Version item = list.Find(x => x.Title.ToLower() == version);
            if (item != null)
                versionId = item.VersionId;

            return versionId;
        }

        public static string GetVersionName()
        {
            bool foundTnTVars = false;

            return GetVersionName(false, out foundTnTVars);
        }

        public static string GetVersionName(bool includeTnTVars)
        {
            bool foundTnTVars = false;

            return GetVersionName(includeTnTVars, out foundTnTVars);
        }

        public static string GetVersionName(bool includeTnTVars, out bool foundTnTVars)
        {
            foundTnTVars = false;

            string versionName = "";
            string version = VersionManager.OriginaUri.AbsolutePath.ToLower();
            //string version = HttpContext.Current.Request.Url.AbsolutePath.ToLower();
            version = version.Substring(0, version.LastIndexOf('/'));
            version = version.Substring(version.LastIndexOf('/') + 1, (version.Length - (version.LastIndexOf('/') + 1)));

            List<CSBusiness.Version> list = (CSFactory.GetCacheSitePref()).VersionItems;
            CSBusiness.Version item = list.Find(x => x.Title.ToLower() == version);
            if (item != null)
                versionName = item.Title;

            if (includeTnTVars)
            {
                // check cart context to see if it has T&T vars loaded, and if so, then return it as complete version.
                HttpContext httpContext = HttpContext.Current;
                if (httpContext != null && httpContext.CurrentHandler is CSBasePage)
                {
                    ClientCartContext cartContext = ((CSBasePage)httpContext.CurrentHandler).ClientOrderData;

                    if (cartContext != null && cartContext.OrderAttributeValues != null
                        && cartContext.OrderAttributeValues.ContainsAttribute("TnTCampaignId")
                        && cartContext.OrderAttributeValues.ContainsAttribute("TnTExperienceId"))
                    {
                        versionName = Analytics.GetCompleteVersionId(versionName,
                            Convert.ToInt32(cartContext.OrderAttributeValues.GetAttributeValue("TnTCampaignId").Value ?? "0"),
                            Convert.ToInt32(cartContext.OrderAttributeValues.GetAttributeValue("TnTExperienceId").Value ?? "0"));

                        foundTnTVars = true;
                    }
                }
            }

            return versionName;
        }

        private void WriteCSBaseJS()
        {
            bool foundTnTVars = false;
            StringBuilder sbCSScript = new StringBuilder();

            string completeVersionScript = Analytics.GetCompleteVersionIdJS(CSBasePage.GetVersionName(true, out foundTnTVars), foundTnTVars);

            // write base js
            sbCSScript.AppendLine(ScriptsResource.ConversionSystemsBase
                
                // T&T related values
                .Replace("<TNT_C_ID>", GetTrackingFieldPageId(TrackingField.TnTCampaignId))
                .Replace("<TNT_E_ID>", GetTrackingFieldPageId(TrackingField.TnTExperienceId))
                .Replace("<VERSION_COMBINE_FUNC>", completeVersionScript)

                // mobile/tablet related values
                .Replace("<IS_MOBILE>", ClientDeviceType == DeviceType.Mobile ? true.ToString().ToLower() : false.ToString().ToLower()) // mobile flag
                .Replace("<IS_TABLET>", ClientDeviceType == DeviceType.Tablet ? true.ToString().ToLower() : false.ToString().ToLower()) // tablet flag
                ); 


            ClientScript.RegisterClientScriptBlock(this.GetType(), "ConversionSystemsBase", sbCSScript.ToString(), true);
        }

        private void LoadTnTVars()
        {
            ClientCartContext cartContext = ClientOrderData;

            if (cartContext != null && !cartContext.TnTVarsLoaded) // load tnt vars once for session as campaign/experience will not change during.
            {
                int tntCId = 0;
                int tntEId = 0;

                if (int.TryParse(Request.Form[GetTrackingFieldPageId(TrackingField.TnTCampaignId)], out tntCId)
                    && int.TryParse(Request.Form[GetTrackingFieldPageId(TrackingField.TnTExperienceId)], out tntEId))
                {
                    cartContext.OrderAttributeValues.AddAttributeValue("TnTCampaignId", new AttributeValue(Convert.ToString(tntCId)));
                    cartContext.OrderAttributeValues.AddAttributeValue("TnTExperienceId", new AttributeValue(Convert.ToString(tntEId)));                    
                    cartContext.TnTVarsLoaded = true;

                    ClientOrderData = cartContext;
                }
            }
        }

        private void DropTestAndTargetScripts()
        {
            if (!string.IsNullOrEmpty(TestAndTargetPostPage))
            {
                StringBuilder sbCSScript = new StringBuilder();                
                sbCSScript.AppendLine(ScriptsResource.Prop_ProcessTnTVars
                    .Replace("<TNT_POST_URL>", TestAndTargetPostPage)
                    .Replace("<TOKEN>", CSCore.TestAndTarget.TnTHelper.GetEncryptedAjaxToken(HttpContext.Current, "savecampaign", ClientOrderData != null ? ClientOrderData.VersionId : 0)));

                ClientScript.RegisterClientScriptBlock(this.GetType(), "Prop_ProcessTnTVars", sbCSScript.ToString(), true);
            }
        }

        protected string GetTrackingFieldPageId(TrackingField trackingField)
        {
            switch (trackingField)
            {
                case TrackingField.TnTCampaignId:
                    return "_cstnt_cid";                    
                case TrackingField.TnTExperienceId:
                    return "_cstnt_eid";                    
            }

            throw new Exception("trackingField page id not defined.");
        }

        public static DeviceType GetClientDeviceType()
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.UserAgent != null)
            {
                string userAgent = HttpContext.Current.Request.UserAgent.ToLower();

                Regex mobileRegex = new Regex(@"ip(hone|od)|ipod|android(.{1,})mobile|blackberry|bb10(.+)mobile|windows (ce|phone)|opera mobi", 
                    RegexOptions.IgnoreCase);
                Regex tabletRegex = new Regex(@"ipad|tablet");

                if (mobileRegex.IsMatch(userAgent))
                    return DeviceType.Mobile;

                if (tabletRegex.IsMatch(userAgent))
                    return DeviceType.Tablet;
            }

            return DeviceType.Default;
        }

        private void SetPageHeaders()
        {
            if (DisableBrowserCache)
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
                Response.Cache.SetValidUntilExpires(true);
            }
        }

        #region NewMethods
        /// <summary>
        /// The Server Name.
        /// </summary>
        public string ServerName
        {
            get
            {
                return Convert.ToString(System.Environment.GetEnvironmentVariable("COMPUTERNAME") ?? string.Empty);
            }
        }

        #endregion
    }
}
