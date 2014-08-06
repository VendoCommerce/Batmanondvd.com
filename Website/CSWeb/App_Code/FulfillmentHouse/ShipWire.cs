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
    public class ShipWire
    {
        XmlNode config = null;
        public ShipWire()
        {
            config = GetConfig();
        }
        public string GetRequest(int orderId, bool CheckOrder, bool RejectedOrder)
        {
            String strXml = String.Empty;
            using (StringWriter str = new StringWriter())
            {

                using (XmlTextWriter xml = new XmlTextWriter(str))
                {
                    //root node
                    Order orderItem = new OrderManager().GetBatchProcessOrders(orderId);
                    List<StateProvince> states = StateManager.GetAllStates(0);
                    xml.WriteStartDocument();
                    xml.WriteWhitespace("\n");
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    xml.WriteStartElement("OrderList");
                    xml.WriteWhitespace("\n");

                        xml.WriteElementString("Username", "amaher@conversionsystems.com");
                        xml.WriteWhitespace("\n");
                        xml.WriteElementString("Password", "5922d4678819620d");
                        xml.WriteWhitespace("\n");
                        xml.WriteElementString("Server", "Test");
                        xml.WriteWhitespace("\n");
                        xml.WriteElementString("Referer", "Test");
                        xml.WriteWhitespace("\n");
                        xml.WriteStartElement("Order");
                        xml.WriteAttributeString("id", "TESTTSE" + orderId);
                        xml.WriteWhitespace("\n");
                        
                             xml.WriteElementString("Warehouse", "00");
                             xml.WriteWhitespace("\n");
                             xml.WriteStartElement("AddressInfo");
                             xml.WriteAttributeString("type", "ship");
                             xml.WriteWhitespace("\n");

                                 xml.WriteStartElement("Name");
                                 xml.WriteWhitespace("\n");

                                    xml.WriteElementString("Full", orderItem.CustomerInfo.ShippingAddress.FirstName + " " + orderItem.CustomerInfo.ShippingAddress.LastName);
                                    xml.WriteWhitespace("\n");

                                 xml.WriteEndElement();
                                 xml.WriteWhitespace("\n");
                                 xml.WriteElementString("Address1", orderItem.CustomerInfo.ShippingAddress.Address1);
                                 xml.WriteWhitespace("\n");
                                 xml.WriteElementString("Address2", orderItem.CustomerInfo.ShippingAddress.Address2);
                                 xml.WriteWhitespace("\n");
                                 xml.WriteElementString("City", orderItem.CustomerInfo.ShippingAddress.City);
                                 xml.WriteWhitespace("\n");
                                 StateProvince itemShippingStateProvince = states.FirstOrDefault(x => x.StateProvinceId == Convert.ToInt32(orderItem.CustomerInfo.ShippingAddress.StateProvinceId));
                                 if (itemShippingStateProvince != null)
                                 {
                                     xml.WriteElementString("State", itemShippingStateProvince.Abbreviation);
                                     xml.WriteWhitespace("\n");
                                 }
                                 else
                                 {
                                     xml.WriteElementString("State", string.Empty);
                                     xml.WriteWhitespace("\n");
                                 }
                                 xml.WriteElementString("Country", orderItem.CustomerInfo.ShippingAddress.CountryCode.Trim());
                                 xml.WriteWhitespace("\n");
                                 xml.WriteElementString("ZIP", orderItem.CustomerInfo.ShippingAddress.ZipPostalCode);
                                 xml.WriteWhitespace("\n");
                                 xml.WriteElementString("Phone", orderItem.CustomerInfo.BillingAddress.PhoneNumber);
                                 xml.WriteWhitespace("\n");
                                 xml.WriteElementString("Email", orderItem.Email);
                                 xml.WriteWhitespace("\n");
                           
                            xml.WriteEndElement();
                            xml.WriteElementString("Shipping", "GD");
                            xml.WriteWhitespace("\n");

                            xml.WriteStartElement("Item");
                            xml.WriteAttributeString("num", "0");
                            xml.WriteWhitespace("\n");

                                xml.WriteElementString("Code", "1000001");
                                xml.WriteWhitespace("\n");
                                xml.WriteElementString("Quantity", "1");
                                xml.WriteWhitespace("\n");

                            xml.WriteEndElement();
                            xml.WriteWhitespace("\n");
                        
                        xml.WriteEndElement();
                        xml.WriteWhitespace("\n");

                    xml.WriteEndElement();
                   
   
   
                    strXml = str.ToString();
                }
            }
            return strXml;
        }

        public string GetShipRateRequest(ClientCartContext cart)
        {
            String strXml = String.Empty;
            using (StringWriter str = new StringWriter())
            {

                using (XmlTextWriter xml = new XmlTextWriter(str))
                {
                    //root node
                   
                    List<StateProvince> states = StateManager.GetAllStates(0);
                    xml.WriteStartDocument();
                    xml.WriteWhitespace("\n");
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    xml.WriteStartElement("RateRequest");
                    xml.WriteWhitespace("\n");
                    
                        xml.WriteElementString("Username", "amaher@conversionsystems.com");
                        xml.WriteWhitespace("\n");
                        xml.WriteElementString("Password", "5922d4678819620d");
                        xml.WriteWhitespace("\n");
                        //xml.WriteElementString("Server", "Test");
                        //xml.WriteWhitespace("\n");
                        //xml.WriteElementString("Referer", "Test");
                        //xml.WriteWhitespace("\n");
                        xml.WriteStartElement("Order");
                        xml.WriteAttributeString("id", "TESTTSE" + cart.CustomerInfo.CustomerId);
                        xml.WriteWhitespace("\n");
                        
                             xml.WriteElementString("Warehouse", "0");
                             xml.WriteWhitespace("\n");
                             xml.WriteStartElement("AddressInfo");
                             xml.WriteAttributeString("type", "ship");
                             xml.WriteWhitespace("\n");
                                 
                                 xml.WriteElementString("Address1", cart.CustomerInfo.ShippingAddress.Address1);
                                 xml.WriteWhitespace("\n");
                                 xml.WriteElementString("Address2", cart.CustomerInfo.ShippingAddress.Address2);
                                 xml.WriteWhitespace("\n");
                                 xml.WriteElementString("City", cart.CustomerInfo.ShippingAddress.City);
                                 xml.WriteWhitespace("\n");
                                 StateProvince itemShippingStateProvince = states.FirstOrDefault(x => x.StateProvinceId == Convert.ToInt32(cart.CustomerInfo.ShippingAddress.StateProvinceId));
                                 if (itemShippingStateProvince != null)
                                 {
                                     xml.WriteElementString("State", itemShippingStateProvince.Abbreviation.Trim());
                                     xml.WriteWhitespace("\n");
                                 }
                                 else
                                 {
                                     xml.WriteElementString("State", string.Empty);
                                     xml.WriteWhitespace("\n");
                                 }

                    string country1 = CountryManager.CountryCode(cart.CustomerInfo.ShippingAddress.CountryId);

                    xml.WriteElementString("Country", country1.Trim());
                                 xml.WriteWhitespace("\n");
                                 xml.WriteElementString("ZIP", cart.CustomerInfo.ShippingAddress.ZipPostalCode);
                                 xml.WriteWhitespace("\n");
                                 
                           
                            xml.WriteEndElement();
                            

                            xml.WriteStartElement("Item");
                            xml.WriteAttributeString("num", "0");
                            xml.WriteWhitespace("\n");

                                xml.WriteElementString("Code", "1000001");
                                xml.WriteWhitespace("\n");
                                xml.WriteElementString("Quantity", "1");
                                xml.WriteWhitespace("\n");

                            xml.WriteEndElement();
                            xml.WriteWhitespace("\n");
                        
                        xml.WriteEndElement();
                        xml.WriteWhitespace("\n");

                    xml.WriteEndElement();
                   
   
   
                    strXml = str.ToString();
                }
            }
            return strXml;
        }

        public bool PostOrder(int orderId)
        {
            bool result = false;
            string req = new ShipWire().GetRequest(orderId, false, false); // Posting order to OMX
            string res = CommonHelper.HttpPost(config.Attributes["transactionUrl"].Value, req);
            Dictionary<string, AttributeValue> orderAttributes = new Dictionary<string, AttributeValue>();
            orderAttributes.Add("Request", new CSBusiness.Attributes.AttributeValue(req));
            orderAttributes.Add("Response", new CSBusiness.Attributes.AttributeValue(res));

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(res);
            XmlNode xnResult = doc.SelectSingleNode("/SubmitOrderResponse/Status");
            if (xnResult.InnerText.ToLower().Equals("0"))
            {
                result = true;
                XmlNode xnTrans = doc.SelectSingleNode("/SubmitOrderResponse/TransactionId");
                orderAttributes.Add("ShipWireTransactionId", new CSBusiness.Attributes.AttributeValue(xnTrans.InnerText.ToLower()));
                
                CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, 2);
            }
            else
            {
                
                CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, 8);
                //sending email to admins
                OrderHelper.SendEmailToAdmins(orderId);
            }
            return result;
        }


        public decimal GetShippingRate(ClientCartContext cart)
        {
            Decimal result = 0;
            string req = new ShipWire().GetShipRateRequest(cart); // Posting order to OMX
            string res = CommonHelper.HttpPost(config.Attributes["transactionUrl"].Value, req);
            Dictionary<string, AttributeValue> orderAttributes = new Dictionary<string, AttributeValue>();
            orderAttributes.Add("Request", new CSBusiness.Attributes.AttributeValue(req));
            orderAttributes.Add("Response", new CSBusiness.Attributes.AttributeValue(res));

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(res);
            XmlNode xnResult = doc.SelectSingleNode("/RateResponse/Status");
            if (xnResult.InnerText.ToLower().Equals("ok"))
            {
                
                XmlNode xnTrans = doc.SelectSingleNode("/RateResponse/Order/Quotes/Quote/Cost");
                result = Convert.ToDecimal(xnTrans.InnerText.ToLower());
                //orderAttributes.Add("ShipWireTransactionId", new CSBusiness.Attributes.AttributeValue(xnTrans.InnerText.ToLower()));


            }
            else
            {


                result = 0;

            }
            return result;
        }
        private XmlNode GetConfig()
        {
            return OrderHelper.GetDefaultFulFillmentHouseConfig("ShipWire");
        }
    }
}