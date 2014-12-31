using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Xml.Linq;
using CSBusiness;
using CSBusiness.Email;
using CSBusiness.OrderManagement;
using CSBusiness.Payment;
using CSBusiness.Resolver;
using CSCore.Utils;
using CSPaymentProvider;
using CSBusiness.Preference;
using CSCore.DataHelper;
using System.Xml;
using CSBusiness.FulfillmentHouse;
using System.Collections;
using CSBusiness.Attributes;
using CSWebBase;
using CSWeb.Tokenization;

/// <summary>
/// Summary description for OrderHelper
/// </summary>

namespace CSWeb
{
    public class OrderHelper
    {
        #region Order Validation
        public static bool AuthorizeOrder(int orderID)
        {
            Request _request = new Request();

            Order orderData = CSResolve.Resolve<IOrderService>().GetOrderDetails(orderID, true);

            _request.CardNumber = orderData.CreditInfo.CreditCardNumber;
            _request.CardType = GetCCType(orderData.CreditInfo.CreditCardName);
            _request.CardCvv = orderData.CreditInfo.CreditCardCSC;
            _request.CurrencyCode = "$";
            _request.ExpireDate = orderData.CreditInfo.CreditCardExpired;
            _request.Amount = (double)orderData.Total;
            _request.FirstName = orderData.CustomerInfo.BillingAddress.FirstName;
            _request.LastName = orderData.CustomerInfo.BillingAddress.LastName;
            _request.Address1 = orderData.CustomerInfo.BillingAddress.Address1;
            _request.Address2 = orderData.CustomerInfo.BillingAddress.Address2;
            _request.City = orderData.CustomerInfo.BillingAddress.City;
            _request.State = StateManager.GetStateName(orderData.CustomerInfo.BillingAddress.StateProvinceId);
            _request.Country = CountryManager.CountryCode(orderData.CustomerInfo.BillingAddress.CountryId);
            _request.ZipCode = orderData.CustomerInfo.BillingAddress.ZipPostalCode;
            _request.TransactionDescription = orderData.CustomerInfo.BillingAddress.FirstName + " " + orderData.CustomerInfo.BillingAddress.LastName;
            _request.CustomerID = orderData.CustomerId.ToString();
            _request.InvoiceNumber = orderData.OrderId.ToString();
            _request.IPAddress = orderData.IpAddress;
            _request.Email = orderData.Email;

            //Make transaction request
            Response _response = TokenexProcessor.GetInstance().PerformAuthRequest(_request);

            //Save gateway transaction
            Dictionary<string, AttributeValue> orderAttributes = new Dictionary<string, AttributeValue>();
            orderAttributes.Add("AuthRequest", new CSBusiness.Attributes.AttributeValue(CSCore.Utils.CommonHelper.Encrypt(_response.GatewayRequestRaw)));
            orderAttributes.Add("AuthResponse", new CSBusiness.Attributes.AttributeValue(_response.GatewayResponseRaw));
            CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderData.OrderId, orderAttributes, null);

            bool _returnValue = false;
            //Save results
            if (_response != null && _response.ResponseType != TransactionResponseType.Approved)
            {
                CSResolve.Resolve<IOrderService>().SaveOrder(orderData.OrderId, 
                    (_response.TransactionID == null) ? string.Empty : _response.TransactionID,
                    (_response.AuthCode == null) ? string.Empty : _response.AuthCode, 7);

                _returnValue = false;
            }
            else if (_response != null && _response.ResponseType == TransactionResponseType.Approved)
            {
                CSResolve.Resolve<IOrderService>().SaveOrder(orderData.OrderId,
                    (_response.TransactionID == null) ? string.Empty : _response.TransactionID, 
                    (_response.AuthCode == null) ? string.Empty : _response.AuthCode, 4);
                _returnValue = true;
            }
            UserSessions.InsertSessionEntry(null, _returnValue, (decimal)(_request.Amount), orderData.CustomerId, orderID);

