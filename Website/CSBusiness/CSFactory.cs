using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using CSData;
using CSBusiness.Shipping;
using CSBusiness.Preference;
using CSCore.Logging;
using System.Data;
using CSBusiness.ShoppingManagement;
using CSBusiness.Cache;
using System.Web;
using CSCore.DataHelper;
using CSBusiness.Coupon;


namespace CSBusiness
{
    public class CSFactory
    {

        public CSFactory()
        {
        }

        public static SitePref GetSitePreference()
        {
            return AdminDAL.GetSitePreference();
        }

        public static void SavePreference(DateTime pathorderDate, string currency, bool includeShippingTax, int orderProcess, bool GeoTarget, bool PaymentGateway, bool fullFillmentHouse,
            string title, string imagePath, int days, string siteName, string siteUrl)
        {
            AdminDAL.SavePreference(pathorderDate, currency, includeShippingTax, orderProcess, GeoTarget, PaymentGateway, fullFillmentHouse, title, imagePath, days, siteName, siteUrl);
        }

        #region SkuManager

        public SkuManager SkuManager()
        {
            SkuManager skuManager = new SkuManager();
            return skuManager;
        }
        #endregion

        #region Category

        public static List<Version> GetAllVersion()
        {
            List<Version> versionList = new List<Version>();
            using (SqlDataReader reader = AdminDAL.GetVersion())
            {
                while (reader.Read())
                {
                    Version item = new Version();
                    item.VersionId = Convert.ToInt32(reader["VersionId"]);
                    item.Title = reader["Title"].ToString();
                    item.ShortName = reader["ShortName"].ToString();
                    item.Visible = Convert.ToBoolean(reader["Visible"]);
                    item.HideRemove = Convert.ToBoolean(reader["flag"]);
                    item.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                    item.CategoryTitle = reader["CategoryTitle"].ToString();
                    versionList.Add(item);

                }

            }

            return versionList;
        }

        public static void SaveVersion(string title, string shortName, bool visible, int categoryId)
        {
            AdminDAL.SaveVersion(title, shortName, visible, categoryId);
        }

        public static void RemoveVersion(int versionId)
        {
            AdminDAL.RemoveVersion(versionId);
        }

        public static void UpdateVersion(int versionId, string title, string shortName, bool visible, int categoryId)
        {
            AdminDAL.UpdateVersion(versionId, title, shortName, visible, categoryId);
        }

        public static List<CouponInfo> GetAllCoupon()
        {
            List<CouponInfo> couponList = new List<CouponInfo>();
            List<CouponItems> couponItemList = new List<CouponItems>();

            using (SqlDataReader reader = AdminDAL.GetCoupon(0))
            {
                while (reader.Read())
                {
                    CouponItems item = new CouponItems();
                    item.CouponId = Convert.ToInt32(reader["CouponId"]);
                    item.SkuId = Convert.ToInt32(reader["SkuId"]);
                    item.RelatedSkuId = Convert.ToInt32(reader["RelatedSkuId"]);
                    item.DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]);
                    item.DiscountType = (CouponTypeEnum)Convert.ToInt32(reader["DiscountType"]);

                    couponItemList.Add(item);
                }

                reader.NextResult();
                while (reader.Read())
                {
                    CouponInfo itemInfo = new CouponInfo();
                    itemInfo.CouponId = Convert.ToInt32(reader["CouponId"]);
                    itemInfo.Title = reader["Title"].ToString();
                    itemInfo.Discount = Convert.ToDecimal(reader["Discount"]);
                    itemInfo.Active = Convert.ToBoolean(reader["active"]);
                    itemInfo.DiscountType = (CouponTypeEnum)Convert.ToInt32(reader["DiscountType"]);
                    itemInfo.TotalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                    if (Convert.ToInt32(reader["DiscountType"]) == (int)CouponTypeEnum.ItemType)
                    {
                        itemInfo.ItemsDiscount = couponItemList.FindAll(x => x.CouponId == Convert.ToInt32(reader["CouponId"]));
                    }
                    if (reader["includeShipping"] != DBNull.Value)
                        itemInfo.IncludeShipping = Convert.ToBoolean(reader["includeShipping"]);
                    else
                        itemInfo.IncludeShipping = true;
                    couponList.Add(itemInfo);

                }

            }

