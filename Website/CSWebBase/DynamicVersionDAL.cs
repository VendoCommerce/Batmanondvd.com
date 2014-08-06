using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSCore.DataHelper;
using System.Data.SqlClient;

namespace CSWebBase
{
    public class DynamicVersionDAL
    {
        public static string GetDynamicVersion(string versionName)
        {
            string xmlData = "";
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_site_get_dynamic_version";
            SqlParameter[] ParamVal = new SqlParameter[6];
            ParamVal[0] = new SqlParameter("VersionName", versionName);
            using (SqlDataReader reader = BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal))
            {
                while (reader.Read())
                {
                    xmlData = reader["DataXML"].ToString();
                }
            }
            return xmlData;
        }


    }
}

