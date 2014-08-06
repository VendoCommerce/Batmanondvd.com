using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSData;
using CSCore;
using CSBusiness.PostSale;
using System.Threading;

namespace CSWeb.Admin
{
    public partial class PathList : BasePage
    {
        public int versionid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session Validation in BasePage            
            this.BaseLoad();

            if (!Page.IsPostBack)
            {
                BindPaths();
                BindVersions();
            }
        }

        private void BindPaths()
        {
            dlPathList.DataSource = new PathManager().GetAllPaths(versionid, false);
            dlPathList.DataKeyField = "PathId";
            dlPathList.DataBind();
            if (versionid == 0)
            {
                imgSave.Visible = false;
               // lbActiveSave.Visible = true;
            }
            else
            {
                imgSave.Visible = true;
               // lbActiveSave.Visible = false;
            }
        }

        private void BindVersions()
        {
            ddlVersion.DataSource = CSFactory.GetAllVersion().FindAll(x => x.Visible == true);
            ddlVersion.DataTextField = "Title";
            ddlVersion.DataValueField = "VersionId";
            ddlVersion.DataBind();
            ddlVersion.Items.Insert(0, new ListItem("All", "0"));
        }       

        protected void dlPathList_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            int pathId = (int)dlPathList.DataKeys[e.Item.ItemIndex];
            switch (e.CommandName)
            {
                case "Delete":
                    new PathManager().RemovePath(pathId);
                    BindPaths();
                    break;

                case "Edit":
                    Response.Redirect("PathItem.aspx?pathId=" + pathId);
                    break;

           
            }
        }

        private bool cvPrecentageCheck_Validate()
        {
            double sum = 0;
            int selecteItem = 0;
            bool notemptyFound = false;
            foreach (DataListItem item in dlPathList.Items)
            {
                TextBox txtWeight = (TextBox)item.FindControl("txtWeight");
                CheckBox cbCheck = (CheckBox) item.FindControl("cbVisible");
                if (cbCheck.Checked)
                {
                        selecteItem += 1;
                        sum += double.Parse(txtWeight.Text);
                }
            }

            if (selecteItem>0 && sum == 100)
                notemptyFound = true;
            else if(selecteItem == 0)
                notemptyFound = true;

            return notemptyFound;
        }

        protected void btnSave_OnClick(object sender, CommandEventArgs e)
        {
          
            if (e.CommandName == "Save")
            {
                Page.Validate();
                cvTemplateStep.IsValid = cvPrecentageCheck_Validate();
                if (Page.IsValid)
                {

                    if (this.dlPathList.Items.Count > 0)
                    {
                        List<Path> itemList = new List<Path>();
                        foreach (DataListItem lst in dlPathList.Items)
                        {
                            if ((lst.ItemType == ListItemType.Item) || (lst.ItemType == ListItemType.AlternatingItem))
                            {
                                Path item = new Path();
                                item.PathId = (int)dlPathList.DataKeys[lst.ItemIndex];
                                TextBox txtWeight = (TextBox)lst.FindControl("txtWeight");
                                CheckBox cbActive = (CheckBox)lst.FindControl("cbVisible");
                                item.Weight = Convert.ToDecimal(txtWeight.Text);
                                item.Active = cbActive.Checked;
                                itemList.Add(item);
                            }

                        }

                        new PathManager().SavePath(itemList);
                        Response.Redirect("Main.aspx");

                    }
                }
            }
            else
               Response.Redirect("Main.aspx");
        }

        protected void ddlVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            versionid = Convert.ToInt32(ddlVersion.SelectedValue);
            BindPaths();
        }
    }
}