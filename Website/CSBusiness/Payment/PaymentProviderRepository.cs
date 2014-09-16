using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSBusiness.PostSale;
using CSPaymentProvider;

namespace CSBusiness.Payment
{
	//TODO: This class should store "Instance" in the cache and be expired every N minutes
	public class PaymentProviderRepository
	{
        
		Dictionary<PaymentProviderType, IPaymentProvider> _allProviders;
		PaymentProviderType _default;

		private static PaymentProviderRepository _instance;
		private static object _lockObject;

		static PaymentProviderRepository()
		{
			_lockObject = new object();
		}

		public static PaymentProviderRepository Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new PaymentProviderRepository();
							_instance.LoadProviders();

						}
					}
				}
				return _instance;
			}
		}

		public IPaymentProvider Get()
		{
			return Get(_default);
		}

		public IPaymentProvider Get(PaymentProviderType providerType)
		{
			if (_allProviders.ContainsKey(providerType))
			{
				return _allProviders[providerType];
			}
			else
			{ 
				throw new NotSupportedException("Missing setting for provider " + providerType.ToString());
			}
		}

		private PaymentProviderRepository()
		{
			_allProviders = new Dictionary<PaymentProviderType, IPaymentProvider>();
			
		}

		private void LoadProviders()
		{
			List<PaymentProviderSetting> allSettings = PaymentProviderManger.GetAllProviders();
			int totalSettings = allSettings.Count;			

			for (int i = 0; i < totalSettings; i++)
			{ 
				PaymentProviderSetting settings = allSettings[i];
				if (settings.Active)
				{
					IPaymentProvider provider = PaymentProviderFactory.CreateProvider(settings.ProviderType, settings.ProviderXML);
					_allProviders[settings.ProviderType] = provider;

					if (settings.IsDefault)
					{
						_default = settings.ProviderType;
					}
				}
			}
		}
	}
}
