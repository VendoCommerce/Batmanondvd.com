using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using CSBusiness.OrderManagement;
using CSCore;
using CSBusiness;
using System.Web.UI;
using System.Data.SqlClient;


namespace CSWeb.Admin
{
    public partial class PromoCodeDetail : BasePage
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
            using (SqlDataReader reader = new OrderManager().GetOrderCouponDetail(timezoneStartDate, timezoneEndDate, true))
            {
                dlCouponList.DataSource = reader;
                dlCouponList.DataBind();
            }


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Session["FilterFromDate"] = rangeDateControlCriteria.StartDateValueLocal.Value.ToShortDateString();
            Session["FilterToDate"] = rangeDateControlCriteria.EndDateValueLocal.Value.ToShortDateString();

            BindData(rangeDateControlCriteria.StartDateValueLocal, rangeDateControlCriteria.EndDateValueLocal);

        }

        protected void dlCouponList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
          

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lblOrderNo = e.Item.FindControl("lblOrderNo") as Label;
                Label lblName = e.Item.FindControl("lblName") as Label;
                Label lblAmount = e.Item.FindControl("lblAmount") as Label;
                Label lblCounponName = e.Item.FindControl("lblCounponName") as Label;
                Label lblCouponAmount = e.Item.FindControl("lblCouponAmount") as Label;
                Label lblOrderDate = e.Item.FindControl("lblOrderDate") as Label;
                Label lblAddr1 = e.Item.FindControl("lblAddr1") as Label;
                Label lblAddr2 = e.Item.FindControl("lblAddr2") as Label;
                Label lblCity = e.Item.FindControl("lblCity") as Label;
                Label lblZip = e.Item.FindControl("lblZip") as Label;
                Label lblEmail = e.Item.FindControl("lblEmail") as Label;
                Label lblPhone = e.Item.FindControl("lblPhone") as Label;
                Label lblState = e.Item.FindControl("lblState") as Label;
                
                

                lblOrderNo.Text = DataBinder.Eval(e.Item.DataItem, "OrderId").ToString();
                lblCounponName.Text = DataBinder.Eval(e.Item.DataItem, "DiscountCode").ToString();
                lblCouponAmount.Text = String.Format("{0:C}", DataBinder.Eval(e.Item.DataItem, "DisCountAmount"));
                lblAmount.Text = String.Format("{0:C}", DataBinder.Eval(e.Item.DataItem, "Total"));
                lblOrderDate.Text = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "CreatedDate")).AddHours(3).ToString();
                lblName.Text = String.Format("{0} {1}", DataBinder.Eval(e.Item.DataItem, "BillingFirstName"), DataBinder.Eval(e.Item.DataItem, "BillingLastName"));
                lblAddr1.Text = DataBinder.Eval(e.Item.DataItem, "BillingAddress1").ToString();
                lblAddr2.Text = DataBinder.Eval(e.Item.DataItem, "BillingAddress2").ToString();
                lblCity.Text = DataBinder.Eval(e.Item.DataItem, "BillingCity").ToString();
                lblZip.Text = DataBinder.Eval(e.Item.DataItem, "BillingZipPostalCode").ToString();
                lblEmail.Text = DataBinder.Eval(e.Item.DataItem, "BillingCity").ToString();
                lblPhone.Text = DataBinder.Eval(e.Item.DataItem, "Email").ToString();
                lblState.Text = StateManager.GetStateName(Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "BillingStateProvince").ToString()));
            }




        }
    }
}