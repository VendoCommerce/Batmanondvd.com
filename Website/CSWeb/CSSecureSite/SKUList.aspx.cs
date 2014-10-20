using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSData;
using CSBusiness.Resolver;
using CSBusiness;
using CSWeb.Admin.UserControls;
using CSCore.DataHelper;


namespace CSWeb.Admin
{
    public partial class SKUList : BasePage
    {
        #region Control Declaration
        protected PlaceHolder pnlAddCategory;
      
        protected System.Web.UI.WebControls.TextBox txtCategory;
     
        protected System.Web.UI.WebControls.RequiredFieldValidator valAddName;
        protected System.Web.UI.WebControls.LinkButton btnAdd, lbSave, lbCancel;
        #endregion Control Declaration

        #region Variable Declaration
            protected bool filter = false;
         #endregion Variable Declaration

        #region Page Load and Pre-Render Events


        private void Page_Load(object sender, System.EventArgs e)
        {
            //Check Session Validation in BasePage

            if (!Page.IsPostBack)
            {
                this.BaseLoad();
                BindSkuList(1, true);
            }

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

            private void BindSkuList(int pageNum, bool refresh)
            {
                int startRec, endRec, totalCount;

                startRec = ((pageNum - 1) * ConfigHelper.PAGE_SIZE) + 1;
                endRec = (pageNum) * ConfigHelper.PAGE_SIZE;

                dlSkuList.DataSource = CSResolve.Resolve<ISkuService>().GetAllSkus(startRec, endRec, out totalCount);
                dlSkuList.DataBind();
                updList.Update();
                if (refresh)
                {
                    pg.SetUp(totalCount, ConfigHelper.PAGE_SIZE);
                    updPg.Update();
                }
            }

            public void OnPaging(Object s, PageChangeArguments args)
            {
                BindSkuList(args.PageNum, false);
                ViewState["PageNo"] = args.PageNum;
            }


        #endregion Common code for the page

        #region General Methods
            protected void btnAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
            {
                Response.Redirect("Main.aspx");
            }
    
            protected void dlSKU_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            int skuId = (int)dlSkuList.DataKeys[e.Item.ItemIndex];
            int pageNum = 1;
            switch (e.CommandName)
            {
                case "Delete":
                    SKUDAL.RemoveSku(Sku.objectName, skuId);
                    
                        if (ViewState["PageNo"] != null)
                        {
                            pageNum = (int)ViewState["PageNo"];
                        }
                        BindSkuList(pageNum, true);
                    lblSuccess.Visible = false;
                    lblCancel.Visible = true;
                    break;

                case "Edit":
                    Response.Redirect("SKUItem.aspx?skuid=" + skuId);
                    break;

                case "Copy":
                     new SkuManager().CopySku(skuId);
                     //int pageNum=1;
                        if (ViewState["PageNo"] != null)
                        {
                            pageNum = (int)ViewState["PageNo"];
                        }
                        BindSkuList(pageNum, true);
                    lblSuccess.Visible = true;
                    lblCancel.Visible = false;
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