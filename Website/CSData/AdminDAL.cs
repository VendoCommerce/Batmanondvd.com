using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSCore.DataHelper;
using System.Data.SqlClient;
using System.Data;
using CSCore;

namespace CSData
{
    public class AdminDAL
    {

        static AdminDAL()
        {
        }

        #region Site Preference

        public static SitePref GetSitePreference()
        {

            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {

                var query = from c in context.SitePrefs
                            select c;
                return (SitePref)query.First();
            }
        }

        public static void SavePreference(DateTime pathorderDate, string currencyName, bool includeShippingTax, int orderProcess, bool geoTarget, bool paymentGateway, bool fullFillmentHouse, string title, string imagePath, int days, string siteName, string siteUrl)
        {
            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {
                SitePref Item = context.SitePrefs.First();
                if (Item != null)
                {
                    Item.PathOrderDate = pathorderDate;
                    Item.Currency = currencyName;
                    Item.OrderTotalShipping = includeShippingTax;
                    Item.OrderProcessType = orderProcess;
                    Item.ArchiveData = days;
                    Item.GeoTargetService = geoTarget;
                    Item.PaymentGatewayService = paymentGateway;
                    Item.FulfillmentHouseService = fullFillmentHouse;
                    Item.SiteHeader = title;
                    Item.LogoPath = imagePath;
                    Item.SiteName = siteName;
                    Item.SiteUrl = siteUrl;
                    context.SubmitChanges();
                }
                else
                {
                    SitePref item = new SitePref
                    {

                        PathOrderDate = pathorderDate,
                        Currency = currencyName,
                        OrderTotalShipping = includeShippingTax,
                        OrderProcessType = orderProcess,
                        GeoTargetService = geoTarget,
                        PaymentGatewayService = paymentGateway,
                        SiteHeader = title,
                        LogoPath = imagePath,
                        ArchiveData = days,
                        SiteName = siteName,
                        SiteUrl = siteUrl
                    };

                    context.SitePrefs.InsertOnSubmit(item);
                    context.SubmitChanges();
                }

            }
        }

        #endregion Site Preference

        #region

        public static SqlDataReader GetCoupon(int couponId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_coupon";
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("couponId", couponId);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);

        }

        public static void RemoveCoupon(int couponId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_remove_coupon";
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("couponId", couponId);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
        }

