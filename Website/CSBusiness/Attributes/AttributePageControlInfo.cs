using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSBusiness.Attributes
{
    public enum RequestFindMethod
    {
        ExactMatch,
        BeginsWith
    }

    [Serializable]
    public class AttributePageControlInfo
    {
        public string UniqueId { get; set; }
        public RequestFindMethod RequestFindMethod { get; set; }
    }
}
