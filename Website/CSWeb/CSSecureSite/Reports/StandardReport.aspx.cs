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

namespace CSWeb.Admin.Reports
{
    public partial class StandardReport : BasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                rangeDateControlCriteria.StartDateValueLocal = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                rangeDateControlCriteria.EndDateValueLocal = DateTime.Now.Date;
                ddlVersion.DataSource = CSFactory.GetAllVersion();
                ddlVersion.DataTextField = "Title";
                ddlVersion.DataValueField = "VersionId";
                ddlVersion.DataBind();
                ddlVersion.Items.Insert(0, new ListItem("Select", "0"));

                ddlPaths.DataSource = new PathManager().GetAllPaths(13,false);
                ddlPaths.DataTextField = "Title";
                ddlPaths.DataValueField = "PathId";
                ddlPaths.DataBind();
                ddlPaths.Items.Insert(0, new ListItem("Select", "0"));


                BindData(rangeDateControlCriteria.StartDateValueLocal, DateTimeUtil.GetEndDate(rangeDateControlCriteria.EndDateValueLocal), 0, 0, rptTotals, rptTotalsItem);
            }


        }

        private string GetPercentage(decimal decimal1, decimal decimal2)
        {
            if (decimal1 <= 0)
                return "--";

            if (decimal2 <= 0)
                return "--";

            return (decimal1 / decimal2).ToString("P1");
        }
        private string GetPercentage(int number1, int number2)
        {
            if (number1 <= 0)
                return "--";

            if (number2 <= 0)
                return "--";

            return (number1 / number2).ToString("P1");
        }
        private string GetDollarAmount(decimal decimal1, int number)
        {
            if (decimal1 <= 0)
                return 0.ToString("C2");

            if (number <= 0)
                return 0.ToString("C2");

            return (decimal1 / number).ToString("C2");
        }

        private void BindData(DateTime? dte1, DateTime? dte2, int versionId, int pathId, Repeater itemRepeater, Repeater itemListRepeater)
        {

            List<Triplet<string, string, string>> itemList = new List<Triplet<string, string, string>>();


            using (SqlDataReader drResult = new OrderManager().GetOrderSummary(dte1, dte2, versionId, pathId))
            {

                while (drResult.Read())
                {
                    decimal totalRevenue = Convert.ToDecimal(drResult["TotalRevenue"]);
                    string totalCount = drResult["TotalOrders"].ToString();
                    decimal averageOrderVal = 0;

                    if (Int32.Parse(totalCount) > 0)
                        averageOrderVal = Math.Round(totalRevenue / Convert.ToInt32(totalCount), 2);


                    itemList.Add(new Triplet<string, string, string>("<span style='color:red;font-weight:bold'>Total Revenue</span>", "<span style='color:red;font-weight:bold'>" + String.Format("{0:0.##}", drResult["TotalRevenue"]) + "</span>", String.Empty));
                    itemList.Add(new Triplet<string, string, string>("Total Orders", totalCount.ToString(), String.Empty));
                    itemList.Add(new Triplet<string, string, string>("Average Order Value", String.Format("{0:0.##}",averageOrderVal), String.Empty));
                    itemList.Add(new Triplet<string, string, string>("&nbsp", "&nbsp", "&nbsp "));

                    itemList.Add(new Triplet<string, string, string>("Product Revenue", String.Format("{0:0.##}",drResult["ProductRevenue"]), GetPercentage(Convert.ToDecimal(drResult["ProductRevenue"]), totalRevenue)));
                    itemList.Add(new Triplet<string, string, string>("Shipping Revenue", String.Format("{0:0.##}",drResult["ShippingRevenue"]), GetPercentage(Convert.ToDecimal(drResult["ShippingRevenue"]), totalRevenue)));
                    itemList.Add(new Triplet<string, string, string>("Sales Tax Revenue",String.Format("{0:0.##}", drResult["SalesTaxRevenue"]), GetPercentage(Convert.ToDecimal(drResult["SalesTaxRevenue"]), totalRevenue)));
                    itemList.Add(new Triplet<string, string, string>("Orders With Upsells", drResult["UpsellItems"].ToString(), GetPercentage(Convert.ToDecimal(drResult["UpsellItems"]), decimal.Parse(totalCount))));

                    itemList.Add(new Triplet<string, string, string>("&nbsp", "&nbsp", "&nbsp "));

                }

                drResult.NextResult();
                itemListRepeater.DataSource = drResult;
                itemListRepeater.DataBind();

                itemRepeater.DataSource = itemList;
                itemRepeater.DataBind();
            }




        }




      

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindData(rangeDateControlCriteria.StartDateValueLocal, DateTimeUtil.GetEndDate(rangeDateControlCriteria.EndDateValueLocal), Convert.ToInt32(ddlVersion.SelectedValue), Convert.ToInt32(ddlPaths.SelectedValue), rptTotals, rptTotalsItem);
          
        }


    }
}