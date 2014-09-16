using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CSCore.DataHelper;

namespace CSData
{
    public class AttributesDAL
    {
        public static SqlDataReader GetAllAttributeValues (string objectName, int objectItemId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_get_item_attribute_values";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("ObjectName", objectName);
            parameters[1] = new SqlParameter("ObjectItemID", objectItemId);
            return BaseSqlHelper.ExecuteReader(connectionString, procName, parameters);
        }

        public static SqlDataReader GetAllAttributeValuesExtended(string objectName, int objectItemId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_get_item_attribute_values_ext";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("ObjectName", objectName);
            parameters[1] = new SqlParameter("ObjectItemID", objectItemId);
            return BaseSqlHelper.ExecuteReader(connectionString, procName, parameters);
        }

        public static int SaveAttribute(string attributeName, string description)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_save_attribute";
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("AttributeName", attributeName);
            parameters[1] = new SqlParameter("Description", description);
            parameters[2] = new SqlParameter() { ParameterName = "AttributeID", Direction = System.Data.ParameterDirection.Output, SqlDbType = System.Data.SqlDbType.Int, Size = 4 };

            BaseSqlHelper.ExecuteScalar(connectionString, procName, parameters);

            return Convert.ToInt32(parameters[2].Value);
        }

        public static void SaveAttributeById(int attributeId, string attributeName, string description, string defaultValueTypeName)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_save_attribute_byid";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("AttributeId", attributeId));
            parameters.Add(new SqlParameter("AttributeName", attributeName));            
            parameters.Add(new SqlParameter("Description", description));
            parameters.Add(new SqlParameter("DefaultValueTypeName", defaultValueTypeName));
            BaseSqlHelper.ExecuteScalar(connectionString, procName, parameters.ToArray());
        }

        public static int SaveAttributeValue(string objectName, string attributeName, int itemId, string value)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_save_attribute_value";
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("ObjectName", objectName);
            parameters[1] = new SqlParameter("AttributeName", attributeName);
            parameters[2] = new SqlParameter("ObjectItemID", itemId);
            parameters[3] = new SqlParameter("Value", value);
            parameters[4] = new SqlParameter() { ParameterName = "ObjectAttributeValueID", Direction = System.Data.ParameterDirection.Output, SqlDbType = System.Data.SqlDbType.Int, Size = 4 };

            BaseSqlHelper.ExecuteScalar(connectionString, procName, parameters);

            return Convert.ToInt32(parameters[4].Value);
        }

        public static void SaveAttributeValues(string objectName, int itemId, string attributeValuesXml)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_save_attribute_values";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("ObjectName", objectName));
            parameters.Add(new SqlParameter("ObjectItemId", itemId));
            parameters.Add(new SqlParameter("AttributeValuesXml", attributeValuesXml));

            BaseSqlHelper.ExecuteScalar(connectionString, procName, parameters.ToArray());
        }

        public static void DeleteAttributeValues(string objectName, int itemId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_remove_item_attribute_values";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("ObjectName", objectName)); 
            parameters.Add(new SqlParameter("ObjectItemID", itemId));            
            BaseSqlHelper.ExecuteScalar(connectionString, procName, parameters.ToArray());
        }

        public static void DeleteSingleAttributeValue(string objectName, int itemId, string attributeNamesXml)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_remove_single_item_attribute_value";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("ObjectName", objectName));
            parameters.Add(new SqlParameter("ObjectItemID", itemId));
            parameters.Add(new SqlParameter("AttributeNamesXml", attributeNamesXml));
            BaseSqlHelper.ExecuteScalar(connectionString, procName, parameters.ToArray());
        }

        public static void DeleteSingleObjectAttribute(int objectId, int attributeId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_remove_single_object_attribute";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("ObjectId", objectId));
            parameters.Add(new SqlParameter("AttributeId", attributeId));            
            BaseSqlHelper.ExecuteScalar(connectionString, procName, parameters.ToArray());
        }


        public static void DeleteAttribute(string attributeName)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_remove_attribute";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("Name", attributeName));
            BaseSqlHelper.ExecuteScalar(connectionString, procName, parameters.ToArray());
        }

        /// <summary>
        /// Gets list of Attributes associated to given object.
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public static SqlDataReader GetObjectAttributes(string objectName)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_get_object_attributes";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ObjectName", objectName);
            return BaseSqlHelper.ExecuteReader(connectionString, procName, parameters);
        }

        /// <summary>
        /// Gets list of Objects associated to given attribute.
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static SqlDataReader GetAttributeObjects(string attributeName)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_get_attribute_objects";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("AttributeName", attributeName);
            return BaseSqlHelper.ExecuteReader(connectionString, procName, parameters);
        }

        public static SqlDataReader GetAllOjbects()
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_get_all_objects";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return BaseSqlHelper.ExecuteReader(connectionString, procName, parameters.ToArray());
        }

        public static SqlDataReader GetAllAttributes()
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_get_all_attributes";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return BaseSqlHelper.ExecuteReader(connectionString, procName, parameters.ToArray());
        }

        public static SqlDataReader GetAllValueTypes()
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_get_all_value_types";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return BaseSqlHelper.ExecuteReader(connectionString, procName, parameters.ToArray());
        }

        public static SqlDataReader GetAllObjectAttributeTypes()
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_get_all_object_attribute_types";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return BaseSqlHelper.ExecuteReader(connectionString, procName, parameters.ToArray());
        }

        public static void SaveObjectAttribute(int objectId, int attributeId, int objectAttributeTypeId, string description, string displayLabel)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String procName = "pr_save_object_attribute";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("ObjectId", objectId));
            parameters.Add(new SqlParameter("AttributeId", attributeId));
            parameters.Add(new SqlParameter("ObjectAttributeTypeId", objectAttributeTypeId));
            parameters.Add(new SqlParameter("Description", description));
            parameters.Add(new SqlParameter("DisplayLabel", displayLabel));
            BaseSqlHelper.ExecuteScalar(connectionString, procName, parameters.ToArray());
        }

    }
}
