using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CSData;
using CSCore;
using CSPaymentProvider;
using CSCore.DataHelper;
using System.Configuration;
using CSBusiness.FulfillmentHouse;

namespace CSBusiness.FulfillmentHouse
{
    public class FulfillmentHouseProviderManger
    {
        public static List<FulfillmentHouseProviderSetting> GetAllProvidersFromDB(bool activeList)
		{
			List<FulfillmentHouseProviderSetting> providerList = new List<FulfillmentHouseProviderSetting>();
            using (SqlDataReader reader = AdminDAL.GetAllFulfillmentHouseProviders(activeList))
			{
				while (reader.Read())
				{
					FulfillmentHouseProviderSetting item = new FulfillmentHouseProviderSetting();
						item.ProviderID = (int)reader["ProviderId"];
                        item.Title = reader["ProviderName"].ToString();
					item.ProviderXML = reader["ProviderXml"].ToString();
					item.Active = Convert.ToBoolean(reader["Active"]);
					item.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
					providerList.Add(item);

				}
			}
			return providerList;
		}

        public static void  SaveProvider(int providerId, string providerName, string providerXML)
        {
            AdminDAL.SaveFulfillmentHouseProvider(providerId, providerName, providerXML);
        }

        public static void SaveProvider(List<Triplet<int, int, int>> Values)
        {
            AdminDAL.SaveFulfillmentHouseProvider(Values);
        }

        public static void RemoveProvider(int providerId)
        {
            AdminDAL.RemoveFulfillmentHouseProvider(providerId);

        }  
    }
}
