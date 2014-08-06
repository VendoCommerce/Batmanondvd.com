using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSData;

namespace CSWeb.Admin
{

    public partial class TaxList : BasePage
    {
     
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session Validation in BasePage
            this.BaseLoad();
      
           if (!Page.IsPostBack)
           {
               lblSuccess.Text = ResourceHelper.GetResoureValue("LabelSuccess");
               lblCancel.Text = ResourceHelper.GetResoureValue("LabelCancel");
               ddlProducts.DataSource = CountryManager.GetActiveCountry();
               ddlProducts.DataTextField = "Name";
               ddlProducts.DataValueField = "CountryId";
               ddlProducts.DataBind();
               BindTaxRegion();
           }
        }

        private void BindTaxRegion()
        {
            dlCountryList.DataSource = CSFactory.GetTaxByCountry(0);
            dlCountryList.DataKeyField = "RegionId";
            dlCountryList.DataBind();
        }

        protected void dlCountryList_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
               TaxRegion taxRegion = e.Item.DataItem as TaxRegion;
                ITextControl lblTitle = e.Item.FindControl("lblTitle") as ITextControl;
                TextBox txtpercentage = e.Item.FindControl("txtOrderNo") as TextBox;
                HyperLink hlLikn = e.Item.FindControl("hlAddState") as HyperLink;
                lblTitle.Text = CountryManager.CountryName(taxRegion.CountryId);
                txtpercentage.Text = String.Format("{0:0.##}", taxRegion.Value);      
                hlLikn.NavigateUrl = "StateTax.aspx?Id=" + taxRegion.CountryId;
            }
        }

        protected void btnAdd_Country(object sender, EventArgs e)
        {
            //Response.Write(ddlProducts.SelectedItem.Text);
            int countryId = Convert.ToInt32(ddlProducts.SelectedItem.Value);
            decimal value = Convert.ToDecimal(txtPercentage.Text);
            AdminDAL.SaveTaxRegion(countryId, 0, value);
            BindTaxRegion();
            mpeThePopup.Hide();
        }

        protected void btnSave_OnClick(object sender, CommandEventArgs e)
        {

            if (e.CommandName == "Save")
            {
                if (this.dlCountryList.Items.Count > 0)
                {
                    Dictionary<int, decimal> itemList = new Dictionary<int, decimal>();
                    foreach (DataListItem lst in dlCountryList.Items)
                    {
                        if ((lst.ItemType == ListItemType.Item) || (lst.ItemType == ListItemType.AlternatingItem))
                        {
                            int regionId = (int)dlCountryList.DataKeys[lst.ItemIndex];
                            TextBox txtOrderNo = (TextBox)lst.FindControl("txtOrderNo");

                             decimal percentage = Convert.ToDecimal(txtOrderNo.Text);
                             itemList.Add(regionId, percentage);
                        }
                    }

                    AdminDAL.SaveCountryTax(itemList);


                }
                lblSuccess.Visible = true;
                lblCancel.Visible = false;
            }
            else
            {
                ddlProducts.DataSource = CountryManager.GetActiveCountry();
                ddlProducts.DataTextField = "Name";
                ddlProducts.DataValueField = "CountryId";
                ddlProducts.DataBind();
                BindTaxRegion();
                lblCancel.Visible = true;
                lblSuccess.Visible = false;
            }
            //redirect 
            //Response.Redirect("Main.aspx");
        }
    }


}