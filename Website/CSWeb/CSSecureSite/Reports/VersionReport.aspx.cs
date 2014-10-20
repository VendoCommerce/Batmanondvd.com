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
using System.Web.UI;
using InfoSoftGlobal;
using System.Drawing;

namespace CSWeb.Admin.Reports
{
    public partial class VersionReport : BasePage
    {
        protected Dictionary<int, List<VersionFieldsReport>> dtCollectionList;
        public int CategoryId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rangeDateControlCriteria.StartDateValueLocal = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                rangeDateControlCriteria.EndDateValueLocal = DateTime.Now.Date;
                BindData(rangeDateControlCriteria.StartDateValueLocal, DateTimeUtil.GetEndDate(rangeDateControlCriteria.EndDateValueLocal));
                
            }


        }

         public string CreateCharts(List<VersionFieldsReport> Items)
        {

            //Create an XML data document in a string variable
            string strXML = string.Empty;
            strXML += "<graph  caption='Version Trafic Report' animation='1'  formatNumberScale='0'   pieSliceDepth='30'  decimalPrecision='0' shownames='1' >";
            Random rnd = new Random();
            foreach(VersionFieldsReport Item in Items)
            {
                Color RandomColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                strXML += "<set name='" + Item.ShortName + "' value='" + Item.TotalOrders + "' color='" + RandomColor.Name + "' />";
            }
        
            strXML += "</graph>";

            //Create the chart - Column 3D Chart with data from strXML variable using dataXML method
            return FusionCharts.RenderChartHTML("../../FusionCharts/FCF_pie3D.swf", "", strXML, "versionChart", "600", "300", false);

        }

        protected void BindData(DateTime? startDate, DateTime? endDate)
        {
            dtCollectionList = new OrderManager().GetVersionSummary(startDate, endDate, cbArchive.Checked); 
            dlVersionCategoryList.DataSource = CSFactory.GetAllVersionCateogry();
            dlVersionCategoryList.DataBind();
         
            FCLiteral.Text = CreateCharts(dtCollectionList[1]);
           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindData(rangeDateControlCriteria.StartDateValueLocal, DateTimeUtil.GetEndDate(rangeDateControlCriteria.EndDateValueLocal));

        }


        protected void dlVersionCategoryList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            CSBusiness.VersionCategory versionItem = e.Item.DataItem as CSBusiness.VersionCategory;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lblCategory = e.Item.FindControl("lblCategory") as Label;
             
                lblCategory.Text = versionItem.Title;
                DataList dlVersionItemList = (DataList)e.Item.FindControl("dlVersionItemList");
                List<VersionFieldsReport> items = dtCollectionList[1].FindAll(y => y.CatgoryId == versionItem.CategoryId);
                CategoryId = versionItem.CategoryId;
                if (items.Count > 0)
                {
                    
                    dlVersionItemList.DataSource = items;
                    dlVersionItemList.DataBind();
                }
        
            }

     
        }


 
        protected void dlVersionList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
          
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblTitle = e.Item.FindControl("lblTitle") as Label;
                Label lblTotalOrder = e.Item.FindControl("lblTotalOrder") as Label;
                Label lblAvgOrder = e.Item.FindControl("lblAvgOrder") as Label;
                Label lblTotalRev = e.Item.FindControl("lblTotalRev") as Label;

                VersionFieldsReport item = e.Item.DataItem as VersionFieldsReport;
                lblTitle.Text = item.ShortName;
                lblTotalOrder.Text = item.TotalOrders.ToString();
                lblAvgOrder.Text = String.Format("${0:0.##}", item.AverageOrder);
                lblTotalRev.Text = String.Format("${0:0.##}", item.TotalRevenue);
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblSumTotalOrder = e.Item.FindControl("lblSumTotalOrder") as Label;
                  Label lblSumAvgOrder = e.Item.FindControl("lblSumAvgOrder") as Label;
                  Label lblSumTotalRev = e.Item.FindControl("lblSumTotalRev") as Label;
                VersionFieldsReport foundItem = dtCollectionList[0].Find(y => y.CatgoryId == CategoryId);
                if (foundItem != null)
                {
                    lblSumTotalOrder.Text = foundItem.TotalOrders.ToString();
                    lblSumAvgOrder.Text = String.Format("${0:0.##}", foundItem.AverageOrder);
                    lblSumTotalRev.Text = String.Format("${0:0.##}", foundItem.TotalRevenue);
                }
                
            }


        }

    }
}