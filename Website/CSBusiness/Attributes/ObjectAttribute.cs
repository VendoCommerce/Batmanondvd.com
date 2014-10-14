using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CSData;
using System.Xml.Linq;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Dynamic;
using CSBusiness.Security;

namespace CSBusiness.Attributes
{    
    [Serializable]
    public class ObjectAttribute : IObjectAttribute
    {
        /// <summary>
        /// Flag indicating if attribute values have been successfully loaded.
        /// </summary>
        private bool attributeValuesLoaded = false;

        private IDictionary<string, AttributeValue> attributeValues; // attribute name => value
                
        [XmlIgnore]
        public IDictionary<string, AttributeValue> AttributeValues
        {
            get
            {
                return attributeValues;
            }
            set
            {
                attributeValues = value;
            }
        }

        /// <summary>
        /// Indicates if this item's attribute values have been successfully loaded.
        /// </summary>        
        [XmlIgnore]
        public bool AttributeValuesLoaded
        {
            get
            {
                return attributeValuesLoaded;
            }
            private set
            {
                attributeValuesLoaded = value;
            }
        }
        
        /// <summary>
        /// This property is for use primarily by serialization - to return data of items that cannot normally be serialized (e.g. Dictionaries). 
        /// </summary>
        public string ObjectDataJson
        {
            get
            {
                dynamic d = new ExpandoObject();                
                d.AttributeValuesLoaded = this.AttributeValuesLoaded;

                List<KeyValuePair<string, string>> attrValues = new List<KeyValuePair<string, string>>();
                foreach (string key in AttributeValues.Keys)
                {
                    attrValues.Add(new KeyValuePair<string, string>(key, AttributeValues[key].Value));
                }

                d.AttributeValues = attrValues;
                
                return CSCore.Serializer.SerializeJS(d);
            }
            set
            {
                // Setter needed for serialization to work. It does not need to do anything, however.
            }
        }

        /// <summary>
        /// Helper delegate for GenericGet field. Define it before using that property.
        /// 
        /// Example: sku => { return ((Sku)sku).GetAttributeValue("Weight");  }
        /// </summary>
        [NonSerialized]
        [XmlIgnore]
        public Func<IObjectAttribute, object> getGenericGetValue;

        /// <summary>
        /// Works in conjuction with getGenericGetValue delegate field - define it before using this property.
        /// </summary>
        public object GenericGet
        {
            get
            {
                if (getGenericGetValue == null)
                    throw new Exception("getGenericGetValue delegate not defined.");

                return getGenericGetValue(this);
            }
        }

        public virtual string ObjectName
        {
            get
            {
                throw new Exception("ObjectName not defined.");
            }
        }

        public virtual int ItemId
        {
            get
            {
                throw new Exception("ItemId not defined.");
            }
        }
        
        public static IEnumerable<Attribute> GetAttributes(string objectName)
        {
            using (SqlDataReader reader = AttributesDAL.GetObjectAttributes(objectName))
            {
                while (reader.Read())
                    yield return new Attribute()
                    {
                        Name = Attribute.CaseFixAttributeName(Convert.ToString(reader["AttributeName"])),
                        Description = Convert.ToString(reader["AttributeDescription"] ?? string.Empty),
                        SqlDbType = Convert.ToString(reader["SqlDbType"]).ToLower(),
                        ValueTypeName = Convert.ToString(reader["ValueTypeName"]).ToLower(),
                        DisplayLabel = Convert.ToString(reader["DisplayLabel"] ?? string.Empty),
                        ObjectAttributeTypeName = Convert.ToString(reader["ObjectAttributeTypeName"] ?? string.Empty)
                    };
            }
        }

        public IEnumerable<string> GetAttributeNames()
        {
            foreach (Attribute attribute in ObjectAttribute.GetAttributes(ObjectName))
                yield return attribute.Name;
        }

        public void LoadAttributeValues()
        {   
            attributeValues = new Dictionary<string, AttributeValue>();

            using (SqlDataReader reader = AttributesDAL.GetAllAttributeValues(ObjectName, ItemId))
            {
                while (reader.Read())
                    attributeValues.Add(Attribute.CaseFixAttributeName(Convert.ToString(reader["AttributeName"])), new AttributeValue(Convert.ToString(reader["Value"] ?? string.Empty)));
            }

            attributeValuesLoaded = true;
        }

        public void AddAttributeValue(string attributeName, AttributeValue attributeValue)
        {
            attributeValues.AddAttributeValue(attributeName, attributeValue);
        }

        public void AddOrUpdateAttributeValue(string attributeName, AttributeValue attributeValue)
        {
            attributeValues.AddOrUpdateAttributeValue(attributeName, attributeValue);
        }

        public string GetAttributeValue(string attributeName)
        {
            if (attributeValues == null)
                throw new Exception("Attribute values not loaded.");

            return attributeValues[Attribute.CaseFixAttributeName(attributeName)].Value;
        }

        public T GetAttributeValue<T>(string attributeName)
        {
            if (attributeValues == null)
                throw new Exception("Attribute values not loaded.");

            Type type = typeof(T);
            string value = attributeValues[Attribute.CaseFixAttributeName(attributeName)].Value;

            if (type.Equals(typeof(bool)))
            {
                
                bool result;

                if (!bool.TryParse(value, out result))
                {
                    switch (value)
                    {
                        case "0":                        
                            return (T)Convert.ChangeType(false, type);
                        case "1":
                            return (T)Convert.ChangeType(true, type);
                        default:
                            // let control go to default cast and error out.
                            break;
                    }
                }
            }
            
            return (T)Convert.ChangeType(value, type);
        }

