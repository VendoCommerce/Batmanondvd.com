using System;
using CSCore.DataHelper;
using System.Data.SqlClient;

namespace CSData
{
    public class SignupDAL
    {


        static SignupDAL()
        {
        }


        public static SqlDataReader GetAllSignupEntries()
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_signup_all";

            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, null);

        }

        public static SqlDataReader GetSignupEntry(string email)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_signup_email";
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("Email", email);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);

        }


        public static void InsertSignupEntry(string email, string Name)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_insert_signup";
            SqlParameter[] ParamVal = new SqlParameter[2];
            ParamVal[0] = new SqlParameter("Email", email);
            ParamVal[1] = new SqlParameter("Name", Name);            
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);

        }        

    }//end of the class


}
