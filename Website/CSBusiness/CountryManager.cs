using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Data.SqlClient;

using CSData;
using CSBusiness.Cache;

namespace CSBusiness
{
    public class CountryManager
    {
        public CountryManager() { }

        public static string CountryName(int countryId)
        {
            string countryName = String.Empty;
            CountryCache cache = new CountryCache(HttpContext.Current);
            List<Country> list = (List<Country>) cache.Value;
            Country item = list.FirstOrDefault(x => x.CountryId == countryId);
            if (item != null)
                countryName = item.Name;
            return countryName;
        }


        public static string CountryCode(int countryId)
        {
            string countryCode = String.Empty;
            CountryCache cache = new CountryCache(HttpContext.Current);
            List<Country> list = (List<Country>)cache.Value;
            Country item = list.FirstOrDefault(x => x.CountryId == countryId);
            if (item != null)
                countryCode = item.Code;
            return countryCode;
        }

        public static int CountryId(string countryName)
        {            
            CountryCache cache = new CountryCache(HttpContext.Current);
            List<Country> list = (List<Country>)cache.Value;
            Country item = list.FirstOrDefault(x => x.Name == countryName);
            if (item != null)
                return item.CountryId;

            return -1;
        }

        public static List<Country> GetActiveCountry()
        {
           
            CountryCache cache = new CountryCache(HttpContext.Current);
            List<Country> list = (List<Country>)cache.Value;
            return  list.FindAll(x => x.Visible == true);
        }

        public static List<Country> GetCacheCountry()
        {

            CountryCache cache = new CountryCache(HttpContext.Current);
            return  (List<Country>)cache.Value;
        
        }

        public static List<Country> GetSpecificCountry(int countryId)
        {

            CountryCache cache = new CountryCache(HttpContext.Current);
            List<Country> list = (List<Country>)cache.Value;
            return list.FindAll(x => x.CountryId == countryId);
        }

       

        public static void ResetCountryCache()
        {
             CountryCache cache = new CountryCache(HttpContext.Current);
             cache.RemoveCacheKey();
        }

        public static List<Country> GetAllMasterCountries()
        {

            List<Country> CountryList = new List<Country>();
            using (SqlDataReader reader = AdminDAL.GetAllMasterCountries())
            {
                while (reader.Read())
                {
                    Country item = new Country();
                    item.CountryId = Convert.ToInt32(reader["CountryId"]);
                    item.Name = reader["Name"].ToString();
                    item.Code = reader["Code"].ToString();
                    item.OrderNo = Convert.ToInt32(reader["OrderNo"]);
                    CountryList.Add(item);

                }

            }

            return CountryList;
        }




        public static List<Country> GetAllCountry()
        {

            List<Country> CountryList = new List<Country>();
            using (SqlDataReader reader = CountryDAL.GetAllCountry())
            {
                while (reader.Read())
                {
                    Country item = new Country();
                    item.CountryId = Convert.ToInt32(reader["CountryId"]);
                    item.Name = reader["Name"].ToString();
                    item.Code = reader["Code"].ToString();
                    item.Visible = Convert.ToBoolean(reader["Visible"]);
                    item.OrderNo = Convert.ToInt32(reader["OrderNo"]);
                    CountryList.Add(item);

                }

            }

            return CountryList;
        }

        public static void CreateCountry(List<Country> countryInfo) //string xml)
        {
            XElement flagXml = new XElement("countrys",
                                        from Country item in countryInfo
                                        select new XElement("country",
                                                new XAttribute("id", item.CountryId),
                                                 new XAttribute("name", item.Name),
                                                new XAttribute("code", item.Code),
                                                new XAttribute("orderno", item.OrderNo)));

            AdminDAL.CreateCountry(flagXml.ToString());


        }
    }
}
