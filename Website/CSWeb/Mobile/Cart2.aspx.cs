using System;
using System.Text;
using CSCore;
using CSCore.Utils;
using CSBusiness.Preference;
using CSBusiness;
using System.Web;

namespace CSWeb.Mobile.Store
{
    public partial class cart2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
           
        }

        public string GetCleanPhoneNumber(string data)
        {
            return OrderHelper.GetCleanPhoneNumber(data);
        }



        public string GetDynamicVersionData(string data)
        {
            return OrderHelper.GetDynamicVersionData(data);
        }
    }
}