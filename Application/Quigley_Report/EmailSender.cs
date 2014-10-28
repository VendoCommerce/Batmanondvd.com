using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Quigley_Report
{
    class EmailSender
    {
        public static void SendFileasAttachment(string filePath)
        {
            try
            {
                string ToEmail = ConfigurationManager.AppSettings["sendemailto"];
                string ToEmailcc = ConfigurationManager.AppSettings["sendemailtocc"];
                StringBuilder sb = new StringBuilder();
                MailMessage message = new MailMessage();
                message.To.Add(ToEmail);
                message.CC.Add(ToEmailcc);
                message.From = new MailAddress("info@conversionsystems.com");

                if (File.Exists(filePath))
                {
                    Attachment attachment1 = new Attachment(filePath); //create the attachment
                    attachment1.Name =filePath.Substring( filePath.LastIndexOf("/")+1);
                    message.Attachments.Add(attachment1);
                }

                message.Subject = "Quigley Report";
                message.Body = "Please see attached for BatmanOnDvd Quigley Report.";
                message.IsBodyHtml = true;

                SmtpClient client;
                // client = new SmtpClient(System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"]);
                // client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;                
                client = new SmtpClient();
                client.Send(message);

                Logger.LogToFile("Email attachments send sucessfully. Total Attachments = " + message.Attachments.Count);

            }
            catch (System.Exception ex)
            {
                string error = ex.Message + " StackTrace: " + ex.StackTrace;
                Logger.LogToFile("Error Sending excel attachments in Email" + error);
                Console.WriteLine("Exception Details  : " + ex.ToString());
            }
        }
    }
}
