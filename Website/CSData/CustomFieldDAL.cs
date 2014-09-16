using System;
using System.Collections.Generic;
using System.Linq;
using CSCore.DataHelper;
using System.Data.SqlClient;

namespace CSData
{
    public class CustomFieldDAL
    {


        static CustomFieldDAL()
        {
        }

        public static List<CustomField> GetCustomFields()
        {

            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {

                var query = from c in context.CustomFields
                            select c;
                return query.ToList<CustomField>();
            }

        }

        public static void SaveField(string fieldName)
        {
            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {
                CustomField cat = new CustomField
                {

                    FieldName = fieldName,
                    FieldType = 1,
                    Active = true
                };

                context.CustomFields.InsertOnSubmit(cat);
                context.SubmitChanges();
            }

        }

        public static void UpdateField(int fieldId, string fieldName)
        {
            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {
                CustomField Val = context.CustomFields.FirstOrDefault(x => x.FieldId == fieldId);
                if (Val != null)
                {

                    Val.FieldName = fieldName;
                    context.SubmitChanges();
                }
            }
        }

        public static void RemoveField(int fieldId)
        {
            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {

                CustomField Val = context.CustomFields.FirstOrDefault(x => x.FieldId == fieldId);

                context.CustomFields.DeleteOnSubmit(Val);
                context.SubmitChanges();

                
            }

        }

    }//end of the class


}
