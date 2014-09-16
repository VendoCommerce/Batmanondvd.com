using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using CSCore;
using CSCore.TestAndTarget;
using CSData;
using CSCore.Utils;
using System.Web;
using System.Web.SessionState;

namespace CSBusiness.Web
{
    public class TnTPostBasePage : System.Web.UI.Page
    {
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            
            int status = 0;

            try
            {
                string token = Request.Form["token"];

                AjaxToken ajaxToken = TnTHelper.DecryptToken(token);

                if (ajaxToken != null && Request.UserHostAddress == ajaxToken.userIP
                    && DateTime.Now.CompareTo(DateTime.Parse(ajaxToken.expireDateTime)) <= 0)
                {
                    if (!string.IsNullOrEmpty(ajaxToken.operation) && ajaxToken.operation.ToLower() == "savecampaign")
                    {
                        string tntCId = Request.Form["tntCId"];
                        string tntEId = Request.Form["tntEId"];

                        int versionId = ajaxToken.versionId;
                        int campaignId, experienceId;

                        if (int.TryParse(tntCId, out campaignId) && int.TryParse(tntEId, out experienceId)) // Save only valid (number) values for campaign and exp id's.
                        {
                            AnalyticsDAL.InsertTnTCampaign(versionId, campaignId, experienceId);

                            status = 1;
                        }
                    }
                }
            }
            catch
            {
                status = -1;
            }
            
            Response.Clear();
            Response.Write(CommonHelper.Encrypt(Convert.ToString(status) + "|" + new Random().Next()));
            Response.End();
        }
    }
}