            return couponList;
        }

        public static CouponInfo GetCoupon(int couponId)
        {
            CouponInfo itemInfo = new CouponInfo();
            List<CouponItems> couponItems = new List<CouponItems>();
            using (SqlDataReader reader = AdminDAL.GetCoupon(couponId))
            {
                while (reader.Read())
                {
                    CouponItems couponItemInfo = new CouponItems();
                    couponItemInfo.CouponId = Convert.ToInt32(reader["CouponId"]);
                    couponItemInfo.SkuId = Convert.ToInt32(reader["SkuId"]);
                    couponItemInfo.RelatedSkuId = Convert.ToInt32(reader["RelatedSkuId"]);
                    couponItemInfo.DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]);
                    couponItemInfo.DiscountType = (CouponTypeEnum)Convert.ToInt32(reader["DiscountType"]);
                    couponItems.Add(couponItemInfo);
                }

                reader.NextResult();
                while (reader.Read())
                {

                    itemInfo.CouponId = Convert.ToInt32(reader["CouponId"]);
                    itemInfo.Title = reader["Title"].ToString();
                    itemInfo.Discount = Convert.ToDecimal(reader["Discount"]);
                    itemInfo.Active = Convert.ToBoolean(reader["active"]);
                    itemInfo.TotalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                    itemInfo.DiscountType = (CouponTypeEnum)Convert.ToInt32(reader["DiscountType"]);
                    if (reader["includeShipping"] != DBNull.Value)
                        itemInfo.IncludeShipping = Convert.ToBoolean(reader["includeShipping"]);
                    else
                        itemInfo.IncludeShipping = true;
                    itemInfo.ItemsDiscount = couponItems;
                }

            }

            return itemInfo;
        }

        public static void ResetCouponCache()
        {
            SitePreferenceCache cache = new SitePreferenceCache(HttpContext.Current);
            cache.RemoveCacheKey();
        }

        public static void RemoveCoupon(int couponId)
        {
            AdminDAL.RemoveCoupon(couponId);
        }

        public static void UpdateCoupon(int couponId, string title, decimal discount, decimal total, int discountType,
            int skuId, int relatedSkuId, int itemDiscountType, decimal itemDiscount, bool active, bool includeShipping)
        {
            AdminDAL.UpdateCoupon(couponId, title, discount, total, discountType, skuId, relatedSkuId, itemDiscountType, itemDiscount, active, includeShipping);
        }


        public static List<VersionCategory> GetAllVersionCateogry()
        {
            List<VersionCategory> cateogryList = new List<VersionCategory>();
            using (SqlDataReader reader = AdminDAL.GetAllVersionCateogry())
            {
                while (reader.Read())
                {
                    VersionCategory item = new VersionCategory();
                    item.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                    item.Title = reader["Title"].ToString();
                    item.Visible = Convert.ToBoolean(reader["Visible"]);
                    item.HideRemove = Convert.ToBoolean(reader["flag"]);

                    cateogryList.Add(item);

                }

            }

            return cateogryList;
        }

        public static void SaveVersionCategory(int categoryId, string title)
        {
            AdminDAL.SaveVersionCategory(categoryId, title);
        }

        public static void RemoveVersionCategory(int categoryId)
        {
            AdminDAL.RemoveVersionCategory(categoryId);
        }

        public static List<Category> GetAllCategories()
        {
            List<Category> categoryList = new List<Category>();
            using (SqlDataReader reader = CategoryDAL.GetCategory())
            {
                while (reader.Read())
                {
                    Category item = new Category();
                    item.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                    item.Title = reader["Title"].ToString();
                    item.Visible = Convert.ToBoolean(reader["Visible"]);
                    item.OrderNo = Convert.ToInt32(reader["OrderNo"]);
                    item.HideRemove = Convert.ToBoolean(reader["flag"]);
                    categoryList.Add(item);

                }

            }

            return categoryList;
        }

        public static void SaveCategoy(string Title, int orderNo)
        {
            CategoryDAL.SaveCategory(Title, orderNo);
        }

        public static List<CSError> GetAllErrors(string StartDate, string EndDate, int startRec, int endRec, out int totalCount)
        {
            List<CSError> ErrorList = new List<CSError>();
            using (DataTable DtTable = AdminDAL.GetAllErrors(StartDate, EndDate, startRec, endRec, out totalCount))
            {
                foreach (DataRow row in DtTable.Rows)
                {
                    CSError item = new CSError
                    {

                        LogId = Convert.ToInt32(row["LogId"]),
                        URL = row["URL"].ToString(),
                        Message = row["Message"].ToString(),
                        EventDate = Convert.ToDateTime(row["EventDate"])
                    };
                    ErrorList.Add(item);

                }

            }

            return ErrorList;
        }
        #endregion

        public static Dictionary<string, string> GetResource()
        {
            Dictionary<string, string> val = new Dictionary<string, string>();

            using (SqlDataReader drReader = AdminDAL.GetResource())
            {
                while (drReader.Read())
                {
                    val.Add(drReader["Key"].ToString(), drReader["Value"].ToString());
                }

            }

            return val;
        }

        /// <summary>
        /// pull from Cache object Wrapper
        /// </summary>
        /// <returns></returns>
        public static SitePreference GetCacheSitePref()
        {
            SitePreferenceCache cache = new SitePreferenceCache(HttpContext.Current);
            return (SitePreference)cache.Value;
        }

        /// <summary>
        /// Check order process scenarios
        /// </summary>
        /// <returns></returns>
        public static int OrderProcessCheck()
        {
            int EnableReviewOrder = 1;
            if (ConfigHelper.ReadAppSetting("EnableReviewOrder") != null)
                EnableReviewOrder = Convert.ToInt32(ConfigHelper.ReadAppSetting("EnableReviewOrder"));
            else
            {
                EnableReviewOrder = (int)(CSFactory.GetCacheSitePref()).OrderProcessType;
            }

            return EnableReviewOrder;
        }


        //This will return site level shipping preference
        public static SitePreference GetCartPrefrence()
        {
            SitePreference prefObject = new SitePreference();

            //Site Level Settings
            SitePref item = CSFactory.GetSitePreference();
            prefObject.PrefID = item.PrefId;
            prefObject.IncludeShippingCostInTaxCalculation = item.OrderTotalShipping;
            prefObject.OrderProcessType = (OrderProcessTypeEnum)item.OrderProcessType;
            prefObject.GeoLocationService = item.GeoTargetService;
            prefObject.VersionItems = GetAllVersion();
            prefObject.CouponItems = GetAllCoupon();

            prefObject.LoadAttributeValues();

            //Shipping Preferences
            ShippingPref val = ShippingDAL.GetShippingPref().Where(p => p.PrefId == 1).First();
            prefObject.ShippingPrefID = val.PrefId;
            prefObject.RushShippingPrefID = val.PrefId;

            prefObject.ShippingOptionId = (ShippingOptionType)val.OptionId;
            if (val.RushOptionId.HasValue)
                prefObject.RushShippingOptionID = (ShippingOptionType)val.RushOptionId.Value;
            else
                prefObject.RushShippingOptionID = prefObject.ShippingOptionId;

            prefObject.IncludeRushShipping = val.InCludeRushShipping;
            prefObject.RushShippingCost = val.RushShippingCost;
            prefObject.FlatShippingCost = val.flatShipping;

            //Default Settings
            prefObject.PaymentGatewayService = item.PaymentGatewayService;
            prefObject.FulfillmentHouseService = item.FulfillmentHouseService;

            return prefObject;
        }

        public static SitePreference GetCartPrefrence(Cart cart)
        {
            SitePreference defaultSetting = GetCartPrefrence();

            if (cart.ShippingAddress.CountryId > 0)
            {
                List<ShippingRegion> shippingRegions = ShippingDAL.GetShippingRegion();
                ShippingRegion region = null;
                if (cart.ShippingAddress.StateProvinceId > 0)
                {
                    region = shippingRegions.Where(r =>
                                    r.CountryId == cart.ShippingAddress.CountryId &&
                                    (!r.StateId.HasValue || r.StateId.Value == cart.ShippingAddress.StateProvinceId))
                                .FirstOrDefault();
                }
                else
                {
                    region = shippingRegions.Where(r =>
                                    r.CountryId == cart.ShippingAddress.CountryId &&
                                    !r.StateId.HasValue)
                                .FirstOrDefault();
                }

                if (region != null)
                {
                    SitePreference settingsToReturn = new SitePreference();
                    ShippingPref val = ShippingDAL.GetShippingPref(region.PrefId);


                    if (val != null)
                    {
                        if ((ShippingOptionType)val.OptionId == ShippingOptionType.SiteLevelPref)
                        {
                            settingsToReturn.ShippingPrefID = defaultSetting.ShippingPrefID;
                            settingsToReturn.ShippingOptionId = defaultSetting.ShippingOptionId;
                            settingsToReturn.FlatShippingCost = defaultSetting.FlatShippingCost;
                        }
                        else
                        {
                            settingsToReturn.ShippingPrefID = val.PrefId;
                            settingsToReturn.ShippingOptionId = (ShippingOptionType)val.OptionId;
                            settingsToReturn.FlatShippingCost = val.flatShipping;
                        }

                        if ((ShippingOptionType)val.RushOptionId == ShippingOptionType.SiteLevelPref)
                        {
                            settingsToReturn.RushShippingPrefID = defaultSetting.RushShippingPrefID;
                            settingsToReturn.RushShippingOptionID = defaultSetting.RushShippingOptionID;
                            settingsToReturn.RushShippingCost = defaultSetting.RushShippingCost;
                        }
                        else
                        {
                            settingsToReturn.RushShippingPrefID = val.PrefId;
                            if (val.RushOptionId.HasValue)
                                settingsToReturn.RushShippingOptionID = (ShippingOptionType)val.RushOptionId.Value;
                            else
                                settingsToReturn.RushShippingOptionID = (ShippingOptionType)val.OptionId;
                            settingsToReturn.RushShippingCost = val.RushShippingCost;
                        }

                        settingsToReturn.IncludeRushShipping = val.InCludeRushShipping;
                        //Overrider Custom Shipping
                        settingsToReturn.IncludeShippingCostInTaxCalculation = defaultSetting.IncludeShippingCostInTaxCalculation;
                        return settingsToReturn;
                    }
                }
            }
            return defaultSetting;
        }

        public static List<TaxRegion> GetTaxByCountry(int countryId)
        {

            List<TaxRegion> RegionList = new List<TaxRegion>();
            using (SqlDataReader reader = AdminDAL.GetTax(countryId))
            {
                while (reader.Read())
                {
                    TaxRegion item = new TaxRegion();
                    item.RegionId = Convert.ToInt32(reader["RegionId"]);
                    item.Value = Convert.ToDecimal(reader["Percentage"]);
                    item.CountryId = Convert.ToInt32(reader["CountryId"]);
                    item.StateId = (reader["StateId"] == System.DBNull.Value) ? 0 : Convert.ToInt32(reader["StateId"]);
                    item.ZipCode = (reader["PostalCode"] == System.DBNull.Value) ? null : Convert.ToString(reader["PostalCode"]);

                    RegionList.Add(item);
                }

            }

            return RegionList;
        }

        public static void RemoveShippingRegion(int prefId)
        {
            ShippingDAL.RemoveShippingRegion(prefId);
        }
    }
}
