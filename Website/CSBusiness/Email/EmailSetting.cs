using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSBusiness.Payment;
using CSPaymentProvider;

namespace CSBusiness
{
    public class EmailSetting
    {


        #region Properties

        /// <summary>
        /// Gets or sets the PathId
        /// </summary>
        public int EmailId { get; set; }

        /// <summary>
        /// Gets or sets the customer Title
        /// </summary>
        public string Title { get; set; }

      
        /// <summary>
        /// Gets or sets the Weight
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the Weight
        /// </summary>
        public string Body { get; set; }


        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        #endregion


    }
}

