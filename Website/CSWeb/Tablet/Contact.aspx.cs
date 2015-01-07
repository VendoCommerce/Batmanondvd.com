using System;
using System.Text;
using CSCore;
using CSWeb.App_Code;
namespace CSWeb.Tablet
{

    public partial class contact : CSWebBase.SiteBasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            NavigationControl.RouteTo("tablet_big3");


        }
    }

}