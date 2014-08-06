using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using CSBusiness.OrderManagement;
using System.Collections;
using CSBusiness;
using CSCore.Utils;
using CSCore.DataHelper;
using CSBusiness.Resolver;
using CSBusiness.FulfillmentHouse;
using System.Xml.Linq;
using CSBusiness.Attributes;
using ChargifyNET;


namespace CSWeb.FulfillmentHouse
{
    public class Chargify
    {
         XmlNode config = null;
         public Chargify()
        {
            config = GetConfig();            
        }
        public string GetRequest(Order orderItem)
        {
            String strXml = String.Empty;
            string lineItems = String.Empty;
            return strXml;
        }
        public bool PostOrder(int orderId)
        {
            bool result = false;
            Order orderItem = new OrderManager().GetBatchProcessOrders(orderId);            
            

            orderItem.LoadAttributeValues();
            ChargifyConnect chargify = new ChargifyConnect();
            chargify.apiKey = config.Attributes["APIKey"].Value;
            chargify.Password = config.Attributes["Password"].Value;
            chargify.URL = config.Attributes["site"].Value;
            
            // Retrieve a list of all your products
            //IDictionary<int, IProduct> products = chargify.GetProductList();

            // Create a new customer
            ICustomer newCustomer = chargify.CreateCustomer(orderItem.CustomerInfo.ShippingAddress.FirstName, orderItem.CustomerInfo.ShippingAddress.LastName, orderItem.Email, "ConversionSystems", "CS_" + "DC_" + orderItem.OrderId.ToString());

            newCustomer.Email = orderItem.Email;
            newCustomer.ShippingAddress = orderItem.CustomerInfo.ShippingAddress.Address1 + "," + orderItem.CustomerInfo.ShippingAddress.Address2;
            newCustomer.ShippingCity = orderItem.CustomerInfo.ShippingAddress.City;
            newCustomer.ShippingCountry = orderItem.CustomerInfo.ShippingAddress.CountryCode;
            newCustomer.ShippingState = orderItem.CustomerInfo.ShippingAddress.StateProvinceName;
            newCustomer.ShippingZip = orderItem.CustomerInfo.ShippingAddress.ZipPostalCode;


            // Create a new customer and subscription
            //ICustomerAttributes charlie = new CustomerAttributes(orderItem.CustomerInfo.BillingAddress.FirstName, orderItem.CustomerInfo.BillingAddress.LastName, orderItem.Email, "", "CS_" + "DC_" + orderItem.OrderId.ToString());
            //charlie.ShippingAddress = orderItem.CustomerInfo.ShippingAddress.Address1 + "," + orderItem.CustomerInfo.ShippingAddress.Address2;
            //charlie.ShippingCity = orderItem.CustomerInfo.ShippingAddress.City;
            //charlie.ShippingCountry = orderItem.CustomerInfo.ShippingAddress.CountryCode;
            //charlie.ShippingState = orderItem.CustomerInfo.ShippingAddress.StateProvinceName;
            //charlie.ShippingZip = orderItem.CustomerInfo.ShippingAddress.ZipPostalCode;

            ICreditCardAttributes charliesPaymentInfo = new CreditCardAttributes();

            charliesPaymentInfo.FirstName = orderItem.CustomerInfo.BillingAddress.FirstName;
            charliesPaymentInfo.LastName = orderItem.CustomerInfo.BillingAddress.LastName;
            charliesPaymentInfo.ExpirationMonth = Convert.ToInt32(orderItem.CreditInfo.CreditCardExpired.ToString("MM"));
            charliesPaymentInfo.ExpirationYear = Convert.ToInt32(orderItem.CreditInfo.CreditCardExpired.ToString("yyyy"));
            charliesPaymentInfo.FullNumber = orderItem.CreditInfo.CreditCardNumber;
            charliesPaymentInfo.CVV = orderItem.CreditInfo.CreditCardCSC;
            charliesPaymentInfo.BillingAddress = orderItem.CustomerInfo.BillingAddress.Address1 + "," + orderItem.CustomerInfo.BillingAddress.Address2;
            charliesPaymentInfo.BillingCity = orderItem.CustomerInfo.BillingAddress.City;
            charliesPaymentInfo.BillingCountry = orderItem.CustomerInfo.BillingAddress.CountryCode;
            charliesPaymentInfo.BillingState = orderItem.CustomerInfo.BillingAddress.StateProvinceName;
            charliesPaymentInfo.BillingZip = orderItem.CustomerInfo.BillingAddress.ZipPostalCode;
            ISubscription newSubscription;
            Dictionary<string, AttributeValue> orderAttributes = new Dictionary<string, AttributeValue>();
            foreach (Sku Item in orderItem.SkuItems)
            {
                try
                {
                    newSubscription = chargify.CreateSubscription(Item.SkuCode.ToLower(), newCustomer.ChargifyID, charliesPaymentInfo);
                    if (newSubscription == null)
                    {
                    orderAttributes.Add("ChargifyCustomerId", new CSBusiness.Attributes.AttributeValue(newCustomer.ChargifyID.ToString()));
                    orderAttributes.Add("ChargifySubscriptionId", new CSBusiness.Attributes.AttributeValue(newSubscription.SubscriptionID.ToString()));
                    result = true;
                    }
                    else
                    {
                        result = false;

                    }
                }
                catch { }             
                
               
            }

            if (result)
            {
                CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, 2);
            }
            else
            {
                CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, 7);
            }
            
            return result;
        }
       
        private XmlNode GetConfig()
        {
            return OrderHelper.GetDefaultFulFillmentHouseConfig();
        }
    }
}