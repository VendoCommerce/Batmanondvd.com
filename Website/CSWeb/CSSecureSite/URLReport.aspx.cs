using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using CSBusiness.OrderManagement;
using CSCore.DataHelper;
using CSWeb.Root.UserControls;
using CSCore;
using CSBusiness;
using CSBusiness.PostSale;
using CSWeb.HitLinks;
using CSCore.Utils;
using System.Web.UI.HtmlControls;
using CSData;
using CSBusiness.Preference;


namespace CSWeb.Admin
{
    public partial class URLReport : BasePage
    {
        protected Dictionary<int, List<ReportFields>> dtCollectionList;
        public Hashtable HitLinkVisitor = new Hashtable();
        public int CategoryId = 0;
        public decimal CategoryUniqueVistiors = 0;
        public decimal TotalCategoryUniqueVistiors = 0, TotalRevenue = 0;
        public int TotalOrders = 0;
        public static string siteName = string.Empty;
        public static string siteUrl = string.Empty;
        public static string siteTitle = string.Empty;
        public string hitsLinkUserName
        {
            get
            {
                return Convert.ToString(Session["hitsLinkUserName"] ?? string.Empty);
            }
            set
            {
                Session["hitsLinkUserName"] = value;
            }
        }

        public string hitsLinkPassword
        {
            get
            {
                return Convert.ToString(Session["hitsLinkPassword"] ?? string.Empty);
            }
            set
            {
                Session["hitsLinkPassword"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BindSettings();
            if (!IsPostBack)
            {
                this.BaseLoad();
                liHeader.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Day.ToString() + ", " + DateTime.Now.Year.ToString();
                liSubHeader.Text = DateTime.Now.DayOfWeek + " " + DateTime.Now.AddHours(3).ToShortTimeString() + " (EST)";

                if (Session["FilterFromDate"] != null && Session["FilterToDate"] != null)
                {
                    rangeDateControlCriteria.StartDateValueLocal = Convert.ToDateTime(Session["FilterFromDate"]);
                    rangeDateControlCriteria.EndDateValueLocal = Convert.ToDateTime(Session["FilterToDate"]);
                }
                else
                {

                    rangeDateControlCriteria.StartDateValueLocal = DateTime.Now.Date;
                    rangeDateControlCriteria.EndDateValueLocal = DateTime.Now.Date;

                }

                BindData(rangeDateControlCriteria.StartDateValueLocal, rangeDateControlCriteria.EndDateValueLocal);
            }
        }

        protected void BindData(DateTime? startDate, DateTime? endDate)
        {

            DateTime? timezoneStartDate = DateTimeUtil.GetEastCoastStartDate(rangeDateControlCriteria.StartDateValueLocal);
            DateTime? timezoneEndDate = DateTimeUtil.GetEastCoastDate(rangeDateControlCriteria.EndDateValueLocal);
            dtCollectionList = GetVersionSummary(timezoneStartDate, timezoneEndDate, false);

            Data rptData = new ReportWSSoapClient().GetDataFromTimeframe(hitsLinkUserName, hitsLinkPassword, ReportsEnum.MultiVariate, TimeFrameEnum.Daily, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate), 100000000, 0, 0);
            for (int i = 0; i <= rptData.Rows.GetUpperBound(0); i++)
            {
                HitLinkVisitor.Add(rptData.Rows[i].Columns[0].Value.ToLower(), rptData.Rows[i].Columns[9].Value);
            }

            //Update Version List information
            foreach (ReportFields item in dtCollectionList[1])
            {
                decimal visitor = 0;
                if (item.Title.ToLower().Equals(item.ShortName.ToLower()))
                {
                    if (HitLinkVisitor.ContainsKey(item.Title))
                    {
                        visitor += Convert.ToDecimal(HitLinkVisitor[item.Title].ToString());
                        visitor = Math.Abs(visitor);
                    }
                }
                else
                {
                    //Added this to fix bug of orderhelper.getversionname()
                    if (HitLinkVisitor.ContainsKey(item.Title))
                    {
                        visitor += Convert.ToDecimal(HitLinkVisitor[item.Title].ToString());
                    }
                    if (HitLinkVisitor.ContainsKey(item.ShortName.ToLower()))
                    {
                        visitor += Convert.ToDecimal(HitLinkVisitor[item.ShortName.ToLower()].ToString());
                    }
                    visitor = Math.Abs(visitor);
                }
                item.UniqueVisitors = visitor;

                if (visitor > 0)
                {
                    item.Conversion = Math.Round((Convert.ToDecimal(item.TotalOrders) * 100) / visitor, 1);
                    item.RevenuePerVisit = Convert.ToDecimal(item.TotalRevenue) / visitor;
                }
                else
                {
                    item.Conversion = 0;
                    item.RevenuePerVisit = 0;
                }
            }


            // Fetching data from 2nd hitslink account
            rptData = new ReportWSSoapClient().GetDataFromTimeframe(hitsLinkUserName, hitsLinkPassword, ReportsEnum.MultiVariate, TimeFrameEnum.Daily, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate), 100000000, 0, 0);
            for (int i = 0; i <= rptData.Rows.GetUpperBound(0); i++)
            {
                if (!HitLinkVisitor.ContainsKey(rptData.Rows[i].Columns[0].Value.ToLower()))
                {
                    HitLinkVisitor.Add(rptData.Rows[i].Columns[0].Value.ToLower(), rptData.Rows[i].Columns[9].Value);
                }
            }

            //Update Version List information - Start
            foreach (ReportFields item in dtCollectionList[2])
            {
                decimal visitor = 0;
                if (endDate <= new DateTime(2013, 03, 25))
                {
                    if (item.Title.ToLower().Equals(item.ShortName.ToLower()))
                    {
                        if (HitLinkVisitor.ContainsKey(item.Title))
                        {
                            visitor += Convert.ToDecimal(HitLinkVisitor[item.Title].ToString());
                            visitor = Math.Abs(visitor);
                        }
                    }
                    else
                    {
                        //Added this to fix bug of orderhelper.getversionname()
                        if (HitLinkVisitor.ContainsKey(item.Title))
                        {
                            visitor += Convert.ToDecimal(HitLinkVisitor[item.Title].ToString());
                        }
                        if (HitLinkVisitor.ContainsKey(item.ShortName.ToLower()))
                        {
                            visitor += Convert.ToDecimal(HitLinkVisitor[item.ShortName.ToLower()].ToString());
                        }
                        visitor = Math.Abs(visitor);
                    }
                }
                else
                {
                    if (!item.Title.Contains("-"))
                    {
                        if (HitLinkVisitor.ContainsKey(item.Title + "-" + item.CategoryTitle))
                        {
                            visitor += Convert.ToDecimal(HitLinkVisitor[item.Title + "-" + item.CategoryTitle].ToString());
                        }
                    }
                    else
                    {
                        if (HitLinkVisitor.ContainsKey(item.Title))
                        {
                            visitor += Convert.ToDecimal(HitLinkVisitor[item.Title].ToString());
                        }
                    }
                    visitor = Math.Abs(visitor);
                }
                item.UniqueVisitors = visitor;

                if (visitor > 0)
                {
                    item.Conversion = Math.Round((Convert.ToDecimal(item.TotalOrders) * 100) / visitor, 1);
                    item.RevenuePerVisit = Convert.ToDecimal(item.TotalRevenue) / visitor;
                }
                else
                {
                    item.Conversion = 0;
                    item.RevenuePerVisit = 0;
                }
            }
            //Update Version List information - End



            dlVersionCategoryList.DataSource = CSFactory.GetAllVersionCateogry();
            dlVersionCategoryList.DataBind();

            //FCLiteral.Text = CreateCharts(dtCollectionList[1dtCollectionList

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //TimeSpan ts = new TimeSpan(24, 0, 0);
            //CommonHelper.SetCookie("FromDate", rangeDateControlCriteria.StartDateValueLocal.Value.ToShortDateString(), ts);
            //CommonHelper.SetCookie("ToDate", rangeDateControlCriteria.EndDateValueLocal.Value.ToShortDateString(), ts);

            Session["FilterFromDate"] = rangeDateControlCriteria.StartDateValueLocal.Value.ToShortDateString();
            Session["FilterToDate"] = rangeDateControlCriteria.EndDateValueLocal.Value.ToShortDateString();

            BindData(rangeDateControlCriteria.StartDateValueLocal, rangeDateControlCriteria.EndDateValueLocal);

        }


