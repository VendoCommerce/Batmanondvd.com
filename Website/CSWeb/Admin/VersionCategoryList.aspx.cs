using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSCore.Utils;

namespace CSWeb.Admin
{
    public partial class VersionCategoryList : BasePage
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


     
        #endregion Page Load and Pre-Render Events

        #region Common code for the page



        private void BindVersion()
        {
            dlVersionList.DataSource = CSFactory.GetAllVersionCateogry();
            dlVersionList.DataKeyField = "CategoryId";
            dlVersionList.DataBind();
        }



        #endregion Common code for the page

        #region General Methods

        protected void btnAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "AddNew":
                    pnlAddCategory.Visible = true;

                    BindVersion();
                    break;
                case "Cancel":
                    pnlAddCategory.Visible = false;
                    txtCategory.Text = "";
         
                    break;
                case "Add":
                    if (Page.IsValid)
                    {
                        CSFactory.SaveVersionCategory(0, CommonHelper.fixquotesAccents(txtCategory.Text));
                    }


                    pnlAddCategory.Visible = false;
                    txtCategory.Text = "";
                  
                    BindVersion();
                    break;

                case "Back":
                    Response.Redirect("VersionList.aspx");
                    break;

            }

        }



        protected void dlVersionList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            CSBusiness.VersionCategory versionItem = e.Item.DataItem as CSBusiness.VersionCategory;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                ITextControl lblTitle = e.Item.FindControl("lblTitle") as ITextControl;
                ITextControl lblStatus = e.Item.FindControl("lblStatus") as ITextControl;
                ITextControl lblCategoy = e.Item.FindControl("lblCategoy") as ITextControl;

                LinkButton lbRemove = e.Item.FindControl("lbRemove") as LinkButton;
                lblTitle.Text = versionItem.Title;

                //Make sure admin mistakenly remove category
                if (versionItem.Title.ToLower() == "default")
                    lbRemove.Visible = false;
     
                if (versionItem.HideRemove)
                    lbRemove.Visible = false;
            }


        }


        protected void dlVersionList_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            int categoryId = (int)dlVersionList.DataKeys[e.Item.ItemIndex];
            switch (e.CommandName)
            {
                case "Delete":
                    CSFactory.RemoveVersionCategory(categoryId);
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
                    TextBox tbedit = (TextBox)e.Item.FindControl("txtEditCategory");

                    CSFactory.SaveVersionCategory(categoryId, CommonHelper.fixquotesAccents(tbedit.Text.Trim()));
                    dlVersionList.EditItemIndex = -1;
                    BindVersion();
                    break;
            }
        }

        #endregion General Methods
    }
}