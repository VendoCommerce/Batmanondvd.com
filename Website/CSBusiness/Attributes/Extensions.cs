using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSBusiness.Attributes
{
    public static class Extensions
    {
        public static void AddAttributeValue(this IDictionary<string, AttributeValue> objectAttributes, string attributeName, AttributeValue attributeValue)
        {
            objectAttributes.Add(Attribute.CaseFixAttributeName(attributeName), attributeValue);
        }

        public static void AddOrUpdateAttributeValue(this IDictionary<string, AttributeValue> objectAttributes, string attributeName, AttributeValue attributeValue)
        {
            if (objectAttributes.ContainsKey(Attribute.CaseFixAttributeName(attributeName)))
                objectAttributes[Attribute.CaseFixAttributeName(attributeName)] = attributeValue;
            else
                AddAttributeValue(objectAttributes, attributeName, attributeValue);
        }

        public static bool ContainsAttribute(this IDictionary<string, AttributeValue> objectAttributes, string attributeName)
        {
            return objectAttributes.ContainsKey(Attribute.CaseFixAttributeName(attributeName));
        }

        public static AttributeValue GetAttributeValue(this IDictionary<string, AttributeValue> objectAttributes, string attributeName)
        {
            return objectAttributes[Attribute.CaseFixAttributeName(attributeName)];
        }
    }
}
