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
    public class DataPakTax
    {
        XmlNode config = null;
        public DataPakTax()
        {
            config = GetConfig();
        }

        private string GetStateCode(int stateID)
        {
            List<StateProvince> states = StateManager.GetAllStates(0);
            return states.FirstOrDefault(x => x.StateProvinceId == Convert.ToInt32(stateID)).Abbreviation;

        }
        public string GetRequest(Order orderItem)
        {
            String strXml = String.Empty;
            using (StringWriter str = new StringWriter())
            {

                using (XmlTextWriter xml = new XmlTextWriter(str))
                {
                    //root node
                    xml.WriteStartDocument();
                    xml.WriteWhitespace("\n");
                    //DatapakServices section
                    xml.WriteStartElement("DatapakServices");
                    xml.WriteWhitespace("\n");
                    //Source section
                    xml.WriteStartElement("Source");
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
                    xml.WriteStartElement("TaxService");
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("CompNum");
                    xml.WriteValue(config.Attributes["CompanyNumber"].Value);
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("ProjNum");
                    xml.WriteValue(config.Attributes["ProjectNumber"].Value);
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("ShipToZip");
                    xml.WriteValue(orderItem.CustomerInfo.ShippingAddress.ZipPostalCode);
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("ShipToState");
                    xml.WriteValue(GetStateCode(orderItem.CustomerInfo.ShippingAddress.StateProvinceId).Trim());
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    xml.WriteStartElement("ShipHandle");
                    xml.WriteValue(CSWebBase.OrderValues.GetShippingCharge(orderItem).ToString("n2"));
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    //Tax Items
                    xml.WriteStartElement("TaxItems");
                    xml.WriteWhitespace("\n");
                    foreach (Sku Item in orderItem.SkuItems)
                    {
                        if (Item.SkuCode != "Shipping")
                        {
                            xml.WriteStartElement("TaxItem");
                            xml.WriteWhitespace("\n");

                            Item.LoadAttributeValues();
                            xml.WriteElementString("ProductID", Item.SkuCode.ToUpper());
                            xml.WriteWhitespace("\n");
                            xml.WriteElementString("Quantity", Item.Quantity.ToString());
                            xml.WriteWhitespace("\n");
                            xml.WriteElementString("Price", Item.FullPrice.ToString("n2"));
                            xml.WriteWhitespace("\n");
                            xml.WriteEndElement();
                            xml.WriteWhitespace("\n");
                        }
                    }
                    //rootEnd node
                    xml.WriteEndElement();
                    xml.WriteEndElement();
                    xml.WriteEndElement();

                    //flush results to string object
                    strXml = str.ToString();
                }
            }
            return strXml;
        }

        public void PostOrderToDataPak(int orderId)
        {
            Order orderItem = new OrderManager().GetBatchProcessOrders(orderId);
            string req = new DataPakTax().GetRequest(orderItem);
            string res = CommonHelper.HttpPost(config.Attributes["taxTransactionUrl"].Value, req);
            Dictionary<string, AttributeValue> orderAttributes = new Dictionary<string, AttributeValue>();
            orderAttributes.Add("TaxRequest", new CSBusiness.Attributes.AttributeValue(req));
            orderAttributes.Add("TaxResponse", new CSBusiness.Attributes.AttributeValue(res));

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(res);
            XmlNode xnResult = doc.SelectSingleNode("/DatapakServices/TaxService/Result/Code");



            if (xnResult.InnerText.ToLower().Equals("001"))
            {
                //Get the tax 
                XmlNode xnTaxAmount = doc.SelectSingleNode("/DatapakServices/TaxService/Result/CalculatedTax");
                if (xnTaxAmount != null && xnResult.InnerText.Length > 0)
                {
                    decimal taxAmount = 0;
                    if (decimal.TryParse(xnTaxAmount.InnerText, out taxAmount))
                    {
                        CSResolve.Resolve<IOrderService>().UpdateOrderTax(orderId, taxAmount);

                    }
                }

                //}
                //else
                //{
                //    CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, null);
                //    //sending email to admins
                //    OrderHelper.SendEmailToAdmins(orderId);
                //}
            }
            CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderId, orderAttributes, null);
        }
        private XmlNode GetConfig()
        {
            return OrderHelper.GetDefaultFulFillmentHouseConfig();
        }
    }
}