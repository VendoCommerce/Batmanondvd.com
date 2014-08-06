using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CSBusiness;
using CSCore.DataHelper;
using System.Data.SqlClient;

namespace CSWebBase
{
    public class ContactDAL
    {
        public static void InsertContact(string firstName, string lastName, string email, string phone, string message, string URL)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_insert_contact";
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("FirstName", firstName));
            parameters.Add(new SqlParameter("LastName", lastName));
            parameters.Add(new SqlParameter("Email", email));
            parameters.Add(new SqlParameter("Phone", phone));
            parameters.Add(new SqlParameter("Message", message));
            parameters.Add(new SqlParameter("URL", URL));

            BaseSqlHelper.ExecuteScalar(connectionString, ProcName, parameters.ToArray());
        }
    }
}
