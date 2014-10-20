using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness.CustomerManagement;
using CSBusiness.Resolver;
using CSBusiness;
using CSCore.Utils;
using CSBusiness.Shipping;
using System.Collections;


namespace CSWeb.Admin
{
    public partial class UserEdit : System.Web.UI.Page
    {
        public int CustId = 0;
        public string Salt = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["custId"] != null)
            {
                CustId = Convert.ToInt32(Request["custId"].ToString());
               
            }

            if (!Page.IsPostBack)
            {

                BindAdminRole();
                if (CustId > 0)
                {
                    Customer custData = CSResolve.Resolve<ICustomerService>().GetCustomer(CustId);
                    txtFirstName.Text = custData.FirstName;
                    txtLastName.Text = custData.LastName;
                    txtUserName.Text = custData.Username;
                    txtEmail.Text = custData.Email;
                    txtPassword.Text = custData.Password;
                    cbAccount.Checked = custData.Active;
                    Salt = custData.SaltKey;
                    ddlAdminType.Items.FindByValue(custData.UserTypeId.ToString()).Selected = true;

                }
            }

        }

        protected void BindAdminRole()
        {
            Hashtable items  = CommonHelper.BindToEnum(typeof(UserTypeEnum));
            items.Remove(UserTypeEnum.User.ToString());
            ddlAdminType.DataSource = items;
            ddlAdminType.DataTextField = "Key";
            ddlAdminType.DataValueField = "Value";
            ddlAdminType.DataBind();

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["Salt"] = Salt;
        }

    
        protected void btnAction_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                if (Page.IsValid)
                {

                    Customer custData = new Customer();
                    custData.CustomerId = CustId;
                    custData.FirstName = txtFirstName.Text;
                    custData.LastName = txtLastName.Text;
                    custData.Username = txtUserName.Text;
                    custData.Email = txtEmail.Text;
                    custData.Password = txtPassword.Text;
                    custData.Active = cbAccount.Checked;
                    custData.UserTypeId = Int32.Parse(ddlAdminType.SelectedValue);
                    if(CustId >0)
                        custData.SaltKey = ViewState["Salt"].ToString();
                    CSResolve.Resolve<ICustomerService>().UpdateUser(custData);

                }
             
                lblSuccess.Visible = true;
                lblCancel.Visible = false;
            }
            else
            {
               
                lblCancel.Visible = true;
                lblSuccess.Visible = false;
            Response.Redirect("Users.aspx");
            }

            

            //Response.Redirect("Main.aspx");
            
        }
    }
}