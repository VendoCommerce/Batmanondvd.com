using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CSCore.DataHelper;

namespace CSData
{
    public class AnalyticsDAL
    {
        public static SqlDataReader GetTnTCampaigns()
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_tnt_campaigns";
            SqlParameter[] ParamVal = new SqlParameter[0];

            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal); 
        }

        public static void InsertTnTCampaign(int versionId, int tntCampaignId, int tntExperienceId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_insert_tnt_campaign";
            SqlParameter[] ParamVal = new SqlParameter[3];

            ParamVal[0] = new SqlParameter("@VersionId", versionId);
            ParamVal[1] = new SqlParameter("@CampaignId", tntCampaignId);
            ParamVal[2] = new SqlParameter("@ExperienceId", tntExperienceId);

            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
        }

        public static SqlDataReader GetSCImportConfig()
        {
            string connectionString = ConfigHelper.GetMasterDBConnection();
            String ProcName = "pr_get_configscimport";
            SqlParameter[] ParamVal = new SqlParameter[0];

            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);   
        }

        public static void SaveSCDayMetrics(int siteId, string scReportSuiteId, string versionName, int? tntCampaignId, int? tntExperienceId,
            DateTime day, string metricsXml)
        {
            string connectionString = ConfigHelper.GetMasterDBConnection();
            String ProcName = "pr_insert_scdaymetrics_single";
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@SiteId", siteId));
            sqlParams.Add(new SqlParameter("@SCReportSuiteId", scReportSuiteId));
            sqlParams.Add(new SqlParameter("@VersionName", versionName));
            sqlParams.Add(new SqlParameter("@TnTCampaignId", tntCampaignId));
            sqlParams.Add(new SqlParameter("@TnTExperienceId", tntExperienceId));
            sqlParams.Add(new SqlParameter("@Day", day));
            sqlParams.Add(new SqlParameter("@Metrics", metricsXml));

            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, sqlParams.ToArray());
        }

        public static SqlDataReader GetSCMetrics(int siteId, DateTime startDate, DateTime endDate)
        {
            string connectionString = ConfigHelper.GetMasterDBConnection();
            String ProcName = "pr_get_scmetrics";
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@SiteId", siteId));
            sqlParams.Add(new SqlParameter("@StartDate", startDate));
            sqlParams.Add(new SqlParameter("@EndDate", endDate));

            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, sqlParams.ToArray());
        }

        public static SqlDataReader GetSCSiteVersion(int siteId)
        {
            string connectionString = ConfigHelper.GetMasterDBConnection();
            String ProcName = "pr_get_site_scversion";
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@SiteId", siteId));            

            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, sqlParams.ToArray());
        }

        public static void InsertVisitorData(string guid, string domain, DateTime? visitDate, string ip, string userAgent, int daysIdUnique, out string newGuid)
        {
            string connectionString = ConfigHelper.GetMasterDBConnection();
            String ProcName = "pr_insert_visitordata";
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@VisitorGuid", guid));
            sqlParams.Add(new SqlParameter("@Domain", domain));
            sqlParams.Add(new SqlParameter("@VisitDate", visitDate));
            sqlParams.Add(new SqlParameter("@IPAddress", ip));
            sqlParams.Add(new SqlParameter("@UserAgent", userAgent));
            sqlParams.Add(new SqlParameter("@DaysIdIsUnique", daysIdUnique));
            sqlParams.Add(new SqlParameter("@NewVisitorGuid", System.Data.SqlDbType.UniqueIdentifier));

            sqlParams[sqlParams.Count - 1].Direction = System.Data.ParameterDirection.Output;
            SqlParameter[] prms = sqlParams.ToArray();

            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, prms);

            newGuid = Convert.IsDBNull(prms[prms.Length - 1].Value) ? null : Convert.ToString(prms[prms.Length - 1].Value);
        }
    }
}
