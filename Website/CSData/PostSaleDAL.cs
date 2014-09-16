using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CSCore.DataHelper;
using System.Data;

namespace CSData
{
    public class PostSaleDAL
    {
        static PostSaleDAL()
		{
		}

        public static SqlDataReader GetAllPaths(int versionId, bool active)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_all_paths";
            SqlParameter[] ParamVal = new SqlParameter[2];
            ParamVal[0] = new SqlParameter("@versionId", versionId);
            ParamVal[1] = new SqlParameter("@active", active);

            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);
        }


        public static SqlDataReader GetPathOrderProcess()
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_path_count";
           
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, null);
        }

        


        public static SqlDataReader GetTemplate(int templateId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_template";
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("@templateId", templateId);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);
        }  

        public static SqlDataReader GetAllTemplates(bool includeExpired)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_all_templates";
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("@includeExpired", (includeExpired) ? 1 : 0);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);
        }

        public static void RemoveTemplate(int templateId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("@templateId", templateId);
            String ProcName = "pr_remove_template";
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
        }

        


        public static SqlDataReader GetUpSalePath(int pathId, bool expired)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_path";
            SqlParameter[] ParamVal = new SqlParameter[2];
            ParamVal[0] = new SqlParameter("pathId", pathId);
            ParamVal[1] = new SqlParameter("active", (expired) ? 1 : 0);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);
        }

        public static void SaveUpsalePath(int pathId, string title, string code, decimal weight, string templateXML, string versionXML)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_path";
            SqlParameter[] Parameters = new SqlParameter[6];
            Parameters[0] = new SqlParameter("@pathId", pathId);
            Parameters[1] = new SqlParameter("@title", title);
            Parameters[2] = new SqlParameter("@code", code);
            Parameters[3] = new SqlParameter("@weight", weight);
            Parameters[4] = new SqlParameter("@templateXML", templateXML);
            Parameters[5] = new SqlParameter("@versionXML", versionXML);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, Parameters);
        }

        public static void RemovePath(int pathId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("@pathId", pathId);
            String ProcName = "pr_remove_path";
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
        }

        public static void SavePath(string pathXml)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_pathlist";
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = new SqlParameter("@pathXML", pathXml);
            BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters);

        }


        public static void SaveTemplate(int templateId, string title, string body, string script, string tag, string uriLabel, DateTime? expireDate, string templateItemXML)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_template";
            SqlParameter[] Parameters = new SqlParameter[8];
            Parameters[0] = new SqlParameter("@templateId", templateId);
            Parameters[1] = new SqlParameter("@title", title);
            Parameters[2] = new SqlParameter("@body", body);
            Parameters[3] = new SqlParameter("@script", script);
            Parameters[4] = new SqlParameter("@tag", tag);
            Parameters[5] = new SqlParameter("@expireDate", expireDate);
            Parameters[6] = new SqlParameter("@templateItemXML", templateItemXML);
            Parameters[7] = new SqlParameter("@uriLabel", uriLabel);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, Parameters);
        }

        public static void CopyTemplate(int templateId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("@templateId", templateId);
            String ProcName = "pr_template_copy";
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
        }

        

   
    }
}
