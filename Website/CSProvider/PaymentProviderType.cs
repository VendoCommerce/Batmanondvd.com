using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSPaymentProvider
{
	public enum PaymentProviderType
	{
		NationalBankcardSystem = 1,
		EPayAccount = 2,
        AuthorizeNetAccount = 3,
        USAePayAccount = 4,
        LitleCorpAccount = 5,
        PayPalProFlowAccount = 6,
        OrbitalChasePaymentechAccount = 7,
        PayPalAdaptivePayment = 8,
        PayPalExpressCheckout = 9,
        PayPalDirectPayment = 10,
        DataPakAccount = 11,
        PaymentX = 12,
        Cielo = 13
	}
}
