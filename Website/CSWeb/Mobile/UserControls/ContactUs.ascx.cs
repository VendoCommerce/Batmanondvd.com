using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSBusiness.CustomerManagement;
using CSCore.Utils;
using CSBusiness.Web;
using CSBusiness.ShoppingManagement;
using System.Net.Mail;
using CSCore.DataHelper;
using CSCore;
using CSBusiness.Preference;
using System.Text;
using CSBusiness.Email;

namespace CSWeb.Mobile.UserControls
{
    public partial class ContactUs : CSBaseUserControl
    {
        protected bool ValidateData()
        {
            bool _error = false;
            StringBuilder sbErrorMessages = new StringBuilder();
            if (CommonHelper.EnsureNotNull(txtFirstName.Text) == String.Empty)
            {
                _error = true;
                sbErrorMessages.Append(ResourceHelper.GetResoureValue("FirstNameErrorMsg") + "<br />");
            }
            if (CommonHelper.EnsureNotNull(txtLastName.Text) == String.Empty)
            {
                _error = true;
                sbErrorMessages.Append(ResourceHelper.GetResoureValue("LastNameErrorMsg") + "<br />");
            }
            if (!CommonHelper.IsValidEmail(txtEmail.Text))
            {
                _error = true;
                sbErrorMessages.Append(ResourceHelper.GetResoureValue("EmailErrorMsg") + "<br />");
            }
            if (CommonHelper.EnsureNotNull(txtMessage.Text) == String.Empty)
            {
                _error = true;
                sbErrorMessages.Append("Please enter your message" + "<br />");
            }
            return _error;
        }
        protected void btnContactSubmit(object sender, EventArgs e)
        {

            try
            {
                if (!ValidateData())
                {
                    int emailId = ConfigHelper.EmailAppSetting("EmailIdCustomerSupport");
                    EmailSetting emailTemplate = EmailManager.GetEmail(emailId);

                    string[] ToAddressList = emailTemplate.ToAddress.Split(';');
                    string toAddress = ToAddressList[0];

                    String BodyTemplate = emailTemplate.Body.Replace("&", "&amp;");
                    BodyTemplate = BodyTemplate.Replace("{FirstName}", "First Name: " + txtFirstName.Text.Trim());
                    BodyTemplate = BodyTemplate.Replace("{LastName}", "Last Name: " + txtLastName.Text.Trim());
                    BodyTemplate = BodyTemplate.Replace("{EmailAddress}", "Email: " + txtEmail.Text.Trim());
                    BodyTemplate = BodyTemplate.Replace("{PhoneNumber}", "Phone: " + txtPhone.Text.Trim());
                    BodyTemplate = BodyTemplate.Replace("{Message}", "Message: " + txtMessage.Text.Trim());

                    MailMessage _oMailMessage = new MailMessage(emailTemplate.FromAddress, toAddress, emailTemplate.Subject, BodyTemplate);
                    _oMailMessage.IsBodyHtml = true;
                    if (ToAddressList.Length > 1)
                    {
                        for (int i = 1; i < ToAddressList.Length; i++)
                        {
                            _oMailMessage.CC.Add(new MailAddress(ToAddressList[i]));
                        }
                    }

                    bool emailstatus = OrderHelper.SendMail(_oMailMessage);

                    if (emailstatus)
                    {
                        Success.Visible = true;
                        txtFirstName.Text = "";
                        txtLastName.Text = "";
                        txtEmail.Text = "";
                        txtPhone.Text = "";
                        txtMessage.Text = "";
                        txtEmailReType.Text = "";


                    }
                }
            }
            catch (Exception)
            {
                Success.Visible = false;
            }

        }
    }
}