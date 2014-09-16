using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

using CSData;
using CSBusiness.Cache;
using System.Web;
using System.Xml.Linq;


namespace CSBusiness
{
    public class StateManager
    {
        public StateManager() { }

        public static List<StateProvince> GetAllMasterStates(int countryId)
        {

            List<StateProvince> StatesList = new List<StateProvince>();
            using (SqlDataReader reader = AdminDAL.GetAllMasterStates(countryId))
            {
                while (reader.Read())
                {
                    StateProvince item = new StateProvince();
                    item.StateProvinceId = Convert.ToInt32(reader["StateId"]);
                    item.Name = reader["Title"].ToString();
                    item.Abbreviation = reader["Code"].ToString();
                    item.DisplayOrder = Convert.ToInt32(reader["OrderNo"]);
                    StatesList.Add(item);

                }

            }

            return StatesList;
        }

        /// <summary>
        /// Seriliaze the object to XML for Database Commitement
        /// </summary>
        /// <param name="States"></param>
        public static void SaveStates(List<StateProvince> States)
        {
            XElement rootNode = new XElement("root");

            foreach (StateProvince itm in States)
            {
                XElement xElem = new XElement("item", 
                                                new XAttribute("Id", itm.StateProvinceId.ToString()),
                                                new XAttribute("Name", itm.Name),
                                                new XAttribute("Code", itm.Abbreviation),
                                                new XAttribute("CountryId", itm.CountryId),
                                                 new XAttribute("OrderNo", itm.DisplayOrder),
                                                new XAttribute("Visible", (itm.Visible) ? 1:0)
                                              );
                  rootNode.Add(xElem);
            }

            AdminDAL.SaveStates(rootNode.ToString());
        }

        public static List<StateProvince> GetAllStates(int countryId)
        {

            List<StateProvince> StatesList = new List<StateProvince>();
            using (SqlDataReader reader = AdminDAL.GetAllStates(countryId))
            {
                while (reader.Read())
                {
                    StateProvince item = new StateProvince();
                    item.StateProvinceId = Convert.ToInt32(reader["StateId"]);
                    item.Name = reader["Title"].ToString();
                    item.Abbreviation = reader["Code"].ToString();
                    item.DisplayOrder = Convert.ToInt32(reader["OrderNo"]);
                    item.CountryId = Convert.ToInt32(reader["CountryId"]);
                    StatesList.Add(item);

                }

            }

            return StatesList;
        }

        public static void ResetStateCache()
        {
            StateCache cache = new StateCache(HttpContext.Current);
            cache.RemoveCacheKey();
        }

        public static string GetStateName(int stateId)
        {
            string stateName = String.Empty;
            StateCache cache = new StateCache(HttpContext.Current);
            List<StateProvince> list = (List<StateProvince>) cache.Value;
            StateProvince item = list.FirstOrDefault(x => x.StateProvinceId == stateId);
            if (item != null)
                stateName = item.Name;
            return stateName;
        }

        public static List<StateProvince> GetCacheStates(int countryId)
        {
           
            StateCache cache = new StateCache(HttpContext.Current);
            List<StateProvince> list = (List<StateProvince>)cache.Value;
            return list.FindAll(x => x.CountryId == countryId);
        
        }

        public static List<StateProvince> GetCacheStates()
        {

            StateCache cache = new StateCache(HttpContext.Current);
            return  (List<StateProvince>)cache.Value;
      
        }

        public static StateProvince GetStateName(int stateId,bool Object)
        {
            string stateName = String.Empty;
            StateCache cache = new StateCache(HttpContext.Current);
            List<StateProvince> list = (List<StateProvince>)cache.Value;
            StateProvince item = list.FirstOrDefault(x => x.StateProvinceId == stateId);                            
            return item;
        }
        
    }
}
