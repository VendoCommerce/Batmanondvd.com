using System.Collections.Generic;
using CSBusiness.CustomerManagement;
using System.Data.SqlClient;

namespace CSBusiness
{
    public interface ICustomerService
    {

        List<Customer> GetAllCustomers(string firstName, string LastName, string Email, int userTypeId, int startRec, int endRec, out int totalCount);

        List<Customer> GetAllCustomerOrders(string firstName, string LastName, string Email, int startRec, int endRec, out int totalCount);

        SqlDataReader GetAllCustomerOrdersDetail(string firstName, string LastName, string Email);

        int Validate(string username, string password);

        Customer GetCustomer(int customerId);

        Customer GetCustomerDetails(int customerId);

        Address GetAddressById(int addressId);

        void UpdateUser(Customer custData);

        int UpdateCustomer(int orderId, Customer custData);

        int InsertCartAbandonment(Customer custData,ClientCartContext context);

        void RemoveCartAbandonment(int cartAbandonmentId);
    }
}


