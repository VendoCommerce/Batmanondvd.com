using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using CSCore.DataHelper;

namespace CSCore
{
    public class EmailHelper
    {
        public static string DefaultSmtpHost
        {
            get
            {
                string host = new System.Net.Mail.SmtpClient().Host; 

                return string.IsNullOrEmpty(host) ? "localhost" : host;
            }
        }

        public static string DefaultFromEmail
        {
            get
            {
                string email = ConfigHelper.ReadAppSetting("CSCore.EmailHelper.DefaultFromEmail");

                return string.IsNullOrEmpty(email) ? "info@conversionsystems.com" : email;
            }
        }

        public static void SendEmail(SmtpClient smtpClient, string from, string to, string subject, string body, bool htmlBody, MailPriority? mailPriority, Encoding bodyEncoding)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(from);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = htmlBody;
            message.Priority = mailPriority.HasValue ? mailPriority.Value : MailPriority.Normal;
            message.BodyEncoding = bodyEncoding;

            message.To.Add(to.Replace(";", ","));

            smtpClient.Send(message);
        }

        public static void SendEmail(string from, string to, string subject, string body, bool htmlBody, MailPriority? mailPriority, Encoding bodyEncoding)
        {
            using (SmtpClient smtpClient = new System.Net.Mail.SmtpClient(DefaultSmtpHost))
            {
                smtpClient.UseDefaultCredentials = true;

                SendEmail(smtpClient, from, to, subject, body, htmlBody, mailPriority, bodyEncoding);
            }
        }

        /// <summary>
        /// Using default SMPT client to send email. Modify config to specify SMPT client parameters.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="htmlBody">True if body is html, false otherwise.</param>
        public static void SendEmail(string from, string to, string subject, string body, bool htmlBody)
        {
            SendEmail(from, to, subject, body, htmlBody, MailPriority.Normal, Encoding.UTF8);
        }

        public static void SendEmail(MailMessage mailMessage)
        {
            using (SmtpClient smtpClient = new System.Net.Mail.SmtpClient(DefaultSmtpHost))
            {
                smtpClient.UseDefaultCredentials = true;

                smtpClient.Send(mailMessage);
            }
        }

        public static void SendEmail(string to, string subject, string body)
        {
            SendEmail(DefaultFromEmail, to, subject, body, false, MailPriority.Normal, Encoding.UTF8);
        }
    }
}
