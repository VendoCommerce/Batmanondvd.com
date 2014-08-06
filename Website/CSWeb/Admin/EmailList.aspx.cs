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
using CSBusiness.Email;

namespace CSWeb.Admin
{
    public partial class EmailList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session Validation in BasePage
            this.BaseLoad();

            if (!Page.IsPostBack)
                BindEmails();
        }

        private void BindEmails()
        {

            dlEmailList.DataSource = EmailManager.GetAllEmailList();
            dlEmailList.DataKeyField = "EmailId";
            dlEmailList.DataBind();

        }

        protected void dlEmailList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            EmailSetting Item = e.Item.DataItem as EmailSetting;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblBody = (Label)e.Item.FindControl("lblBody");
                lblBody.Text = (Item.Body.Length <= 100) ? Item.Body : String.Format("{0}.....",Item.Body.Substring(0, 100));
                lblBody.ToolTip = Item.Body;
            }
        }

        protected void dlEmailList_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            int emailId = (int)dlEmailList.DataKeys[e.Item.ItemIndex];
            switch (e.CommandName)
            {
                case "Delete":
                    EmailManager.RemoveEmail(emailId);
                    BindEmails();
                    break;
                case "Edit":
                    Response.Redirect("EmailItem.aspx?emailId=" + emailId);
                    break;
            }
        }

        protected void btnAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            Response.Redirect("Main.aspx");
        }
    }
}