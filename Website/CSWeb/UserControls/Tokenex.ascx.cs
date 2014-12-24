using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSBusiness.Preference;
using CSWeb.Tokenization;
using System.Configuration;

namespace CSWeb.UserControls
{
    public partial class Tokenex : System.Web.UI.UserControl
    {

        public string TokenExJSFile
        {
            get
            {
                return ConfigurationManager.AppSettings["TokenExJavaUrl"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            TxEncryptionKey.Value = ConfigurationManager.AppSettings["TokenExPublicKey"];
            hlTokenExAPIUrl.Value = ConfigurationManager.AppSettings["TokenExAPIUrl"];
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
                //return TokenexProcessor.GetInstance().Tokenize(EncryptedCcNum);
                return hlToken.Value;
            }
        }

        public string GetCCNumToken()
        {
            return TokenexProcessor.GetInstance().Tokenize(EncryptedCcNum);
        }
    }
}