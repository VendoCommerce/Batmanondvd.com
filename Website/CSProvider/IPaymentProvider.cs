using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


namespace CSPaymentProvider
{
	public interface IPaymentProvider
	{
		PaymentProviderType Type { get; }
		void Initialize(XElement xmlSettings);
		Response PerformRequest(Request request);
		Response PerformVoidRequest(Request request);
        Response PerformVoidSettledRequest(Request request);
	    Response PerformValidationRequest(Request request);
	}
}
