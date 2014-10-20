using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSData;
using CSCore;
using CSBusiness.PostSale;
using System.Threading;
using CSCore.Utils;
using CSCore.Common;
using CSBusiness.FulfillmentHouse;

namespace CSWeb.Admin
{


    public partial class FulfillmentHouseProviderList : BasePage
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session Validation in BasePage
            this.BaseLoad();
            lblSuccess.Text = ResourceHelper.GetResoureValue("LabelSuccess");
            lblCancel.Text = ResourceHelper.GetResoureValue("LabelCancel");
            if (!Page.IsPostBack)
                BindProviders();
        }

                

        private void BindProviders()
        {

            dlProviderList.DataSource = FulfillmentHouseProviderManger.GetAllProvidersFromDB(false); 
            dlProviderList.DataKeyField = "ProviderId";
            dlProviderList.DataBind();
            
        }

        protected void dlProviderList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            FulfillmentHouseProviderSetting item = e.Item.DataItem as FulfillmentHouseProviderSetting;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GroupRadioButton rbButton = (GroupRadioButton) e.Item.FindControl("rbAlign");

                rbButton.Checked = item.IsDefault;
            }
        }

        protected void dlProviderList_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            int providerId = (int)dlProviderList.DataKeys[e.Item.ItemIndex];
            switch (e.CommandName)
            {
                case "Delete":
                    FulfillmentHouseProviderManger.RemoveProvider(providerId);              
                    BindProviders();
                    break;

                case "Edit":
                    dlProviderList.EditItemIndex = e.Item.ItemIndex;
                    BindProviders();
                    break;
                case "Cancel":
                    dlProviderList.EditItemIndex = -1;
                    BindProviders();
                    break;
                case "Update":
                    TextBox tbedit = (TextBox)e.Item.FindControl("txtEditTitle");
                    TextBox tbconfig = (TextBox)e.Item.FindControl("txtEditConfig");

                    FulfillmentHouseProviderManger.SaveProvider(providerId, CommonHelper.fixquotesAccents(tbedit.Text.Trim()), tbconfig.Text);
                    dlProviderList.EditItemIndex = -1;
                    BindProviders();
                    break;
           
            }
        }

        protected void btnAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "AddNew":
                    pnlAddCategory.Visible = true;
                    BindProviders();
                    ScriptManager.RegisterStartupScript(pnlAddCategory, this.GetType(), "scrolltobottom", "window.scrollTo(0,document.body.scrollHeight);", true);
                    break;
                case "Cancel":
                    pnlAddCategory.Visible = false;
                    txtTitle.Text = "";
                    txtConfig.Text = "";
                    break;
                case "Add":
                    if (Page.IsValid)
                    {
                        FulfillmentHouseProviderManger.SaveProvider(0, CommonHelper.fixquotesAccents(txtTitle.Text), txtConfig.Text);
                    }
                    pnlAddCategory.Visible = false;
                    txtTitle.Text = "";
                    txtConfig.Text = "";
                    BindProviders();
                    break;

            }

        }

 
        protected void btnSave_OnClick(object sender, CommandEventArgs e)
        {

            if (e.CommandName == "Save")
            {
                if (this.dlProviderList.Items.Count > 0)
                {
                    List<Triplet<int, int, int>> itemList = new List<Triplet<int, int, int>>();
                    foreach (DataListItem lst in dlProviderList.Items)
                    {
                        if ((lst.ItemType == ListItemType.Item) || (lst.ItemType == ListItemType.AlternatingItem))
                        {
                            int providerId = (int)dlProviderList.DataKeys[lst.ItemIndex];
                            CheckBox cbVisible = (CheckBox)lst.FindControl("cbVisible");
                            GroupRadioButton rbButton = (GroupRadioButton)lst.FindControl("rbAlign");
                            int active = (cbVisible.Checked) ? 1 : 0;
                            int defaultVal = (rbButton.Checked) ? 1 : 0;
                            itemList.Add(new Triplet<int, int, int>(providerId, active, defaultVal));
                        }
                    }

                    FulfillmentHouseProviderManger.SaveProvider(itemList);


                }
                lblSuccess.Visible = true;
                lblCancel.Visible = false;
            }
            //else
            //{
            //    Response.Redirect("Main.aspx");
            //}

            ////Response.Redirect("Main.aspx");

            ////redirect 
            //Response.Redirect("Main.aspx");
        }
    }
}