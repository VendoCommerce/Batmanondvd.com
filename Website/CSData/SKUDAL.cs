using System;
using CSCore.DataHelper;
using System.Data.SqlClient;
using System.Data;

namespace CSData
{
    public class SKUDAL
    {
        public enum DataTpe
        {
            Int,
            String,
            SmallDateTime
        }

        static SKUDAL()
		{
		}




        public static DataTable GetAllSkus(int startRec, int endRec, out int totalCount)
        {

            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_sku_all";
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@startRec", startRec);
            Parameters[1] = new SqlParameter("@endRec", endRec);
            SqlParameter paramter1 = new SqlParameter("@totalCount", SqlDbType.Int);
            paramter1.Direction = ParameterDirection.Output;
            Parameters[2] = paramter1;
            DataTable dtTable = new DataTable();

            BaseSqlHelper.Execute(connectionString, ProcName, Parameters, dtTable, false);
            totalCount = Convert.ToInt32(Parameters[2].Value);
            return dtTable;
        }


        public static SqlDataReader GetAllSkus(int skuId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_sku";
            SqlParameter[] ParamVal = new SqlParameter[1];
            if (skuId == 0)
                ParamVal[0] = new SqlParameter("skuId", System.DBNull.Value);
            else
                ParamVal[0] = new SqlParameter("skuId", skuId);

            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);
        }
   
        public static void RemoveSku(string objectName, int skuId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            SqlParameter[] ParamVal = new SqlParameter[2];
            ParamVal[0] = new SqlParameter("ObjectName", objectName);
            ParamVal[1] = new SqlParameter("skuId", skuId);
            String ProcName = "pr_remove_sku";
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
        }
        public static void InsertSku(int skuId, string skuXML)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_insert_sku";
            SqlParameter[] ParamVal = new SqlParameter[2];
            ParamVal[0] = new SqlParameter("skuId", skuId);
            ParamVal[1] = new SqlParameter("skuXML", skuXML);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);

        }

        public static void CopySku(int skuId, string objectName)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_copy_sku";
            SqlParameter[] ParamVal = new SqlParameter[2];
            ParamVal[0] = new SqlParameter("skuId", skuId);
            ParamVal[1] = new SqlParameter("ObjectName", objectName);
    
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
        }

    }//end of the class

}
