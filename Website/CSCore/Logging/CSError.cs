using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSCore.Logging
{
    public class CSError
    {

        public int LogId { get; set; }

        /// <summary>
        /// Gets or sets the customer Guid
        /// </summary>
        public string URL { get; set; }


        /// <summary>
        /// Gets or sets the Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the customer Guid
        /// </summary>
        public DateTime EventDate { get; set; }
    }
}
