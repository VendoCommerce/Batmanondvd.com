using CSBusiness;
using CSBusiness.OrderManagement;
using CSBusiness.Resolver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSWeb.App_Code
{
    public class OrderProcessor
    {
        /// <summary>
        /// Loads all orders and processes them for gateway and fullfilment.
        /// </summary>
        public static void ProcessAllOrders()
        {
            Hashtable AllItems = new OrderManager().GetBatchProcessOrders();
            List<Order> orders = (List<Order>)AllItems["allOrders"];
            //string baseUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
            foreach (Order orderItem in orders)
            {
                try
                {
                    ProcessOrder(orderItem.OrderId);
                }
                catch (Exception ex)
                {
                    CSCore.CSLogger.Instance.LogException("Batch error - auth error - orderid: " + Convert.ToString(orderItem.OrderId), ex);
                    OrderHelper.SendOrderFailedEmail(orderItem.OrderId);
                }
            }
        }

        public static void ProcessOrder(int orderId)
        {
            Order orderData = CSResolve.Resolve<IOrderService>().GetOrderDetails(orderId, true);

            if (orderData.OrderStatusId == 2) return;

            //Calculate and save tax
            new CSWeb.FulfillmentHouse.DataPakTax().PostOrderToDataPak(orderId);

            string[] testCreditCards;

            testCreditCards = ResourceHelper.GetResoureValue("TestCreditCard").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); ;
            foreach (string word in testCreditCards)
            {
                if (orderData.CreditInfo.CreditCardNumber.Equals(word))
                {
                    CSResolve.Resolve<IOrderService>().UpdateOrderStatus(orderData.OrderId, 7);
                    UserSessions.InsertSessionEntry(HttpContext.Current, false, 0, 0, orderId);

                    return;
                }
            }
            bool authSuccess = false;
            // Check if payment gateway service is enabled or not.
            if (CSFactory.GetCacheSitePref().PaymentGatewayService)
            {
                try
                {
                    authSuccess = orderData.OrderStatusId == 4
                        || orderData.OrderStatusId == 5 // fulfillment failure (fulfillment was attempted after payment success), so don't charge again.
                        || OrderHelper.AuthorizeOrder(orderId);
                    if (!authSuccess)
                        OrderHelper.SendOrderDeclinedEmail(orderId);
                }
                catch (Exception ex)
                {
                    CSCore.CSLogger.Instance.LogException("AuthorizeOrder - auth error - orderid: " + Convert.ToString(orderId), ex);
                }
            }
            else
            {
                authSuccess = true;
            }

            if (authSuccess)
            {
                // Check if fulfillment gateway service is enabled or not.
                if (CSFactory.GetCacheSitePref().FulfillmentHouseService)
                {
                    try
                    {
                        new CSWeb.FulfillmentHouse.DataPak().PostOrderToDataPak(orderId);
                    }
                    catch (Exception ex)
                    {
                        CSCore.CSLogger.Instance.LogException("Fullfilment Error - orderid: " + Convert.ToString(orderId), ex);
                        OrderHelper.SendEmailToAdmins(orderId);
                    }
                }
            }
        }
    }
}