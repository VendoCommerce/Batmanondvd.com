using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSData;
using CSCore;
using CSBusiness.PostSale;
using System.Web.UI.HtmlControls;

namespace CSWeb.Admin
{
    public partial class TemplateList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session Validation in BasePage
            this.BaseLoad();
            if (!Page.IsPostBack)
                BindTemplates();
        }

        private void BindTemplates()
        {
            dlTemplateList.DataSource = new PathManager().GetAllTemplates(false);
            dlTemplateList.DataKeyField = "TemplateId";
            dlTemplateList.DataBind();
        }

        protected void dlTemplateList_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            int templateId = (int)dlTemplateList.DataKeys[e.Item.ItemIndex];
            switch (e.CommandName)
            {
                case "Delete":
                    new PathManager().RemoveTemplate(templateId);
                    BindTemplates();
                    break;
                case "Copy":
                    new PathManager().CopyTemplate(templateId);
                     BindTemplates();
                    break;

                case "Edit":
                    Response.Redirect("TemplateItem.aspx?templateId=" + templateId);
                    break;


            }
        }

        protected void dlTemplateList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Template Item = (Template)e.Item.DataItem;
                HtmlContainerControl holderQuantity = e.Item.FindControl("holderExpireDate") as HtmlContainerControl;
                LinkButton lbRemove = e.Item.FindControl("lbRemove") as LinkButton;
                Label lblExpireDate = e.Item.FindControl("lblExpireDate") as Label;

                if (Item.ExpireDate.Value.Year != 2079)
                    lblExpireDate.Text = Item.ExpireDate.ToString();


                if (DateTime.Compare(DateTime.Now, (DateTime) Item.ExpireDate) > 0)
                {
                    holderQuantity.Attributes.Add("style", "color: #ff0000;");
                }
                if (Item.HideRemove)
                    lbRemove.Visible = false;

            }
        }

        protected void btnAction_Command(object sender, CommandEventArgs e)
        {
            //redirect 
            Response.Redirect("PathList.aspx");
        }

    }
}