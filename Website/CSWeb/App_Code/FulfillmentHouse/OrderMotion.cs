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
    public class OrderMotion
    {
        XmlNode config = null;
        public OrderMotion()
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
                    xml.WriteStartDocument();
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("UDOARequest");
                    xml.WriteAttributeString("version", "2.00");
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("UDIParameter");
                    xml.WriteWhitespace("\n");

                    xml.WriteStartElement("Parameter");
                    xml.WriteAttributeString("key", "UDIAuthToken");
                    xml.WriteValue(config.Attributes["OMXBizzId"].Value);
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    Order orderItem = new OrderManager().GetBatchProcessOrders(orderId);
                    string keycode = "";
                    foreach (Sku Item in orderItem.SkuItems)
                    {

                        keycode = Item.OfferCode;

                    }
                    xml.WriteStartElement("Parameter");
                    xml.WriteAttributeString("key", "Keycode"); // Dont hard code. Should be a value at SKU Category level
                    xml.WriteValue(keycode);
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    xml.WriteStartElement("Parameter");
                    xml.WriteAttributeString("key", "VerifyFlag");
                    if (CheckOrder)
                    {
                        xml.WriteValue("True"); // This means that we are not posting order to OMX, we just need the tax info from them.
                    }
                    else
                    {
                        xml.WriteValue("False"); // This means we are posting the order to OMX.
                    }
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    xml.WriteStartElement("Parameter");
                    xml.WriteAttributeString("key", "QueueFlag");
                    xml.WriteValue("True");
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    //xml.WriteStartElement("Parameter");
                    //xml.WriteAttributeString("key", "OriginalEntryKeycode");
                    //xml.WriteValue(keycode);
                    //xml.WriteEndElement();
                    //xml.WriteWhitespace("\n");

                    //xml.WriteStartElement("Parameter");
                    //xml.WriteAttributeString("key", "CallToAction");
                    //xml.WriteEndElement();
                    //xml.WriteWhitespace("\n");

                    //xml.WriteStartElement("Parameter");
                    //xml.WriteAttributeString("key", "Vendor");
                    //xml.WriteValue("DIRECT");
                    //xml.WriteEndElement();
                    //xml.WriteWhitespace("\n");

                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    xml.WriteWhitespace("\n");



                    //Write HeadTag
                    xml.WriteStartElement("Header");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("OrderDate", orderItem.CreatedDate.ToString());
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("OriginType", config.Attributes["OriginType"].Value);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("OrderID", config.Attributes["OrderIdPrefix"].Value + orderItem.OrderId.ToString());
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("StoreCode", config.Attributes["StoreCode"].Value);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("CustomerIP", orderItem.IpAddress);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Username", config.Attributes["UserName"].Value);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Reference", "");
                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    List<StateProvince> states = StateManager.GetAllStates(0);

                    //Customer section
                    xml.WriteStartElement("Customer");
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("Address");
                    xml.WriteAttributeString("type", "BillTo");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("TitleCode", "0");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Company", "");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Firstname", orderItem.CustomerInfo.BillingAddress.FirstName);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("MidInitial", "");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Lastname", orderItem.CustomerInfo.BillingAddress.LastName);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Address1", orderItem.CustomerInfo.BillingAddress.Address1);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Address2", orderItem.CustomerInfo.BillingAddress.Address2);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("City", orderItem.CustomerInfo.BillingAddress.City);
                    xml.WriteWhitespace("\n");
                    //xml.WriteElementString("State", orderItem.CustomerInfo.BillingAddress.StateProvinceName);
                    //xml.WriteWhitespace("\n");
                    StateProvince itemBillingStateProvince = states.FirstOrDefault(x => x.StateProvinceId == Convert.ToInt32(orderItem.CustomerInfo.BillingAddress.StateProvinceId));
                    if (itemBillingStateProvince != null)
                    {
                        xml.WriteElementString("State", itemBillingStateProvince.Abbreviation);
                        xml.WriteWhitespace("\n");
                    }
                    else
                    {
                        xml.WriteElementString("State", string.Empty);
                        xml.WriteWhitespace("\n");
                    }
                    xml.WriteElementString("ZIP", orderItem.CustomerInfo.BillingAddress.ZipPostalCode);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("TLD", orderItem.CustomerInfo.BillingAddress.CountryCode.Trim());
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("PhoneNumber", orderItem.CustomerInfo.BillingAddress.PhoneNumber);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Email", orderItem.Email);
                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    //Shipping information
                    xml.WriteStartElement("ShippingInformation");
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("Address");
                    xml.WriteAttributeString("type", "ShipTo");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("TitleCode", "0");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Firstname", orderItem.CustomerInfo.ShippingAddress.FirstName);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Lastname", orderItem.CustomerInfo.ShippingAddress.LastName);
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

                    xml.WriteElementString("ZIP", orderItem.CustomerInfo.ShippingAddress.ZipPostalCode);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("TLD", orderItem.CustomerInfo.ShippingAddress.CountryCode.Trim());
                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement(); // CLose Address Tag
                    xml.WriteWhitespace("\n");

                    xml.WriteElementString("MethodCode", config.Attributes["MethodCode"].Value);//Dont want to harcode this
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("MethodName", ""); //Dont want to harcode this
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("ShippingAmount", orderItem.ShippingCost.ToString());
                    xml.WriteWhitespace("\n");

                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");


                    //Payment informaiton
                    xml.WriteStartElement("Payment");
                    xml.WriteAttributeString("type", "1");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("CardNumber", orderItem.CreditInfo.CreditCardNumber);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("CardVerification", orderItem.CreditInfo.CreditCardCSC);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("CardExpDateMonth", orderItem.CreditInfo.CreditCardExpired.ToString("MM"));
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("CardExpDateYear", orderItem.CreditInfo.CreditCardExpired.ToString("yyyy"));
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("RealTimeCreditCardProcessing", "False");
                    xml.WriteWhitespace("\n");
                    if (!RejectedOrder)
                    {
                        xml.WriteElementString("CardStatus", "11");
                        xml.WriteWhitespace("\n");
                        xml.WriteElementString("CardTransactionID", orderItem.CreditInfo.TransactionCode);
                    }
                    else
                    {
                        xml.WriteElementString("CardStatus", "0");
                        xml.WriteWhitespace("\n");
                        xml.WriteElementString("CardTransactionID", "");
                    }
                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");


                    //SkuItems
                    xml.WriteStartElement("OrderDetail");
                    xml.WriteWhitespace("\n");
                    int lineItemCount = 1;
                    foreach (Sku Item in orderItem.SkuItems)
                    {
                        Item.LoadAttributeValues();
                        xml.WriteStartElement("LineItem");
                        xml.WriteAttributeString("lineNumber", lineItemCount.ToString());
                        xml.WriteWhitespace("\n");
                        xml.WriteElementString("PaymentPlanID", Item.AttributeValues["paymentplanid"].Value);
                        xml.WriteWhitespace("\n");
                        xml.WriteElementString("ItemCode", Item.SkuCode);
                        xml.WriteWhitespace("\n");
                        xml.WriteElementString("Quantity", Item.Quantity.ToString());
                        xml.WriteWhitespace("\n");
                        xml.WriteElementString("UnitPrice", Item.InitialPrice.ToString());
                        xml.WriteWhitespace("\n");
                        if (Item.AttributeValues.ContainsKey("standingorderid"))
                        {
                            if (Item.AttributeValues["standingorderid"].Value != "")
                            {
                                xml.WriteStartElement("StandingOrder");
                                xml.WriteAttributeString("configurationID", Item.AttributeValues["standingorderid"].Value);
                                xml.WriteEndElement();
                            }
                        }
                        xml.WriteEndElement();
                        xml.WriteWhitespace("\n");
                        lineItemCount++;
                    }

                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");



                    //Custom Fields
                    xml.WriteStartElement("CustomFields");//CustomFields Starts
                    xml.WriteStartElement("Report");//Report Starts

                    xml.WriteStartElement("Field");
                    xml.WriteAttributeString("fieldName", "OriginalOrderDate");// Dont hard code. Should be a value at DB level
                    xml.WriteValue(orderItem.CreatedDate.ToString());
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    xml.WriteStartElement("Field");
                    xml.WriteAttributeString("fieldName", "URL");// Dont hard code. Should be a value at DB level
                    xml.WriteValue(config.Attributes["SiteUrl"].Value);
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    xml.WriteStartElement("Field");
                    xml.WriteAttributeString("fieldName", "CustomerIP");// Dont hard code. Should be a value at DB level
                    xml.WriteValue(orderItem.IpAddress);
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    xml.WriteEndElement(); //Report End
                    xml.WriteEndElement(); //CustomFields End

                    //rootEnd node
                    xml.WriteEndElement();
                    //flush results to string object
                    strXml = str.ToString();
                }
            }
            return strXml;
        }
        public decimal GetTaxFromOMX(int orderId)
        {
            decimal taxAmount = 0;
            string req = new OrderMotion().GetRequest(orderId, true, false);
            string res = CommonHelper.HttpPost(config.Attributes["OMXUrl"].Value, req);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(res);
            XmlNode xnResult = doc.SelectSingleNode("/UDOAResponse/Success");
            XmlNode xnTax = doc.SelectSingleNode("/UDOAResponse/UDOARequest/Header/Tax");
            if (xnResult.InnerText.ToLower().Equals("1"))
            {
                if (xnTax != null)
                {
                    taxAmount = Convert.ToDecimal(xnTax.InnerText);
                }
            }

            return taxAmount;
        }
        public void PostOrderToOmx(int orderId)
        {

            string req = new OrderMotion().GetRequest(orderId, false, false); // Posting order to OMX
            string res = CommonHelper.HttpPost(config.Attributes["OMXUrl"].Value, req);
            Dictionary<string, AttributeValue> orderAttributes = new Dictionary<string, AttributeValue>();
            orderAttributes.Add("Request", new CSBusiness.Attributes.AttributeValue(req));
            orderAttributes.Add("Response", new CSBusiness.Attributes.AttributeValue(res));

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(res);
            XmlNode xnResult = doc.SelectSingleNode("/UDOAResponse/Success");
            if (xnResult.InnerText.ToLower().Equals("1"))
            {
                CSResolve.Resolve<IOrderService>().SaveOrderInfo(orderId, 2, req.ToLower().Replace("utf-8", "utf-16"), res.ToLower().Replace("utf-8", "utf-16"));
                CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, 2);
            }
            else
            {
                CSResolve.Resolve<IOrderService>().SaveOrderInfo(orderId, 8, req.ToLower().Replace("utf-8", "utf-16"), res.ToLower().Replace("utf-8", "utf-16"));
                CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, 8);
                //sending email to admins
                OrderHelper.SendEmailToAdmins(orderId);
            }
        }
        private XmlNode GetConfig()
        {
            return OrderHelper.GetDefaultFulFillmentHouseConfig();
        }
    }
}