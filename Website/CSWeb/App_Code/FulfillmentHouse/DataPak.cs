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
using CSPaymentProvider;

namespace CSWeb.FulfillmentHouse
{
    public class DataPak
    {
        XmlNode config = null;
        public DataPak()
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
                    Order orderItem = new OrderManager().GetBatchProcessOrders(orderId);
                    //root node
                    xml.WriteStartDocument();
                    xml.WriteWhitespace("\n");
                    //DatapakServices section
                    xml.WriteStartElement("DatapakServices");
                    xml.WriteAttributeString("method", "submit_order");
                    xml.WriteWhitespace("\n");
                    //Source section
                    xml.WriteStartElement("Source");
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("ID");
                    xml.WriteValue(config.Attributes["ID"].Value);
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("Username");
                    xml.WriteValue(config.Attributes["login"].Value);
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("Password");
                    xml.WriteValue(config.Attributes["password"].Value);
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    //Source section End
                    //Order section
                    xml.WriteStartElement("Order");
                    xml.WriteAttributeString("method", "order");
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("CompanyNumber");
                    xml.WriteValue(config.Attributes["CompanyNumber"].Value);
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("ProjectNumber");
                    xml.WriteValue(config.Attributes["ProjectNumber"].Value);
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("OrderNumber");
                    xml.WriteValue(config.Attributes["OrderIdPrefix"].Value + orderId.ToString());
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("SourceCode");
                    xml.WriteValue(config.Attributes["SourceCode"].Value);
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("TrackingCode");
                    xml.WriteValue(config.Attributes["TrackingCode"].Value);
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    //xml.WriteStartElement("MediaCode");
                    //xml.WriteValue(config.Attributes["MediaCode"].Value);
                    //xml.WriteEndElement();
                    //xml.WriteWhitespace("\n");
                    xml.WriteStartElement("OrderDate");
                    xml.WriteValue(orderItem.CreatedDate.ToString("MM/dd/yyyy"));
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("OrderTime");
                    xml.WriteValue(orderItem.CreatedDate.ToString("HH:mm"));
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    //Giftwrap
                    //Sku giftSku = null; 
                    Sku shippingSku = null;
                    //string giftWrap = "N"; 
                    //decimal giftCharge = 0;
                    string shippingMethod = "01"; decimal shippingCharge = 0;
                    foreach (Sku Item in orderItem.SkuItems)
                    {
                        //if (Item.SkuCode == "Gift")
                        //    giftSku = Item;
                        //Shipping method

                        if (Item.SkuCode == "Shipping")
                            shippingSku = Item;
                    }

                    //if (giftSku != null)
                    //{
                    //    giftWrap = "Y";
                    //    giftCharge = giftSku.FullPrice ;
                    //    orderItem.SkuItems.Remove(giftSku);
                    //}
                    if (shippingSku != null)
                    {

                        shippingSku.LoadAttributeValues();
                        if (shippingSku.AttributeValues.ContainsKey("isupsell") && shippingSku.AttributeValues["isupsell"].Value != "")
                        {
                            shippingMethod = shippingSku.AttributeValues["isupsell"].Value;
                            shippingCharge = shippingSku.FullPrice;
                            orderItem.SkuItems.Remove(shippingSku);
                        }
                    }


                    decimal orderTotal = (orderItem.SubTotal + orderItem.Tax);

                    if (orderItem.CustomerInfo.ShippingAddress.CountryId == 46) // Canada
                    {
                        shippingMethod = "09";
                    }
                    else if (orderItem.CustomerInfo.ShippingAddress.CountryId == 231) //US
                    {
                        if (orderItem.CustomerInfo.ShippingAddress.StateProvinceId == 1 ||
                             orderItem.CustomerInfo.ShippingAddress.StateProvinceId == 389 ||
                             orderItem.CustomerInfo.ShippingAddress.StateProvinceId == 388 ||
                             orderItem.CustomerInfo.ShippingAddress.StateProvinceId == 11 ||
                             orderItem.CustomerInfo.ShippingAddress.StateProvinceId == 390)
                        {
                            shippingMethod = "09";
                        }
                    }

