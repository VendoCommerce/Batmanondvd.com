using System;
using System.Web.UI;
using CSBusiness;
using CSBusiness.Resolver;
using CSBusiness.Shipping;

namespace CSWeb.Admin
{
    public partial class Users : BasePage
    {
        public int totalCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BaseLoad();
                BindCustomers(string.Empty, string.Empty, string.Empty);
            }
        }

        protected void lblCustomer_Search(object sender, EventArgs e)
        {

            BindCustomers(txtFirstName.Text, txtLastName.Text, txtEmail.Text);
        }

        protected void BindCustomers(string firstName, string lastName, string email)
        {
            int userTypeId = (int)UserTypeEnum.Admin + (int)UserTypeEnum.ClientAdmin;
            dlCustomerList.DataSource = CSResolve.Resolve<ICustomerService>().GetAllCustomers(firstName, lastName, email, userTypeId, 1, 1000, out totalCount);
            dlCustomerList.DataBind();
        }
    }
}