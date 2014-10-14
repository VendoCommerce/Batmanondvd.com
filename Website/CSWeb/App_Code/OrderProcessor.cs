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
        public static void ProcessAllOrders( HttpSessionStateBase session)
        {
            Hashtable AllItems = new OrderManager().GetBatchProcessOrders();
            List<Order> orders = (List<Order>)AllItems["allOrders"];
            string baseUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
            foreach (Order orderItem in orders)
            {
                try
                {
                  HttpContext.Current.Session["oid"] = orderItem.OrderId;
                    CSCore.Utils.CommonHelper.HttpPost(baseUrl + "/authorizeorder.aspx","");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error :: " + e.Message);
                }
            }
        }

        public static void ProcessOrder(HttpSessionStateBase session,HttpRequest request,HttpResponse response,int orderId)
        {
            ClientCartContext CartContext = session["ClientOrderData"] as ClientCartContext;
            string[] parts = request.Url.AbsolutePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (session["oid"] != null)
            {
                orderId = Convert.ToInt32(session["oid"].ToString());
            }
            else
            {
                orderId = CartContext.OrderId;
            }
            Order orderData = CSResolve.Resolve<IOrderService>().GetOrderDetails(orderId, true);


            if (orderData.OrderStatusId == 2)
            {
                // this means that  customer clicked back, so should be directed to receipt page.
                response.Redirect("receipt.aspx");
            }

            if (session["oid"] == null && OrderHelper.IsCustomerOrderFlowCompleted(CartContext.OrderId))
            {
                response.Redirect("receipt.aspx");
            }

            //if (!IsPostBack)
            //{
                //Calculate and save tax
                new CSWeb.FulfillmentHouse.DataPakTax().PostOrderToDataPak(orderId);

                string[] testCreditCards;

                testCreditCards = ResourceHelper.GetResoureValue("TestCreditCard").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); ;

                foreach (string word in testCreditCards)
                {
                    if (orderData.CreditInfo.CreditCardNumber.Equals(word))
                    {
                        CSResolve.Resolve<IOrderService>().UpdateOrderStatus(orderData.OrderId, 7);
                        // This will avoid order from getting posted to OMX for test orders
                        response.Redirect("receipt.aspx");
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
                        ////if (!authSuccess)
                        ////    OrderHelper.SendOrderDeclinedEmail(orderId);
                    }
                    catch (Exception ex)
                    {
                        CSCore.CSLogger.Instance.LogException("AuthorizeOrder - auth error - orderid: " + Convert.ToString(orderId), ex);

                        throw;
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
                            CSCore.CSLogger.Instance.LogException("AuthorizeOrder - fulfillment post error - orderid: " + Convert.ToString(orderId), ex);

                            throw;
                        }

                        if (request.QueryString != null)
                        {
                            response.Redirect("receipt.aspx?" + request.QueryString);
                        }
                        else
                        {
                            response.Redirect("receipt.aspx");
                        }
                    }
                }
                else
                {
                    response.Redirect(string.Format("carddecline.aspx?returnUrl={0}", string.Concat("/", string.Join("/", parts, 0, parts.Length - 1), "/receipt.aspx")), true);
                }
            //}
            response.Redirect("receipt.aspx");


        }
    }
}