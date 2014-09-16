using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSBusiness.PostSale
{
    public class TemplateSku
    {

        /// <summary>
        /// Gets or sets the PathId
        /// </summary>
        public int SkuId { get; set; }

        /// <summary>
        /// Gets or sets the customer Title
        /// </summary>
        public TemplateItemTypeEnum TypeId { get; set; }

        /// <summary>
        /// Gets or sets the customer Title
        /// </summary>
        public int ReplaceSkuId { get; set; }
    }
}
