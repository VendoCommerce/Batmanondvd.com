using System;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;


namespace CSPaymentProvider
{
	public class ProviderConfigurationHandler : ConfigurationSection
	{
		public List<PaymentProviderSetting> Providers
		{
			get;
			set;
		}

		[ConfigurationProperty("defaultProvider")]
		public string DefaultProviderName
		{
			get { return (string)this["defaultProvider"]; }
			set { this["defaultProvider"] = value; }
		}

		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			if (elementName.ToLower() == "providers")
			{
				Providers = new List<PaymentProviderSetting>();
				while (reader.Read() && reader.Name != "providers")
				{
					if (reader.Name.ToLower() == "add" && reader.NodeType == XmlNodeType.Element)
					{
						PaymentProviderSetting setting = new PaymentProviderSetting();
						PaymentProviderType type = PaymentProviderType.EPayAccount;
						if (Enum.TryParse<PaymentProviderType>(reader.GetAttribute("name"), out type))
						{
							setting.ProviderType = type;
							setting.Title = type.ToString();
							setting.Active = true;
							setting.ProviderXML = reader.ReadOuterXml();
							if (setting.ProviderType.ToString().ToLower() == DefaultProviderName.ToLower())
							{
								setting.IsDefault = true;
							}
							Providers.Add(setting);
						}
						else
						{
							throw new ConfigurationErrorsException("Unknown provider configuration. Provider Name: " + reader.GetAttribute("name") + ", Please check the file.", reader);
						}
					}
				}
				return true;
			}
			else
				return base.OnDeserializeUnrecognizedElement(elementName, reader);
		}
	}
}
