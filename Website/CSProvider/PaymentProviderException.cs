using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSPaymentProvider
{
	public class PaymentProviderException : ApplicationException
	{
		public PaymentProviderException()
			: base("A gateway exception has occured")
		{
		}

		public PaymentProviderException(string Message)
			: base(Message)
		{
		}

		public PaymentProviderException(string Message, Exception ex)
			: base(Message, ex)
		{

		}
	}
}
