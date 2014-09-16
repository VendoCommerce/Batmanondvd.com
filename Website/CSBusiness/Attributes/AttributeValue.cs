using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CSBusiness.Attributes
{
    [Serializable]    
    public class AttributeValue
    {        
        public bool BooleanValue
        {
            get
            {
                return AttributeValue.GetBooleanFromValue(Value);
            }
        }

        public string Value { get; set; }

        public AttributeValue(string value)
        {
            this.Value = value;
        }

        public AttributeValue(bool value)
        {
            this.Value = value ? "1" : "0";
        }

        public AttributeValue() : this(null)
        {
        }

        public static bool GetBooleanFromValue(string value)
        {
            switch (value.ToLower())
                {
                    case "0":
                    case "false":
                    case "no":
                        return false;
                    case "1":
                    case "true":
                    case "yes":
                        return true;
                }

            throw new Exception("Could not infer boolean from attribute value.");
        }

        public static string GetBooleanString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return GetBooleanFromValue(value) ? "1" : "0";
        }
    }
}
