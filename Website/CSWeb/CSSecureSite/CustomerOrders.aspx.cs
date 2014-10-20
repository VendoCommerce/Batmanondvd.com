using System;
using System.Web.UI;
using CSBusiness.OrderManagement;
using CSBusiness.Resolver;
using CSBusiness;
using System.Web.UI.WebControls;
using CSBusiness.CustomerManagement;
using System.Web;
using CSWeb.Admin.UserControls;
using CSCore.DataHelper;

namespace CSWeb.Admin
{
    public partial class CustomerOrders : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.BaseLoad();
            if(!Page.IsPostBack)
            {

            }

        }

        protected void lblOrder_Search(object sender, EventArgs e)
        {
            BindOrders(1, true);
        }

        protected void BindOrders(int pageNum, bool refresh)
        {
            int startRec, endRec, totalCount=0;

            startRec = ((pageNum - 1) * ConfigHelper.PAGE_SIZE) + 1;
            endRec = (pageNum) * ConfigHelper.PAGE_SIZE;

            dlordersList.DataSource = CSResolve.Resolve<ICustomerService>().GetAllCustomerOrders(txtFirstName.Text, txtLastName.Text, txtEmail.Text, startRec, endRec, out totalCount);
            dlordersList.DataBind();

            if (refresh)
            {
                pg.SetUp(totalCount, ConfigHelper.PAGE_SIZE);
                updPg.Update();
            }
        }

        public void OnPaging(Object s, PageChangeArguments args)
        {
            BindOrders(args.PageNum, false);

        }


        protected void dlordersList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            Customer custItem = e.Item.DataItem as Customer;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink hlView = e.Item.FindControl("hlView") as HyperLink;

                hlView.NavigateUrl = "CustomerOrderDetail.aspx?f=" + Server.UrlEncode(custItem.FirstName) + "&l=" + Server.UrlEncode(custItem.LastName) +
                    "&e=" + Server.UrlEncode(custItem.Email);
            }
        }
    }
}