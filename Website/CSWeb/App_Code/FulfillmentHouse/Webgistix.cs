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
    public class Webgistix
    {
        XmlNode config = null;
        public Webgistix()
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

                    //int i = 0;
                    //foreach (Sku s in orderItem.SkuItems)
                    //{
                    //    i++;
                    //    lineItems += s.SkuCode + "," + s.FullPrice + "," + s.Quantity + "," + "0";
                    //    if (i != orderItem.SkuItems.Count)
                    //    {
                    //        lineItems += "|";
                    //    }
                    //}

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
                    xml.WriteStartElement("OrderXML");
                    xml.WriteElementString("CustomerID", config.Attributes["login"].Value);
                    xml.WriteElementString("Password", config.Attributes["login"].Value);
                    xml.WriteStartElement("Order");
                    xml.WriteElementString("ReferenceNumber", "LPW" + orderItem.OrderId.ToString());
                    xml.WriteElementString("GROUP_CODE", config.Attributes["GroupCode"].Value);
                    xml.WriteElementString("Company", "");
                    xml.WriteElementString("NAME", orderItem.CustomerInfo.ShippingAddress.FirstName + " " + orderItem.CustomerInfo.ShippingAddress.LastName);
                    xml.WriteElementString("Address1", orderItem.CustomerInfo.ShippingAddress.Address1);
                    xml.WriteElementString("Address2", orderItem.CustomerInfo.ShippingAddress.Address2);
                    xml.WriteElementString("Address3", "");
                    xml.WriteElementString("City", orderItem.CustomerInfo.ShippingAddress.City);
                    xml.WriteElementString("State", StateManager.GetStateName(orderItem.CustomerInfo.ShippingAddress.StateProvinceId));
                    xml.WriteElementString("ZipCode", orderItem.CustomerInfo.ShippingAddress.ZipPostalCode);
                    xml.WriteElementString("COUNTRY", orderItem.CustomerInfo.ShippingAddress.CountryCode.Trim());
                    xml.WriteElementString("EMAIL", orderItem.CustomerInfo.BillingAddress.Email);
                    xml.WriteElementString("PHONE", orderItem.CustomerInfo.ShippingAddress.PhoneNumber);
                    string shippinginstruction = "GROUND";
                    if (orderItem.AttributeValues.ContainsAttribute("ShippingMethod"))
                    {
                        string keyvalue = orderItem.AttributeValues["ShippingMethod"].Value;

                        if (keyvalue.Equals("1"))
                        {

                        }
                        else if (keyvalue.Equals("2"))
                        {
                            shippinginstruction = "2nd Day Air";
                        }
                        else if (keyvalue.Equals("3"))
                        {
                            shippinginstruction = "3-Day Select";
                        }
                    }
                    xml.WriteElementString("ShippingInstructions", shippinginstruction);
                    xml.WriteElementString("Approve", "1");


                    int i = 0;
                    foreach (Sku s in orderItem.SkuItems)
                    {
                        i++;
                        xml.WriteStartElement("Item");
                        xml.WriteElementString("ItemID", s.OfferCode);
                        xml.WriteElementString("ItemQty", s.Quantity.ToString());
                        xml.WriteEndElement();

                    }

                    xml.WriteEndElement();


                    xml.WriteEndElement();
                    strXml = str.ToString();
                }
            }
            return strXml;
        }
        public bool PostOrder(int orderId)
        {
            bool result = false;
            Order orderItem = new OrderManager().GetBatchProcessOrders(orderId);
            string req = new Webgistix().GetRequest(orderItem);
            string res = CommonHelper.HttpPost(config.Attributes["transactionUrl"].Value, req);

            Dictionary<string, AttributeValue> orderAttributes = new Dictionary<string, AttributeValue>();
            orderAttributes.Add("Request", new CSBusiness.Attributes.AttributeValue(req));
            orderAttributes.Add("Response", new CSBusiness.Attributes.AttributeValue(res));
            if (orderItem.CreditInfo.CreditCardNumber.Equals("4111111111111111"))
            {
                CSResolve.Resolve<IOrderService>().SaveOrderInfo(orderId, 8, req.ToLower().Replace("utf-8", "utf-16"), res.ToLower().Replace("utf-8", "utf-16"));
                CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, 8);
                result = true;
            }
            else if (res.ToLower().Contains("<success>true</success>"))
            {
                CSResolve.Resolve<IOrderService>().SaveOrderInfo(orderId, 2, req.ToLower().Replace("utf-8", "utf-16"), res.ToLower().Replace("utf-8", "utf-16"));
                CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, 2);
                result = true;
            }
            else
            {
                CSResolve.Resolve<IOrderService>().SaveOrderInfo(orderId, 5, req.ToLower().Replace("utf-8", "utf-16"), res.ToLower().Replace("utf-8", "utf-16"));
                CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, 5);
                result = false;
            }


            return result;
        }

        private XmlNode GetConfig()
        {
            return OrderHelper.GetDefaultFulFillmentHouseConfig();
        }
    }
}