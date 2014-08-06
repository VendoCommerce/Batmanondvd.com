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
using CSCore.Utils;
using System.Web.UI;
using CSData;

namespace CSWeb.Admin
{
    public partial class ClientReport : BasePage
    {
        public int totalCount = 0;
        public static string siteName = string.Empty;
        public static string siteUrl = string.Empty;
        public static string siteTitle = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            BindSettings();

            if (!IsPostBack)
            {
                this.BaseLoad();
                liHeader.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Day.ToString() + ", " + DateTime.Now.Year.ToString();
                liSubHeader.Text = DateTime.Now.DayOfWeek + " " + DateTime.Now.AddHours(3).ToShortTimeString() + " (EST)";

                ddlVersion.DataSource = CSFactory.GetAllVersion().FindAll(x => x.Visible == true);
                ddlVersion.DataTextField = "Title";
                ddlVersion.DataValueField = "VersionId";
                ddlVersion.DataBind();
                ddlVersion.Items.Insert(0, new ListItem("All", "0"));

                //ddlPaths.DataSource = new PathManager().GetAllPaths(false);
                //ddlPaths.DataTextField = "Title";
                //ddlPaths.DataValueField = "PathId";
                //ddlPaths.DataBind();
                //ddlPaths.Items.Insert(0, new ListItem("Select", "0"));

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

                BindData(DateTimeUtil.GetEastCoastStartDate(rangeDateControlCriteria.StartDateValueLocal), DateTimeUtil.GetEastCoastDate(rangeDateControlCriteria.EndDateValueLocal), 0, 0);
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

        private void BindData(DateTime? dte1, DateTime? dte2, int versionId, int pathId)
        {

            List<Triplet<string, string, string>> itemList = new List<Triplet<string, string, string>>();


            using (SqlDataReader drResult = new OrderManager().GetOrderSummary(dte1, dte2, versionId, pathId))
            {

                while (drResult.Read())
                {
                    decimal totalRevenue = Convert.ToDecimal(drResult["TotalRevenue"]);
                    totalCount = Convert.ToInt32(drResult["TotalOrders"].ToString());
                    decimal averageOrderVal = 0;

                    if (totalCount > 0)
                        averageOrderVal = Math.Round(totalRevenue / Convert.ToInt32(totalCount), 2);


                    itemList.Add(new Triplet<string, string, string>("<span style='color:red;font-weight:bold'>Total Revenue</span>", "<span style='color:red;font-weight:bold'>" + String.Format("{0:C}", drResult["TotalRevenue"]) + "</span>", String.Empty));
                    itemList.Add(new Triplet<string, string, string>("Total Orders", string.Format("{0:##,##}", totalCount), String.Empty));
                    itemList.Add(new Triplet<string, string, string>("Average Order Value", String.Format("{0:C}", averageOrderVal), String.Empty));
                    itemList.Add(new Triplet<string, string, string>("&nbsp", "&nbsp", "&nbsp "));

                    itemList.Add(new Triplet<string, string, string>("Product Revenue", String.Format("{0:C}", drResult["ProductRevenue"]), GetPercentage(Convert.ToDecimal(drResult["ProductRevenue"]), totalRevenue)));
                    itemList.Add(new Triplet<string, string, string>("Shipping Revenue", String.Format("{0:C}", drResult["ShippingRevenue"]), GetPercentage(Convert.ToDecimal(drResult["ShippingRevenue"]), totalRevenue)));
                    itemList.Add(new Triplet<string, string, string>("Sales Tax Revenue", String.Format("{0:C}", drResult["SalesTaxRevenue"]), GetPercentage(Convert.ToDecimal(drResult["SalesTaxRevenue"]), totalRevenue)));
                    itemList.Add(new Triplet<string, string, string>("Orders With Upsells", string.Format("{0:##,##}", drResult["UpsellItems"]), GetPercentage(Convert.ToDecimal(drResult["UpsellItems"]), decimal.Parse(totalCount.ToString()))));

                    itemList.Add(new Triplet<string, string, string>("&nbsp", "&nbsp", "&nbsp "));

                    itemList.Add(new Triplet<string, string, string>("Orders w/ 1 Upsell", string.Format("{0:##,##}", drResult["oneUpSellCount"]), GetPercentage(Convert.ToDecimal(drResult["oneUpSellCount"]), decimal.Parse(totalCount.ToString()))));
                    itemList.Add(new Triplet<string, string, string>("Orders w/ 2 Upsell", string.Format("{0:##,##}", drResult["TwoUpSellCount"]), GetPercentage(Convert.ToDecimal(drResult["TwoUpSellCount"]), decimal.Parse(totalCount.ToString()))));
                    itemList.Add(new Triplet<string, string, string>("Orders w/ 3 Upsell", string.Format("{0:##,##}", drResult["ThreeUpSellCount"]), GetPercentage(Convert.ToDecimal(drResult["ThreeUpSellCount"]), decimal.Parse(totalCount.ToString()))));
                    itemList.Add(new Triplet<string, string, string>("Orders w/ 4 Upsell", string.Format("{0:##,##}", drResult["FourUpSellCount"]), GetPercentage(Convert.ToDecimal(drResult["FourUpSellCount"]), decimal.Parse(totalCount.ToString()))));
                    itemList.Add(new Triplet<string, string, string>("Orders w/ 5 Upsell", string.Format("{0:##,##}", drResult["FiveUpSellCount"]), GetPercentage(Convert.ToDecimal(drResult["FiveUpSellCount"]), decimal.Parse(totalCount.ToString()))));
                    itemList.Add(new Triplet<string, string, string>("Orders w/ 6 Upsell", string.Format("{0:##,##}", drResult["SixUpSellCount"]), GetPercentage(Convert.ToDecimal(drResult["SixUpSellCount"]), decimal.Parse(totalCount.ToString()))));

                } //end of while loop



                rptTotals.DataSource = itemList;
                rptTotals.DataBind();

                drResult.NextResult();
                rptTotalsItem.DataSource = drResult;
                rptTotalsItem.DataBind();


            }//end of using




        }


        protected void rptTotalsItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {


                Label lblPercentage = e.Item.FindControl("lblPercentage") as Label;
                decimal Qty = Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Qty"));
                if (totalCount > 0)
                    lblPercentage.Text = GetPercentage(Qty, decimal.Parse(totalCount.ToString()));
                else
                    lblPercentage.Text = "--";



            }

        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //TimeSpan ts = new TimeSpan(24, 0, 0);
            //CommonHelper.SetCookie("FromDate", rangeDateControlCriteria.StartDateValueLocal.Value.ToShortDateString(), ts);
            //CommonHelper.SetCookie("ToDate", rangeDateControlCriteria.EndDateValueLocal.Value.ToShortDateString(), ts);

            Session["FilterFromDate"] = rangeDateControlCriteria.StartDateValueLocal.Value.ToShortDateString();
            Session["FilterToDate"] = rangeDateControlCriteria.EndDateValueLocal.Value.ToShortDateString();

            BindData(DateTimeUtil.GetEastCoastStartDate(rangeDateControlCriteria.StartDateValueLocal), DateTimeUtil.GetEastCoastDate(rangeDateControlCriteria.EndDateValueLocal), Convert.ToInt32(ddlVersion.SelectedValue), 0);

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
            }
        }
    }
}