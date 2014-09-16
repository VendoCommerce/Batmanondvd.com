using System;
using CSCore.DataHelper;
using System.Data.SqlClient;
using CSCore;
using System.Data;

namespace CSData
{
    public class CustomerDAL
    {


        static CustomerDAL()
        {
        }

        public static DataTable GetCustomers(string firstName, string lastName, string email, int userTypeId, int startRec, int endRec, out int totalCount)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_customers";
            DataTable dtTable = new DataTable();

            SqlParameter[] Parameters = new SqlParameter[7];
            Parameters[0] = new SqlParameter("@fistname", firstName);
            Parameters[1] = new SqlParameter("@lastname", lastName);
            Parameters[2] = new SqlParameter("@email", email);
            Parameters[3] = new SqlParameter("@userTypeId", userTypeId);
            Parameters[4] = new SqlParameter("@startRec", startRec);
            Parameters[5] = new SqlParameter("@endRec", endRec);
            SqlParameter paramter1 = new SqlParameter("@totalCount", SqlDbType.Int);
            paramter1.Direction = ParameterDirection.Output;
            Parameters[6] = paramter1;
            BaseSqlHelper.Execute(connectionString, ProcName, Parameters, dtTable, false);
            totalCount = Convert.ToInt32(Parameters[6].Value);
            return dtTable;

        }

        public static DataTable GetAllCustomerOrders(string firstName, string lastName, string email, int startRec, int endRec, out int totalCount)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_customer_orders";
            DataTable dtTable = new DataTable();
            SqlParameter[] Parameters = new SqlParameter[6];
            Parameters[0] = new SqlParameter("@fistname", firstName);
            Parameters[1] = new SqlParameter("@lastname", lastName);
            Parameters[2] = new SqlParameter("@email", email);
            Parameters[3] = new SqlParameter("@startRec", startRec);
            Parameters[4] = new SqlParameter("@endRec", endRec);
            SqlParameter paramter1 = new SqlParameter("@totalCount", SqlDbType.Int);
            paramter1.Direction = ParameterDirection.Output;
            Parameters[5] = paramter1;
            BaseSqlHelper.Execute(connectionString, ProcName, Parameters, dtTable, false);
            totalCount = Convert.ToInt32(Parameters[5].Value);
            return dtTable;
          

        }


        public static SqlDataReader GetAllCustomerOrdersDetail(string firstName, string lastName, string email)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_customer_orders_detail";
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@fistname", firstName);
            Parameters[1] = new SqlParameter("@lastname", lastName);
            Parameters[2] = new SqlParameter("@email", email);
       
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters);

        }

        

        

        public static SqlDataReader GetCustomer(int CustomerId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_customer";
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@UserName", String.Empty);
            Parameters[1] = new SqlParameter("@CustomerId", CustomerId);

            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters);

        }

        public static SqlDataReader GetCustomerDetails(int CustomerId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_customer_full";
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = new SqlParameter("@CustomerId", CustomerId);

            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters);

        }

        public static Triplet<string, string, int> ValidateUser(string username)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_customer";
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@UserName", username);
            Parameters[1] = new SqlParameter("@CustomerId", 0);

            Triplet<string, string, int> pair = new Triplet<string, string, int>("", "", 0);

            using (SqlDataReader reader = BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters))
            {
                while (reader.Read())
                {

                    pair.Item1 = reader["value"].ToString();
                    pair.Item2 = reader["salt"].ToString();
                    pair.Item3 = Convert.ToInt32(reader["UserTypeId"]);

                }
            }

            return pair;
        }

        public static void UpdateUser(int customerId, string custXML)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_user";
            SqlParameter[] ParamVal = new SqlParameter[2];
            ParamVal[0] = new SqlParameter("customerId", customerId);
            ParamVal[1] = new SqlParameter("custXML", custXML);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);

        }

        public static int UpdateCustomer(int orderId, string custXML)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_customer";
            SqlParameter[] ParamVal = new SqlParameter[2];
            ParamVal[1] = new SqlParameter("orderId", orderId);
            ParamVal[0] = new SqlParameter("custxml", custXML);
            return Convert.ToInt32(BaseSqlHelper.ExecuteScalar(connectionString, ProcName, ParamVal));

        }
        public static int InsertCartAbandonment(string cartAbandonmentXML)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_insert_cartAbandonment";
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("cartAbandonmentXML", cartAbandonmentXML);
            return Convert.ToInt32(BaseSqlHelper.ExecuteScalar(connectionString, ProcName, ParamVal));

    }
        public static void RemoveCartAbandonment(int cartAbandonmentId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_remove_cartAbandonment";
            SqlParameter[] ParamVal = new SqlParameter[1];           
            ParamVal[0] = new SqlParameter("cartAbandonmentId", cartAbandonmentId);
            BaseSqlHelper.ExecuteScalar(connectionString, ProcName, ParamVal);
        }
    }
}
