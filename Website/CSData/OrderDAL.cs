using System;
using CSCore.DataHelper;
using System.Data.SqlClient;
using System.Data;

namespace CSData.Order
{
    public class OrderDAL
    {
        static OrderDAL()
        {

           
        }

        public static SqlDataReader GetOrderSummary(DateTime? startDate, DateTime? endDate, int versionId, int pathId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_report_order_summary";
            SqlParameter[] ParamVal = new SqlParameter[4];
             ParamVal[0] = new SqlParameter("@startDate", startDate);
            ParamVal[1] = new SqlParameter("@endDate", endDate);
            ParamVal[2] = new SqlParameter("@version", versionId);
             ParamVal[3] = new SqlParameter("@pathId", pathId);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);
        }

        public static SqlDataReader GetVersionSummary(DateTime? startDate, DateTime? endDate, bool includeArchiveData)
        {           
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_report_order_version";
            SqlParameter[] ParamVal = new SqlParameter[3];
             ParamVal[0] = new SqlParameter("@startDate", startDate);
            ParamVal[1] = new SqlParameter("@endDate", endDate);
            ParamVal[2] = new SqlParameter("@includeArchiveData", includeArchiveData);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal); 
        }

        public static SqlDataReader GetTnTVersionSummary_HL(DateTime? startDate, DateTime? endDate, bool includeArchiveData)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_report_order_version_tnt_hl";
            SqlParameter[] ParamVal = new SqlParameter[3];
            ParamVal[0] = new SqlParameter("@startDate", startDate);
            ParamVal[1] = new SqlParameter("@endDate", endDate);
            ParamVal[2] = new SqlParameter("@includeArchiveData", includeArchiveData);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);
        }

        public static SqlDataReader GetTnTVersionSummary_SC(DateTime? startDate, DateTime? endDate, bool includeArchiveData)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_report_order_version_tnt_sc";
            SqlParameter[] ParamVal = new SqlParameter[3];
            ParamVal[0] = new SqlParameter("@startDate", startDate);
            ParamVal[1] = new SqlParameter("@endDate", endDate);
            ParamVal[2] = new SqlParameter("@includeArchiveData", includeArchiveData);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);
        }

        //Same as MID report
        public static SqlDataReader GetOrderCustomFieldReport(DateTime? startDate, DateTime? endDate, int customFieldId, bool includeArchiveData)
        {

            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_report_order_customfield";
            SqlParameter[] ParamVal = new SqlParameter[4];
            ParamVal[0] = new SqlParameter("@startDate", startDate);
            ParamVal[1] = new SqlParameter("@endDate", endDate);
            ParamVal[2] = new SqlParameter("@customFieldId", customFieldId);
            ParamVal[3] = new SqlParameter("@includeArchiveData", includeArchiveData);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);

        }

        public static SqlDataReader GetOrderTransaction(DateTime? startDate, DateTime? endDate, int fieldId, bool includeArchiveData)
        {
                       
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_order_transaction";
            SqlParameter[] ParamVal = new SqlParameter[4];
            ParamVal[0] = new SqlParameter("@startDate", startDate);
            ParamVal[1] = new SqlParameter("@endDate", endDate);
            ParamVal[2] = new SqlParameter("@fieldId", fieldId);
            ParamVal[3] = new SqlParameter("@includeArchiveData", includeArchiveData);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);
   

        }

        public static SqlDataReader GetOrderCouponDetail(DateTime? startDate, DateTime? endDate,  bool includeArchiveData)
        {

            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_report_order_coupon_detail";
            SqlParameter[] ParamVal = new SqlParameter[3];
            ParamVal[0] = new SqlParameter("@startDate", startDate);
            ParamVal[1] = new SqlParameter("@endDate", endDate);
            ParamVal[2] = new SqlParameter("@includeArchiveData", includeArchiveData);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);


        }

        public static SqlDataReader GetOrderCouponSummary(DateTime? startDate, DateTime? endDate, bool includeArchiveData)
        {

            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_report_order_coupon_summary";
            SqlParameter[] ParamVal = new SqlParameter[3];
            ParamVal[0] = new SqlParameter("@startDate", startDate);
            ParamVal[1] = new SqlParameter("@endDate", endDate);
            ParamVal[2] = new SqlParameter("@includeArchiveData", includeArchiveData);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);


        }

        public static DataTable GetAllOrders(DateTime? startDate, DateTime? endDate, bool includeArchiveData, int startRec, int endRec, 
            string firstName, string lastName, string email, 
            out int totalCount)
        {

            DataTable dtTable = new DataTable();
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_all_orders";
            SqlParameter[] ParamVal = new SqlParameter[9];
            ParamVal[0] = new SqlParameter("@startDate", startDate);
            ParamVal[1] = new SqlParameter("@endDate", endDate);
            ParamVal[2] = new SqlParameter("@includeArchiveData", includeArchiveData);
            ParamVal[3] = new SqlParameter("@startRec", startRec);
            ParamVal[4] = new SqlParameter("@endRec", endRec);

            ParamVal[5] = new SqlParameter("@firstName", firstName);
            ParamVal[6] = new SqlParameter("@lastName", lastName);
            ParamVal[7] = new SqlParameter("@email", email);

            SqlParameter paramter1 = new SqlParameter("@totalCount", SqlDbType.Int);
            paramter1.Direction = ParameterDirection.Output;
            ParamVal[8] = paramter1;

            BaseSqlHelper.Execute(connectionString, ProcName, ParamVal, dtTable, false);
            totalCount = Convert.ToInt32(ParamVal[8].Value);
            return dtTable;

        }

        public static DataTable GetAllOrders(DateTime? startDate, DateTime? endDate, bool includeArchiveData, int startRec, int endRec, out int totalCount)
        {
            return GetAllOrders(startDate, endDate, includeArchiveData, startRec, endRec, string.Empty, string.Empty, string.Empty, out totalCount);
        }

        public static SqlDataReader GetOrderSummary(DateTime? startDate, DateTime? endDate, bool includeArchiveData)
        {
                       
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_order_summary";
            SqlParameter[] ParamVal = new SqlParameter[3];
            ParamVal[0] = new SqlParameter("@startDate", startDate);
            ParamVal[1] = new SqlParameter("@endDate", endDate);
            ParamVal[2] = new SqlParameter("@includeArchiveData", includeArchiveData);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);        

        }

        public static SqlDataReader GetOrder(int orderId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_order";
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("@orderId", orderId);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);

        }
          

        public static SqlDataReader GetOrderDetails(int orderId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_order_detail";
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("@orderId", orderId);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);

        }

        //public static int SaveOrder(string orderXML)
        //{
        //    return SaveOrder(orderXML, String.Empty);
      
        //}
        public static void SaveOrderPath(int orderId, int pathId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_order_path";
            SqlParameter[] ParamVal = new SqlParameter[2];
            ParamVal[0] = new SqlParameter("@orderId", orderId);
            ParamVal[1] = new SqlParameter("@pathId", pathId);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);

        }

        

        public static  void SaveOrder(int orderId, string transactionCode, string authCode, int orderStatusId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_order_trans";
            SqlParameter[] ParamVal = new SqlParameter[4];
            ParamVal[0] = new SqlParameter("orderId", orderId);
            ParamVal[1] = new SqlParameter("transactionCode", transactionCode);
            ParamVal[2] = new SqlParameter("authCode", authCode);
            ParamVal[3] = new SqlParameter("orderStatusId", orderStatusId);
             BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
        }

        public static  void SaveOrderInfo(int orderId, int orderStatusId, string fullfillmentRequest, string fullfillResponse)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_order_update_orderInfo";
            SqlParameter[] ParamVal = new SqlParameter[4];
            ParamVal[0] = new SqlParameter("orderId", orderId);
            ParamVal[1] = new SqlParameter("orderStatuId", orderStatusId);
            ParamVal[2] = new SqlParameter("fullfillRequest", fullfillmentRequest);
            ParamVal[3] = new SqlParameter("fullfillResponse", fullfillResponse);
             BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
        }


        public static int SaveOrder(int orderId, string orderXML, string requestParam)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_order";
            SqlParameter[] ParamVal = new SqlParameter[3];
            ParamVal[0] = new SqlParameter("orderId", orderId);
            ParamVal[1] = new SqlParameter("orderXML", orderXML);
            ParamVal[2] = new SqlParameter("requestParam", requestParam);
            return Convert.ToInt32(BaseSqlHelper.ExecuteScalar(connectionString, ProcName, ParamVal));

        }

         public static void UpdateOrder(int orderId, string orderXML)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_order_detail";
            SqlParameter[] ParamVal = new SqlParameter[2];
            ParamVal[0] = new SqlParameter("orderId", orderId);
            ParamVal[1] = new SqlParameter("orderXML", orderXML);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);

        }

         public static void UpdateOrderAttributes(int orderId, string orderXML, int? orderStatusId)
         {
             string connectionString = ConfigHelper.GetDBConnection();
             String ProcName = "pr_update_order_attributes";
             SqlParameter[] ParamVal = new SqlParameter[3];
             ParamVal[0] = new SqlParameter("orderId", orderId);
             ParamVal[1] = new SqlParameter("orderXML", orderXML);
             ParamVal[2] = new SqlParameter("orderStatusId", orderStatusId);
             BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
         }

         public static void UpdateOrderStatus(int orderId, int orderStatusId)
         {
             string connectionString = ConfigHelper.GetDBConnection();
             String ProcName = "pr_update_order_status";
             SqlParameter[] ParamVal = new SqlParameter[2];
             ParamVal[0] = new SqlParameter("orderId", orderId);             
             ParamVal[1] = new SqlParameter("orderStatusId", orderStatusId);
             BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
         }

         public static void UpdateOrderTax(int orderId, decimal taxAmount)
         {
             string connectionString = ConfigHelper.GetDBConnection();
             String ProcName = "pr_update_order_tax";
             SqlParameter[] ParamVal = new SqlParameter[2];
             ParamVal[0] = new SqlParameter("@orderId", orderId);
             ParamVal[1] = new SqlParameter("@taxAmount", taxAmount);
             BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);

         }

        public static void  UpdateOrderAfterUpSell(int orderId, string orderXML)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_ordertotal";
            SqlParameter[] ParamVal = new SqlParameter[2];
            ParamVal[0] = new SqlParameter("orderId", orderId);
            ParamVal[1] = new SqlParameter("orderXML", orderXML);
            BaseSqlHelper.ExecuteScalar(connectionString, ProcName, ParamVal);

        }


        public static void FireEmailLog(int orderId, string email, string subject, string body, DateTime processDate)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_emaillog";
            SqlParameter[] ParamVal = new SqlParameter[5];
            ParamVal[0] = new SqlParameter("orderId", orderId);
            ParamVal[1] = new SqlParameter("email", email);
            ParamVal[2] = new SqlParameter("subject", subject);
            ParamVal[3] = new SqlParameter("body", body);
            ParamVal[4] = new SqlParameter("processDate", processDate);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
        }

        public static SqlDataReader GetBatchProcessOrders()
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_order_batchprocess";
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, null);

        }

        public static SqlDataReader GetBatchProcessOrders(int orderId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_order_batch";
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("orderId", orderId);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);

        }

        public static void RemoveOrder(int orderId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("@orderId", orderId);
            String ProcName = "pr_remove_order";
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
        }
        
    }
}
