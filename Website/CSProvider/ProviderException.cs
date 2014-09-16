using System;
using System.Collections.Generic;
using System.Text;

namespace CSPaymentProvider
{
    public class ProviderException : ApplicationException
    {

        public ProviderException()
            : base("A gateway exception has occured")
        {
        }

        public ProviderException(string Message)
            : base(Message)
        {
        }

        public ProviderException(string Message, Exception ex)
            : base(Message, ex)
        {

        }

    }
}
