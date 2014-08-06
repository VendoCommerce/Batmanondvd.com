using System;
using System.Web.UI;
using CSBusiness.OrderManagement;
using CSBusiness.Resolver;
using CSBusiness;
using System.Web.UI.WebControls;

namespace CSWeb.Admin
{
    public partial class CustomerOrderDetail : BasePage
    {
        public string firstName, lastName, email;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.BaseLoad();
            if (!Page.IsPostBack)
            {
                if (Request.Params["f"] != null)
                    firstName = Server.UrlDecode(Request.Params["f"].ToString());

                if (Request.Params["l"] != null)
                    lastName =Server.UrlDecode(Request.Params["l"].ToString());

                if (Request.Params["l"] != null)
                    email = Server.UrlDecode(Request.Params["e"].ToString());

                BindOrders(firstName, lastName, email);
               
            }

        }

        protected void BindOrders(string firstName, string lastName, string email)
        {
            dlordersList.DataSource = CSResolve.Resolve<ICustomerService>().GetAllCustomerOrdersDetail(firstName, lastName, email);
            dlordersList.DataBind();
        }


        protected void btnAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {

            switch (e.CommandName)
            {
               
                case "Back":
                    Response.Redirect("CustomerOrders.aspx");
                    break;

            }

        }

    }
}