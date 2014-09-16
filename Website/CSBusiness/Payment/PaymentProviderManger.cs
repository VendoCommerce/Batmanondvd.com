using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CSData;
using CSCore;
using CSPaymentProvider;
using CSCore.DataHelper;
using System.Configuration;

namespace CSBusiness.PostSale
{
    public class PaymentProviderManger
    {
               
		public static List<PaymentProviderSetting> GetAllProviders()
        {
			//Config file takes precedence over database
			ProviderConfigurationHandler config = ConfigurationManager.GetSection("PaymentProvider") as ProviderConfigurationHandler;

            if (ConfigurationManager.AppSettings["DBPaymentGateway"] == null)
            {

                return GetAllProvidersFromDB(true);

            }
            else if (Convert.ToBoolean(ConfigurationManager.AppSettings["DBPaymentGateway"]))
            {
                return GetAllProvidersFromDB(true);
            }
            else
            {
                if (config != null && config.Providers != null && config.Providers.Count > 0)              
                    return config.Providers;               
                else
                    return GetAllProvidersFromDB(true);
            }
        }

        public static List<PaymentProviderSetting> GetAllProvidersFromDB(bool activeList)
		{
			List<PaymentProviderSetting> providerList = new List<PaymentProviderSetting>();
            using (SqlDataReader reader = AdminDAL.GetAllProviders(activeList))
			{
				while (reader.Read())
				{
					PaymentProviderType type = PaymentProviderType.EPayAccount;
					if (System.Enum.TryParse<PaymentProviderType>(reader["ProviderName"].ToString(), out type))
					{
					PaymentProviderSetting item = new PaymentProviderSetting();
						item.ProviderID = (int)reader["ProviderId"];
						item.ProviderType = type;
						item.Title = type.ToString();
					item.ProviderXML = reader["ProviderXml"].ToString();
					item.Active = Convert.ToBoolean(reader["Active"]);
					item.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
					providerList.Add(item);
					}

				}
			}
			return providerList;
		}

        public static void  SaveProvider(int providerId, string providerName, string providerXML)
        {
            AdminDAL.SaveProvider(providerId, providerName, providerXML);
        }

        public static void SaveProvider(List<Triplet<int, int, int>> Values)
        {
            AdminDAL.SaveProvider(Values);
        }

        public static void RemoveProvider(int providerId)
        {
            AdminDAL.RemoveProvider(providerId);
        }  
    }
}
