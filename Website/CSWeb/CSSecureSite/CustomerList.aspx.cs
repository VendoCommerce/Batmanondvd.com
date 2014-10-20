using System;
using System.Web.UI;
using CSBusiness;
using CSBusiness.Resolver;
using CSBusiness.Shipping;
using System.Web.UI.WebControls;
using CSCore.DataHelper;
using CSWeb.Admin.UserControls;


namespace CSWeb.Admin
{
    public partial class CustomerList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BaseLoad();
                BindCustomers(1, true);
            }
        }

        protected void btnSearch_Command(object sender, EventArgs e)
        {

            BindCustomers(1, true);
        }

        protected void BindCustomers(int pageNum, bool refresh)
        {
            int startRec, endRec, totalCount=0;

            startRec = ((pageNum - 1) * ConfigHelper.PAGE_SIZE) + 1;
            endRec = (pageNum) * ConfigHelper.PAGE_SIZE;

            dlCustomerList.DataSource = CSResolve.Resolve<ICustomerService>().GetAllCustomers(txtFirstName.Text, txtLastName.Text, txtEmail.Text, (int)UserTypeEnum.User, startRec, endRec, out totalCount);
            dlCustomerList.DataBind();
            updList.Update();

            if (refresh)
            {
                pg.SetUp(totalCount, ConfigHelper.PAGE_SIZE);
                updPg.Update();
            }
        }


        public void OnPaging(Object s, PageChangeArguments args)
        {
            BindCustomers(args.PageNum, false);
          
        }

        protected void btnAction_Command(object sender, CommandEventArgs e)
        {
            //redirect 
            Response.Redirect("Main.aspx");
        }
    }
}