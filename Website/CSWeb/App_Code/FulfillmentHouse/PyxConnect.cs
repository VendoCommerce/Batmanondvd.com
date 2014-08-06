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
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace CSWeb.FulfillmentHouse
{
    public class Pyx
    {
        System.Xml.XmlNode config = null;
        public Pyx()
        {
            config = GetConfig();
        }

        public bool PostOrder(int orderId)
        {
            bool result = false;

            try
            {
                Order orderItem = new OrderManager().GetBatchProcessOrders(orderId);

                if (!orderItem.AttributeValuesLoaded)
                    orderItem.LoadAttributeValues();

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                PyxConnect.Api.PYXConnectServiceClient client = new PyxConnect.Api.PYXConnectServiceClient("EndPointHTTP", config.Attributes["transactionUrl"].Value);

                PyxConnect.Api.Requestor requestor = new PyxConnect.Api.Requestor();

                requestor.AccountID = config.Attributes["accountId"].Value;
                requestor.Password = config.Attributes["password"].Value;

                PyxConnect.Api.Customer customer = new PyxConnect.Api.Customer();

                customer.ExternalCustomerId = orderItem.CustomerId.ToString();
                customer.FirstName = orderItem.CustomerInfo.BillingAddress.FirstName;
                customer.LastName = orderItem.CustomerInfo.BillingAddress.LastName;
                customer.Address1 = orderItem.CustomerInfo.BillingAddress.Address1;
                customer.Address2 = orderItem.CustomerInfo.BillingAddress.Address2;
                customer.City = orderItem.CustomerInfo.BillingAddress.City;
                customer.State = CSBusiness.StateManager.GetAllStates(orderItem.CustomerInfo.BillingAddress.CountryId).Single<StateProvince>(x =>
                {
                    return x.StateProvinceId == orderItem.CustomerInfo.BillingAddress.StateProvinceId;
                }).Abbreviation.Trim();
                customer.Country = PyxConnect.Api.CountryEnum.USA;
                customer.Zipcode = orderItem.CustomerInfo.BillingAddress.ZipPostalCode;
                customer.HomePhone = orderItem.CustomerInfo.BillingAddress.PhoneNumber;
                customer.EmailAddress = orderItem.CustomerInfo.Email;
                customer.ClientCode = config.Attributes["clientCode"].Value;

                Dictionary<string, AttributeValue> orderAttributes = new Dictionary<string, AttributeValue>();
                orderAttributes.Add("RequestCustomer", new CSBusiness.Attributes.AttributeValue(CSCore.Serializer.Serialize(customer)));
                PyxConnect.Api.Response custResponse = null;
                try
                {
                    custResponse = client.CreateCustomer(requestor, customer);

                    orderAttributes.Add("ResponseCustomer", new CSBusiness.Attributes.AttributeValue(CSCore.Serializer.Serialize(custResponse)));
                }
                catch (Exception ex)
                {
                    orderAttributes.Add("ResponseCustomer", new CSBusiness.Attributes.AttributeValue("Error : " + ex.Message + " " + ex.StackTrace));
                }

                CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, 5);

                if (custResponse != null && custResponse.Status.ToLower() == "success")
                {
                    PyxConnect.Api.OrderHeader orderHeader = new PyxConnect.Api.OrderHeader();

                    orderHeader.ExternalOrderId = orderId.ToString();
                    orderHeader.OrderSource = PyxConnect.Api.OrderSourceEnum.Web;
                    orderHeader.EffortCode = config.Attributes["effortCode"].Value;
                    orderHeader.OrderDate = DateTime.Now;
                    orderHeader.CustomerId = custResponse.ID;

                    // billing            
                    orderHeader.BillToFirstName = orderItem.CustomerInfo.BillingAddress.FirstName;
                    orderHeader.BillToLastName = orderItem.CustomerInfo.BillingAddress.LastName;
                    orderHeader.BillToAddress1 = orderItem.CustomerInfo.BillingAddress.Address1;
                    orderHeader.BillToAddress2 = orderItem.CustomerInfo.BillingAddress.Address2;
                    orderHeader.BillToCity = orderItem.CustomerInfo.BillingAddress.City;
                    orderHeader.BillToCountry = PyxConnect.Api.CountryEnum.USA;
                    orderHeader.BillToState = CSBusiness.StateManager.GetAllStates(orderItem.CustomerInfo.BillingAddress.CountryId).Single<StateProvince>(x =>
                    {
                        return x.StateProvinceId == orderItem.CustomerInfo.BillingAddress.StateProvinceId;
                    }).Abbreviation.Trim();
                    orderHeader.BillToZipcode = orderItem.CustomerInfo.BillingAddress.ZipPostalCode;

                    // shipping
                    orderHeader.ShipToFirstName = orderItem.CustomerInfo.ShippingAddress.FirstName;
                    orderHeader.ShipToLastName = orderItem.CustomerInfo.ShippingAddress.LastName;
                    orderHeader.ShipToAddress1 = orderItem.CustomerInfo.ShippingAddress.Address1;
                    orderHeader.ShipToAddress2 = orderItem.CustomerInfo.ShippingAddress.Address2;
                    orderHeader.ShipToCity = orderItem.CustomerInfo.ShippingAddress.City;
                    orderHeader.ShipToCountry = PyxConnect.Api.CountryEnum.USA;
                    orderHeader.ShipToState = CSBusiness.StateManager.GetAllStates(orderItem.CustomerInfo.ShippingAddress.CountryId).Single<StateProvince>(x =>
                    {
                        return x.StateProvinceId == orderItem.CustomerInfo.ShippingAddress.StateProvinceId;
                    }).Abbreviation.Trim();
                    orderHeader.ShipToZipcode = orderItem.CustomerInfo.ShippingAddress.ZipPostalCode;

                    // credit card
                    orderHeader.NameOnCC = orderHeader.BillToFirstName + " " + orderHeader.BillToLastName;
                    string ccType = null;
                    switch ((CSBusiness.CreditCard.CreditCardTypeEnum)orderItem.CreditInfo.CreditCardType)
                    {
                        case CSBusiness.CreditCard.CreditCardTypeEnum.VISA:
                            ccType = "VI";
                            break;
                        case CSBusiness.CreditCard.CreditCardTypeEnum.AmericanExpress:
                            ccType = "AX";
                            break;
                        case CSBusiness.CreditCard.CreditCardTypeEnum.Discover:
                            ccType = "DC";
                            break;
                        case CSBusiness.CreditCard.CreditCardTypeEnum.MasterCard:
                            ccType = "MC";
                            break;
                    }
                    orderHeader.CCType = ccType;
                    orderHeader.CCNumber = orderItem.CreditInfo.CreditCardNumber;
                    orderHeader.CCExpirtyDate = orderItem.CreditInfo.CreditCardExpired.ToString("MMyy");
                    orderHeader.CVV = orderItem.CreditInfo.CreditCardCSC;

                    // order details
                    List<PyxConnect.Api.SurCharge> surcharges = new List<PyxConnect.Api.SurCharge>();
                    PyxConnect.Api.SurCharge surcharge = new PyxConnect.Api.SurCharge();

                    List<PyxConnect.Api.OrderDetail> orderDetails = new List<PyxConnect.Api.OrderDetail>();
                    PyxConnect.Api.OrderDetail skuItem;
                    int i = 1;
                    decimal merchandiseTotal = 0;
                    foreach (Sku sku in orderItem.SkuItems)
                    {
                        if (!sku.SkuCode.ToUpper().StartsWith("SHIP"))
                        {
                            merchandiseTotal += (sku.InitialPrice * sku.Quantity);

                            skuItem = new PyxConnect.Api.OrderDetail();
                            skuItem.LineNo = i;
                            skuItem.ItemCode = sku.SkuCode.ToUpper().StartsWith("5000-SYS") ? "5000-SYS" : sku.SkuCode;
                            skuItem.ItemName = sku.Title;
                            skuItem.ItemType = sku.SkuCode.ToUpper() == "5000-SYS-2" ? "STANDALONE" : "UPSELL";
                            skuItem.Quantity = sku.Quantity;
                            skuItem.UnitPrice = sku.InitialPrice;
                            skuItem.LineTotal = (sku.InitialPrice * sku.Quantity);
                            orderDetails.Add(skuItem);

                            i++;
                        }
                    }

                    foreach (Sku sku in orderItem.SkuItems)
                    {
                        if (sku.SkuCode.ToUpper() == "SHIP1" || sku.SkuCode.ToUpper() == "SHIP2" || sku.SkuCode.ToUpper() == "SHIP3")
                        {
                            surcharge = new PyxConnect.Api.SurCharge();

                            if (sku.SkuCode.ToUpper() == "SHIP1")
                            {
                                surcharge.SurchargeCode = "LOCATION";
                            }
                            else if (sku.SkuCode.ToUpper() == "SHIP2")
                            {
                                surcharge.SurchargeCode = "PRIORITY";
                            }
                            else if (sku.SkuCode.ToUpper() == "SHIP3")
                            {
                                surcharge.SurchargeCode = "RUSH";
                            }

                            surcharge.SurchargeAmount = sku.InitialPrice;

                            surcharges.Add(surcharge);
                        }
                    }

                    orderHeader.Orderdetail = orderDetails.ToArray();
                    orderHeader.Surcharge = surcharges.ToArray();

                    orderHeader.OrderTotal = orderItem.Total;
                    orderHeader.MerchandiseTotal = merchandiseTotal;
                    orderHeader.TaxAmount = orderItem.Tax;
                    orderHeader.TaxRate = orderItem.GetAttributeValue<decimal>("TaxRate", 0);
                    orderHeader.PHAmount = orderItem.ShippingCost;
                    orderHeader.ShippingMethod = "FDSMART";
                    orderHeader.ShipCharge = 0; // pass 0                
                    orderHeader.ClientCode = config.Attributes["clientCode"].Value;

                    orderAttributes = new Dictionary<string, AttributeValue>();
                    orderAttributes.Add("Request", new CSBusiness.Attributes.AttributeValue(CSCore.Serializer.Serialize(orderHeader)));

                    PyxConnect.Api.Response response = null;
                    try
                    {
                        response = client.CreateOrder(requestor, orderHeader);
                        orderAttributes.Add("Response", new CSBusiness.Attributes.AttributeValue(CSCore.Serializer.Serialize(response)));
                    }
                    catch (Exception ex)
                    {
                        orderAttributes.Add("Response", new CSBusiness.Attributes.AttributeValue("Error : " + ex.Message + " " + ex.StackTrace));
                    }

                    CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, 5);

                    if (response != null && response.Status.ToLower() == "success")
                    {
                        CSResolve.Resolve<IOrderService>().SaveOrder(orderId, response.OrderNumber, string.Empty, 2);

                        CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, 2);

                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CSCore.CSLogger.Instance.LogException(ex.Message, ex);
            }

            return result;
        }

        private XmlNode GetConfig()
        {
            return OrderHelper.GetDefaultFulFillmentHouseConfig();
        }
    }
}