using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSWeb.Admin.UserControls;


namespace CSWeb.Admin
{
    public partial class ErrorLog : BasePage
    {
        private const int PAGE_SIZE = 20;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.BaseLoad();
            if (!Page.IsPostBack)
            {
                rangeDateControlCriteria.StartDateValueLocal = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                rangeDateControlCriteria.EndDateValueLocal = DateTime.Now.Date;
                BindErrors(1, true);
                
            }
        }

        protected void lblOrder_Search(object sender, EventArgs e)
        {

            BindErrors(1, true);
        }

        protected void BindErrors(int pageNum, bool refresh)
        {
            int startRec, endRec, totalCount;
            
            startRec = ((pageNum - 1) * PAGE_SIZE) + 1;
            endRec = (pageNum) * PAGE_SIZE;


            dlErrorList.DataSource = CSFactory.GetAllErrors(rangeDateControlCriteria.StartDateValueLocal.ToString(), rangeDateControlCriteria.EndDateValueLocal.ToString(), startRec, endRec, out totalCount);
            dlErrorList.DataBind();
            updList.Update();
            if (refresh)
            {
                pg.SetUp(totalCount, PAGE_SIZE);
                updPg.Update();
            }

        }

        public void OnPaging(Object s, PageChangeArguments args)
        {
            BindErrors(args.PageNum, false);
        }


        protected void btnAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {

           Response.Redirect("Main.aspx");
         
        }

    }
}