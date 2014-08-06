using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSData;

namespace CSWeb.Admin
{
    public partial class StateTax : BasePage
    {

        
        protected int countryId;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session Validation in BasePage
            this.BaseLoad();

            if (Request.Params["Id"] != null)
                countryId = Convert.ToInt32(Request.Params["Id"]);
            else
                countryId = 0;
            if (!Page.IsPostBack)
            {
                lblSuccess.Text = ResourceHelper.GetResoureValue("LabelSuccess");
                lblCancel.Text = ResourceHelper.GetResoureValue("LabelCancel");
                ddlStates.DataSource = StateManager.GetAllStates(countryId);
                ddlStates.DataTextField = "Name";
                ddlStates.DataValueField = "StateProvinceId";
                ddlStates.DataBind();
                BindTaxRegion();
            }
        }

        private void BindTaxRegion()
        {
            dlStateList.DataSource = CSFactory.GetTaxByCountry(countryId);
            dlStateList.DataKeyField = "RegionId";
            dlStateList.DataBind();
        }

        protected void dlStateList_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TaxRegion taxRegion = e.Item.DataItem as TaxRegion;
                ITextControl lblTitle = e.Item.FindControl("lblTitle") as ITextControl;
                TextBox txtpercentage = e.Item.FindControl("txtOrderNo") as TextBox;
                HyperLink hlLikn = e.Item.FindControl("hlAddState") as HyperLink;
                lblTitle.Text = StateManager.GetStateName(taxRegion.StateId);
                txtpercentage.Text = String.Format("{0:0.##}", taxRegion.Value);
          
            }
        }

        protected void btnAdd_Country(object sender, EventArgs e)
        {
            //Response.Write(ddlStates.SelectedItem.Text);
            int stateId = Convert.ToInt32(ddlStates.SelectedItem.Value);
            decimal value = Convert.ToDecimal(txtPercentage.Text);
            AdminDAL.SaveTaxRegion(countryId, stateId, value);
            mpeThePopup.Hide();
            BindTaxRegion();
            UPStateList.Update();
                 
            
        }

        protected void btnSave_OnClick(object sender, CommandEventArgs e)
        {

            if (e.CommandName == "Save")
            {
                if (this.dlStateList.Items.Count > 0)
                {
                    Dictionary<int, decimal> itemList = new Dictionary<int, decimal>();
                    foreach (DataListItem lst in dlStateList.Items)
                    {
                        if ((lst.ItemType == ListItemType.Item) || (lst.ItemType == ListItemType.AlternatingItem))
                        {
                            int regionId = (int)dlStateList.DataKeys[lst.ItemIndex];
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
                Response.Redirect("TaxList.aspx");
                //lblCancel.Visible = true;
                //lblSuccess.Visible = false;
            }

            //redirect 
            //Response.Redirect("TaxList.aspx");
        }

    }
}