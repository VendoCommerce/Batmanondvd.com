using System;
using System.Text;
using CSCore;
using CSWeb.App_Code;
namespace CSWeb.Root
{

    public partial class contact : CSWebBase.SiteBasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            NavigationControl.RouteTo("big1");


        }
    }

}