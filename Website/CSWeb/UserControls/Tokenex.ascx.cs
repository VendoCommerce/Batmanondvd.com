using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSBusiness.Preference;
using CSWeb.Tokenization;

namespace CSWeb.UserControls
{
    public partial class Tokenex : System.Web.UI.UserControl
    {
        public string TokenXURL = "";
        public string TokenXEncryptionKey = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SitePreference sitePreference = CSFactory.GetCacheSitePref();
                sitePreference.LoadAttributeValues();
                if (sitePreference.AttributeValues["tokenxurl"] != null)
                    TokenXURL = sitePreference.AttributeValues["tokenxurl"].Value;
                if (sitePreference.AttributeValues["tokenxencryptionkey"] != null)
                    TokenXEncryptionKey = sitePreference.AttributeValues["tokenxencryptionkey"].Value;

            }
        }

        public string EncryptedCcNum
        {
            get
            {
                return hlEncryptedCCNum.Value;
            }
        }

        public string ReceivedToken
        {
            get
            {
                return hlToken.Value;
            }
        }

        public string GetCCNumToken()
        {
           return TokenexProcessor.GetInstance().Tokenize(EncryptedCcNum);
        }
    }
}