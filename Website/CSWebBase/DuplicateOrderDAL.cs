using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CSBusiness.Web;
using CSCore.DataHelper;

namespace CSWebBase
{
    public class DuplicateOrderDAL 
    {
        public static bool IsDuplicateOrder(string email)
        {
            using (SqlDataReader reader = GetDuplicateOrder(email))
            {
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public static SqlDataReader GetDuplicateOrder(string email)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_order_email";
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("@email", email);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);
        }

    }
}
