using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSCore.Utils;
using CSBusiness;
using CSData;
using CSBusiness.ShoppingManagement;
using CSBusiness.Preference;

namespace CSWeb.Admin
{
    public partial class SitePrefItem : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblSuccess.Text = ResourceHelper.GetResoureValue("LabelSuccess");
            lblCancel.Text = ResourceHelper.GetResoureValue("LabelCancel");

            if (!Page.IsPostBack)
            {
                SitePref item = CSFactory.GetSitePreference();
                if (item.PathOrderDate.Value.Year != 2079)
                    dateControlStart.ValueLocal = item.PathOrderDate;
                tblCurrency.Text = item.Currency;
                CbShippingOption.Checked = item.OrderTotalShipping;
                cbGeoTarget.Checked = item.GeoTargetService;
                txtDays.Text = item.ArchiveData.ToString();
                txtTitle.Text = item.SiteHeader;
                txtImagePath.Text = item.LogoPath;
                txtSiteName.Text = item.SiteName;
                txtSiteUrl.Text = item.SiteUrl;
                cbPaymentGateway.Checked = item.PaymentGatewayService;
                cbFulfillmentHouse.Checked = item.FulfillmentHouseService;
                BindOrderProcess(item.OrderProcessType);                
                ucAttributes.Populate(CSFactory.GetCacheSitePref());
            }
        }

        private void BindOrderProcess(int typeId)
        {
            ddlOrderProcessList.Items.Clear();
            ddlOrderProcessList.DataSource = CommonHelper.BindToEnum(typeof(OrderProcessTypeEnum));
            ddlOrderProcessList.DataTextField = "Key";
            ddlOrderProcessList.DataValueField = "Value";
            ddlOrderProcessList.DataBind();
            if (typeId > 0)
                ddlOrderProcessList.Items.FindByValue(typeId.ToString()).Selected = true;
        }

        protected void btnAction_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                if (Page.IsValid)
                {
                    DateTime ExpireDate;
                    if (dateControlStart.ValueLocal.HasValue)
                        ExpireDate = Convert.ToDateTime(dateControlStart.ValueLocal.ToString());
                    else
                        ExpireDate = Convert.ToDateTime("1/1/2079");
                    string currency = CommonHelper.fixquotesAccents(tblCurrency.Text);

                    int days = (txtDays.Text.Length > 0) ? Convert.ToInt32(txtDays.Text) : 0;

                    CSFactory.SavePreference(ExpireDate, currency, CbShippingOption.Checked,
                                             Convert.ToInt32(ddlOrderProcessList.SelectedValue), cbGeoTarget.Checked,
                                             cbPaymentGateway.Checked, cbFulfillmentHouse.Checked, txtTitle.Text,
                                             txtImagePath.Text, days, txtSiteName.Text, txtSiteUrl.Text);

                    SitePreference sitePreference = CSFactory.GetCacheSitePref();

                    ucAttributes.SaveAllEnteredAttributeValues(sitePreference.ObjectName, sitePreference.ItemId);
                }
                lblSuccess.Visible = true;
                lblCancel.Visible = false;
            }
            else
            {
                SitePref item = CSFactory.GetSitePreference();
                if (item.PathOrderDate.Value.Year != 2079)
                    dateControlStart.ValueLocal = item.PathOrderDate;
                tblCurrency.Text = item.Currency;
                CbShippingOption.Checked = item.OrderTotalShipping;
                cbGeoTarget.Checked = item.GeoTargetService;
                txtDays.Text = item.ArchiveData.ToString();
                txtTitle.Text = item.SiteHeader;
                txtImagePath.Text = item.LogoPath;
                txtSiteName.Text = item.SiteName;
                txtSiteUrl.Text = item.SiteUrl;
                cbPaymentGateway.Checked = item.PaymentGatewayService;
                cbFulfillmentHouse.Checked = item.FulfillmentHouseService;
                BindOrderProcess(item.OrderProcessType);
                ucAttributes.Populate(CSFactory.GetCacheSitePref());
                lblCancel.Visible = true;
                lblSuccess.Visible = false;
            }

            

            //Response.Redirect("Main.aspx");
        }
    }
}