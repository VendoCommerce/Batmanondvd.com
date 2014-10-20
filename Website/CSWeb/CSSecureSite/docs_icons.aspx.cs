using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
using System.Web;

namespace CSWeb.Admin
{
    public partial class docs_icons : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BaseLoad();
            }
        }

        
    }
}