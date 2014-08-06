using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using CSBusiness.OrderManagement;
using CSCore;
using CSBusiness;
using System.Web.UI;
using System.Data.SqlClient;

namespace CSWeb.Admin.Reports
{
    public partial class PromoCodeSummary : BasePage
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

            //Sri: Always use using statement to make sure connection closed.
            using (SqlDataReader reader = new OrderManager().GetOrderCouponSummary(timezoneStartDate, timezoneEndDate, true))
            {
                dlCouponSummaryList.DataSource = reader;
                dlCouponSummaryList.DataBind();
            }


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Session["FilterFromDate"] = rangeDateControlCriteria.StartDateValueLocal.Value.ToShortDateString();
            Session["FilterToDate"] = rangeDateControlCriteria.EndDateValueLocal.Value.ToShortDateString();

            BindData(rangeDateControlCriteria.StartDateValueLocal, rangeDateControlCriteria.EndDateValueLocal);

        }

        protected void dlCouponSummaryList_ItemDataBound(object sender, DataListItemEventArgs e)
        {


            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lblPromotion = e.Item.FindControl("lblPromotion") as Label;
                Label lblOrderNo = e.Item.FindControl("lblOrderNo") as Label;
                Label lblOrderTotal = e.Item.FindControl("lblOrderTotal") as Label;
                Label lblPromotionCategory = e.Item.FindControl("lblPromotionCategory") as Label;

                if (DataBinder.Eval(e.Item.DataItem, "IsPercentage").ToString().Equals("0"))
                    lblPromotionCategory.Text = "by Category - Amount";
                else
                    lblPromotionCategory.Text = "by Category - Percentage";                    
                
                            lblPromotion.Text = DataBinder.Eval(e.Item.DataItem, "DiscountCode").ToString();
                lblOrderNo.Text = DataBinder.Eval(e.Item.DataItem, "OrderCount").ToString();
                lblOrderTotal.Text = String.Format("{0:C}", DataBinder.Eval(e.Item.DataItem, "OrderTotal"));
                
            }




        }
    }
}