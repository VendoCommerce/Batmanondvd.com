using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSWeb.Tokenization;

namespace CSWeb.UserControls
{
    public partial class Tokenex : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string EncryptedCCNum
        {
            get
            {
                return hlEncryptedCCNum.Value;
            }
        }

        public string GetCCNumToken()
        {
           return TokenexProcessor.GetInstance().Tokenize(EncryptedCCNum);
        }
    }
}