using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSData;
using CSBusiness.Cache;
using CSCore.Utils;

namespace CSWeb.Admin
{
    public partial class ResourceList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.BaseLoad();
            lblSuccess.Text = ResourceHelper.GetResoureValue("ResetCache");
            lblCancel.Text = ResourceHelper.GetResoureValue("LabelCancel");
            if (!Page.IsPostBack)
                BindResources();
        }

         private void BindResources()
        {
            dlItemList.DataSource = AdminDAL.GetResource();
            dlItemList.DataKeyField = "ResourceId";
            dlItemList.DataBind();
        }


         protected void btnAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
         {

             switch (e.CommandName)
             {
                 case "AddNew":
                     pnlAddCategory.Visible = true;
                     ScriptManager.RegisterStartupScript(dlItemList, this.GetType(), "scrolltobottom", "window.scrollTo(0,document.body.scrollHeight);", true);
                     break;
                 case "Cancel":
                     pnlAddCategory.Visible = false;
                     txtKeyName.Text = "";
                     txtValueName.Text = "";
                     break;
                 case "Add":
                     if (Page.IsValid)
                     {

                         AdminDAL.UpdateResource(0, CommonHelper.fixquotesAccents(txtKeyName.Text.Trim()), CommonHelper.fixquotesAccents(txtValueName.Text.Trim()));
                     }


                     pnlAddCategory.Visible = false;
                         txtKeyName.Text = "";
                     txtValueName.Text = "";
                     BindResources();
                     break;

             }

         }

       

         protected void dlItem_ItemCommand(object sender, DataListCommandEventArgs e)
         {
             int resourceId = (int)dlItemList.DataKeys[e.Item.ItemIndex];
             switch (e.CommandName)
             {
                 case "Delete":
                     AdminDAL.RemoveResource(resourceId);
                     BindResources();
                     break;
                 case "Edit":
                     dlItemList.EditItemIndex = e.Item.ItemIndex;
                     BindResources();
                     break;
                 case "Cancel":
                     dlItemList.EditItemIndex = -1;
                     BindResources();
                     break;
                 case "Update":
                     TextBox tbKey = (TextBox)e.Item.FindControl("txtEditKeyName");
                     TextBox tbValue = (TextBox)e.Item.FindControl("txtEditValueName");
                     AdminDAL.UpdateResource(resourceId, CommonHelper.fixquotesAccents(tbKey.Text.Trim()), CommonHelper.fixquotesAccents(tbValue.Text.Trim()));
                     dlItemList.EditItemIndex = -1;
                     BindResources();
                   
                     break;
             }
         }


         protected void btnSave_OnClick(object sender, CommandEventArgs e)
         {

             if (e.CommandName == "Save")
             {
                 ResourceHelper.RemoveCache();

                 lblSuccess.Visible = true;
             }
                 
             //redirect 
            // Response.Redirect("Main.aspx");
         }
    }
}

