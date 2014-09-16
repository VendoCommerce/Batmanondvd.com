using System;
using System.Collections.Generic;
using System.Linq;
using CSCore.DataHelper;
using System.Data.SqlClient;


namespace CSData
{
    public class ShippingDAL
    {


        static ShippingDAL()
        {
        }

        public static List<ShippingOrderValue> GetShippingOrderValue(ShippingOptionType shippingType)
        {

            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {

                var query = from c in context.ShippingOrderValues
                            where c.TypeId == (int)shippingType
                            select c;
                return query.ToList<ShippingOrderValue>();
            }
        }
   
        public static List<ShippingOrderValue> GetShippingOrderValue(ShippingOptionType shippingType, bool includeRushShipping, int prefId)
        {

            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {

                var query = from c in context.ShippingOrderValues
                            where (c.TypeId == (int)shippingType) && (c.IncludeRushShipping == includeRushShipping) && (c.PrefId == prefId)

                            select c;
                return query.ToList<ShippingOrderValue>();
            }
        }

        public static List<SkuShipping> GetSkuShipping()
        {
            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {

                var query = from c in context.SkuShippings
                            select c;
                return query.ToList<SkuShipping>();
            }

        }


        public static List<SkuShipping> GetSkuShipping(bool includeRushShipping, int prefId)
        {

            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {

                var query = from c in context.SkuShippings
                            where (c.IncludeRushShipping == includeRushShipping) && (c.PrefId == prefId)
                            select c;
                return query.ToList<SkuShipping>();
            }


        }


        public static List<ShippingRegion> GetShippingRegion()
        {

            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {

                var query = from c in context.ShippingRegions
                            select c;
                return query.ToList<ShippingRegion>();
            }

        }


        public static bool IsValidShippingRegion(int countryId, int? stateId)
        {
            bool retValue= false;
            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {

                var query = from c in context.ShippingRegions
                            where (c.CountryId == countryId) && object.Equals(c.StateId, stateId)
                            select c;

                if (query.Count() > 0)
                    retValue = true;
                return retValue;
            }

        }

        public static ShippingRegion GetShippingRegion(int RId)
        {
            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {

                var query = from c in context.ShippingRegions
                            where (c.RegionId == RId)
                            select c;
                return query.ToList<ShippingRegion>().FirstOrDefault();
            }
        }

        public static List<ShippingPref> GetShippingPref()
        {

            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {

                var query = from c in context.ShippingPrefs
                            select c;
                return query.ToList<ShippingPref>();
            }

        }


        public static ShippingPref GetShippingPref(int PrefId)
        {

            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {

                var query = from c in context.ShippingPrefs
                            where (c.PrefId == PrefId)
                            select c;
                return query.ToList<ShippingPref>().FirstOrDefault();
            }

        }

        public static ShippingCharge GetShippingCharge(int prefId, string key)
        {
            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {
                var query = from c in context.ShippingCharges
                            where c.PrefId == prefId && c.Key == key
                            select c;
                return query.ToList<ShippingCharge>().FirstOrDefault();
            }
        }

        public static List<ShippingCharge> GetAllShippingCharges()
        {
            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {
                var query = from c in context.ShippingCharges
                            select c;
                return query.ToList<ShippingCharge>();
            }
        }

        public static List<ShippingCharge> GetShippingChargesByPref(int prefId)
        {
            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {
                var query = from c in context.ShippingCharges
                            where c.PrefId == prefId
                            select c;
                return query.ToList<ShippingCharge>();
            }
        }

        public static void RemoveShippingCharge(int shippingChargeId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_remove_shipping_charge";
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("ShippingChargeId", shippingChargeId);

            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
        }

        public static void RemoveShippingRegion(int prefId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_remove_shippingRegion";
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("prefId", prefId);

            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);

        }

        

        public static void UpdateCategory(int categoryId, string Name, bool active, int orderNo)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_category";
            SqlParameter[] ParamVal = new SqlParameter[4];
            ParamVal[0] = new SqlParameter("CategoryId", categoryId);
            ParamVal[1] = new SqlParameter("Title", Name);
            ParamVal[2] = new SqlParameter("Active", (active) ? 1 : 0);
            ParamVal[3] = new SqlParameter("OrderNo", orderNo);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);

        }

        public static void RemoveCategory(int categoryId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_remove_category";
            SqlParameter[] ParamVal = new SqlParameter[1];
            ParamVal[0] = new SqlParameter("categoryId", categoryId);

            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);

        }

    }//end of the class


}