        public T GetAttributeValue<T>(string attributeName, T badCastDefault, T nonExistentDefault)
        {
            if (attributeValues == null || !attributeValues.ContainsKey(Attribute.CaseFixAttributeName(attributeName)))
                return nonExistentDefault;

            try
            {
                return GetAttributeValue<T>(attributeName);
            }
            catch (Exception ex)
            {
                Type errType = ex.GetType();

                if (errType.Equals(typeof(InvalidCastException)) ||
                    errType.Equals(typeof(FormatException)) ||
                    errType.Equals(typeof(OverflowException)) ||
                    errType.Equals(typeof(ArgumentNullException)))
                    return badCastDefault;

                throw;
            }            
        }

        public T GetAttributeValue<T>(string attributeName, T defaultValue)
        {
            try
            {
                return GetAttributeValue<T>(attributeName);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public AttributeValue GetAttributeValueObj(string attributeName)
        {
            if (attributeValues == null)
                throw new Exception("Attribute values not loaded.");

            return attributeValues[Attribute.CaseFixAttributeName(attributeName)];
        }

        public bool ContainsAttribute(string attributeName)
        {
            return attributeValues.ContainsAttribute(attributeName);
        }

        public static bool IsValidAttributeName(string attributeName)
        {
            // must start with one letter. Rest can be letters, numbers, or underscore. Total length must be 50 or less.
            Regex regex = new Regex("^[a-zA-Z]{1,1}[a-zA-Z0-9_]{0,49}$");
            return regex.IsMatch(attributeName);
        }

        #region DAL Wrappers

        public void SaveAttributeValue(string attributeName, string value)
        {
            //Encrypt sensitive data 
            //Encryption.EncryptValues(value); 

            AttributesDAL.SaveAttributeValue(ObjectName, attributeName, ItemId, value);
        }

        public void SaveAttributeValues()
        {
            ObjectAttribute.SaveAttributeValues(this);
        }

        public static void SaveAttributeValues(ObjectAttribute objectAttribute)
        {
            SaveAttributeValues(objectAttribute.ObjectName, objectAttribute.ItemId, (Dictionary<string, AttributeValue>)objectAttribute.AttributeValues);
        }

        public static void SaveAttributeValues(string objectName, int itemId, Dictionary<string, AttributeValue> attributeValues)
        {
            XElement attributeValuesElem = new XElement("AttributeValues");

            if (attributeValues != null)
                foreach (string attributeName in attributeValues.Keys)
                {
                    //Encrypt sensitive data 
                    //Encryption.EncryptValues(attributeValues[attributeName].Value); 

                    attributeValuesElem.Add(new XElement(Attributes.Attribute.CaseFixAttributeName(attributeName), attributeValues[attributeName].Value));
                }

            AttributesDAL.SaveAttributeValues(objectName, itemId, attributeValuesElem.ToString());
        }

        public void DeleteAttributeValues()
        {
            AttributesDAL.DeleteAttributeValues(ObjectName, ItemId);
        }

        public static void DeleteAttributes(string objectName, int itemId, List<string> attributeNames)
        {
            XElement deleteAttributesElem = new XElement("DeleteAttributes");

            foreach (string attributeName in attributeNames)
                deleteAttributesElem.Add(new XElement(attributeName));

            AttributesDAL.DeleteSingleAttributeValue(objectName, itemId, deleteAttributesElem.ToString());
        }

        public static DataTable GetObjects()
        {
            DataSet result = new DataSet();

            using (SqlDataReader reader = AttributesDAL.GetAllOjbects())
                result = CSCore.DataHelper.BaseSqlHelper.GetDataSet(reader);

            return result.Tables[0];
        }

        public static SqlDataReader GetAllAttributes()
        {
            return AttributesDAL.GetAllAttributes();
        }

        public static int SaveAttribute(string attributeName, string description)
        {
            return AttributesDAL.SaveAttribute(attributeName, description);
        }

        public static void SaveAttributeById(int attributeId, string attributeName, string description, string defaultValueTypeName)
        {
            AttributesDAL.SaveAttributeById(attributeId, attributeName, description, defaultValueTypeName);
        }

        public static SqlDataReader GetAllValueTypes()
        {
            return AttributesDAL.GetAllValueTypes();
        }

        public static SqlDataReader GetObjectAttributes(string objectName)
        {
            return AttributesDAL.GetObjectAttributes(objectName);
        }

        public static SqlDataReader GetAttributeObjects(string attributeName)
        {
            return AttributesDAL.GetAttributeObjects(attributeName);
        }

        public static SqlDataReader GetAllObjectAttributeTypes()
        {
            return AttributesDAL.GetAllObjectAttributeTypes();
        }

        public static void SaveObjectAttribute(int objectId, int attributeId, int objectAttributeTypeId, string description, string displayLabel)
        {
            AttributesDAL.SaveObjectAttribute(objectId, attributeId, objectAttributeTypeId, description, displayLabel);
        }

        public static void DeleteSingleObjectAttribute(int objectId, int attributeId)
        {
            AttributesDAL.DeleteSingleObjectAttribute(objectId, attributeId);
        }

        public static void DeleteAttribute(string attributeName)
        {
            AttributesDAL.DeleteAttribute(attributeName);
        }

        #endregion
    }
}
