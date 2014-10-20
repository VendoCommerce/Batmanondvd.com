using System;
using System.Web.UI;
using CSBusiness;
using CSBusiness.Resolver;
using CSBusiness.Shipping;

namespace CSWeb.Admin
{
    public partial class CustomerPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                switch (Request["print"])
                {
                    case "1": BindCustomers(string.Empty, string.Empty, string.Empty); 
                        break;
                    case "2":
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.ContentEncoding = System.Text.Encoding.UTF8;
                        Response.Charset = String.Empty;
                        Page.EnableViewState = false;
                        string time = DateTime.Now.ToLongTimeString().Replace(":", "_").ToString();
                        string FileName = "attachment;filename= CS_Customers" + time + ".xls";
                        Response.AddHeader("Content-Disposition", FileName);
                        BindCustomers(string.Empty, string.Empty, string.Empty);
                        break;
                    default:
                        BindCustomers(string.Empty, string.Empty, string.Empty); 
                        break;
                }

                
            }
        }

        protected void BindCustomers(string firstName, string lastName, string email)
        {
            int totalCount = 0;
            dlCustomerList.DataSource = CSResolve.Resolve<ICustomerService>().GetAllCustomers(firstName, lastName, email, (int)UserTypeEnum.User, 1, 10000, out totalCount);
            dlCustomerList.DataBind();
        }
    }
}