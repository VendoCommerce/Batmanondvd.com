using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSData;
using CSCore;

namespace CSWeb.Admin
{
    public partial class CountryList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session Validation in BasePage
            this.BaseLoad();
            if (!Page.IsPostBack)
                BindCategory();
        }

        private void BindCategory()
        {
            dlCountryList.DataSource = CountryManager.GetAllCountry();
            dlCountryList.DataKeyField = "CountryId";
            dlCountryList.DataBind();
        }

        protected void dlCountryList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            CSBusiness.Country countryItem = e.Item.DataItem as CSBusiness.Country;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                
                ITextControl lblTitle = e.Item.FindControl("lblTitle") as ITextControl;
                CheckBox cbVisible = e.Item.FindControl("cbVisible") as CheckBox;
                TextBox txtOrderNo = e.Item.FindControl("txtOrderNo") as TextBox;
                HyperLink hlLikn = e.Item.FindControl("hlAddState") as HyperLink;
                lblTitle.Text = countryItem.Name;
                cbVisible.Checked = countryItem.Visible;
                txtOrderNo.Text = countryItem.OrderNo.ToString();
                hlLikn.NavigateUrl = "State.aspx?Id=" + countryItem.CountryId;
            }
        }

        protected void btnSave_OnClick(object sender, CommandEventArgs e)
        {

            if (e.CommandName == "Save")
            {
                if (this.dlCountryList.Items.Count > 0)
                {
                    List<Triplet<int, int, int>> itemList = new List<Triplet<int, int, int>>();
                    foreach (DataListItem lst in dlCountryList.Items)
                    {
                        if ((lst.ItemType == ListItemType.Item) || (lst.ItemType == ListItemType.AlternatingItem))
                        {
                            int countryId = (int)dlCountryList.DataKeys[lst.ItemIndex];
                            CheckBox cbVisible = (CheckBox)lst.FindControl("cbVisible");
                            TextBox txtOrderNo = (TextBox)lst.FindControl("txtOrderNo");
                            int active = (cbVisible.Checked) ? 1 : 0;
                            int orderNo = (txtOrderNo.Text.Length > 0) ? Convert.ToInt32(txtOrderNo.Text) : 0;
                            itemList.Add(new Triplet<int, int, int>(countryId, active, orderNo));
                        }
                    }

                    AdminDAL.SaveCountry(itemList);


                }
                lblSuccess.Visible = true;
                lblCancel.Visible = false;
            }
            else
            {
                //redirect 
                Response.Redirect("Main.aspx");
            }

            
        }
    }
}