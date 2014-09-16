using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CSPaymentProvider;
using System.Configuration;

namespace CSPaymentProvider
{
	public class PaymentProviderSetting
    {
		#region Properties
        
		    /// <summary>
            /// Gets or sets the PathId
            /// </summary>
		
		    public int ProviderID { get; set; }
            public PaymentProviderType ProviderType { get; set; }

            /// <summary>
            /// Gets or sets the customer Title
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// Gets or sets the customer Title
            /// </summary>
            public bool Active { get; set; }

            /// <summary>
            /// Gets or sets the Weight
            /// </summary>
            public string ProviderXML { get; set; }
       
            public bool IsDefault { get; set; }

        #endregion
    }
}