        protected void dlVersionCategoryList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            CSBusiness.VersionCategory versionItem = e.Item.DataItem as CSBusiness.VersionCategory;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lblCategory = e.Item.FindControl("lblCategory") as Label;
                HtmlContainerControl CategoryHeaderRow = e.Item.FindControl("CategoryHeaderRow") as HtmlContainerControl;

                lblCategory.Text = versionItem.Title;
                if (dtCollectionList[0].Count == 1)
                {
                    CategoryHeaderRow.Visible = false;

                }
                else
                    CategoryHeaderRow.Visible = true;


                DataList dlVersionItemList = (DataList)e.Item.FindControl("dlVersionItemList");
                List<ReportFields> items = dtCollectionList[1].FindAll(y => y.CatgoryId == versionItem.CategoryId);
                CategoryId = versionItem.CategoryId;
                if (items.Count > 0)
                {
                    CategoryUniqueVistiors = 0;
                    dlVersionItemList.DataSource = items;
                    dlVersionItemList.DataBind();
                }


            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblTotalSumHitLinkVisitor = e.Item.FindControl("lblTotalSumHitLinkVisitor") as Label;
                Label lblTotalSumTotalOrder = e.Item.FindControl("lblTotalSumTotalOrder") as Label;
                Label lblTotalSumAvgOrder = e.Item.FindControl("lblTotalSumAvgOrder") as Label;
                Label lblTotalSumTotalRev = e.Item.FindControl("lblTotalSumTotalRev") as Label;
                Label lblTotalSumTotalConversion = e.Item.FindControl("lblTotalSumTotalConversion") as Label;
                Label lblTotalSumRevenuePerClick = e.Item.FindControl("lblTotalSumRevenuePerClick") as Label;


