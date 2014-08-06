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

namespace CSWeb.FulfillmentHouse
{
    public class Acmg
    {
        XmlNode config = null;
        public Acmg()
        {
            config = GetConfig();            
        }
        public string GetRequest(Order orderItem)
        {
            String strXml = String.Empty;
            string lineItems = String.Empty;
            orderItem.LoadAttributeValues();
            using (StringWriter str = new StringWriter())
            {

                using (XmlTextWriter xml = new XmlTextWriter(str))
                {
                    //root node

                    int i = 0;
                    foreach (Sku s in orderItem.SkuItems)
                    {
                        i++;
                        lineItems += s.SkuCode + "," + s.FullPrice + "," + s.Quantity + "," + "0";
                        if (i != orderItem.SkuItems.Count)
                        {
                            lineItems += "|";
                        }
                    }
                    xml.WriteStartElement("placeOrder");
                    xml.WriteElementString("sessionId", orderItem.AttributeValues["customorderid"].Value); 
                    xml.WriteStartElement("orders");
                        xml.WriteStartElement("orders");
                            xml.WriteElementString("orderId", orderItem.AttributeValues["customorderid"].Value);
                            xml.WriteElementString("partnerId", config.Attributes["partnerId"].Value);
                            xml.WriteElementString("partnerPass", config.Attributes["partnerPass"].Value);
                            xml.WriteStartElement("orderInfo");
                                xml.WriteElementString("lineItems", lineItems);
                                xml.WriteElementString("subtotal", orderItem.FullPriceSubTotal.ToString());
                                xml.WriteElementString("tax", orderItem.FullPriceTax.ToString());
                                xml.WriteElementString("orderTotal", (orderItem.FullPriceSubTotal + orderItem.FullPriceTax + orderItem.ShippingCost).ToString());
                                xml.WriteElementString("freightAmount", orderItem.ShippingCost.ToString());
                                xml.WriteElementString("shippingMethod", config.Attributes["shippingMethod"].Value);
                                xml.WriteElementString("submitted", orderItem.CreatedDate.ToString("yyyyMMdd hh:mm:ss"));
                            xml.WriteEndElement();//orderInfo ends

                            xml.WriteStartElement("userInfo");
                                xml.WriteElementString("userIpAddress", orderItem.IpAddress);
                            xml.WriteEndElement();//userInfo ends

                            xml.WriteStartElement("paymentInfo");
                                xml.WriteElementString("cardCcv", orderItem.CreditInfo.CreditCardCSC);
                                xml.WriteElementString("cardNumber", orderItem.CreditInfo.CreditCardNumber);
                                xml.WriteElementString("cardExpMonth", orderItem.CreditInfo.CreditCardExpired.ToString("MM"));
                                xml.WriteElementString("cardExpYear", orderItem.CreditInfo.CreditCardExpired.ToString("yyyy"));
                            xml.WriteEndElement();//paymentInfo ends

                            xml.WriteStartElement("billingNameAddress");
                                xml.WriteElementString("firstName", orderItem.CustomerInfo.BillingAddress.FirstName);
                                xml.WriteElementString("lastName", orderItem.CustomerInfo.BillingAddress.LastName);
                                xml.WriteElementString("address", orderItem.CustomerInfo.BillingAddress.Address1);
                                xml.WriteElementString("address2", orderItem.CustomerInfo.BillingAddress.Address2);
                                xml.WriteElementString("city", orderItem.CustomerInfo.BillingAddress.City);
                                xml.WriteElementString("state", orderItem.CustomerInfo.BillingAddress.StateProvinceName);
                                xml.WriteElementString("zip", orderItem.CustomerInfo.BillingAddress.ZipPostalCode);
                                xml.WriteElementString("country", orderItem.CustomerInfo.BillingAddress.CountryCode.Trim());
                                xml.WriteElementString("phone", orderItem.CustomerInfo.BillingAddress.PhoneNumber);
                                xml.WriteElementString("email", orderItem.CustomerInfo.BillingAddress.Email);
                            xml.WriteEndElement();//billingNameAddress ends

                            xml.WriteStartElement("shippingNameAddress");
                                xml.WriteElementString("firstName", orderItem.CustomerInfo.ShippingAddress.FirstName);
                                xml.WriteElementString("lastName", orderItem.CustomerInfo.ShippingAddress.LastName);
                                xml.WriteElementString("address", orderItem.CustomerInfo.ShippingAddress.Address1);
                                xml.WriteElementString("address2", orderItem.CustomerInfo.ShippingAddress.Address2);
                                xml.WriteElementString("city", orderItem.CustomerInfo.ShippingAddress.City);
                                xml.WriteElementString("state", orderItem.CustomerInfo.ShippingAddress.StateProvinceName);
                                xml.WriteElementString("zip", orderItem.CustomerInfo.ShippingAddress.ZipPostalCode);
                                xml.WriteElementString("country", orderItem.CustomerInfo.ShippingAddress.CountryCode.Trim());
                                xml.WriteElementString("phone", orderItem.CustomerInfo.ShippingAddress.PhoneNumber);
                                xml.WriteElementString("email", orderItem.CustomerInfo.ShippingAddress.Email);
                            xml.WriteEndElement();//shippingNameAddress ends

                            string sid = "default";
                            string afid = string.Empty;
                            string cid = string.Empty;
                            string adid = string.Empty;
                            if (orderItem.CustomFiledInfo != null)
                            {
                                if (orderItem.CustomFiledInfo.Count > 0)
                                {
                                    try
                                    {
                                        if (orderItem.CustomFiledInfo.Find(x => x.FieldName.ToLower() == "afid") != null)
                                        {
                                            afid = orderItem.CustomFiledInfo.Find(x => x.FieldName.ToLower() == "afid").FieldValue.ToString();
                                        }
                                        if (orderItem.CustomFiledInfo.Find(x => x.FieldName.ToLower() == "sid") != null)
                                        {
                                            sid = orderItem.CustomFiledInfo.Find(x => x.FieldName.ToLower() == "sid").FieldValue.ToString();
                                        }
                                        if (orderItem.CustomFiledInfo.Find(x => x.FieldName.ToLower() == "adid") != null)
                                        {
                                            adid = orderItem.CustomFiledInfo.Find(x => x.FieldName.ToLower() == "adid").FieldValue.ToString();
                                        }
                                        if (orderItem.CustomFiledInfo.Find(x => x.FieldName.ToLower() == "cid") != null)
                                        {
                                            cid = orderItem.CustomFiledInfo.Find(x => x.FieldName.ToLower() == "cid").FieldValue.ToString();
                                        }

                                    }
                                    catch { }
                                }
                            }
                            string cmpCode = adid + "," + cid;
                            if (cmpCode.Equals(","))
                            {
                                cmpCode = "";
                            }
                            xml.WriteStartElement("trackingInfo");
                                xml.WriteElementString("source", afid);
                                xml.WriteElementString("dnis", config.Attributes["dnis"].Value);
                                xml.WriteElementString("sid", sid);
                                xml.WriteElementString("orderPage", orderItem.AttributeValues["landing_url"].Value);
                                xml.WriteElementString("campaign_tracking_code", cmpCode);
                            xml.WriteEndElement();//trackingInfo ends

                            xml.WriteEndElement();//order ends
                        xml.WriteEndElement();//orders ends
                    xml.WriteEndElement();// placeOrder ends                    
                    ////flush results to string object
                    strXml = str.ToString();
                }
            }
            return strXml;
        }
        public bool PostOrder(int orderId)
        {
            bool result = false;
            Order orderItem = new OrderManager().GetBatchProcessOrders(orderId);            
            string req = String.Format(CommonHelper.GetSoapEnvelope(), new Acmg().GetRequest(orderItem));
            string res = CommonHelper.SoapRequest(req, config.Attributes["transactionUrl"].Value,Convert.ToInt32(config.Attributes["timeOut"].Value));
            Dictionary<string, AttributeValue> orderAttributes =  new Dictionary<string, AttributeValue>();
            orderAttributes.Add("Request", new CSBusiness.Attributes.AttributeValue(req));
            orderAttributes.Add("Response", new CSBusiness.Attributes.AttributeValue(res));

            if (res.ToLower().Contains("good"))
            {
                CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId,orderAttributes,2);
                result = true;
            }
            else if (orderItem.CreditInfo.CreditCardNumber.Equals("4111111111111111"))
            {   
                CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, 5);
                result = true;
            }
            else
            {
                CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, 5);
                result = false;
            }          
            //Prepaid Check
            GetPrepiadResponse(orderId, orderItem.CreditInfo.CreditCardNumber.Substring(0, 13));

            return result;
        }
        public void GetPrepiadResponse(int orderId, string cardNumber)
        {
            String strXml = String.Empty;           
            using (StringWriter str = new StringWriter())
            {
                using (XmlTextWriter xml = new XmlTextWriter(str))
                {
                    xml.WriteStartElement("isPrepaidCard");
                        xml.WriteElementString("key",config.Attributes["prepaidCheckKey"].Value);
                        xml.WriteElementString("binCode",cardNumber);                    
                    xml.WriteEndElement();// isPrepaidCard ends                    
                    ////flush results to string object
                    strXml = str.ToString();
                }
            }
            string PrePaidRes = CommonHelper.SoapRequest(String.Format(CommonHelper.GetSoapEnvelope(), strXml), config.Attributes["prepaidCheckUrl"].Value, Convert.ToInt32(config.Attributes["timeOut"].Value));

            Dictionary<string, AttributeValue> orderAttributes = new Dictionary<string, AttributeValue>();
            orderAttributes.Add("PrepaidRequest", new CSBusiness.Attributes.AttributeValue(String.Format(CommonHelper.GetSoapEnvelope(), strXml)));
            orderAttributes.Add("PrepaidResponse", new CSBusiness.Attributes.AttributeValue(PrePaidRes));
            if (PrePaidRes.ToLower().Contains("b:0"))
            {
                orderAttributes.Add("IsPrepaid", new CSBusiness.Attributes.AttributeValue("false"));
            }
            else
            {
                orderAttributes.Add("IsPrepaid", new CSBusiness.Attributes.AttributeValue("true"));
            }
            CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, null);
            
        }
        private XmlNode GetConfig()
        {
            return OrderHelper.GetDefaultFulFillmentHouseConfig();
        }
    }
}