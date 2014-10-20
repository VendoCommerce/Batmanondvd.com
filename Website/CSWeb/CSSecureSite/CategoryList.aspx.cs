using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSData;
using CSBusiness;
using CSCore.Utils;



namespace CSWeb.Admin
{
    /// <summary>
    /// Summary description for Category.
    /// </summary>
    public class CategoryList : BasePage
    {
        #region Control Declaration
        protected PlaceHolder pnlAddCategory;
        protected System.Web.UI.WebControls.DataList dlCategoryList;
        protected System.Web.UI.WebControls.TextBox txtCategory, txtorder;
        protected System.Web.UI.WebControls.ValidationSummary valError;
        protected System.Web.UI.WebControls.RequiredFieldValidator valAddName;
        protected System.Web.UI.WebControls.LinkButton btnAdd,lbSave, lbCancel, lbItemAdd;
        #endregion Control Declaration

        #region Variable Declaration
        protected bool filter = false;

        #endregion Variable Declaration

        #region Page Load and Pre-Render Events

        
        private void Page_Load(object sender, System.EventArgs e)
        {
            //Check Session Validation in BasePage
            this.BaseLoad();
            if (!Page.IsPostBack)
                BindCategory();

        }

        private void Page_PreRender(object sender, System.EventArgs e)
        {
            
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

        }
        #endregion Page Load and Pre-Render Events

        #region Common code for the page
       


        private void BindCategory()
        {
            dlCategoryList.DataSource = CSFactory.GetAllCategories();
            dlCategoryList.DataKeyField = "CategoryId";
            dlCategoryList.DataBind();
        }

 
        #endregion Common code for the page

        #region General Methods

        protected void btnAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "AddNew":
                    pnlAddCategory.Visible = true;
                    //LanguageControl.Bind();
                    BindCategory();
                    break;
                case "Cancel":
                    pnlAddCategory.Visible = false;
                    txtCategory.Text = "";
                    break;
                case "Add":
                    if (Page.IsValid)
                    {
                        int orderno = 0;
                        if (!string.IsNullOrEmpty(txtorder.Text))
                            orderno = Convert.ToInt32(txtorder.Text);
                        CSFactory.SaveCategoy(txtCategory.Text, orderno);
                      }


                    pnlAddCategory.Visible = false;
                    txtCategory.Text = "";
                    BindCategory();
                    break;

                case "Back":
                    Response.Redirect("Main.aspx");
                    break;
               
            }

        }

       

        protected void dlCategoryList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CSBusiness.Category categoryItem = e.Item.DataItem as CSBusiness.Category;
                ITextControl lblTitle = e.Item.FindControl("lblTitle") as ITextControl;
                ITextControl lblStatus = e.Item.FindControl("lblStatus") as ITextControl;
                ITextControl lblOrder = e.Item.FindControl("lblOrder") as ITextControl;
                LinkButton lbRemove = e.Item.FindControl("lbRemove") as LinkButton;
                lblTitle.Text = categoryItem.Title;
                lblStatus.Text = categoryItem.Visible ? "Active" : "Inactive";
                lblOrder.Text = categoryItem.OrderNo.ToString();
                if (categoryItem.HideRemove)
                    lbRemove.Visible = false;
            }
        }


        protected void dlCategory_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            int categoryId = (int)dlCategoryList.DataKeys[e.Item.ItemIndex];
            switch (e.CommandName)
            {
                case "Delete":
                    CategoryDAL.RemoveCategory(categoryId);
                    BindCategory();
                    break;
                case "Edit":
                    dlCategoryList.EditItemIndex = e.Item.ItemIndex;
                    BindCategory();
                    break;
                case "Cancel":
                    dlCategoryList.EditItemIndex = -1;
                    BindCategory();
                    break;
                case "Update":
                    TextBox tbedit = (TextBox)e.Item.FindControl("txtEditCategory");
                    TextBox tborder = (TextBox)e.Item.FindControl("txEdittorder");
                    CheckBox cbVisible = (CheckBox)e.Item.FindControl("cbVisible");
                    int orderno = 0;
                    if (!string.IsNullOrEmpty(tborder.Text))
                        orderno = Convert.ToInt32(tborder.Text);

                    CategoryDAL.UpdateCategory(categoryId, CommonHelper.fixquotesAccents(tbedit.Text.Trim()), cbVisible.Checked, orderno);
                    dlCategoryList.EditItemIndex = -1;
                    BindCategory();
                    break;
            }
        }

        #endregion General Methods

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.PreRender += new System.EventHandler(this.Page_PreRender);
        }
        #endregion
    }
}
