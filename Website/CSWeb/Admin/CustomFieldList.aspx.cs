using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSData;

namespace CSWeb.Admin
{
    public partial class CustomFieldList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session Validation in BasePage
            this.BaseLoad();
            if (!Page.IsPostBack)
                BindCustomFields();
        }

        private void BindCustomFields()
        {
            dlCustomFieldList.DataSource = CustomFieldDAL.GetCustomFields();
            dlCustomFieldList.DataKeyField = "FieldId";
            dlCustomFieldList.DataBind();
        }

        protected void btnAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "AddNew":
                    pnlAddCategory.Visible = true;
                    //LanguageControl.Bind();
                    BindCustomFields();
                    break;
                case "Cancel":
                    pnlAddCategory.Visible = false;
                    txtCustomField.Text = "";
                    break;
                case "Add":
                    if (Page.IsValid)
                    {
                        CustomFieldDAL.SaveField(txtCustomField.Text);
                    }


                    pnlAddCategory.Visible = false;
                    txtCustomField.Text = "";
                    BindCustomFields();
                    break;

            }

        }



        protected void dlCustomFieldList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            CSData.CustomField categoryItem = e.Item.DataItem as CSData.CustomField;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                
                ITextControl lblTitle = e.Item.FindControl("lblTitle") as ITextControl;
                ITextControl lblStatus = e.Item.FindControl("lblStatus") as ITextControl;
                ITextControl lblOrder = e.Item.FindControl("lblOrder") as ITextControl;
                lblTitle.Text = categoryItem.FieldName;
                lblStatus.Text = (bool) categoryItem.Active ? "Active" : "Inactive";
              
            }
        }


        protected void dlCustomFieldList_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            int fieldId = (int)dlCustomFieldList.DataKeys[e.Item.ItemIndex];
            switch (e.CommandName)
            {
                case "Delete":
                    CustomFieldDAL.RemoveField(fieldId);
                    BindCustomFields();
                    break;
                case "Edit":
                    dlCustomFieldList.EditItemIndex = e.Item.ItemIndex;
                    BindCustomFields();
                    break;
                case "Cancel":
                    dlCustomFieldList.EditItemIndex = -1;
                    BindCustomFields();
                    break;
                case "Update":
                    TextBox txtEditCustomField = (TextBox)e.Item.FindControl("txtEditCustomField");
                    CustomFieldDAL.UpdateField(fieldId, txtEditCustomField.Text.Trim());
                    dlCustomFieldList.EditItemIndex = -1;
                    BindCustomFields();
                    break;
            }
        }
    }
}