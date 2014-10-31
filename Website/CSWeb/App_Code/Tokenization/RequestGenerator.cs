using CSPaymentProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace CSWeb.App_Code.Tokenization
{
    public class RequestGenerator
    {
        public static string GetAuthorizationRequest(Request request)
        {
            string strXml = string.Empty;
            //String strXml = String.Empty;
            using (StringWriter str = new StringWriter())
            {
                using (XmlTextWriter xml = new XmlTextWriter(str))
                {
                    //root node
                    ////xml.WriteStartDocument();
                    //******* ProcessTransactionAction ***********
                    //It's the main container element for all transactions
                    xml.WriteStartElement("ProcessTransactionAction");
                    //Gateway info 
                    //Contains gateway information to use for transaction processing, First Data in this case.
                    xml.WriteStartElement("gateway");
                    xml.WriteElementString("name", "[GATEWAYNAME]");
                    xml.WriteElementString("login", "[LOGIN]");
                    xml.WriteElementString("password", "[PASSWORD]");
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    //Credit Card info
                    xml.WriteStartElement("credit_card");
                    xml.WriteElementString("first_name", request.FirstName.Trim());
                    xml.WriteElementString("last_name", request.LastName.Trim());
                    xml.WriteElementString("number", request.CardNumber);
                    xml.WriteElementString("brand", request.CardType.ToString().ToLower());
                    xml.WriteElementString("month", request.ExpireDate.Month.ToString());
                    xml.WriteElementString("year", request.ExpireDate.Year.ToString());
                    xml.WriteElementString("verification_value", request.CardCvv);
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");
                    //Transaction info
                    xml.WriteStartElement("transaction");
                    xml.WriteElementString("amount", (request.Amount * 100).ToString("0"));
                    xml.WriteElementString("order_id", request.InvoiceNumber);
                    xml.WriteElementString("description", request.TransactionDescription.Trim());
                    xml.WriteElementString("email", request.Email.Trim());
                    xml.WriteElementString("ip", request.IPAddress);
                    xml.WriteElementString("customer", request.CustomerID);
                    //Billing address info 
                    xml.WriteStartElement("billing_address");
                    xml.WriteElementString("address1", request.Address1.Trim());
                    xml.WriteElementString("city", request.City.Trim());
                    xml.WriteElementString("state", request.State.Trim());
                    xml.WriteElementString("zip", request.ZipCode.Trim());
                    xml.WriteElementString("country", request.Country.Trim());
                    xml.WriteEndElement();

                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    ////rootEnd node
                    //xml.WriteEndElement();
                    //flush results to string object
                    strXml = str.ToString();
                }
            }
            return strXml;

        }
    }
}