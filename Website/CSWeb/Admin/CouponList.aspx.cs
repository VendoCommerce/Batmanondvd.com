using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSData;
using CSBusiness;
using CSCore.Utils;
using CSBusiness.Coupon;

namespace CSWeb.Admin
{
    public partial class CouponList : BasePage
    {


        #region Variable Declaration
        protected bool filter = false;

        #endregion Variable Declaration

        #region Page Load and Pre-Render Events


        protected void Page_Load(object sender, System.EventArgs e)
        {
            //Check Session Validation in BasePage
            this.BaseLoad();
            if (!Page.IsPostBack)
                BindCoupons();

        }


        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

        }
        #endregion Page Load and Pre-Render Events

        #region Common code for the page



        private void BindCoupons()
        {
            dlCouponList.DataSource = CSFactory.GetAllCoupon();
            dlCouponList.DataKeyField = "CouponId";
            dlCouponList.DataBind();
        }



        #endregion Common code for the page

        #region General Methods

        protected void btnAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {

            switch (e.CommandName)
            {


                case "Back":
                    Response.Redirect("Main.aspx");
                    break;

            }

        }



        protected void dlCouponList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            CouponInfo couponItem = e.Item.DataItem as CouponInfo;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                ITextControl lblTitle = e.Item.FindControl("lblTitle") as ITextControl;
                ITextControl lblStatus = e.Item.FindControl("lblStatus") as ITextControl;
                ITextControl lblDiscount = e.Item.FindControl("lblDiscount") as ITextControl;
                ITextControl lblDiscountType = e.Item.FindControl("lblDiscountType") as ITextControl;
                ITextControl lblTotalAmount = e.Item.FindControl("lblTotalAmount") as ITextControl;
                LinkButton lbRemove = e.Item.FindControl("lbRemove") as LinkButton;
                HyperLink hlEditLink = e.Item.FindControl("hlEditLink") as HyperLink;
                lblTitle.Text = couponItem.Title;
               
                lblTotalAmount.Text = String.Format("{0:C}", couponItem.TotalAmount);
                lblStatus.Text = couponItem.Active ? "Active" : "Inactive";
                if ((int)couponItem.DiscountType == 1)
                {
                    lblDiscountType.Text = "%";
                    lblDiscount.Text = String.Format("{0:0.##}%", Math.Round(couponItem.Discount,2));
                }
                else
                {
                    lblDiscountType.Text = "$";
                    lblDiscount.Text = String.Format("{0:C}", couponItem.Discount);
                }
                hlEditLink.NavigateUrl = "CouponItem.aspx?cId=" + couponItem.CouponId;

            }


        }


        protected void dlCouponList_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            int couponId = (int)dlCouponList.DataKeys[e.Item.ItemIndex];
            switch (e.CommandName)
            {
                case "Delete":
                    CSFactory.RemoveCoupon(couponId);
                    BindCoupons();
                    break;

            }
        }

        #endregion General Methods
    }
}