                    //if (config.SelectSingleNode("@ShippingMethod_" + orderItem.CustomerInfo.ShippingAddress.CountryId.ToString()) != null)
                    //{
                    //    ShippingMethod = config.Attributes["ShippingMethod_" + orderItem.CustomerInfo.ShippingAddress.CountryId.ToString()].Value;
                    //}

                    xml.WriteStartElement("ShippingMethod");
                    xml.WriteValue(shippingMethod);
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");





                    List<StateProvince> states = StateManager.GetAllStates(0);


                    //BillingInfo section
                    xml.WriteStartElement("BillingInfo");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("FirstName", orderItem.CustomerInfo.BillingAddress.FirstName);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("LastName", orderItem.CustomerInfo.BillingAddress.LastName);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Address1", orderItem.CustomerInfo.BillingAddress.Address1);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Address2", orderItem.CustomerInfo.BillingAddress.Address2);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("City", orderItem.CustomerInfo.BillingAddress.City);
                    xml.WriteWhitespace("\n");
                    StateProvince itemBillingStateProvince = states.FirstOrDefault(x => x.StateProvinceId == Convert.ToInt32(orderItem.CustomerInfo.BillingAddress.StateProvinceId));
                    if (itemBillingStateProvince != null)
                    {
                        xml.WriteElementString("State", itemBillingStateProvince.Abbreviation.Trim());
                        xml.WriteWhitespace("\n");
                    }
                    else
                    {
                        xml.WriteElementString("State", string.Empty);
                        xml.WriteWhitespace("\n");
                    }
                    xml.WriteElementString("ZipCode", orderItem.CustomerInfo.BillingAddress.ZipPostalCode);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Country", orderItem.CustomerInfo.BillingAddress.CountryCode.Trim());
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Phone", orderItem.CustomerInfo.BillingAddress.PhoneNumber);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Email", orderItem.Email);
                    xml.WriteWhitespace("\n");

                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    //BillingInfo section End


                    //ShippingInfo section
                    xml.WriteStartElement("ShippingInfo");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("FirstName", orderItem.CustomerInfo.ShippingAddress.FirstName);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("LastName", orderItem.CustomerInfo.ShippingAddress.LastName);
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
                        xml.WriteElementString("State", itemShippingStateProvince.Abbreviation.Trim());
                        xml.WriteWhitespace("\n");
                    }
                    else
                    {
                        xml.WriteElementString("State", string.Empty);
                        xml.WriteWhitespace("\n");
                    }
                    xml.WriteElementString("ZipCode", orderItem.CustomerInfo.ShippingAddress.ZipPostalCode);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Country", orderItem.CustomerInfo.ShippingAddress.CountryCode.Trim());
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Phone", orderItem.CustomerInfo.ShippingAddress.PhoneNumber);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("Email", orderItem.Email);
                    xml.WriteWhitespace("\n");

                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    //ShippingInfo section End

                    //PaymentInfo informaiton
                    xml.WriteStartElement("PaymentInfo");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("PaymentType", UpdateCreditCardType(orderItem.CreditInfo.CreditCardName));
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("CardNumber", orderItem.CreditInfo.CreditCardNumber);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("ExpirationMonth", orderItem.CreditInfo.CreditCardExpired.ToString("MM"));
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("ExpirationYear", orderItem.CreditInfo.CreditCardExpired.ToString("yyyy"));
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("CVV", orderItem.CreditInfo.CreditCardCSC);
                    xml.WriteWhitespace("\n");
                    decimal additionalPayments = 0;
                    int nop = 1;//number of payments

                    foreach (Sku Item in orderItem.SkuItems)
                    {
                        Item.LoadAttributeValues();
                        if (Item.AttributeValues.ContainsKey("numberofpayments"))
                        {
                            if (Item.AttributeValues["numberofpayments"].Value != null)
                            {
                                nop = Convert.ToInt32(Item.AttributeValues["numberofpayments"].Value);

                                additionalPayments += Item.FullPrice - Item.InitialPrice ;
                            }

                        }
                    }

                    xml.WriteElementString("NumberOfPayments", nop.ToString());
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("Payment");
                    xml.WriteAttributeString("number", "1");
                    xml.WriteValue(orderTotal.ToString("n2"));
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    for (int i = 2; i <= nop; i++)
                    {
                        xml.WriteStartElement("Payment");
                        xml.WriteAttributeString("number", i.ToString());
                        xml.WriteValue(((additionalPayments / (nop - 1))).ToString("n2"));
                        xml.WriteEndElement();
                        xml.WriteWhitespace("\n");
                    }

                    xml.WriteElementString("MerchandiseTotal", (orderTotal + additionalPayments -shippingCharge-orderItem.Tax).ToString("n2"));
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("ShippingCharge", shippingCharge.ToString("n2"));
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("RushCharge", orderItem.RushShippingCost.ToString("n2"));
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("PriorityHandling", "0.00");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("SalesTax", orderItem.Tax.ToString("n2"));
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("OrderTotal", (orderItem.Total + additionalPayments ).ToString("n2"));
                    xml.WriteWhitespace("\n");




                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    //PaymentInfo section End

                    //SkuItems
                    foreach (Sku Item in orderItem.SkuItems)
                    {
                        xml.WriteStartElement("Item");
                        xml.WriteWhitespace("\n");

                        Item.LoadAttributeValues();
                        xml.WriteElementString("ItemCode", Item.SkuCode.ToUpper());
                        xml.WriteWhitespace("\n");
                        xml.WriteElementString("Sequence", Item.OfferCode);
                        xml.WriteWhitespace("\n");
                        xml.WriteElementString("Quantity", Item.Quantity.ToString());
                        xml.WriteWhitespace("\n");
                        xml.WriteElementString("Price", Item.FullPrice.ToString("n2"));
                        xml.WriteWhitespace("\n");
                        if (Item.AttributeValues.ContainsKey("isupsell"))
                        {
                            if (Item.AttributeValues["isupsell"].Value != "")
                            {
                                xml.WriteElementString("Upsell", Item.AttributeValues["isupsell"].Value);
                                xml.WriteWhitespace("\n");
                            }
                        }
                        xml.WriteElementString("GiftWrap", "N");
                        xml.WriteWhitespace("\n");
                        xml.WriteElementString("GiftWrapCharge", "0.00");
                        xml.WriteWhitespace("\n");
                        xml.WriteEndElement();
                        xml.WriteWhitespace("\n");

                    }



                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    //Order section End


                    //rootEnd node
                    xml.WriteEndElement();
                    //flush results to string object
                    strXml = str.ToString();
                }
            }
            return strXml;
        }

        public static string UpdateCreditCardType(string cardtype)
        {
            // VI VISA AX AMERICAN EXPRESS DI DISCOVER MC MASTER CARD 

            //AMEX
            //Discover
            //MasterCard
            //VISA
            string returnValue = "";
            if (cardtype.ToLower().Equals("visa")) { returnValue = "XV"; }
            if (cardtype.ToLower().Equals("americanexpress")) { returnValue = "XA"; }
            if (cardtype.ToLower().Equals("discover")) { returnValue = "XD"; }
            if (cardtype.ToLower().Equals("mastercard")) { returnValue = "XM"; }

            return returnValue;
        }

        public void PostOrderToDataPak(int orderId)
        {

            string req = new DataPak().GetRequest(orderId, false, false); // Posting order to OMX
            string res = CommonHelper.HttpPost(config.Attributes["transactionUrl"].Value, req);
            Dictionary<string, AttributeValue> orderAttributes = new Dictionary<string, AttributeValue>();
            //orderAttributes.Add("Request", new CSBusiness.Attributes.AttributeValue(req));
            orderAttributes.Add("Response", new CSBusiness.Attributes.AttributeValue(res));

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(res);
            XmlNode xnResult = doc.SelectSingleNode("/DatapakServices/Order/Result/Code");



            if (xnResult.InnerText.ToLower().Equals("001"))
            {
                //CSResolve.Resolve<IOrderService>().SaveOrderInfo(orderId, 2, req.ToLower().Replace("utf-8", "utf-16"), res.ToLower().Replace("utf-8", "utf-16"));
                CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, 2);
                OrderHelper.SendOrderCompletedEmail(orderId);
            }
            else
            {
                //CSResolve.Resolve<IOrderService>().SaveOrderInfo(orderId, 5, req.ToLower().Replace("utf-8", "utf-16"), res.ToLower().Replace("utf-8", "utf-16"));
                CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, 5);
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