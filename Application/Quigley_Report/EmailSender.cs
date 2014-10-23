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
        private static void SendFileasAttachment(DataTable fileAttachments)
        {
            string excelFileName = "";
            string fileNameOnly = "";
            try
            {
                string ToEmail = ConfigurationManager.AppSettings["sendemailto"];
                string ToEmailcc = ConfigurationManager.AppSettings["sendemailtocc"];
                StringBuilder sb = new StringBuilder();
                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(ToEmail));
                message.CC.Add(new MailAddress(ToEmailcc));
                message.From = new MailAddress("info@conversionsystems.com");

                foreach (DataRow dRow in fileAttachments.Rows) // Loop over the rows.
                {
                    excelFileName = dRow["excelFileName"].ToString();
                    fileNameOnly = dRow["fileNameOnly"].ToString();
                    if (File.Exists(excelFileName))
                    {
                        Attachment attachment1 = new Attachment(excelFileName); //create the attachment
                        attachment1.Name = fileNameOnly;
                        message.Attachments.Add(attachment1);
                    }
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
                string error = ex.Message + " StackTrace:: " + ex.StackTrace;
                Logger.LogToFile("Error Sending excel attachments in Email" + error);
                Console.WriteLine("Exception Details  : " + ex.ToString());
            }
        }
    }
}
