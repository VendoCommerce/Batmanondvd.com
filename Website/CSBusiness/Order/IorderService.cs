using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using CSBusiness.ShoppingManagement;
using CSCore;
using CSBusiness.Attributes;


namespace CSBusiness.OrderManagement
{
    public interface IOrderService
    {
        SqlDataReader GetOrderSummary(DateTime? startDate, DateTime? endDate, int versionId, int pathId);
        int SaveOrder(ClientCartContext custData);
        void  UpdateOrderAfterUpSell(int orderId,  Cart cartData);
        void SaveOrder(int OrderId, string transactionCode, string authCode, int orderStatusId);
        void SaveOrderInfo(int orderId, int orderStatusId, string fullfillmentRequest, string fullfillResponse);
        void UpdateOrder(int orderId, ClientCartContext custData);
        void UpdateOrderTax(int orderId, decimal tax);
        void UpdateOrderPath(int orderId, int pathId);
        void UpdateOrderStatus(int orderId, int orderStatusId);
        List<Order> GetAllOrders(DateTime? startDate, DateTime? endDate, bool includeArchiveData, int startRec, int endRec, out int totalCount);
        List<Order> GetAllOrders(DateTime? startDate, DateTime? endDate, bool includeArchiveData, int startRec, int endRec, string firstName, string lastName, string email, out int totalCount);
        Order GetOrder(int orderId);
        Order GetOrderDetails(int orderId);
        Order GetOrderDetails(int orderId, bool paymentInfo);
        List<Pair<string, string>> GetOrderSummary(DateTime? startDate, DateTime? endDate, bool includeArchiveData);
        void RemoveOrder(int orderId);
        void UpdateOrderAttributes(int orderId, IDictionary<string, AttributeValue> orderAttributeValues, int? orderStatusId);
    }
}