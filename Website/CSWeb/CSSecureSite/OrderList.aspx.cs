using System;
using System.Web.UI;
using CSBusiness.OrderManagement;
using CSBusiness.Resolver;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using CSCore.DataHelper;
using CSWeb.Admin.UserControls;
using InfoSoftGlobal;
using CSCore;
using System.Drawing;

namespace CSWeb.Admin
{
    public partial class OrderList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.BaseLoad();
            if(!Page.IsPostBack)
            {

            }

        }

        public void CbInCludeFullAmount_CheckedChanged(Object s, EventArgs e)
        {
            if(CbInCludeFullAmount.Checked)
                lblHeader.Text = "FullAmount SubTotal";
            else
                    lblHeader.Text = "Sub Total";

            int pageNum=1;
            if (ViewState["PageNo"] != null)
                pageNum = (int)ViewState["PageNo"];
            BindOrders(pageNum, false, true);
            
           
        }

        protected void lblOrder_Search(object sender, EventArgs e)
        {

            BindOrders(1, true, false);
        }

        protected void BindOrders(int pageNum, bool refresh, bool Cachedata)
        {
            int startRec, endRec, totalCount=0;

            startRec = ((pageNum - 1) * ConfigHelper.PAGE_SIZE) + 1;
            endRec = (pageNum) * ConfigHelper.PAGE_SIZE;

            if (! Cachedata)
            {
                List<Order> OrdersList = CSResolve.Resolve<IOrderService>().GetAllOrders(rangeDateControlCriteria.StartDateValueLocal, 
                    DateTimeUtil.GetEndDate(rangeDateControlCriteria.EndDateValueLocal), cbArchive.Checked, startRec, endRec, 
                    txtFirstName.Text, txtLastName.Text, txtEmail.Text,
                    out totalCount);
                dlordersList.DataSource = OrdersList;
                ViewState["orderList"] = OrdersList;
            }
            else
                dlordersList.DataSource = (List<Order>)ViewState["orderList"];
            dlordersList.DataBind();
            updList.Update();

            if (refresh)
            {
                pg.SetUp(totalCount, ConfigHelper.PAGE_SIZE);
                updPg.Update();
            }
  
           

        }

        public void OnPaging(Object s, PageChangeArguments args)
        {
            BindOrders(args.PageNum, false, false);
            ViewState["PageNo"] = args.PageNum;
        }

        protected void dlOrderList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Order orderItem = e.Item.DataItem as Order;
                Label lblTotal = e.Item.FindControl("lblTotal") as Label;
                Label lblSubTotal = e.Item.FindControl("lblSubTotal") as Label;
                //HyperLink hlDetail = e.Item.FindControl("hlDetail") as HyperLink;

                //hlDetail.NavigateUrl = "OrderDetail.aspx?sId=1&oId=" + orderItem.OrderId;
                if (CbInCludeFullAmount.Checked)
                {
                    lblSubTotal.Text = String.Format(" ${0:0.##}", orderItem.FullPriceSubTotal);
                    lblTotal.Text = String.Format(" ${0:0.##}", (orderItem.FullPriceSubTotal + orderItem.Tax + orderItem.ShippingCost));
                }
                else
                {
                    lblSubTotal.Text = String.Format(" ${0:0.##}", orderItem.SubTotal);
                    lblTotal.Text = String.Format(" ${0:0.##}", orderItem.Total);
                }
    
            }
                    
                
        }

        protected void dlOrderList_ItemCommand(object sender, DataListCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "Delete":

                    CSResolve.Resolve<IOrderService>().RemoveOrder(Convert.ToInt32(e.CommandArgument));
                    break;
                case "LoadOrder":
                    Session["oId"] =(string) e.CommandArgument;
                    Response.Redirect("OrderDetail.aspx?sId=1");
                    break;
            }
            BindOrders(1, true, false);
        }
    }
} 