            return _returnValue;
        }

        //public enum CustomCreditCardTypeEnum
        //{
        //    Discover = 2,
        //    MasterCard = 4,
        //    VISA = 8
        //}

        private static CreditCardType GetCCType(string ccName)
        {
            switch (ccName.ToLower())
            {
                case "visa":
                    return CreditCardType.Visa;
                case "mastercard":
                    return CreditCardType.Mastercard;
                case "americanexpress":
                    return CreditCardType.AmericanExpress;
                case "discover":
                    return CreditCardType.Discover;
            }
            return CreditCardType.Visa;
        }

        public static bool ValidationCheck(int orderID)
        {
            Request _request = new Request();

            Order orderData = CSResolve.Resolve<IOrderService>().GetOrderDetails(orderID, true);
            List<StateProvince> states = StateManager.GetAllStates(0);
            _request.CardNumber = orderData.CreditInfo.CreditCardNumber;
            _request.CardCvv = orderData.CreditInfo.CreditCardCSC;
            _request.CurrencyCode = "$";
            _request.ExpireDate = orderData.CreditInfo.CreditCardExpired;
            _request.Amount = (double)orderData.Total;
            _request.FirstName = orderData.CustomerInfo.BillingAddress.FirstName;
            _request.LastName = orderData.CustomerInfo.BillingAddress.LastName;
            _request.Address1 = orderData.CustomerInfo.BillingAddress.Address1;
            _request.City = orderData.CustomerInfo.BillingAddress.City;
            StateProvince itemStateProvince = states.FirstOrDefault(x => x.StateProvinceId == Convert.ToInt32(orderData.CustomerInfo.BillingAddress.StateProvinceId));
            if (itemStateProvince != null)
            {
                _request.State = itemStateProvince.Abbreviation.Trim();

            }

            _request.Country = CountryManager.CountryCode(orderData.CustomerInfo.BillingAddress.CountryId).Trim();
            _request.ZipCode = orderData.CustomerInfo.BillingAddress.ZipPostalCode;
            _request.TransactionDescription = orderData.CustomerInfo.BillingAddress.FirstName + " " + orderData.CustomerInfo.BillingAddress.LastName;
            _request.CustomerID = orderData.CustomerId.ToString();
            _request.InvoiceNumber = orderData.OrderId.ToString();

            _request.ShipToFirstName = orderData.CustomerInfo.ShippingAddress.FirstName;
            _request.ShipToLastName = orderData.CustomerInfo.ShippingAddress.LastName;
            _request.ShipToAddress = orderData.CustomerInfo.ShippingAddress.Address1;
            StateProvince itemStateProvince1 = states.FirstOrDefault(x => x.StateProvinceId == Convert.ToInt32(orderData.CustomerInfo.ShippingAddress.StateProvinceId));
            if (itemStateProvince != null)
            {
                _request.ShipToState = itemStateProvince1.Abbreviation.Trim();

            }
            //_request.ShipToState = StateManager.GetStateName(orderData.CustomerInfo.ShippingAddress.StateProvinceId);
            _request.ShipToZipCode = orderData.CustomerInfo.ShippingAddress.ZipPostalCode;
            _request.ShipToCity = orderData.CustomerInfo.ShippingAddress.City;
            _request.IPAddress = orderData.IpAddress;
            _request.Phone = orderData.CustomerInfo.BillingAddress.PhoneNumber;


            _request.ShipToCountry = CountryManager.CountryCode(orderData.CustomerInfo.ShippingAddress.CountryId).Trim();

            _request.Email = orderData.Email;


            Response _response = PaymentProviderRepository.Instance.Get().PerformValidationRequest(_request);


            if (_response != null && _response.ResponseType != TransactionResponseType.Approved)
            {
                CSResolve.Resolve<IOrderService>().SaveOrder(orderData.OrderId, _response.TransactionID, _response.AuthCode, 7);

                return false;
            }
            else if (_response != null && _response.ResponseType == TransactionResponseType.Approved)
            {
                CSResolve.Resolve<IOrderService>().SaveOrder(orderData.OrderId, _response.TransactionID, _response.AuthCode, 4);
                return true;
            }

            return true;
        }

