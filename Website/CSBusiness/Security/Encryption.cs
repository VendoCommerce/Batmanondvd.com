using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CSBusiness.Security
{
    class Encryption
    {
        /// <summary>
        /// Encrypts all sensitive data in an object
        /// </summary>
        /// <param name="obj">Object to be encrypted.</param>
        /// <returns>New instantiation of encrypted object.</returns>
        public static object EncryptValues(object obj)
        {
            return IterateMembers(obj, CSCore.Utils.CommonHelper.Encrypt,true);
        }

        /// <summary>
        /// Decrypts all sensitive data in an object
        /// </summary>
        /// <param name="obj">Object to be decrypted.</param>
        /// <returns>New instantiation of decrypted object.</returns>
        public static object DecryptValues(object obj)
        {
            return IterateMembers(obj, CSCore.Utils.CommonHelper.Decrypt,false);
        }

        private static object GetSpecificAttributePropertyValue(object obj, Type attr)
        {
            //Get the default encrption status property
            IEnumerable<PropertyInfo> props = obj.GetType().GetProperties().Where(
                  prop => Attribute.IsDefined(prop, typeof(IsEncryptedAttribute)));
            if (props.Count() > 0)
            {
                //Check if object was already encrypted
                return props.First().GetValue(obj, null);
            }
            return null;
        }

        /// <summary>
        /// This method will set the default encryption status property of an object to True or False to keep track of
        /// the encryption status of an object. This will prevent an object from being encrypted/decrypted multiple times.
        /// </summary>
        /// <param name="obj">Object that was encrypted/decrypted.</param>
        /// <param name="attr">The attribute that is set on encryptor status</param>
        /// <param name="value">True/False</param>
        private static void SetSpecificAttributePropertyValue(object obj, Type attr, object value)
        {
            //Set the default encryption status property
            IEnumerable<PropertyInfo> props = obj.GetType().GetProperties().Where(
                  prop => Attribute.IsDefined(prop, typeof(IsEncryptedAttribute)));
            if (props.Count() > 0)
                props.First().SetValue(obj, value, null);
        }

        private static object IterateMembers(object obj, Func<string, string> Encryptor,bool isEncrypting)
        {
            //Only if not already encrypted
            if ((bool)GetSpecificAttributePropertyValue(obj, typeof(IsEncryptedAttribute)) != isEncrypting)
            {
                //Encrypt all property values
                Type type = obj.GetType();
                PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo property in properties)
                {
                    if (property.GetCustomAttributes(typeof(SensitiveDataAttribute), false).Length > 0)
                    {
                        Type propertyType = property.PropertyType;
                        if (propertyType.IsPrimitive || propertyType == typeof(Decimal) || propertyType == typeof(String))
                        {
                            object value = property.GetValue(obj, null);
                            if (value != null)
                            {
                                string data = Convert.ToString(value);
                                if (data.Length > 0)
                                {
                                    value = Encryptor(Convert.ToString(value));
                                    property.SetValue(obj, value, null);
                                }
                            }
                        }
                        else
                        {
                            property.SetValue(obj, IterateMembers(property.GetValue(obj, null), Encryptor,isEncrypting), null);
                        }
                    }
                }
                //Set this object as encrypted
                SetSpecificAttributePropertyValue(obj, typeof(IsEncryptedAttribute), isEncrypting);
            }
            return obj;
        }
    }

    /// <summary>
    /// Custom attribute to be applied to properties with sensitive data
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    sealed class SensitiveDataAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    sealed class IsEncryptedAttribute : Attribute
    {
    }

}
