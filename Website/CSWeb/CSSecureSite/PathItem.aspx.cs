using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSBusiness.Cache;
using CSBusiness.PostSale;
using System.Web.UI.HtmlControls;
using CSCore.Utils;

namespace CSWeb.Admin
{
    public partial class PathItem : BasePage
    {
        public int PathId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Request["PathId"] != null)
                PathId = Convert.ToInt32(Request["PathId"].ToString());
            else
                PathId = 0;
            if (!Page.IsPostBack)
            {
                lblSuccess.Text = ResourceHelper.GetResoureValue("LabelSuccess");
                lblCancel.Text = ResourceHelper.GetResoureValue("LabelCancel");
                this.BaseLoad(); 
                BindPathInfo(PathId);                
            }
        }

        public void BindPathInfo(int PathId)
        {
            //pull the list from Master location
            PathManager pathMgr = new PathManager();
             List<Template> MasterTemplateList;
             if (PathId > 0)
             {
                 MasterTemplateList = pathMgr.GetAllTemplates(false);
                 Path pathItem = pathMgr.GetUpSalePath(PathId, true);
                 txtTitle.Text = pathItem.Title;
                 txtWeight.Text = String.Format("{0:0.##}", pathItem.Weight);
                 foreach (Template item in pathItem.Templates)
                 {
                     Template existItem = MasterTemplateList.FirstOrDefault(x => x.TemplateId == item.TemplateId);
                     if (existItem != null)
                     {
                         existItem.Active = true;
                         existItem.OrderNo = item.OrderNo;
                     }
                 }
                 lstVersion.DataSource = CSFactory.GetAllVersion().FindAll(x => x.Visible == true);
                 lstVersion.DataTextField = "Title";
                 lstVersion.DataValueField = "VersionId";
                 lstVersion.DataBind();
                 foreach (Int32 item in pathItem.Versions)
                 {
                     ListItem listItem = lstVersion.Items.FindByValue(item.ToString());
                     listItem.Selected = true;
                 }
             }
             else
             {
                 MasterTemplateList = pathMgr.GetAllTemplates(true);
                 lstVersion.DataSource = CSFactory.GetAllVersion().FindAll(x => x.Visible == true);
                 lstVersion.DataTextField = "Title";
                 lstVersion.DataValueField = "VersionId";
                 lstVersion.DataBind();
             }

            dlTemplateList.DataSource = MasterTemplateList;
            dlTemplateList.DataKeyField = "TemplateId";
            dlTemplateList.DataBind();            
        }       

        protected void dlTemplateList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Template Item = (Template)e.Item.DataItem;
                HtmlContainerControl holderQuantity = e.Item.FindControl("holderExpireDate") as HtmlContainerControl;
                if (DateTime.Compare(DateTime.Now, (DateTime) Item.ExpireDate) > 0)
                {
                    holderQuantity.Attributes.Add("style", "color: #ff0000;");
                }

            }
        }


     


        protected void btnAction_Command(object sender, CommandEventArgs e)
        {
            List<Int32> versionsList = new List<int>();
            if (e.CommandName == "Save")
            {
                Page.Validate();
          
           
                if (Page.IsValid)
                {
                    Path pathItem = new Path();
                    pathItem.Templates = new List<Template>();
                    foreach (DataListItem lst in dlTemplateList.Items)
                    {
                        if ((lst.ItemType == ListItemType.Item) || (lst.ItemType == ListItemType.AlternatingItem))
                        {

                            int templateId = (int)dlTemplateList.DataKeys[lst.ItemIndex];
                            TextBox txtOrderNo = (TextBox)lst.FindControl("txtOrderNo");
                            CheckBox cbVisible = (CheckBox)lst.FindControl("cbVisible");
                            if (cbVisible.Checked)
                            {
                                int orderNo = 0;
                                if (txtOrderNo.Text.Length > 0)
                                    orderNo = Convert.ToInt32(txtOrderNo.Text);

                                Template SelectedItem = new Template { TemplateId = templateId, OrderNo = orderNo };
                                pathItem.Templates.Add(SelectedItem);
                            }
                        }
                    }

                    foreach (ListItem x in lstVersion.Items)
                    {
                        if (x.Selected)
                            versionsList.Add(Convert.ToInt32(x.Value));
                    }
                   

                    pathItem.PathId = PathId;
                    pathItem.Title = CommonHelper.fixquotesAccents(txtTitle.Text);
                    pathItem.Code = String.Empty;
                    pathItem.Versions = versionsList;
                  
                    if (txtWeight.Text.Length > 0)
                        pathItem.Weight = Convert.ToDecimal(txtWeight.Text);
                    else
                        pathItem.Weight = 0;
                    new PathManager().SaveUpsalePath(pathItem);
                    //Response.Redirect("PathList.aspx");
                    lblCancel.Visible = false;
                    lblSuccess.Visible = true;
                    
                }
                
            }
            else
                Response.Redirect("PathList.aspx");

           
        }
    }
}