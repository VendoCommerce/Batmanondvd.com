using System;
using System.Xml.Linq;
using CSPaymentProvider;
using CSPaymentProvider.Providers;

namespace CSPaymentProvider
{
	public class PaymentProviderFactory
	{
		public static IPaymentProvider CreateProvider(PaymentProviderType type, string providerXML)
		{
			IPaymentProvider provider = null;
			switch (type)
			{
				case PaymentProviderType.EPayAccount:
					provider = new ePayAccount();
					break;
				case PaymentProviderType.NationalBankcardSystem:
					provider = new NationalBankcardAccount();
					break;
                case PaymentProviderType.AuthorizeNetAccount:
                    provider = new AuthorizeNetAccount();
                    break;
                case PaymentProviderType.USAePayAccount:
                    provider = new USAePayAccount();
                    break;
                case PaymentProviderType.LitleCorpAccount:
                    provider = new LitleCorpAccountAccount();
                    break;
                case PaymentProviderType.PayPalProFlowAccount:
                    provider = new PayPalProFlowAccount();
                    break;
                case PaymentProviderType.OrbitalChasePaymentechAccount:
                    provider = new OrbitalChasePaymentechAccount();
                    break;
                case PaymentProviderType.PayPalAdaptivePayment:
                    provider = new PayPalAdaptivePaymentAccount();
                    break;
                case PaymentProviderType.PayPalExpressCheckout:
                    provider = new PayPalExpressCheckoutAccount();
                    break;          
                case PaymentProviderType.PayPalDirectPayment:
                    provider = new PayPalDirectPayment();
                    break;
                case PaymentProviderType.DataPakAccount:
                    provider = new DataPakAccount();
                    break;
                case PaymentProviderType.PaymentX:
                    provider = new PaymentX();
                    break;
                case PaymentProviderType.Cielo:
                    provider = new Cielo();
                    break;
                default:
					break;
			}

			if (provider == null)
				throw new ApplicationException("Provider " + type.ToString() + " is not implemented");

			provider.Initialize(XElement.Parse(providerXML));

			return provider;
		}
	}
}
