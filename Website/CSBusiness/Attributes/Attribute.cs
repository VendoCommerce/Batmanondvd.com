using CSBusiness.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSBusiness.Attributes
{
    [Serializable]
    public class Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ValueTypeName { get; set; }
        public string SqlDbType { get; set; }
        public string DisplayLabel { get; set; }
        public string ObjectAttributeTypeName { get; set; }

        public static string CaseFixAttributeName(string attributeName)
        {
            return attributeName.ToLower();
        }
    }
}
