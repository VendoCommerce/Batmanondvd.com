using System;
using CSCore.DataHelper;
using System.Data.SqlClient;

namespace CSData
{
    public class CategoryDAL
    {


        static CategoryDAL()
        {
        }


        public static SqlDataReader GetCategory()
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_get_category";

            return BaseSqlHelper.ExecuteReader(connectionString, ProcName, null);
           
        }


     

        /// <summary>
        /// LINQ to SQL Connection for Commitment
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <param name="Ordernum"></param>
        public static void SaveCategory(string CategoryName, int Ordernum)
        {
            using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
            {

                Category cat = new Category
                {

                    Title = CategoryName,
                    Orderno = Ordernum,
                    Visible = true
                };

                context.Categories.InsertOnSubmit(cat);
                context.SubmitChanges();

            }
        }
            

        public static void UpdateCategory(int categoryId, string Name, bool active, int orderNo)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_update_category";
            SqlParameter[] ParamVal = new SqlParameter[4];
            ParamVal[0] = new SqlParameter("CategoryId", categoryId);
            ParamVal[1] = new SqlParameter("Title", Name);
            ParamVal[2] = new SqlParameter("Active", (active) ? 1:0);
            ParamVal[3] = new SqlParameter("OrderNo", orderNo);
            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);
            
        }

        public static void RemoveCategory(int categoryId)
        {
            string connectionString = ConfigHelper.GetDBConnection();
            String ProcName = "pr_remove_category";
            SqlParameter[] ParamVal =  new SqlParameter[1];
            ParamVal[0] = new SqlParameter("categoryId", categoryId);

            BaseSqlHelper.ExecuteNonQuery(connectionString, ProcName, ParamVal);

        }
      
    }//end of the class


}
