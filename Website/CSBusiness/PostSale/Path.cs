using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSBusiness.PostSale
{
    public class Path
    {
        

        #region Properties

        /// <summary>
        /// Gets or sets the PathId
        /// </summary>
        public int PathId { get; set; }

        /// <summary>
        /// Gets or sets the customer Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the customer Title
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the Weight
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Gets or sets the Templates
        /// </summary>
        public List<Template> Templates { get; set; }

        
        /// <summary>
        /// Gets or sets the CreatedDate
        /// </summary>
        public DateTime CreateDate { get; set; }

        public bool Active { get; set; }

		/// <summary>
		/// This will store the actual path execution count
		/// </summary>
		public int ProcessedCount
		{
			get;
			set;
		}

        /// <summary>
        /// Gets or sets the PathVersion
        /// </summary>
        public List<Int32> Versions { get; set; }
        #endregion


    }
}
