using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSData;
using CSBusiness;
using CSCore.Utils;

namespace CSWeb.Admin
{
    public partial class VersionList : BasePage
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
                BindVersion();

        }


        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

        }
        #endregion Page Load and Pre-Render Events

        #region Common code for the page



        private void BindVersion()
        {
            dlVersionList.DataSource = CSFactory.GetAllVersion();
            dlVersionList.DataKeyField = "VersionId";
            dlVersionList.DataBind();
        }

        private void BindVersionCategory()
        {
            ddlCategory.DataSource = CSFactory.GetAllVersionCateogry();
            ddlCategory.DataTextField = "Title";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Select", ""));
        }


        #endregion Common code for the page

        #region General Methods

        protected void btnAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "AddNew":
                    pnlAddCategory.Visible = true;
                    BindVersionCategory();
                     BindVersion();
                    break;
                case "Cancel":
                    pnlAddCategory.Visible = false;
                    txtTitle.Text = "";
                    ddlCategory.SelectedValue = "";
                    break;
                case "Add":
                    if (Page.IsValid)
                    {
                        CSFactory.SaveVersion(CommonHelper.fixquotesAccents(txtTitle.Text), CommonHelper.fixquotesAccents(txtShortName.Text), cbVisible.Checked, Convert.ToInt32(ddlCategory.SelectedValue));
                    }


                    pnlAddCategory.Visible = false;
                    txtTitle.Text = "";
                    ddlCategory.SelectedValue = "";
                    BindVersion();
                    break;

                case "Back":
                    Response.Redirect("Main.aspx");
                    break;

            }

        }



        protected void dlVersionList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
             CSBusiness.Version versionItem = e.Item.DataItem as CSBusiness.Version;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
               
                ITextControl lblTitle = e.Item.FindControl("lblTitle") as ITextControl;
                ITextControl lblStatus = e.Item.FindControl("lblStatus") as ITextControl;
                ITextControl lblShortName = e.Item.FindControl("lblShortName") as ITextControl;
                  ITextControl lblCategoy = e.Item.FindControl("lblCategoy") as ITextControl;
         
                LinkButton lbRemove = e.Item.FindControl("lbRemove") as LinkButton;
                lblTitle.Text = versionItem.Title;
                lblShortName.Text = versionItem.ShortName;
                lblCategoy.Text = versionItem.CategoryTitle;
                lblStatus.Text = versionItem.Visible ? "Active" : "Inactive";

                //Make sure admin mistakenly remove category
                if(versionItem.ShortName.ToLower() == "control")
                    lbRemove.Visible = false;

                if (versionItem.HideRemove)
                    lbRemove.Visible = false;
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                DropDownList ddlEditCategory = (DropDownList)e.Item.FindControl("ddlEditCategory");
                ddlEditCategory.DataSource = CSFactory.GetAllVersionCateogry();
                ddlEditCategory.DataTextField = "Title";
                ddlEditCategory.DataValueField = "CategoryId";
                ddlEditCategory.DataBind();
                ddlEditCategory.Items.Insert(0, new ListItem("Select", ""));
                if (versionItem.CategoryId > 0)
                {
                    ddlEditCategory.Items.FindByValue(versionItem.CategoryId.ToString()).Selected = true;
                }
            }
        }


        protected void dlVersionList_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            int versionId = (int)dlVersionList.DataKeys[e.Item.ItemIndex];
            switch (e.CommandName)
            {
                case "Delete":
                    CSFactory.RemoveVersion(versionId);
                    BindVersion();
                    break;
                case "Edit":
                    dlVersionList.EditItemIndex = e.Item.ItemIndex;
                    BindVersion();
                    break;
                case "Cancel":
                    dlVersionList.EditItemIndex = -1;
                    BindVersion();
                    break;
                case "Update":
                    TextBox txtEditTitle = (TextBox)e.Item.FindControl("txtEditTitle");
                    TextBox txtEditShortName = (TextBox)e.Item.FindControl("txtEditShortName");
                    CheckBox cbVisible = (CheckBox)e.Item.FindControl("cbVisible");
                    DropDownList ddlCategory = (DropDownList)e.Item.FindControl("ddlEditCategory");
                    CSFactory.UpdateVersion(versionId, CommonHelper.fixquotesAccents(txtEditTitle.Text.Trim()), CommonHelper.fixquotesAccents(txtEditShortName.Text), cbVisible.Checked, Convert.ToInt32(ddlCategory.SelectedValue));
                    dlVersionList.EditItemIndex = -1;
                    BindVersion();
                    break;
            }
        }

        #endregion General Methods
    }
}