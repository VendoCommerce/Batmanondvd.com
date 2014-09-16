using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSData;
using System.Data.SqlClient;
using System.Web;
using CSBusiness.Web;
using CSBusiness.Attributes;

namespace CSBusiness
{
    public class Analytics
    {
        public enum TestAndTargetApiMethod
        {
            CampaignView
        }

        public const char CompleteVersionIdPartsSeparator = ':';

        public int[] GetTnTCampaignIds()
        {
            List<int> campaignIds = new List<int>();
            using (SqlDataReader reader = AnalyticsDAL.GetTnTCampaigns())
            {
                while (reader.Read())
                {
                    campaignIds.Add(Convert.ToInt32(reader["CampaignId"]));
                }
            }

            return campaignIds.ToArray();
        }

        /// <summary>
        /// Combines CS version name ("A1", "B2", etc.), T&T's campaign and experience IDs to return a combined version string. e.g. "C2:54812:3"
        /// </summary>
        /// <param name="versionId"></param>
        /// <param name="campaignId"></param>
        /// <param name="experience"></param>
        /// <returns></returns>
        public static string GetCompleteVersionId(string versionName, int tntCampaignId, int tntExperience)
        {
            return string.Format("{1}{0}{2}{0}{3}",
                CompleteVersionIdPartsSeparator, Convert.ToString(versionName), Convert.ToString(tntCampaignId), Convert.ToString(tntExperience));
        }
                
        public static string GetCompleteVersionIdJS(string versionName, bool includesTnTVars)
        {
            string format = string.Empty;
            
            if (includesTnTVars) // the JS can return versionName if it already contains T&T vars (... no other operations are needed).
            {
                format = @"
function (vid, cid, eid) {                 
    if (typeof (dbgCSVersion) != 'undefined')
        alert('{0}');

    return '{0}'; 

}".Replace("{0}", versionName);
            }
            else // otherwise, regenerate it given T&T vars passed to func.
            {   
                format = @"
function (vid, cid, eid)
{
    var separator = '{0}';
    var retvid = vid;
    if (typeof (cid) == 'undefined' || typeof (eid) == 'undefined')
        retvid = vid;
    else
        retvid = vid + separator + cid + separator + eid;

    if (typeof (dbgCSVersion) != 'undefined')
        alert(retvid);

    return retvid;
}
".Replace("{0}", Convert.ToString(Analytics.CompleteVersionIdPartsSeparator));
            }

            return format;
        }

        public static bool GetPartsFromCompleteVersion(string completeVersion, ref string versionName, ref int? tntCampaignId, ref int? tntExperienceId)
        {
            versionName = completeVersion;
            tntCampaignId = null;
            tntExperienceId = null;

            if (completeVersion == null || completeVersion.Trim() == string.Empty)
                return false;

            string[] parts = completeVersion.Split(CompleteVersionIdPartsSeparator);

            if (parts.Length != 3)
                return false;

            try
            {
                versionName = parts[0].ToUpper();
                tntCampaignId = Convert.ToInt32(parts[1]);
                tntExperienceId = Convert.ToInt32(parts[2]);

                return true;
            }
            catch
            {
                versionName = completeVersion;
                tntCampaignId = null;
                tntExperienceId = null;
            }

            return false;
        }
    }
}
