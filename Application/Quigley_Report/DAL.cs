using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quigley_Report
{
    class DAL
    {
        public static DataTable GetDataTableByDate(DateTime startDate, DateTime endDate)
        {
            string storedProcedure = ConfigurationManager.AppSettings["storedProcedure"];
            string connstr = ConfigurationManager.AppSettings["connectionstring"];

            SqlCommand oCmd = null;
            SqlDataAdapter da;

            DataTable dt = null;
            try
            {
                oCmd = new SqlCommand();
                oCmd.CommandText = storedProcedure;
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add("@startDate", SqlDbType.DateTime).Value = startDate;
                oCmd.Parameters.Add("@endDate", SqlDbType.DateTime).Value = endDate;

                oCmd.Connection = new SqlConnection(connstr);
                oCmd.Prepare();
                dt = new DataTable();
                da = new SqlDataAdapter(oCmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                string strMessage = "Problem running procedure:  " + oCmd.CommandText + ". Error---" + ex.Message;
                Logger.LogToFile(strMessage);
            }
            finally
            {
                if (oCmd != null)
                {
                    oCmd.Parameters.Clear();
                    oCmd.Connection = null;
                    oCmd.Dispose();
                }
            }
            oCmd = null;
            return dt;
        }
    }
}
