using CSCore.DataHelper;
using System.Data.SqlClient;


namespace Cornerstone.Core.Data.CreditCard
{
    public static class CreditCardAccess
    {
        public static SqlDataReader GetCreditCardTypes()
        {
            string connString = ConfigHelper.GetDBConnection();
            string proc = "pr_credit_card_type_get";
            return BaseSqlHelper.ExecuteReader(connString, proc);
        }
        public static void GetCreditCardInfo()
        {
            string connString = ConfigHelper.GetDBConnection();
            string proc = "pr_credit_card_type_get";
            BaseSqlHelper.ExecuteNonQuery(connString, proc);
        }
        public static void SaveCreditCardInfo(int customerId, string cardHolderName, string cardNumber, int cardTypeId, int expiryMonth, int expiryYear, int cvsNumber, int addressId, int createdBy)
        {
            string connString = ConfigHelper.GetDBConnection();
            string proc = "pr_credit_card_type_get";
             BaseSqlHelper.ExecuteNonQuery(connString, proc);
        }
    }
}
