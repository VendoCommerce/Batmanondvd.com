using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using CSBusiness.OrderManagement;
using CSCore;
using CSBusiness;

namespace CSWeb.Admin
{
    public partial class TransactionReport : BasePage
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Security setting turned off
                //this.BaseLoad();
                liHeader.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Day.ToString() + ", " + DateTime.Now.Year.ToString();
                liSubHeader.Text = DateTime.Now.DayOfWeek + " " + DateTime.Now.AddHours(3).ToShortTimeString() + " (EST)";


                if (Session["FilterFromDate"] != null && Session["FilterToDate"] != null)
                {
                    rangeDateControlCriteria.StartDateValueLocal = Convert.ToDateTime(Session["FilterFromDate"]);
                    rangeDateControlCriteria.EndDateValueLocal = Convert.ToDateTime(Session["FilterToDate"]);
                }
                else
                {

                    rangeDateControlCriteria.StartDateValueLocal = DateTime.Now.Date;
                    rangeDateControlCriteria.EndDateValueLocal = DateTime.Now.Date;

                }

                BindData(rangeDateControlCriteria.StartDateValueLocal, rangeDateControlCriteria.EndDateValueLocal);
            }
        }

        protected void BindData(DateTime? startDate, DateTime? endDate)
        {

            DateTime? timezoneStartDate = DateTimeUtil.GetEastCoastStartDate(rangeDateControlCriteria.StartDateValueLocal);
            DateTime? timezoneEndDate = DateTimeUtil.GetEastCoastDate(rangeDateControlCriteria.EndDateValueLocal);

            List<ReportFields> OrdersList = new OrderManager().GetOrderTransaction(timezoneStartDate, timezoneEndDate, 1, true);
            dlTransactionList.DataSource = OrdersList;
            dlTransactionList.DataBind();


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Session["FilterFromDate"] = rangeDateControlCriteria.StartDateValueLocal.Value.ToShortDateString();
            Session["FilterToDate"] = rangeDateControlCriteria.EndDateValueLocal.Value.ToShortDateString();

            BindData(rangeDateControlCriteria.StartDateValueLocal, rangeDateControlCriteria.EndDateValueLocal);

        }

        protected void dlTransactionList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            ReportFields item = e.Item.DataItem as ReportFields;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lblOrderNo = e.Item.FindControl("lblOrderNo") as Label;
                Label lblOrderStatus = e.Item.FindControl("lblOrderStatus") as Label;
                Label lblAmount = e.Item.FindControl("lblAmount") as Label;
                Label lblAuthCode = e.Item.FindControl("lblAuthCode") as Label;
                Label lblTransaction = e.Item.FindControl("lblTransaction") as Label;
                Label lblTransactionDate = e.Item.FindControl("lblTransactionDate") as Label;
                Label lblAffiliate = e.Item.FindControl("lblAffiliate") as Label;
                


                lblOrderNo.Text = item.orderId.ToString();
                lblOrderStatus.Text = item.OrderStatus;
                lblAmount.Text = String.Format("{0:C}", item.TotalRevenue);
                if(item.AuthorizationCode !=null)
                    lblAuthCode.Text = item.AuthorizationCode;
                else
                     lblAuthCode.Text = String.Empty;

                if (item.TransactionCode != null)
                    lblTransaction.Text = item.TransactionCode;
                else
                    lblTransaction.Text = String.Empty;

                if (item.TransactionDate.Length>0)
                    lblTransactionDate.Text = Convert.ToDateTime(item.TransactionDate).AddHours(3).ToString();
                else
                    lblTransactionDate.Text = String.Empty;

            }

            


        }
    }

}