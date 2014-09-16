using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CSData;
using CSCore;
using CSPaymentProvider;

namespace CSBusiness.Email
{
    public class EmailManager
    {
               
		public static List<EmailSetting> GetAllEmailList()
        {
            List<EmailSetting> emailList = new List<EmailSetting>();
            using (SqlDataReader reader = AdminDAL.GetAllEmailList(0))
            {
                while (reader.Read())
                {
                    EmailSetting item = new EmailSetting();
                    item.EmailId = Convert.ToInt32(reader["EmailId"].ToString());
                    item.Title = reader["Title"].ToString();
                    item.Subject = reader["Subject"].ToString();
                    item.Body = reader["Body"].ToString();
                    item.FromAddress = reader["FromAddress"].ToString();
                    item.ToAddress = reader["ToAddress"].ToString();
                    emailList.Add(item);

                }
            }
            return emailList;
        }

        public static EmailSetting GetEmail(int emailId)
        {
            EmailSetting emailTemplate = new EmailSetting();
            using (SqlDataReader reader = AdminDAL.GetAllEmailList(emailId))
            {
                while (reader.Read())
                {

                    emailTemplate.EmailId = Convert.ToInt32(reader["EmailId"].ToString());
                    emailTemplate.Title = reader["Title"].ToString();
                    emailTemplate.Subject = reader["Subject"].ToString();
                    emailTemplate.Body = reader["Body"].ToString();
                    emailTemplate.FromAddress = reader["FromAddress"].ToString();
                    emailTemplate.ToAddress = reader["ToAddress"].ToString();
                }
            }
            return emailTemplate;
        }
 
        public static void RemoveEmail(int emailId)
        {
            AdminDAL.RemoveEmail(emailId);
        }

        public static void SaveEmail(EmailSetting emailItem)
        {
            AdminDAL.SaveEmail(emailItem.EmailId, emailItem.Title, emailItem.FromAddress, emailItem.ToAddress, emailItem.Subject, emailItem.Body);              
        }
    }
}
