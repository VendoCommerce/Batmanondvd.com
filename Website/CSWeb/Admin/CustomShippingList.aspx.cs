using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSData;
using CSBusiness;

namespace CSWeb.Admin
{
    public partial class CustomShippingList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session Validation in BasePage
            this.BaseLoad();
            if (!Page.IsPostBack)
                BindRegion();
        }


        private void BindRegion()
        {
            dlVersionList.DataSource = ShippingDAL.GetShippingRegion();
            dlVersionList.DataKeyField = "RegionId";
            dlVersionList.DataBind();
        }

        public string GetCountryName(string countryId)
        {
            return CountryManager.CountryName(Convert.ToInt32(countryId));
        }

        protected void dlVersionList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            ShippingRegion item = e.Item.DataItem as ShippingRegion;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                
                ITextControl lblCountryTitle = e.Item.FindControl("lblCountryTitle") as ITextControl;
                ITextControl lblStateTitle = e.Item.FindControl("lblStateTitle") as ITextControl;
                ITextControl lblPrefId = e.Item.FindControl("lblPrefId") as ITextControl;
                HyperLink hlEdit = e.Item.FindControl("hlEdit") as HyperLink;
                LinkButton lbRemove = e.Item.FindControl("lbRemove") as LinkButton;
                hlEdit.NavigateUrl = "CustomShipping.aspx?RId=" + item.RegionId;
                lblCountryTitle.Text = CountryManager.CountryName(item.CountryId);
                lblPrefId.Text = item.PrefId.ToString();
                if(item.StateId != null)
                    lblStateTitle.Text = StateManager.GetStateName(Convert.ToInt32(item.StateId));

            }


        }

        protected void dlVersionList_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            int RegionId = (int)dlVersionList.DataKeys[e.Item.ItemIndex];
            switch (e.CommandName)
            {
                case "Delete":
                    Label lblPrefId = (Label)e.Item.FindControl("lblPrefId");
                    CSFactory.RemoveShippingRegion(Convert.ToInt32(lblPrefId.Text));
                    BindRegion();
                    break;
            }
        }

        protected void btnAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {             
                    Response.Redirect("Shipping.aspx");

        }
    }
}