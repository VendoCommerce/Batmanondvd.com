using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSBusiness.Web
{
    public class CSBaseUserControl : System.Web.UI.UserControl
    {
        public ClientCartContext ClientOrderData
        {
            get
            {
                CSBasePage page = System.Web.HttpContext.Current.CurrentHandler as CSBasePage;

                if (page != null)
                    return page.ClientOrderData;

                return null;
            }
            set
            {
                CSBasePage page = System.Web.HttpContext.Current.CurrentHandler as CSBasePage;

                if (page != null)
                    page.ClientOrderData = value;
                else
                    throw new Exception("Page is not CSBasePage.");
            }
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
