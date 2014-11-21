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

namespace CSWeb.Admin.Reports
{
    public partial class MIdReport : BasePage
    {
        public Hashtable HitLinkVisitor = new Hashtable();
        public decimal CategoryUniqueVistiors = 0, totalRevenue=0;
        public int totalOrders = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                liHeader.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Day.ToString() + ", " + DateTime.Now.Year.ToString();
                liSubHeader.Text = DateTime.Now.DayOfWeek + " " + DateTime.Now.AddHours(3).ToShortTimeString() + " (EST)";

                ddlVersion.DataSource = CSFactory.GetAllVersion();
                ddlVersion.DataTextField = "Title";
                ddlVersion.DataValueField = "VersionId";
                ddlVersion.DataBind();
                ddlVersion.Items.Insert(0, new ListItem("Select", "0"));

                if (Request.Cookies["FromDate"] != null && Request.Cookies["ToDate"] != null)
                {
                    rangeDateControlCriteria.StartDateValueLocal = Convert.ToDateTime(Request.Cookies["FromDate"]);
                    rangeDateControlCriteria.EndDateValueLocal = Convert.ToDateTime(Request.Cookies["ToDate"]);
                }
                else
                {

                    rangeDateControlCriteria.StartDateValueLocal = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    rangeDateControlCriteria.EndDateValueLocal = DateTime.Now.Date;

                }

                BindData(rangeDateControlCriteria.StartDateValueLocal, rangeDateControlCriteria.EndDateValueLocal);
            }
        }

        protected void BindData(DateTime? startDate, DateTime? endDate)
        {

            DateTime? timezoneStartDate = DateTimeUtil.GetEastCoastStartDate(rangeDateControlCriteria.StartDateValueLocal);
            DateTime? timezoneEndDate = DateTimeUtil.GetEastCoastDate(rangeDateControlCriteria.EndDateValueLocal);
            List<ReportFields> ItemList = new OrderManager().GetOrderCustomFieldReport(timezoneStartDate, timezoneEndDate, 1,  false);

            Data rptData = new ReportWSSoapClient().GetDataFromTimeframe("tgelman", "china2006", ReportsEnum.eCommerceActivitySummary, TimeFrameEnum.Daily, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate), 100000000, 0, 0);
            for (int i = 0; i <= rptData.Rows.GetUpperBound(0); i++)
            {
                HitLinkVisitor.Add(rptData.Rows[i].Columns[0].Value.ToLower(), rptData.Rows[i].Columns[7].Value);
            }

            //Update Version List information
            foreach (ReportFields item in ItemList)
            {
                if (HitLinkVisitor.ContainsKey(item.Title))
                {
                    decimal visitor = Convert.ToDecimal(HitLinkVisitor[item.Title].ToString());
                    visitor = Math.Abs(visitor);
                    item.UniqueVisitors = visitor;
                    if (visitor > 0)
                    {

                        item.Conversion = Math.Round((Convert.ToDecimal(item.TotalOrders) * 100) / visitor, 1);
                    item.RevenuePerVisit = Convert.ToDecimal(item.TotalRevenue) / visitor;
                    }
                    else
                    {
                        item.UniqueVisitors = 0;
                        item.Conversion = 0;
                    item.RevenuePerVisit = 0;
                    }
                }
                else
                {
                    item.UniqueVisitors = 0;
                    item.Conversion = 0;
                item.RevenuePerVisit = 0;
                }
            }


            dlVersionList.DataSource = ItemList;
            dlVersionList.DataBind();

            //FCLiteral.Text = CreateCharts(dtCollectionList[1dtCollectionList

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan(24, 0, 0);
            CommonHelper.SetCookie("FromDate", rangeDateControlCriteria.StartDateValueLocal.Value.ToShortDateString(), ts);
            CommonHelper.SetCookie("ToDate", rangeDateControlCriteria.EndDateValueLocal.Value.ToShortDateString(), ts);

            BindData(rangeDateControlCriteria.StartDateValueLocal, rangeDateControlCriteria.EndDateValueLocal);

        }

        protected void dlVersionList_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lblTitle = e.Item.FindControl("lblTitle") as Label;
                Label lblTotalOrder = e.Item.FindControl("lblTotalOrder") as Label;
                Label lblTotalRev = e.Item.FindControl("lblTotalRev") as Label;
                Label lbHitLinkVisitor = e.Item.FindControl("lbHitLinkVisitor") as Label;
                Label lblConversion = e.Item.FindControl("lblConversion") as Label;             

                ReportFields item = e.Item.DataItem as ReportFields;

                lblTitle.Text = item.Title;
                lblTotalOrder.Text = item.TotalOrders.ToString();
                if( item.UniqueVisitors >0)
                     lbHitLinkVisitor.Text = string.Format("{0:##,##}", item.UniqueVisitors);
                else
                    lbHitLinkVisitor.Text = "0";

                lblConversion.Text = String.Format("{0}", item.Conversion);

                lblTotalRev.Text = String.Format("{0:0.##}", Math.Round(item.TotalRevenue, 2).ToString("n2"));

                CategoryUniqueVistiors += item.UniqueVisitors;
                totalOrders += item.TotalOrders;
                totalRevenue +=  item.TotalRevenue;

            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblSumTotalOrder = e.Item.FindControl("lblSumTotalOrder") as Label;
                Label lblSumTotalRev = e.Item.FindControl("lblSumTotalRev") as Label;
                Label lblSumHitLinkVisitor = e.Item.FindControl("lblSumHitLinkVisitor") as Label;
                Label lblSumTotalConversion = e.Item.FindControl("lblSumTotalConversion") as Label;

                    
                    if (CategoryUniqueVistiors > 0)
                    {
                        lblSumTotalConversion.Text = String.Format("{0}", Math.Round((totalOrders * 100) / CategoryUniqueVistiors, 2));
                        lblSumHitLinkVisitor.Text = string.Format("{0:##,##}", CategoryUniqueVistiors);
                    }
                    else
                    {
                        lblSumTotalConversion.Text = "0";
                        lblSumHitLinkVisitor.Text = "0";
                    }

                    lblSumTotalOrder.Text = totalOrders.ToString();
                    lblSumTotalRev.Text = String.Format("{0:C}", totalRevenue);
           
            }


        }
    }
}