        public static void UpdateCoupon(int couponId, string title, decimal discount, decimal total, int discountType,
             int skuId, int relatedSkuId, int itemDiscountType, decimal itemDiscount, bool active, bool includeShipping)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_coupon";
            SqlParameter[] ParamVal = new SqlParameter[10];
            ParamVal[0] = new SqlParameter("@couponId", couponId);
            ParamVal[1] = new SqlParameter("@title", title);
            ParamVal[2] = new SqlParameter("@discount", discount);
            ParamVal[3] = new SqlParameter("@total", total);
            ParamVal[4] = new SqlParameter("@discountType", discountType);
            ParamVal[5] = new SqlParameter("@skuId", skuId);
            ParamVal[6] = new SqlParameter("@relatedskuId", relatedSkuId);
            ParamVal[7] = new SqlParameter("@itemdiscounttype", itemDiscountType);
            ParamVal[8] = new SqlParameter("@itemdiscount", itemDiscount);
            ParamVal[9] = new SqlParameter("@includeShipping", includeShipping);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
        }

        #endregion

        #region vesion

        public static SqlDataReader GetAllSites()
        {
            string connectionString = ConfigHelper.GetMasterDBConnection();
            String ProcName = "pr_get_all_sites";

            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, null);
        }

        public static SqlDataReader GetSiteDetails(int siteId)
        {
            string connectionString = ConfigHelper.GetMasterDBConnection();
            String ProcName = "pr_get_sites_details";

            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[1] = new SqlParameter("@SiteId", siteId);

            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, ParamVal);
        }

        public static SqlDataReader GetVersion()
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_version";

            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, null);
        }

        public static SqlDataReader GetAllVersionCateogry()
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_versioncategory";

            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, null);

        }

        public static void RemoveVersionCategory(int categoryId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_remove_versioncategory";
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("categoryId", categoryId);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
        }

        public static void SaveVersionCategory(int categoryId, string CategoryName)
        {
            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {
                VersionCategory Item = context.VersionCategories.FirstOrDefault(x => x.CategoryId == categoryId);
                if (Item != null)
                {
                    Item.Title = CategoryName;
                    context.SubmitChanges();
                }
                else
                {
                    VersionCategory item = new VersionCategory
                    {

                        Title = CategoryName,
                        Visible = true
                    };

                    context.VersionCategories.InsertOnSubmit(item);
                    context.SubmitChanges();
                }

            }
        }



        /// <summary>
        /// LINQ to SQL Connection for Commitment
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <param name="Ordernum"></param>
        public static void SaveVersion(string versionName, string shortName, bool visible, int categoryId)
        {
            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {

                Version verItem = new Version
                {

                    Title = versionName,
                    ShortName = shortName,
                    Visible = visible,
                    CategoryId = categoryId
                };

                context.Versions.InsertOnSubmit(verItem);
                context.SubmitChanges();

            }
        }



        public static void RemoveVersion(int versionId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_remove_version";
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("versionId", versionId);

            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);

        }

        public static void UpdateVersion(int versionId, string Name, string shortName, bool active, int categoryId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_version";
            SqlParameter[] ParamVal = new SqlParameter[5];
            ParamVal[0] = new SqlParameter("versionId", versionId);
            ParamVal[1] = new SqlParameter("title", Name);
            ParamVal[2] = new SqlParameter("shortName", shortName);
            ParamVal[3] = new SqlParameter("active", (active) ? 1 : 0);
            ParamVal[4] = new SqlParameter("categoryId", categoryId);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);

        }
        #endregion vesion

        #region Resource

        public static void RemoveResource(int resourceId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_remove_resource";
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = new SqlParameter("@resourceId", resourceId);
            BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters);
        }

        public static void UpdateResource(int resourceId, string key, string Value)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_resource";
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@resourceId", resourceId);
            Parameters[1] = new SqlParameter("@key", key);
            Parameters[2] = new SqlParameter("@value", Value);
            BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters);
        }

        public static SqlDataReader GetResource()
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_resource";
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, null);

        }

        #endregion Resource

        #region Country&States
        public static SqlDataReader GetAllMasterCountries()
        {
            string connectionString = ConfigHelper.GetMasterDBConnection();
            String ProcName = "pr_get_all_country";

            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, null);

        }

        public static DataTable GetAllErrors(string startDate, string endDate, int startRec, int endRec, out int totalCount)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_errors";
            SqlParameter[] Parameters = new SqlParameter[5];
            Parameters[0] = new SqlParameter("@startDate", startDate);
            Parameters[1] = new SqlParameter("@endDate", endDate);
            Parameters[2] = new SqlParameter("@startRec", startRec);
            Parameters[3] = new SqlParameter("@endRec", endRec);
            SqlParameter paramter1 = new SqlParameter("@totalCount", SqlDbType.Int);
            paramter1.Direction = ParameterDirection.Output;
            Parameters[4] = paramter1;
            DataTable dtTable = new DataTable();

            BaseSqlHelper.Execute(connectionString, ProcName, Parameters, dtTable, false);
            totalCount = Convert.ToInt32(Parameters[4].Value);
            return dtTable;
        }

        public static SqlDataReader GetAllMasterStates(int countryId)
        {
            string connectionString = ConfigHelper.GetMasterDBConnection();
            String ProcName = "pr_get_all_states";
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = new SqlParameter("@countryId", countryId);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters);

        }

        public static SqlDataReader GetAllStates(int countryId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_all_states";
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = new SqlParameter("@countryId", countryId);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters);

        }

        public static void SaveStates(string stateXML)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_states";
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = new SqlParameter("@statexml", stateXML);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, Parameters);

        }

        public static SqlDataReader SaveCountry(List<Triplet<int, int, int>> Values)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_country";
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = new SqlParameter("@TableParam", Values.ToTableType());
            Parameters[0].SqlDbType = SqlDbType.Structured;
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters);

        }

        public static void CreateCountry(string countryXML)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_insert_country";

            try
            {
                SqlParameter[] Parameters = new SqlParameter[1];
                Parameters[0] = new SqlParameter("@countryXML", countryXML);
                BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, Parameters);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("CreateCountry() error: " + ex.Message, ex);
            }
        }

        #endregion Country&States

        #region Tax Region
        public static SqlDataReader GetTax(int countryId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_tax_by_country";
            SqlParameter[] Parameters = new SqlParameter[1];
            if (countryId == 0)
                Parameters[0] = new SqlParameter("@countryId", System.DBNull.Value);
            else
                Parameters[0] = new SqlParameter("@countryId", countryId);


            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters);

        }
        public static void SaveTaxRegion(int countryId, int stateId, decimal value)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_insert_taxregion";

            try
            {
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@countryId", countryId);
                if (stateId == 0)
                    Parameters[1] = new SqlParameter("@stateId", System.DBNull.Value);
                else
                    Parameters[1] = new SqlParameter("@stateId", stateId);
                Parameters[2] = new SqlParameter("@value", value);
                BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, Parameters);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("CreateCountry() error: " + ex.Message, ex);
            }
        }

        public static void SaveCountryTax(Dictionary<int, decimal> Values)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_taxregion";
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = new SqlParameter("@TableParam", Values.ToTableType());
            Parameters[0].SqlDbType = SqlDbType.Structured;
            BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters);

        }


        #endregion Tax Region

        #region Providers
        //Sri Comment: Make sure to set Parameter to helper class
        public static SqlDataReader GetAllProviders(bool active)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_all_provider";
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = new SqlParameter("@active", active);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters);

        }

        public static void SaveProvider(int providerId, string providerName, string providerXML)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_insert_paymentprovider";
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@providerId", providerId);
            Parameters[1] = new SqlParameter("@providerName", providerName);
            Parameters[2] = new SqlParameter("@providerXml", providerXML);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, Parameters);

        }

        public static SqlDataReader SaveProvider(List<Triplet<int, int, int>> Values)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_paymentprovider";
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = new SqlParameter("@TableParam", Values.ToTableType());
            Parameters[0].SqlDbType = SqlDbType.Structured;
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters);

        }


        public static void RemoveProvider(int providerId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_remove_paymentprovider";
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = new SqlParameter("@providerId", providerId);

            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, Parameters);

        }

        #endregion Providers

        #region Email
        public static SqlDataReader GetAllEmailList(int emailId)
        {

            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_all_emails";
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = new SqlParameter("@emailId", emailId);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters);

        }

        public static void RemoveEmail(int emailId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_remove_email";
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = new SqlParameter("@emailId", emailId);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, Parameters);

        }

        public static void SaveEmail(int emailId, string title, string fromAddress, string toAddress, string subject, string body)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_email";
            SqlParameter[] Parameters = new SqlParameter[6];
            Parameters[0] = new SqlParameter("@emailId", emailId);
            Parameters[1] = new SqlParameter("@title", title);
            Parameters[2] = new SqlParameter("@subject", subject);
            Parameters[3] = new SqlParameter("@fromAddress", fromAddress);
            Parameters[4] = new SqlParameter("@toAddress", toAddress);
            Parameters[5] = new SqlParameter("@body", body);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, Parameters);
        }

        #endregion Email

        #region FulfillmentHouseProviders
        //Sri Comment: Make sure to set Parameter to helper class
        public static SqlDataReader GetAllFulfillmentHouseProviders(bool active)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_all_fulfillmenthouseprovider";
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = new SqlParameter("@active", active);
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters);

        }

        public static void SaveFulfillmentHouseProvider(int providerId, string providerName, string providerXML)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_insert_fulfillmenthouseprovider";
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@providerId", providerId);
            Parameters[1] = new SqlParameter("@providerName", providerName);
            Parameters[2] = new SqlParameter("@providerXml", providerXML);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, Parameters);

        }

        public static SqlDataReader SaveFulfillmentHouseProvider(List<Triplet<int, int, int>> Values)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_fulfillmenthouseprovider";
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = new SqlParameter("@TableParam", Values.ToTableType());
            Parameters[0].SqlDbType = SqlDbType.Structured;
            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, Parameters);

        }


        public static void RemoveFulfillmentHouseProvider(int providerId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_remove_fulfillmenthouseprovider";
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = new SqlParameter("@providerId", providerId);

            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, Parameters);

        }

        #endregion FulfillmentHouseProviders

    }//end of the class


}