                if (TotalCategoryUniqueVistiors > 0)
                    lblTotalSumHitLinkVisitor.Text = string.Format("{0:##,##}", TotalCategoryUniqueVistiors);
                else
                    lblTotalSumHitLinkVisitor.Text = "0";


                if (TotalOrders > 0)
                {
                    lblTotalSumTotalOrder.Text = string.Format("{0:##,##}", TotalOrders);
                    lblTotalSumAvgOrder.Text = String.Format("{0:C}", Math.Round(TotalRevenue / TotalOrders, 2));
                }
                else
                {
                    lblTotalSumTotalOrder.Text = "0";
                    lblTotalSumAvgOrder.Text = "0";
                }

                lblTotalSumTotalRev.Text = String.Format("{0:C}", TotalRevenue);

                if (TotalCategoryUniqueVistiors > 0)
                {
                    lblTotalSumTotalConversion.Text = String.Format("{0}%", Math.Round((TotalOrders * 100) / TotalCategoryUniqueVistiors, 1));
                    lblTotalSumRevenuePerClick.Text = String.Format("{0:C}", Math.Round(TotalRevenue / TotalCategoryUniqueVistiors, 2));
                }
                else
                {
                    lblTotalSumTotalConversion.Text = "0";
                    lblTotalSumRevenuePerClick.Text = "0";
                }


            }


        }



        protected void dlVersionList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            CSBusiness.VersionCategory versionItem = e.Item.DataItem as CSBusiness.VersionCategory;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lblTitle = e.Item.FindControl("lblTitle") as Label;
                Label lblTotalOrder = e.Item.FindControl("lblTotalOrder") as Label;
                Label lblAvgOrder = e.Item.FindControl("lblAvgOrder") as Label;
                Label lblTotalRev = e.Item.FindControl("lblTotalRev") as Label;
                Label lbHitLinkVisitor = e.Item.FindControl("lbHitLinkVisitor") as Label;
                Label lblConversion = e.Item.FindControl("lblConversion") as Label;
                Label lblRevenuePerVisit = e.Item.FindControl("lblRevenuePerVisit") as Label;

                ReportFields item = e.Item.DataItem as ReportFields;

                lblTitle.Text = item.ShortName;
                if (item.TotalOrders > 0)
                    lblTotalOrder.Text = string.Format("{0:##,##}", item.TotalOrders);
                else
                    lblTotalOrder.Text = "0";

                if (item.UniqueVisitors > 0)
                    lbHitLinkVisitor.Text = string.Format("{0:##,##}", item.UniqueVisitors);
                else
                    lbHitLinkVisitor.Text = "0";


                lblConversion.Text = String.Format("{0}%", item.Conversion);
                lblRevenuePerVisit.Text = String.Format("{0:C}", item.RevenuePerVisit);
                lblAvgOrder.Text = String.Format("{0:C}", item.AverageOrder);
                lblTotalRev.Text = String.Format("{0:C}", item.TotalRevenue);

                CategoryUniqueVistiors += item.UniqueVisitors;


                DataList dlUrlList = (DataList)e.Item.FindControl("dlUrlList");
                List<ReportFields> items = dtCollectionList[2].FindAll(y => y.VersionId == item.VersionId);
                if (items.Count > 0)
                {
                    dlUrlList.DataSource = items;
                    dlUrlList.DataBind();
                }
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblSumTotalOrder = e.Item.FindControl("lblSumTotalOrder") as Label;
                Label lblSumAvgOrder = e.Item.FindControl("lblSumAvgOrder") as Label;
                Label lblSumTotalRev = e.Item.FindControl("lblSumTotalRev") as Label;
                Label lblSumHitLinkVisitor = e.Item.FindControl("lblSumHitLinkVisitor") as Label;
                Label lblSumTotalConversion = e.Item.FindControl("lblSumTotalConversion") as Label;
                Label lblSumRevenuePerClick = e.Item.FindControl("lblSumRevenuePerClick") as Label;

                HtmlContainerControl versionFooter = e.Item.FindControl("versionFooter") as HtmlContainerControl;
                ReportFields foundItem = dtCollectionList[0].Find(y => y.CatgoryId == CategoryId);
                if (dtCollectionList[0].Count == 1)
                {
                    versionFooter.Visible = false;

                }
                else
                    versionFooter.Visible = true;
                if (foundItem != null)
                {

                    if (CategoryUniqueVistiors > 0)
                    {
                        lblSumHitLinkVisitor.Text = string.Format("{0:##,##}", CategoryUniqueVistiors);

                        lblSumTotalConversion.Text = String.Format("{0}%", Math.Round((foundItem.TotalOrders * 100) / CategoryUniqueVistiors, 1));
                        lblSumRevenuePerClick.Text = String.Format("{0:C}", Math.Round(foundItem.TotalRevenue / CategoryUniqueVistiors, 2));
                    }
                    else
                    {
                        lblSumHitLinkVisitor.Text = "0";
                        lblSumTotalConversion.Text = "0";
                        lblSumRevenuePerClick.Text = "0";
                    }

                    if (foundItem.TotalOrders > 0)
                        lblSumTotalOrder.Text = string.Format("{0:##,##}", foundItem.TotalOrders);
                    else
                        lblSumTotalOrder.Text = "0";


                    lblSumAvgOrder.Text = String.Format("{0:C}", foundItem.AverageOrder);
                    lblSumTotalRev.Text = String.Format("{0:C}", foundItem.TotalRevenue);

                    TotalCategoryUniqueVistiors += CategoryUniqueVistiors;
                    TotalOrders += foundItem.TotalOrders;
                    TotalRevenue += foundItem.TotalRevenue;
                }

            }


        }
        protected void dlUrlList_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lblTitle = e.Item.FindControl("lblTitle") as Label;
                Label lblTotalOrder = e.Item.FindControl("lblTotalOrder") as Label;
                Label lblAvgOrder = e.Item.FindControl("lblAvgOrder") as Label;
                Label lblTotalRev = e.Item.FindControl("lblTotalRev") as Label;
                Label lbHitLinkVisitor = e.Item.FindControl("lbHitLinkVisitor") as Label;
                Label lblConversion = e.Item.FindControl("lblConversion") as Label;
                Label lblRevenuePerVisit = e.Item.FindControl("lblRevenuePerVisit") as Label;

                ReportFields item = e.Item.DataItem as ReportFields;

                lblTitle.Text = item.ShortName;
                try
                {
                    lblTitle.Text = "&nbsp;&nbsp;&nbsp;" + item.ShortName.Split('-')[0];
                }
                catch
                {

                }
                if (item.TotalOrders > 0)
                    lblTotalOrder.Text = string.Format("{0:##,##}", item.TotalOrders);
                else
                    lblTotalOrder.Text = "0";

                if (item.UniqueVisitors > 0)
                    lbHitLinkVisitor.Text = string.Format("{0:##,##}", item.UniqueVisitors);
                else
                    lbHitLinkVisitor.Text = "0";


                lblConversion.Text = String.Format("{0}%", item.Conversion);
                lblRevenuePerVisit.Text = String.Format("{0:C}", item.RevenuePerVisit);
                lblAvgOrder.Text = String.Format("{0:C}", item.AverageOrder);
                lblTotalRev.Text = String.Format("{0:C}", item.TotalRevenue);
            }
        }

        public void BindSettings()
        {
            if (!Page.IsPostBack)
            {
                SitePref PrefObject = CSFactory.GetSitePreference();
                siteTitle = PrefObject.SiteHeader;
                siteName = PrefObject.SiteName;
                siteUrl = PrefObject.SiteUrl;
                imgLogo.ImageUrl = PrefObject.LogoPath;

                SitePreference sitePreference = CSFactory.GetCacheSitePref();
                sitePreference.LoadAttributeValues();

                if (sitePreference.AttributeValues["hitslinkusername"] != null)
                {
                    hitsLinkUserName = sitePreference.AttributeValues["hitslinkusername"].Value;
                }
                if (sitePreference.AttributeValues["hitslinkpassword"] != null)
                {
                    hitsLinkPassword = sitePreference.AttributeValues["hitslinkpassword"].Value;
                }
            }
        }

        public Dictionary<int, List<ReportFields>> GetVersionSummary(DateTime? startDate, DateTime? endDate, bool includeArchiveData)
        {
            Dictionary<int, List<ReportFields>> item = new Dictionary<int, List<ReportFields>>();
            dtCollectionList = new OrderManager().GetVersionSummary(startDate, endDate, false);

            using (SqlDataReader reader = GetVersionSummaryData(startDate, endDate, includeArchiveData))
            {
                List<ReportFields> CategorySummary = new List<ReportFields>();
                while (reader.Read())
                {
                    ReportFields rowitem = new ReportFields();
                    rowitem.CatgoryId = Int32.Parse(reader["CategoryId"].ToString());
                    rowitem.TotalOrders = Int32.Parse(reader["TotalOrders"].ToString());
                    rowitem.TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                    rowitem.AverageOrder = Convert.ToDecimal(reader["AverageOrder"]);
                    CategorySummary.Add(rowitem);

                }

                item.Add(0, CategorySummary);

                reader.NextResult();

                List<ReportFields> VersionSummary = new List<ReportFields>();
                while (reader.Read())
                {
                    ReportFields rowitem = new ReportFields();
                    rowitem.VersionId = Int32.Parse(reader["VersionId"].ToString());
                    rowitem.CatgoryId = Int32.Parse(reader["CategoryId"].ToString());
                    rowitem.Title = reader["Title"].ToString().ToLower();
                    rowitem.ShortName = reader["ShortName"].ToString();
                    rowitem.TotalOrders = Int32.Parse(reader["TotalOrders"].ToString());
                    rowitem.TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                    rowitem.AverageOrder = Convert.ToDecimal(reader["AverageOrder"]);
                    VersionSummary.Add(rowitem);

                }

                item.Add(1, VersionSummary);

                reader.NextResult();

                //while (reader.Read())
                //{
                //    ReportFields rowitem = new ReportFields();
                //    rowitem.VersionId = Int32.Parse(reader["VersionId"].ToString());
                //    rowitem.Title = reader["URL"].ToString().ToLower();
                //    rowitem.ShortName = reader["URL"].ToString();
                //    rowitem.TotalOrders = Int32.Parse(reader["TotalOrders"].ToString());
                //    rowitem.TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                //    rowitem.AverageOrder = Convert.ToDecimal(reader["AverageOrder"]);
                //    UrlSummary.Add(rowitem);
                //}
                //reader.NextResult();
            }
            using (SqlDataReader reader = GetURLSummaryData(startDate, endDate, includeArchiveData))
            {
                List<ReportFields> UrlSummary = new List<ReportFields>();
                while (reader.Read())
                {
                    ReportFields rowitem = new ReportFields();
                    rowitem.VersionId = Int32.Parse(reader["VersionId"].ToString());
                    rowitem.Title = reader["URL"].ToString().ToLower();
                    rowitem.CategoryTitle = reader["Title"].ToString().ToLower();
                    rowitem.ShortName = reader["URL"].ToString();
                    rowitem.TotalOrders = Int32.Parse(reader["TotalOrders"].ToString());
                    rowitem.TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                    rowitem.AverageOrder = Convert.ToDecimal(reader["AverageOrder"]);
                    UrlSummary.Add(rowitem);
                }

                item.Add(2, UrlSummary);

            }
            return item;
        }

        #region Datalayer

        private static SqlDataReader GetVersionSummaryData(DateTime? startDate, DateTime? endDate, bool includeArchiveData)
        {
            return GetData("pr_report_order_version", startDate, endDate, includeArchiveData);
        }

        private static SqlDataReader GetURLSummaryData(DateTime? startDate, DateTime? endDate, bool includeArchiveData)
        {
            return GetData("pr_report_order_url_detail", startDate, endDate, includeArchiveData);
        }

        public static SqlDataReader GetData(string ProcName, DateTime? startDate, DateTime? endDate, bool includeArchiveData)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            //String ProcName = "pr_report_order_url_detail";
            SqlParameter[] ParamVal = new SqlParameter[3];
            ParamVal[0] = new SqlParameter("@startDate", startDate);
            ParamVal[1] = new SqlParameter("@endDate", endDate);
            ParamVal[2] = new SqlParameter("@includeArchiveData", includeArchiveData);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);
        }
        #endregion Datalayer
    }
}