        #endregion Order Validation

        #region Emails
        public static bool SendOrderCompletedEmail(int orderId)
        {
            //pull Specific Email Template
            int emailId = ConfigHelper.EmailAppSetting("EmailId");

            EmailSetting emailTemplate = EmailManager.GetEmail(emailId);
            OrderManager orderMgr = new OrderManager();
            Order orderData = orderMgr.GetOrderDetails(orderId);


            if (emailTemplate.Body != null)
            {
                //Subject Translation
                emailTemplate.Subject = emailTemplate.Subject.Replace("{ORDER_NUMBER}", orderData.OrderId.ToString());

                //Body Translation
                String BodyTemplate = emailTemplate.Body.Replace("&", "&amp;");

                BodyTemplate = BodyTemplate.Replace("{SUBTOTAL}", orderData.SubTotal.ToString("N2"));
                BodyTemplate = BodyTemplate.Replace("{SHIPPING_HANDLING}", orderData.ShippingCost.ToString("N2"));
                BodyTemplate = BodyTemplate.Replace("{TAX}", orderData.Tax.ToString("N2"));
                BodyTemplate = BodyTemplate.Replace("{TOTAL}", orderData.Total.ToString("N2"));
                BodyTemplate = BodyTemplate.Replace("{ORDER_ID}", orderData.OrderId.ToString());
                BodyTemplate = BodyTemplate.Replace("{ORDER_NUMBER}", orderData.OrderId.ToString());
                BodyTemplate = BodyTemplate.Replace("{ORDER_DATE}", orderData.CreatedDate.ToString("dd MMM yyyy hh:mm:ss"));

                CSBusiness.CustomerManagement.Address billing = orderData.CustomerInfo.BillingAddress;
                if (billing != null)
                {

                    BodyTemplate = BodyTemplate.Replace("{BILLING_COMPANY}", CommonHelper.EnsureNotNull(billing.Company));
                    BodyTemplate = BodyTemplate.Replace("{BILLING_ADDRESS2}", CommonHelper.EnsureNotNull(billing.Address2));
                    BodyTemplate = BodyTemplate.Replace("{BILLING_NAME}", billing.FirstName + " " + billing.LastName);
                    BodyTemplate = BodyTemplate.Replace("{BILLING_ADDRESS}", billing.Address1);
                    BodyTemplate = BodyTemplate.Replace("{BILLING_CITY}", billing.City);
                    BodyTemplate = BodyTemplate.Replace("{BILLING_STATE}", StateManager.GetStateName(billing.StateProvinceId)); //pull from Cache
                    BodyTemplate = BodyTemplate.Replace("{BILLING_ZIP}", billing.ZipPostalCode);
                    BodyTemplate = BodyTemplate.Replace("{BILLING_EMAIL}", orderData.Email);
                    BodyTemplate = BodyTemplate.Replace("{BILLING_COUNTRY}", CountryManager.CountryName(billing.CountryId)); //pull from Cache

                }



                CSBusiness.CustomerManagement.Address shippingAddress = orderData.CustomerInfo.ShippingAddress;
                if (shippingAddress != null)
                {
                    BodyTemplate = BodyTemplate.Replace("{SHIPPING_COMPANY}", CommonHelper.EnsureNotNull(shippingAddress.Company));
                    BodyTemplate = BodyTemplate.Replace("{SHIPPING_NAME}", shippingAddress.FirstName + " " + shippingAddress.LastName);
                    BodyTemplate = BodyTemplate.Replace("{SHIPPING_ADDRESS}", shippingAddress.Address1);
                    BodyTemplate = BodyTemplate.Replace("{SHIPPING_ADDRESS2}", CommonHelper.EnsureNotNull(shippingAddress.Address2));
                    BodyTemplate = BodyTemplate.Replace("{SHIPPING_CITY}", shippingAddress.City);
                    BodyTemplate = BodyTemplate.Replace("{SHIPPING_STATE}", StateManager.GetStateName(shippingAddress.StateProvinceId)); //pull from Cache
                    BodyTemplate = BodyTemplate.Replace("{SHIPPING_ZIP}", shippingAddress.ZipPostalCode);
                    BodyTemplate = BodyTemplate.Replace("{SHIPPING_COUNTRY}", CountryManager.CountryName(shippingAddress.CountryId)); //pull from Cache
                    BodyTemplate = BodyTemplate.Replace("{SHIPPING_PHONE}", CommonHelper.EnsureNotNull(shippingAddress.PhoneNumber));
                }

                XElement elem = XElement.Parse("<root>" + BodyTemplate + "</root>", LoadOptions.PreserveWhitespace);
                var nodes = from XElement e in elem.Descendants()
                            where e.Attribute("cart") != null
                            select e;

                StringBuilder sb = new StringBuilder();
                foreach (XElement node in nodes)
                {
                    string originalString = node.ToString();

                    int totalSkuItems = orderData.SkuItems.Count;
                    for (int i = 0; i < totalSkuItems; i++)
                    {
                        Sku sku = orderData.SkuItems[i];
                        string resultString = originalString;
                        resultString = resultString
                            .Replace("{SKU}", sku.ImagePath)
                            .Replace("{SKU_QTY}", sku.Quantity.ToString())
                            .Replace("{SKU_DESCR}", sku.LongDescription)
                            .Replace("{SKU_PRICE}", sku.FullPrice.ToString("N2"));

                        sb.Append(resultString);
                    }
                    BodyTemplate = BodyTemplate.Replace(originalString, sb.ToString());
                    sb.Clear();
                    BodyTemplate = BodyTemplate.Replace("&amp;", "&");
                }


                try
                {
                    //Prepare Mail Message
                    MailMessage _oMailMessage = new MailMessage(emailTemplate.FromAddress, orderData.Email, emailTemplate.Subject, BodyTemplate);
                    _oMailMessage.IsBodyHtml = true;
                    SendMail(_oMailMessage);
                    //Fire OrderConfirmation Log
                    orderMgr.FireEmailLog(orderData.OrderId, orderData.Email, emailTemplate.Subject, BodyTemplate, DateTime.Now);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
            else
                return false;
        }
        public static bool SendEmailToAdmins(int orderId)
        {
            //pull Specific Email Template
            int emailId = ConfigHelper.EmailAppSetting("AdminAlertEmailId");
            EmailSetting emailTemplate = EmailManager.GetEmail(emailId);

            if (emailTemplate.Body != null)
            {
                //Body Translation
                String BodyTemplate = emailTemplate.Body.Replace("&", "&amp;");
                BodyTemplate = BodyTemplate.Replace("{OrderId}", orderId.ToString());
                BodyTemplate = BodyTemplate.Replace("&amp;", "&");
                try
                {
                    //Prepare Mail Message
                    MailMessage _oMailMessage = new MailMessage(emailTemplate.FromAddress, emailTemplate.ToAddress, emailTemplate.Subject, BodyTemplate);
                    _oMailMessage.IsBodyHtml = true;
                    SendMail(_oMailMessage);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
            else
                return false;
        }

        public static bool SendOrderDeclinedEmail(int orderId)
        {
            //pull Specific Email Template
            int emailId = 2;

            EmailSetting emailTemplate = EmailManager.GetEmail(emailId);
            OrderManager orderMgr = new OrderManager();
            Order orderData = orderMgr.GetOrderDetails(orderId);

            if (emailTemplate.Body != null)
            {
                //Body Translation
                String BodyTemplate = emailTemplate.Body.Replace("&", "&amp;");

                CSBusiness.CustomerManagement.Address billing = orderData.CustomerInfo.BillingAddress;
                if (billing != null)
                {
                    BodyTemplate = BodyTemplate.Replace("{BILLING_NAME}", billing.FirstName + " " + billing.LastName);
                }

                XElement elem = XElement.Parse("<root>" + BodyTemplate + "</root>", LoadOptions.PreserveWhitespace);
                var nodes = from XElement e in elem.Descendants()
                            where e.Attribute("cart") != null
                            select e;

                try
                {
                    //Prepare Mail Message
                    MailMessage _oMailMessage = new MailMessage(emailTemplate.FromAddress, orderData.Email, emailTemplate.Subject, BodyTemplate);
                    _oMailMessage.IsBodyHtml = true;
                    SendMail(_oMailMessage);
                    //Fire OrderDecline Log
                    orderMgr.FireEmailLog(orderData.OrderId, orderData.Email, emailTemplate.Subject, BodyTemplate, DateTime.Now);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
            else
                return false;
        }

        public static bool SendOrderFailedEmail(int orderId)
        {
            //pull Specific Email Template
            int emailId = 3;

            EmailSetting emailTemplate = EmailManager.GetEmail(emailId);
            OrderManager orderMgr = new OrderManager();
            Order orderItem = new OrderManager().GetBatchProcessOrders(orderId);
            orderItem.LoadAttributeValues();
            if (emailTemplate.Body != null)
            {
                //Body Translation
                String BodyTemplate = emailTemplate.Body.Replace("&", "&amp;");


                BodyTemplate = BodyTemplate.Replace("{orderID}", orderId.ToString());
                BodyTemplate = BodyTemplate.Replace("{RESPONSE}", orderItem.AttributeValues["response"].Value);



                //XElement elem = XElement.Parse("<root>" + BodyTemplate + "</root>", LoadOptions.PreserveWhitespace);
                //var nodes = from XElement e in elem.Descendants()
                //            where e.Attribute("cart") != null
                //            select e;

                try
                {
                    //Prepare Mail Message
                    MailMessage _oMailMessage = new MailMessage(emailTemplate.FromAddress, emailTemplate.ToAddress, emailTemplate.Subject, BodyTemplate);
                    _oMailMessage.IsBodyHtml = true;
                    SendMail(_oMailMessage);
                    //Fire OrderDecline Log
                    orderMgr.FireEmailLog(orderItem.OrderId, orderItem.Email, emailTemplate.Subject, BodyTemplate, DateTime.Now);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
            else
                return false;
        }

        public static bool SendMail(MailMessage oMsg)
        {

            bool bResult = false;

            try
            {
                SmtpClient client;
                oMsg.BodyEncoding = System.Text.Encoding.UTF8;
                oMsg.CC.Clear();
                oMsg.Bcc.Clear();
                client = new SmtpClient();
                client.Send(oMsg);
                bResult = true;

            }
            catch (Exception)
            {

                bResult = false;
            }
            return bResult;
        }
        #endregion Emails

        public static int GetVersion()
        {
            int versionId = 1;
            string version = HttpContext.Current.Request.Url.AbsolutePath.ToLower();
            version = version.Substring(0, version.LastIndexOf('/'));
            version = version.Substring(version.LastIndexOf('/') + 1, (version.Length - (version.LastIndexOf('/') + 1)));


            List<CSBusiness.Version> list = (CSFactory.GetCacheSitePref()).VersionItems;
            CSBusiness.Version item = list.Find(x => x.Title.ToLower() == version);
            if (item != null)
                versionId = item.VersionId;

            return versionId;
        }

        public static XmlNode GetDefaultFulFillmentHouseConfig()
        {
            XmlDocument doc = new XmlDocument();
            List<FulfillmentHouseProviderSetting> allSettings = FulfillmentHouseProviderManger.GetAllProvidersFromDB(true);
            int totalSettings = allSettings.Count;

            for (int i = 0; i < totalSettings; i++)
            {
                FulfillmentHouseProviderSetting settings = allSettings[i];
                if (settings.Active)
                {
                    if (settings.IsDefault)
                    {
                        doc.LoadXml(settings.ProviderXML);
                        return doc.FirstChild;
                    }
                }
            }
            return null;
        }

        public static XmlNode GetDefaultFulFillmentHouseConfig(string title)
        {
            XmlDocument doc = new XmlDocument();
            List<FulfillmentHouseProviderSetting> allSettings = FulfillmentHouseProviderManger.GetAllProvidersFromDB(true);
            int totalSettings = allSettings.Count;

            for (int i = 0; i < totalSettings; i++)
            {
                FulfillmentHouseProviderSetting settings = allSettings[i];
                if (settings.Active)
                {
                    if (settings.Title.ToLower().Equals(title.ToLower()))
                    {
                        doc.LoadXml(settings.ProviderXML);
                        return doc.FirstChild;
                    }
                }
            }
            return null;
        }

        public static string GetVersionName()
        {
            return CSBusiness.Web.CSBasePage.GetVersionName();
        }


        public static bool IsCustomerOrderFlowCompleted(int OrderId)
        {
            bool OrderFlowCompleted = false;
            Order orderData = CSResolve.Resolve<IOrderService>().GetOrderDetails(OrderId, true);
            if (!orderData.AttributeValuesLoaded)
                orderData.LoadAttributeValues();

            if (orderData.AttributeValues.ContainsAttribute("OrderFlowCompleted"))
            {
                if (orderData.AttributeValues.GetAttributeValue("OrderFlowCompleted").Value.Equals("1"))
                {
                    OrderFlowCompleted = true;
                }
            }

            return OrderFlowCompleted;
        }


        public static string GetDynamicVersionData(string dataName)
        {
            string radioVersionData = "";
            ClientCartContext context = (ClientCartContext)HttpContext.Current.Session["ClientOrderData"];
            if (context.OrderAttributeValues != null && context.OrderAttributeValues.ContainsKey("DynamicVerionData"))
            {
                radioVersionData = context.OrderAttributeValues["DynamicVerionData"].Value;
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(radioVersionData);
            doc.SelectSingleNode("version");

            string returnData = "";
            switch (dataName)
            {
                case "phone":
                    returnData = doc.SelectSingleNode("Version")["Phone"].InnerText;
                    break;

                case "image1":
                    returnData = doc.SelectSingleNode("Version")["Image1"].InnerText;
                    break;

                case "MainKitBlack":
                    returnData = doc.SelectSingleNode("Version")["MainKitBlack"].InnerText;
                    break;

                case "MainKitDarkGray":
                    returnData = doc.SelectSingleNode("Version")["MainKitDarkGray"].InnerText;
                    break;

                case "MainKitLightGray":
                    returnData = doc.SelectSingleNode("Version")["MainKitLightGray"].InnerText;
                    break;

                case "imageSelector":
                    returnData = doc.SelectSingleNode("Version")["imageSelector"].InnerText;
                    break;

                case "homepageimage":
                    returnData = doc.SelectSingleNode("Version")["Image1"].InnerText;
                    break;

                case "ctaimage":
                    returnData = doc.SelectSingleNode("Version")["Image2"].InnerText;
                    break;

                case "cartimage":
                    returnData = doc.SelectSingleNode("Version")["Image3"].InnerText;
                    break;
            }
            return returnData;
        }

        public static string GetVersionNameByReferrer(ClientCartContext CartContext)
        {
            string versionName = "";
            try
            {
                versionName = HttpContext.Current.Request.Url.Host.ToUpper().Replace("WWW.", "");
                if (CartContext.OrderAttributeValues != null)
                {
                    Uri uri = new Uri(CartContext.OrderAttributeValues["ref_url"].Value);
                    versionName = uri.Host.ToUpper().Replace("WWW.", "");
                    SitePreference sitePrefCache = CSFactory.GetCacheSitePref();
                    if (!sitePrefCache.AttributeValuesLoaded)
                        sitePrefCache.LoadAttributeValues();
                    string[] strRedirectDomains = sitePrefCache.AttributeValues["redirectdomainnames"].Value.ToLower().Split(';');
                    if (strRedirectDomains.Any(versionName.ToLower().Contains))
                    {
                        versionName += "-" + GetDynamicVersionName();
                    }
                    else if (versionName.StartsWith("TRYKYRO.COM"))
                    {
                        versionName = "Direct" + "-" + GetDynamicVersionName();
                    }
                    else
                    {
                        versionName = "Referral-" + GetDynamicVersionName();
                    }
                }
            }
            catch { versionName = "Direct" + "-" + GetDynamicVersionName(); }
            return versionName.ToUpper();
        }

        public static bool SetDynamicLandingPageVersion(string version, ClientCartContext context)
        {
            string radioVersionData = "";
            if (context.OrderAttributeValues != null && context.OrderAttributeValues.ContainsKey("DynamicVerionData"))
            {
                radioVersionData = context.OrderAttributeValues["DynamicVerionData"].Value;
            }
            else
            {
                radioVersionData = DynamicVersionDAL.GetDynamicVersion(version);
                context.OrderAttributeValues.Add("DynamicVerionData", new AttributeValue(radioVersionData));
                HttpContext.Current.Session["ClientOrderData"] = context;
            }
            return true;
        }

        //Please see if there are better ways of doing this
        public static string GetDynamicVersionName()
        {
            string strDynamicVersion = "";
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["ClientOrderData"] != null)
            {
                ClientCartContext clientData = (ClientCartContext)HttpContext.Current.Session["ClientOrderData"];
                if (clientData.OrderAttributeValues != null)
                {
                    if (clientData.OrderAttributeValues.ContainsKey("DynamicVerionName"))
                    {
                        strDynamicVersion = clientData.OrderAttributeValues["DynamicVerionName"].Value;
                    }
                    else
                    {
                        strDynamicVersion = CSBusiness.Web.CSBasePage.GetVersionName();
                    }
                }
                return strDynamicVersion.ToUpper();
            }
            return GetVersionName();
        }

        public static bool IsMobileBrowser()
        {
            bool result = false;
            try
            {
                string ua = HttpContext.Current.Request.UserAgent.ToLower();
                if (ua != null && (ua.Contains("iphone") || ua.Contains("blackberry") || ua.Contains("android")))
                {
                    result = true;
                }
            }
            catch { }
            return result;
        }



        public static int CountNums(string s)
        {
            string s1 = s;

            int i;
            int j = 0;
            for (i = 0; i < s1.Length; i++)
            {
                if (isnum(s1[i]) == true) { j++; }
            }

            return j;

        }

        public static bool isnum(char c)
        {
            bool b = false;
            if ((((int)c) >= 48) && (((int)c) <= 57)) b = true;
            return b;
        }

        public static bool onlynums(string s)
        {
            string s1 = s;

            int i;
            bool b = true;

            for (i = 0; i < s1.Length; i++)
            {
                if (isnum(s1[i]) != true) { b = false; }
            }

            return b;

        }

        public static string GetCleanPhoneNumber(string phone)
        {
            string result = string.Empty;

            int i = 0;
            if (phone.Length > 0)
            {
                i = CountNums(phone);
            }

            int cnum = 0;

            foreach (char c in phone)
            {
                cnum++;
                if (!((i > 10) && (cnum == 1) && (c == '1')))
                {
                    if (char.IsDigit(c))
                        result += c;
                }
            }
            return result;
        }

    }
}

