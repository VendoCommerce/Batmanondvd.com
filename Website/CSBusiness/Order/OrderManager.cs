using System;
using System.Collections.Generic;
using System.Linq;
using CSData.Order;
using System.Xml.Linq;
using CSBusiness.Resolver;
using System.Data.SqlClient;
using CSCore.Utils;
using CSData;
using CSCore;
using System.Data;
using CSBusiness.ShoppingManagement;
using CSBusiness.Payment;
using CSBusiness.CustomerManagement;
using System.Collections;
using CSBusiness.Attributes;
using CSBusiness.Security;


namespace CSBusiness.OrderManagement
{
    public class OrderManager : IOrderService
    {
        public OrderManager()
        {

        }


        public SqlDataReader GetOrderSummary(DateTime? startDate, DateTime? endDate, int versionId, int pathId)
        {
            return OrderDAL.GetOrderSummary(startDate, endDate, versionId, pathId);
        }


        public Dictionary<int, List<ReportFields>> GetVersionSummary(DateTime? startDate, DateTime? endDate, bool includeArchiveData)
        {
            Dictionary<int, List<ReportFields>> item = new Dictionary<int, List<ReportFields>>();
            using (SqlDataReader reader = OrderDAL.GetVersionSummary(startDate, endDate, includeArchiveData))
            {
                List<ReportFields> CategorySummary = new List<ReportFields>();
                while (reader.Read())
                {
                    ReportFields rowitem = new ReportFields();
                    rowitem.CatgoryId = Int32.Parse(reader["CategoryId"].ToString());
                    rowitem.TotalOrders = Int32.Parse(reader["TotalOrders"].ToString());
                    rowitem.TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                    rowitem.AverageOrder = Convert.ToDecimal(reader["AverageOrder"]);
                    CategorySummary.Add(rowitem);

                }

                item.Add(0, CategorySummary);

                reader.NextResult();

                List<ReportFields> VersionSummary = new List<ReportFields>();
                while (reader.Read())
                {
                    ReportFields rowitem = new ReportFields();
                    rowitem.VersionId = Int32.Parse(reader["VersionId"].ToString());
                    rowitem.CatgoryId = Int32.Parse(reader["CategoryId"].ToString());
                    rowitem.Title = reader["Title"].ToString().ToLower();
                    rowitem.ShortName = reader["ShortName"].ToString();
                    rowitem.TotalOrders = Int32.Parse(reader["TotalOrders"].ToString());
                    rowitem.TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                    rowitem.AverageOrder = Convert.ToDecimal(reader["AverageOrder"]);
                    VersionSummary.Add(rowitem);

                }

                item.Add(1, VersionSummary);


            }
            return item;
        }

        public Dictionary<int, List<ReportFields>> GetTnTVersionSummary_HL(DateTime? startDate, DateTime? endDate, bool includeArchiveData)
        {
            Dictionary<int, List<ReportFields>> item = new Dictionary<int, List<ReportFields>>();
            using (SqlDataReader reader = OrderDAL.GetTnTVersionSummary_HL(startDate, endDate, includeArchiveData))
            {
                List<ReportFields> CategorySummary = new List<ReportFields>();
                while (reader.Read())
                {
                    ReportFields rowitem = new ReportFields();
                    rowitem.CatgoryId = Int32.Parse(reader["CategoryId"].ToString());
                    rowitem.TotalOrders = Int32.Parse(reader["TotalOrders"].ToString());
                    rowitem.TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                    rowitem.AverageOrder = Convert.ToDecimal(reader["AverageOrder"]);
                    CategorySummary.Add(rowitem);

                }

                item.Add(0, CategorySummary);

                reader.NextResult();

                List<ReportFields> VersionSummary = new List<ReportFields>();
                while (reader.Read())
                {
                    ReportFields rowitem = new ReportFields();
                    rowitem.VersionId = Int32.Parse(reader["VersionId"].ToString());
                    rowitem.CatgoryId = Int32.Parse(reader["CategoryId"].ToString());
                    rowitem.TnTCampaignId = Convert.IsDBNull(reader["TntCampaignId"]) ? (int?)null : Int32.Parse(Convert.ToString(reader["TntCampaignId"]));
                    rowitem.TnTExperienceId = Convert.IsDBNull(reader["TntExperienceId"]) ? (int?)null : Int32.Parse(Convert.ToString(reader["TntExperienceId"]));
                    rowitem.Title = reader["Title"].ToString().ToLower();
                    rowitem.ShortName = reader["ShortName"].ToString();
                    rowitem.TotalOrders = Int32.Parse(reader["TotalOrders"].ToString());
                    rowitem.TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                    rowitem.AverageOrder = Convert.ToDecimal(reader["AverageOrder"]);
                    VersionSummary.Add(rowitem);

                }

                item.Add(1, VersionSummary);


            }
            return item;
        }

        public Dictionary<int, List<ReportFields>> GetTnTVersionSummary_SC(int siteId, DateTime? startDate, DateTime? endDate, bool includeArchiveData)
        {
            Dictionary<int, List<ReportFields>> item = new Dictionary<int, List<ReportFields>>();
            using (SqlDataReader reader = OrderDAL.GetTnTVersionSummary_SC(startDate, endDate, includeArchiveData))
            {
                List<ReportFields> CategorySummary = new List<ReportFields>();
                while (reader.Read())
                {
                    ReportFields rowitem = new ReportFields();
                    rowitem.CatgoryId = Int32.Parse(reader["CategoryId"].ToString());
                    rowitem.TotalOrders = Int32.Parse(reader["TotalOrders"].ToString());
                    rowitem.TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                    rowitem.AverageOrder = Convert.ToDecimal(reader["AverageOrder"]);
                    CategorySummary.Add(rowitem);

                }

                item.Add(0, CategorySummary);

                reader.NextResult();

                List<ReportFields> VersionSummary = new List<ReportFields>();
                while (reader.Read())
                {
                    ReportFields rowitem = new ReportFields();
                    rowitem.VersionId = Int32.Parse(reader["VersionId"].ToString());
                    rowitem.CatgoryId = Int32.Parse(reader["CategoryId"].ToString());
                    rowitem.TnTCampaignId = Convert.IsDBNull(reader["TntCampaignId"]) ? (int?)null : Int32.Parse(Convert.ToString(reader["TntCampaignId"]));
                    rowitem.TnTExperienceId = Convert.IsDBNull(reader["TntExperienceId"]) ? (int?)null : Int32.Parse(Convert.ToString(reader["TntExperienceId"]));
                    rowitem.Title = reader["Title"].ToString().ToLower();
                    rowitem.ShortName = reader["ShortName"].ToString();
                    rowitem.TotalOrders = Int32.Parse(reader["TotalOrders"].ToString());
                    rowitem.TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                    rowitem.AverageOrder = Convert.ToDecimal(reader["AverageOrder"]);
                    VersionSummary.Add(rowitem);

                }

                item.Add(1, VersionSummary);
            }

            List<Version> versions = CSFactory.GetAllVersion();
            int defaultCategoryId = CSFactory.GetAllCategories().Single(x => x.Title == "Base Category").CategoryId;
            Version version;

            // append version records as captured by Sitecatalyst but not already in list.
            using (SqlDataReader reader = AnalyticsDAL.GetSCSiteVersion(siteId))
            {
                while (reader.Read())
                {
                    ReportFields rowitem = new ReportFields();
                    rowitem.TnTCampaignId = Convert.IsDBNull(reader["TntCampaignId"]) ? (int?)null : Int32.Parse(Convert.ToString(reader["TntCampaignId"]));
                    rowitem.TnTExperienceId = Convert.IsDBNull(reader["TntExperienceId"]) ? (int?)null : Int32.Parse(Convert.ToString(reader["TntExperienceId"]));
                    rowitem.Title = rowitem.ShortName = reader["VersionName"].ToString().ToUpper();

                    version = versions.SingleOrDefault(x => x.Title.ToUpper() == rowitem.Title.ToUpper());

                    rowitem.CatgoryId = version != null ? version.CategoryId : defaultCategoryId;

                    if (item[1].Count<ReportFields>(x => x.Title.ToLower() == rowitem.Title.ToLower()
                        && x.TnTCampaignId == rowitem.TnTCampaignId
                        && x.TnTExperienceId == rowitem.TnTExperienceId) == 0)
                    {
                        rowitem.TotalOrders = 0;
                        rowitem.TotalRevenue = 0;
                        rowitem.AverageOrder = 0;

                        item[1].Add(rowitem);
                    }
                }
            }

            item[1].Sort((a, b) =>
            {
                int x = string.Compare(a.Title, b.Title);

                if (x != 0)
                    return x;

                if (a.TnTCampaignId.HasValue && b.TnTCampaignId.HasValue)
                    x = a.TnTCampaignId.Value - b.TnTCampaignId.Value;
                else if (a.TnTCampaignId.HasValue && !b.TnTCampaignId.HasValue)
                    x = 1;
                else if (!a.TnTCampaignId.HasValue && b.TnTCampaignId.HasValue)
                    x = -1;
                else
                    return x;

                if (x != 0)
                    return x;

                if (a.TnTExperienceId.HasValue && b.TnTExperienceId.HasValue)
                    return a.TnTExperienceId.Value - b.TnTExperienceId.Value;
                else if (a.TnTExperienceId.HasValue && !b.TnTExperienceId.HasValue)
                    x = 1;
                else if (!a.TnTExperienceId.HasValue && b.TnTExperienceId.HasValue)
                    x = -1;

                return x;
            });


            return item;
        }

        public List<ReportFields> GetOrderCustomFieldReport(DateTime? startDate, DateTime? endDate, int customFieldId, bool includeArchiveData)
        {
            List<ReportFields> FieldSummary = new List<ReportFields>();
            using (SqlDataReader reader = OrderDAL.GetOrderCustomFieldReport(startDate, endDate, customFieldId, includeArchiveData))
            {
                while (reader.Read())
                {
                    ReportFields rowitem = new ReportFields();
                    int totalOrder = Int32.Parse(reader["TotalOrders"].ToString());
                    rowitem.Title = reader["FieldValue"].ToString().ToLower();


                    rowitem.TotalOrders = totalOrder;
                    rowitem.TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                    if (totalOrder > 0)
                        rowitem.AverageOrder = Math.Round(Convert.ToDecimal(reader["TotalRevenue"]) / totalOrder, 2);
                    else
                        rowitem.AverageOrder = 0;

                    FieldSummary.Add(rowitem);

                }
            }
            return FieldSummary;
        }

        public List<Pair<string, string>> GetOrderSummary(DateTime? startDate, DateTime? endDate, bool includeArchiveData)
        {
            List<Pair<string, string>> orderList = new List<Pair<string, string>>();
            using (SqlDataReader reader = OrderDAL.GetOrderSummary(startDate, endDate, includeArchiveData))
            {
                while (reader.Read())
                {
                    Pair<string, string> Item = new Pair<string, string>();
                    Item.Item1 = reader["Month"].ToString() + "/" + reader["Day"].ToString();
                    Item.Item2 = reader["Count"].ToString();
                    orderList.Add(Item);
                }
            }

            return orderList;
        }

        public List<Order> GetAllOrders(DateTime? startDate, DateTime? endDate, bool includeArchiveData, int startRec, int endRec, out int totalCount)
        {
            return GetAllOrders(startDate, endDate, includeArchiveData, startRec, endRec, string.Empty, string.Empty, string.Empty, out totalCount);
        }

        public List<Order> GetAllOrders(DateTime? startDate, DateTime? endDate, bool includeArchiveData, int startRec, int endRec,
            string firstName, string lastName, string email, out int totalCount)
        {

            List<Order> OrderList = new List<Order>();

            if (email.Length>0)
                email = CSCore.Utils.CommonHelper.Encrypt(email);

            using (DataTable DtTable = OrderDAL.GetAllOrders(startDate, endDate, includeArchiveData, startRec, endRec, firstName, lastName, email, out totalCount))
            {
                foreach (DataRow row in DtTable.Rows)
                {
                    Order item = new Order();
                    item.OrderId = Convert.ToInt32(row["OrderId"]);
                    item.Email = row["Email"].ToString();
                    item.SubTotal = Convert.ToDecimal(row["SubTotal"]);
                    item.FullPriceSubTotal = Convert.ToDecimal(row["FullPriceSubTotal"]);
                    item.ShippingCost = Convert.ToDecimal(row["ShippingCost"]);
                    item.AdditionalShippingCharge = Convert.ToDecimal(Convert.IsDBNull(row["AdditionalShippingCharge"]) ? 0m : row["AdditionalShippingCharge"]);
                    item.Tax = Convert.ToDecimal(row["Tax"]);
                    item.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                    item.Total = Convert.ToDecimal(row["Total"]);
                    item.FullPriceTax = Convert.ToDecimal(Convert.IsDBNull(row["FullPriceTax"]) ? 0m : row["FullPriceTax"]);
                    item.OrderStatus = Convert.ToString(row["OrderStatus"]);
                    item.OrderStatusId = Convert.ToInt32(row["OrderStatusid"].ToString());

                    Customer customer = new Customer();
                    customer.FirstName = Convert.ToString(row["firstName"]);
                    customer.LastName = Convert.ToString(row["lastName"]);

                    item.CustomerInfo = customer;

                    //Decrypy sensitive data before save
                    item.IsEncrpyed = true;
                    Encryption.DecryptValues(item); 

                    OrderList.Add(item);
                }

            }

            return OrderList;
        }

        public Order GetOrder(int orderId)
        {

            Order item = new Order();
            using (SqlDataReader reader = OrderDAL.GetOrder(orderId))
            {
                while (reader.Read())
                {

                    item.OrderId = Convert.ToInt32(reader["OrderId"]);
                    item.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                    item.Email = reader["Email"].ToString();
                    item.SubTotal = Convert.ToDecimal(reader["SubTotal"]);
                    item.FullPriceSubTotal = Convert.ToDecimal(reader["FullPriceSubTotal"]);
                    item.ShippingCost = Convert.ToDecimal(reader["ShippingCost"]);
                    item.RushShippingCost = Convert.ToDecimal(reader["RushShippingCost"]);
                    item.AdditionalShippingCharge = Convert.ToDecimal(reader["AdditionalShippingCharge"]);
                    item.Tax = Convert.ToDecimal(reader["Tax"]);
                    item.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    item.Total = Convert.ToDecimal(reader["Total"]);
                    item.FullPriceTax = Convert.ToDecimal(reader["FullPriceTax"]);

                }

            }

            //Decrypy sensitive data before save
            item.IsEncrpyed = true;
            Encryption.DecryptValues(item); 

            return item;
        }

        public Order GetOrderDetails(int orderId)
        {
            return GetOrderDetails(orderId, false);
        }
        public Hashtable GetBatchProcessOrders()
        {
            Hashtable htItems = new Hashtable();
            List<Order> items = new List<Order>();
            List<StateProvince> states = StateManager.GetAllStates(0);
            List<Country> countries = CountryManager.GetAllCountry();

            using (SqlDataReader reader = OrderDAL.GetBatchProcessOrders())
            {
                while (reader.Read())
                {
                    Order item = new Order();
                    item.OrderId = Convert.ToInt32(reader["OrderId"]);
                    item.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                    item.Email = reader["Email"].ToString();
                    item.SubTotal = Convert.ToDecimal(reader["SubTotal"]);
                    item.FullPriceSubTotal = Convert.ToDecimal(reader["FullPriceSubTotal"]);
                    item.ShippingCost = Convert.ToDecimal(reader["ShippingCost"]);
                    item.RushShippingCost = Convert.ToDecimal(reader["RushShippingCost"]);
                    item.AdditionalShippingCharge = Convert.ToDecimal(reader["AdditionalShippingCharge"]);
                    item.Tax = Convert.ToDecimal(reader["Tax"]);
                    item.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    item.Total = Convert.ToDecimal(reader["Total"]);
                    item.IpAddress = reader["IpAddress"].ToString();
                    item.VersionName = reader["Version"].ToString();
                    item.FullPriceTax = Convert.ToDecimal(reader["FullPriceTax"]);

                    item.CustomerInfo = new Customer();
                    item.CustomerInfo.IsEncrpyed = true;
                    item.CustomerInfo.BillingAddress = new CSBusiness.CustomerManagement.Address();
                    item.CustomerInfo.BillingAddress.IsEncrpyed = true;
                    item.CustomerInfo.ShippingAddress = new CSBusiness.CustomerManagement.Address();
                    item.CustomerInfo.ShippingAddress.IsEncrpyed = true;
                    item.CustomerInfo.BillingAddress.Company = reader["BillingCompany"].ToString();
                    item.CustomerInfo.BillingAddress.FirstName = reader["BillingFirstName"].ToString();
                    item.CustomerInfo.BillingAddress.LastName = reader["BillingLastName"].ToString();
                    item.CustomerInfo.BillingAddress.Address1 = reader["BillingAddress1"].ToString();
                    item.CustomerInfo.BillingAddress.Address2 = reader["BillingAddress2"].ToString();
                    item.CustomerInfo.BillingAddress.City = reader["BillingCity"].ToString();
                    item.CustomerInfo.BillingAddress.ZipPostalCode = reader["BillingZipPostalCode"].ToString();
                    item.CustomerInfo.BillingAddress.StateProvinceId = Convert.ToInt32(reader["BillingStateProvince"].ToString());
                    item.CustomerInfo.BillingAddress.CountryId = Convert.ToInt32(reader["BillingCountryId"].ToString());
                    StateProvince itemStateProvince = states.FirstOrDefault(x => x.StateProvinceId == Convert.ToInt32(reader["BillingStateProvince"].ToString()));
                    if (itemStateProvince != null)
                    {
                        item.CustomerInfo.BillingAddress.StateProvinceName = itemStateProvince.Name;
                    }
                    else
                    {
                        item.CustomerInfo.BillingAddress.StateProvinceName = string.Empty;
                    }

                    Country itemCountry = countries.FirstOrDefault(x => x.CountryId == Convert.ToInt32(reader["BillingCountryId"].ToString()));
                    if (itemCountry != null)
                    {
                        item.CustomerInfo.BillingAddress.CountryCode = itemCountry.Code;
                    }
                    else
                    {
                        item.CustomerInfo.BillingAddress.StateProvinceName = string.Empty;
                    }
                    item.CustomerInfo.BillingAddress.PhoneNumber = reader["BillingPhoneNumber"].ToString();
                    item.CustomerInfo.BillingAddress.FaxNumber = reader["BillingFaxNumber"].ToString();

                    item.CustomerInfo.ShippingAddress.Company = reader["ShippingCompany"].ToString();
                    item.CustomerInfo.ShippingAddress.FirstName = reader["ShippingFirstName"].ToString();
                    item.CustomerInfo.ShippingAddress.LastName = reader["ShippingLastName"].ToString();
                    item.CustomerInfo.ShippingAddress.Address1 = reader["ShippingAddress1"].ToString();
                    item.CustomerInfo.ShippingAddress.Address2 = reader["ShippingAddress2"].ToString();
                    item.CustomerInfo.ShippingAddress.City = reader["ShippingCity"].ToString();
                    item.CustomerInfo.ShippingAddress.ZipPostalCode = reader["ShippingZipPostalCode"].ToString();
                    item.CustomerInfo.ShippingAddress.StateProvinceId = Convert.ToInt32(reader["ShippingStateProvince"].ToString());
                    item.CustomerInfo.ShippingAddress.CountryId = Convert.ToInt32(reader["ShippingCountryId"].ToString());
                    item.CustomerInfo.ShippingAddress.PhoneNumber = reader["ShippingPhoneNumber"].ToString();
                    item.CustomerInfo.ShippingAddress.FaxNumber = reader["ShippingFaxNumber"].ToString();
                    StateProvince itemShippingStateProvince = states.FirstOrDefault(x => x.StateProvinceId == Convert.ToInt32(reader["ShippingStateProvince"].ToString()));
                    if (itemShippingStateProvince != null)
                    {
                        item.CustomerInfo.ShippingAddress.StateProvinceName = itemShippingStateProvince.Name;
                    }
                    else
                    {
                        item.CustomerInfo.ShippingAddress.StateProvinceName = string.Empty;
                    }

                    Country itemShippingCountry = countries.FirstOrDefault(x => x.CountryId == Convert.ToInt32(reader["ShippingCountryId"].ToString()));
                    if (itemShippingCountry != null)
                    {
                        item.CustomerInfo.ShippingAddress.CountryCode = itemShippingCountry.Code;
                    }
                    else
                    {
                        item.CustomerInfo.ShippingAddress.CountryCode = string.Empty;
                    }

                    PaymentInformation paymentDataInfo = new PaymentInformation();
                    paymentDataInfo.CreditCardNumber = CommonHelper.Decrypt(reader["CreditCardNumber"].ToString());
                    paymentDataInfo.CreditCardType = Convert.ToInt32(reader["CreditCardType"]);
                    paymentDataInfo.CreditCardName = reader["CreditCardName"].ToString(); ;
                    paymentDataInfo.CreditCardExpired = Convert.ToDateTime(reader["CreditCardExpired"]);
                    paymentDataInfo.CreditCardCSC = reader["CreditCardCSC"].ToString();
                    paymentDataInfo.AuthorizationCode = reader["AuthorizationCode"].ToString();
                    paymentDataInfo.TransactionCode = reader["TransactionCode"].ToString();
                    item.CreditInfo = paymentDataInfo;

                    Security.Encryption.DecryptValues(item);
                        
                    items.Add(item);
                }

                htItems.Add("allOrders", items);

                reader.NextResult();
                List<Sku> orderSku = new List<Sku>();
                while (reader.Read())
                {
                    Sku skuItem = new Sku();
                    skuItem.OrderId = Convert.ToInt32(reader["OrderId"]);
                    skuItem.SkuId = Convert.ToInt32(reader["SkuId"]);
                    skuItem.LongDescription = reader["LongDescription"].ToString();
                    skuItem.InitialPrice = Convert.ToDecimal(reader["InitialAmount"]);
                    skuItem.FullPrice = Convert.ToDecimal(reader["FullPrice"]);  //sku level info
                    skuItem.Quantity = Convert.ToInt32(reader["Quantity"]);
                    skuItem.TaxableFullAmount = Convert.ToDecimal(reader["TaxAmount"]);
                    skuItem.Title = reader["Title"].ToString();
                    skuItem.SkuCode = reader["SkuCode"].ToString();
                    skuItem.OfferCode = reader["OfferCode"].ToString();
                    skuItem.TotalPrice = Math.Round(Convert.ToInt32(reader["Quantity"]) * Convert.ToDecimal(reader["InitialAmount"]), 2);
                    orderSku.Add(skuItem);
                }
                htItems.Add("allOrderSkus", orderSku);
            }

            
            return htItems;

        }

        public Order GetOrderDetails(int orderId, bool paymentInfo)
        {

            Order item = new Order();
            using (SqlDataReader reader = OrderDAL.GetOrderDetails(orderId))
            {
                while (reader.Read())
                {

                    item.OrderId = Convert.ToInt32(reader["OrderId"]);
                    item.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                    item.Email = reader["Email"].ToString();
                    item.SubTotal = Convert.ToDecimal(reader["SubTotal"]);
                    item.FullPriceSubTotal = Convert.ToDecimal(reader["FullPriceSubTotal"]);
                    item.ShippingCost = Convert.ToDecimal(reader["ShippingCost"]);
                    item.RushShippingCost = Convert.ToDecimal(reader["RushShippingCost"]);
                    item.Tax = Convert.ToDecimal(reader["Tax"]);
                    item.AdditionalShippingCharge = Convert.ToDecimal(reader["AdditionalShippingCharge"]);
                    item.DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]);
                    item.DiscountCode = reader["DiscountCode"].ToString();
                    item.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    item.Total = Convert.ToDecimal(reader["Total"]);
                    item.IpAddress = reader["IpAddress"].ToString();
                    item.VersionName = Convert.ToString(reader["Version"] ?? string.Empty);
                    item.FullPriceTax = Convert.ToDecimal(reader["FullPriceTax"]);
                    item.OrderStatusId = Convert.ToInt32(reader["OrderStatusId"]);
                    item.OrderStatus = Convert.ToString(reader["OrderStatus"]);
                    if (paymentInfo)
                    {
                        PaymentInformation paymentDataInfo = new PaymentInformation();

                        paymentDataInfo.CreditCardNumber = CommonHelper.Decrypt(reader["CreditCardNumber"].ToString());
                        paymentDataInfo.CreditCardType = Convert.ToInt32(reader["CreditCardType"]);
                        paymentDataInfo.CreditCardName = reader["CreditCardName"].ToString(); ;
                        paymentDataInfo.CreditCardExpired = Convert.ToDateTime(reader["CreditCardExpired"]);
                        paymentDataInfo.CreditCardCSC = reader["CreditCardCSC"].ToString();
                        paymentDataInfo.AuthorizationCode = Convert.ToString(reader["AuthorizationCode"] ?? string.Empty);
                        paymentDataInfo.TransactionCode = Convert.ToString(reader["TransactionCode"] ?? string.Empty);

                        item.CreditInfo = paymentDataInfo;
                    }

                }

                reader.NextResult();
                item.SkuItems = new List<Sku>();
                while (reader.Read())
                {
                    Sku skuItem = new Sku();
                    skuItem.SkuId = Convert.ToInt32(reader["SkuId"]);
                    skuItem.LongDescription = reader["LongDescription"].ToString();
                    skuItem.InitialPrice = Convert.ToDecimal(reader["InitialAmount"]);
                    skuItem.FullPrice = Convert.ToDecimal(reader["FullPrice"]);  //sku level info
                    skuItem.Quantity = Convert.ToInt32(reader["Quantity"]);
                    skuItem.TaxableFullAmount = Convert.ToDecimal(reader["TaxAmount"]);
                    skuItem.Title = reader["Title"].ToString();
                    skuItem.SkuCode = reader["SkuCode"].ToString();
                    skuItem.OfferCode = reader["OfferCode"].ToString();
                    skuItem.TotalPrice = Math.Round(Convert.ToInt32(reader["Quantity"]) * Convert.ToDecimal(reader["InitialAmount"]), 2);
                    if (reader["ImagePath"]!= null)
                        skuItem.ImagePath = reader["ImagePath"].ToString();
                    item.SkuItems.Add(skuItem);

                }

                reader.NextResult();
                item.CustomFiledInfo = new List<OrderCustomField>();
                while (reader.Read())
                {
                    OrderCustomField filedItem = new OrderCustomField();
                    filedItem.FieldId = Convert.ToInt32(reader["FieldId"]);
                    filedItem.FieldName = reader["FieldName"].ToString();
                    filedItem.FieldValue = reader["FieldValue"].ToString();
                    item.CustomFiledInfo.Add(filedItem);

                }



            }

            //Decrypy sensitive data before save
            item.IsEncrpyed = true;
            Encryption.DecryptValues(item); 

            return item;
        }

        public void UpdateOrderPath(int orderId, int pathId)
        {
            OrderDAL.SaveOrderPath(orderId, pathId);

        }

        /// <summary>
        /// Sri Comment: First time Order informaiton saved based on the database setting
        /// </summary>
        /// <param name="custData"></param>
        /// <returns></returns>
        public int SaveOrder(ClientCartContext custData)
        {
            XElement rootNode = new XElement("orders");
            string guid = String.Empty, passVal = string.Empty;
            try
            {
                XElement attributeValuesElem = new XElement("AttributeValues");

                if (custData.OrderAttributeValues != null)
                    foreach (string attributeName in custData.OrderAttributeValues.Keys)
                        attributeValuesElem.Add(new XElement(Attributes.Attribute.CaseFixAttributeName(attributeName), custData.OrderAttributeValues[attributeName].Value));

                int CustomerId = CSResolve.Resolve<ICustomerService>().UpdateCustomer(0, custData.CustomerInfo);

                //Encrypt sensitive data before save
                Encryption.EncryptValues(custData.CustomerInfo); 

                XElement xElem = new XElement("order",
                                                    new XAttribute("ObjectName", Order.objectName),
                                                    new XAttribute("CustomerId", CustomerId.ToString()),
                                                    new XAttribute("Email", custData.CustomerInfo.Email),
                                                    new XAttribute("SubTotal", custData.CartInfo.SubTotal),
                                                    new XAttribute("TaxSubTotal", custData.CartInfo.SubTotalTax),
                                                    new XAttribute("FullPriceSubTotal", custData.CartInfo.SubTotalFullPrice),
                                                    new XAttribute("Tax", custData.CartInfo.TaxCost),
                                                    new XAttribute("ShippingCost", custData.CartInfo.ShippingCost),
                                                     new XAttribute("RushShippingCost", custData.CartInfo.RushShippingCost),
                                                     new XAttribute("AdditionalShippingCharge", custData.CartInfo.AdditionalShippingCharge),
                                                        new XAttribute("DiscountAmount", custData.CartInfo.DiscountAmount),
                                                        new XAttribute("DiscountCode", custData.CartInfo.DiscountCode),
                                                    new XAttribute("OrderStatusId", 1),
                                                    new XAttribute("CreditCardType", custData.PaymentInfo.CreditCardType),
                                                    new XAttribute("CreditCardName", custData.PaymentInfo.CreditCardName),
                                                    new XAttribute("CreditCardNumber", custData.PaymentInfo.CreditCardNumber),
                                                     new XAttribute("CreditCardExpired", custData.PaymentInfo.CreditCardExpired),
                                                     new XAttribute("CreditCardCSC", custData.PaymentInfo.CreditCardCSC),
                                                     new XAttribute("VersionId", custData.VersionId),
                                                     new XAttribute("IpAddress", custData.IpAddress),
                                                     new XAttribute("FullPriceTax", custData.CartInfo.TaxFullPrice),
                                                     attributeValuesElem
                                                    );

                rootNode.Add(xElem);

                XElement xItemElem = new XElement("orderitems",
                                                       from skuItem in custData.CartInfo.CartItems
                                                       select new XElement("Items",
                                                                           new XAttribute("SkuId", skuItem.SkuId),
                                                                           new XAttribute("Quantity", skuItem.Quantity),
                                                                           new XAttribute("InitialAmount", skuItem.InitialPrice),
                                                                           new XAttribute("FullAmount", skuItem.FullPrice),
                                                                           new XAttribute("TaxAmount", skuItem.TaxableFullAmount),
                                                                            new XAttribute("IsUpsell", skuItem.IsUpSell)
                                                                            )
                                                  );

                rootNode.Add(xItemElem);

                //Parse any order Custom field in URL
                if (!String.IsNullOrEmpty(custData.RequestParam))
                {
                    Dictionary<string, string> qDict = CommonHelper.QueryCollection(custData.RequestParam);
                    List<Pair<int, string>> custFielditem = new List<Pair<int, string>>();
                    foreach (CustomField Item in CustomFieldDAL.GetCustomFields())
                    {
                        if (qDict.ContainsKey(Item.FieldName.ToLower()))
                        {
                            Pair<int, string> existItem = new Pair<int, string>();
                            existItem.Item1 = Item.FieldId;
                            existItem.Item2 = qDict[Item.FieldName.ToLower()];
                            custFielditem.Add(existItem);
                        }
                    }

                    XElement cItemElem = new XElement("ordercustomfield",
                                                               from Item in custFielditem
                                                               select new XElement("customfield",
                                                                                       new XAttribute("filedId", Item.Item1),
                                                                                       new XAttribute("fieldValue", Item.Item2)

                                                                                     )
                                                       );

                    rootNode.Add(cItemElem);
                }

                //Decrypt sensitive data before save
                Encryption.DecryptValues(custData.CustomerInfo); 

                return OrderDAL.SaveOrder(0, rootNode.ToString(), custData.RequestParam);

            }
            catch (Exception ex)
            {                
                CSCore.CSLogger.Instance.LogException(ex.Message, ex);
            }
            return -1;
        }

        /// <summary>
        /// Sri Comment: This method used for Order Reject Re-Submission usecase - VenacuraDirect model
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="custData"></param>
        public void UpdateOrder(int orderId, ClientCartContext custData)
        {
            XElement rootNode = new XElement("orders");
            string guid = String.Empty, passVal = string.Empty;
            try
            {
                int CustomerId = CSResolve.Resolve<ICustomerService>().UpdateCustomer(orderId, custData.CustomerInfo);

                XElement attributeValuesElem = new XElement("AttributeValues");

                foreach (string attributeName in custData.OrderAttributeValues.Keys)
                    attributeValuesElem.Add(new XElement(Attributes.Attribute.CaseFixAttributeName(attributeName), custData.OrderAttributeValues[attributeName].Value));

                //Encrypt sensitive data before save
                Encryption.EncryptValues(custData.CustomerInfo); 


                XElement xElem = new XElement("order",
                                                    new XAttribute("ObjectName", Order.objectName),
                                                    new XAttribute("Email", custData.CustomerInfo.Email),
                                                    new XAttribute("SubTotal", custData.CartInfo.SubTotal),
                                                    new XAttribute("TaxSubTotal", custData.CartInfo.SubTotalTax),
                                                    new XAttribute("FullPriceSubTotal", custData.CartInfo.SubTotalFullPrice),
                                                    new XAttribute("Tax", custData.CartInfo.TaxCost),
                                                    new XAttribute("ShippingCost", custData.CartInfo.ShippingCost),
                                                     new XAttribute("RushShippingCost", custData.CartInfo.RushShippingCost),
                                                     new XAttribute("AdditionalShippingCharge", custData.CartInfo.AdditionalShippingCharge),
                                                    new XAttribute("DiscountAmount", custData.CartInfo.DiscountAmount),
                                                     new XAttribute("DiscountCode", custData.CartInfo.DiscountCode),
                                                    new XAttribute("OrderStatusId", 1),
                                                    new XAttribute("CreditCardType", custData.PaymentInfo.CreditCardType),
                                                    new XAttribute("CreditCardName", custData.PaymentInfo.CreditCardName),
                                                    new XAttribute("CreditCardNumber", custData.PaymentInfo.CreditCardNumber),
                                                     new XAttribute("CreditCardExpired", custData.PaymentInfo.CreditCardExpired),
                                                     new XAttribute("CreditCardCSC", custData.PaymentInfo.CreditCardCSC),
                                                     new XAttribute("VersionId", custData.VersionId),
                                                     new XAttribute("IpAddress", custData.IpAddress),
                                                     new XAttribute("FullPriceTax", custData.CartInfo.TaxFullPrice),
                                                     attributeValuesElem
                                                    );

                rootNode.Add(xElem);

                OrderDAL.SaveOrder(orderId, rootNode.ToString(), "");

                //Decrypt sensitive data before save
                Encryption.DecryptValues(custData.CustomerInfo); 

            }
            catch (Exception ex)
            {
                CSCore.CSLogger.Instance.LogException(ex.Message, ex);
            }

        }

        public void UpdateOrderAttributes(int orderId, IDictionary<string, AttributeValue> orderAttributeValues, int? orderStatusId)
        {
            XElement rootNode = new XElement("orders");

            try
            {
                XElement attributeValuesElem = new XElement("AttributeValues");

                foreach (string attributeName in orderAttributeValues.Keys)
                    attributeValuesElem.Add(new XElement(Attributes.Attribute.CaseFixAttributeName(attributeName), orderAttributeValues[attributeName].Value));

                XElement xElem = new XElement("order",
                    new XAttribute("ObjectName", Order.objectName),
                        attributeValuesElem
                    );

                rootNode.Add(xElem);

                OrderDAL.UpdateOrderAttributes(orderId, rootNode.ToString(), orderStatusId);
            }
            catch (Exception ex)
            {
                CSCore.CSLogger.Instance.LogException(ex.Message, ex);
            }
        }

        public void UpdateOrderStatus(int orderId, int orderStatusId)
        {            
            OrderDAL.UpdateOrderStatus(orderId, orderStatusId);
        }

        /// <summary>
        /// Sri Comment: This method used to update tax information coming from 3rd party tax integration module.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="custData"></param>
        public void UpdateOrderTax(int orderId, decimal tax)
        {
            try
            {
                OrderDAL.UpdateOrderTax(orderId, tax);
            }
            catch (Exception ex)
            {
                CSCore.CSLogger.Instance.LogException(ex.Message, ex);
            }

        }

        public void UpdateOrderAfterUpSell(int orderId, Cart cartData)
        {
            XElement rootNode = new XElement("orders");
            string guid = String.Empty, passVal = string.Empty;

            XElement xElem = new XElement("order",

                                                  new XAttribute("SubTotal", cartData.SubTotal),
                                                  new XAttribute("TaxSubTotal", cartData.SubTotalTax),
                                                  new XAttribute("FullPriceSubTotal", cartData.SubTotalFullPrice),
                                                  new XAttribute("Tax", cartData.TaxCost),
                                                  new XAttribute("ShippingCost", cartData.ShippingCost),
                                                   new XAttribute("DiscountCode", cartData.DiscountCode),
                                                   new XAttribute("DiscountAmount", cartData.DiscountAmount),
                                                   new XAttribute("RushShippingCost", cartData.RushShippingCost),
                                                   new XAttribute("AdditionalShippingCharge", cartData.AdditionalShippingCharge),
                                                   new XAttribute("FullPriceTax", cartData.TaxFullPrice)
                                                  );

            rootNode.Add(xElem);

            XElement xItemElem = new XElement("orderitems",
                                   from skuItem in cartData.CartItems
                                   select new XElement("Items",
                                       new XAttribute("SkuId", skuItem.SkuId),
                                       new XAttribute("Quantity", skuItem.Quantity),
                                       new XAttribute("InitialAmount", skuItem.InitialPrice),
                                       new XAttribute("FullAmount", skuItem.FullPrice),
                                        new XAttribute("TaxAmount", skuItem.TaxableFullAmount),
                                         new XAttribute("IsUpsell", skuItem.IsUpSell)
                                                   )
                                              );

            rootNode.Add(xItemElem);

            OrderDAL.UpdateOrderAfterUpSell(orderId, rootNode.ToString());
        }

        public void SaveOrder(int orderId, string transactionCode, string authCode, int orderStatusId)
        {
            OrderDAL.SaveOrder(orderId, transactionCode, authCode, orderStatusId);
        }

        public void SaveOrderInfo(int orderId, int orderStatuId, string fullfillmentRequest, string fullfillResponse)
        {
            OrderDAL.SaveOrderInfo(orderId, orderStatuId, fullfillmentRequest, fullfillResponse);
        }

        public void FireEmailLog(int orderId, string email, string subject, string body, DateTime sentDate)
        {
            OrderDAL.FireEmailLog(orderId, email, subject, body, sentDate);
        }

        public Order GetBatchProcessOrders(int orderId)
        {

            List<StateProvince> states = StateManager.GetAllStates(0);
            List<Country> countries = CountryManager.GetAllCountry();
            Order item = new Order();

            using (SqlDataReader reader = OrderDAL.GetBatchProcessOrders(orderId))
            {
                while (reader.Read())
                {

                    item.OrderId = Convert.ToInt32(reader["OrderId"]);
                    item.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                    item.Email = reader["Email"].ToString();
                    item.SubTotal = Convert.ToDecimal(reader["SubTotal"]);
                    item.FullPriceSubTotal = Convert.ToDecimal(reader["FullPriceSubTotal"]);
                    item.ShippingCost = Convert.ToDecimal(reader["ShippingCost"]);
                    item.RushShippingCost = Convert.ToDecimal(reader["RushShippingCost"]);
                    item.AdditionalShippingCharge = Convert.ToDecimal(reader["AdditionalShippingCharge"]);
                    item.Tax = Convert.ToDecimal(reader["Tax"]);
                    item.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    item.Total = Convert.ToDecimal(reader["Total"]);
                    item.IpAddress = reader["IpAddress"].ToString();
                    item.VersionName = reader["Version"].ToString();
                    item.FullPriceTax = Convert.ToDecimal(reader["FullPriceTax"]);
                    item.CustomerInfo = new Customer();
                    item.CustomerInfo.IsEncrpyed = true;
                    item.CustomerInfo.BillingAddress = new CSBusiness.CustomerManagement.Address();
                    item.CustomerInfo.BillingAddress.IsEncrpyed = true;
                    item.CustomerInfo.ShippingAddress = new CSBusiness.CustomerManagement.Address();
                    item.CustomerInfo.ShippingAddress.IsEncrpyed = true;
                    item.CustomerInfo.BillingAddress.Company = reader["BillingCompany"].ToString();
                    item.CustomerInfo.BillingAddress.FirstName = reader["BillingFirstName"].ToString();
                    item.CustomerInfo.BillingAddress.LastName = reader["BillingLastName"].ToString();
                    item.CustomerInfo.BillingAddress.Address1 = reader["BillingAddress1"].ToString();
                    item.CustomerInfo.BillingAddress.Address2 = reader["BillingAddress2"].ToString();
                    item.CustomerInfo.BillingAddress.City = reader["BillingCity"].ToString();
                    item.CustomerInfo.BillingAddress.ZipPostalCode = reader["BillingZipPostalCode"].ToString();
                    item.CustomerInfo.BillingAddress.StateProvinceId = Convert.ToInt32(reader["BillingStateProvince"].ToString());
                    item.CustomerInfo.BillingAddress.CountryId = Convert.ToInt32(reader["BillingCountryId"].ToString());
                    StateProvince itemStateProvince = states.FirstOrDefault(x => x.StateProvinceId == Convert.ToInt32(reader["BillingStateProvince"].ToString()));
                    if (itemStateProvince != null)
                    {
                        item.CustomerInfo.BillingAddress.StateProvinceName = itemStateProvince.Name;
                    }
                    else
                    {
                        item.CustomerInfo.BillingAddress.StateProvinceName = string.Empty;
                    }

                    Country itemCountry = countries.FirstOrDefault(x => x.CountryId == Convert.ToInt32(reader["BillingCountryId"].ToString()));
                    if (itemCountry != null)
                    {
                        item.CustomerInfo.BillingAddress.CountryCode = itemCountry.Code;
                    }
                    else
                    {
                        item.CustomerInfo.BillingAddress.StateProvinceName = string.Empty;
                    }
                    item.CustomerInfo.BillingAddress.PhoneNumber = reader["BillingPhoneNumber"].ToString();
                    item.CustomerInfo.BillingAddress.FaxNumber = reader["BillingFaxNumber"].ToString();

                    item.CustomerInfo.ShippingAddress.Company = reader["ShippingCompany"].ToString();
                    item.CustomerInfo.ShippingAddress.FirstName = reader["ShippingFirstName"].ToString();
                    item.CustomerInfo.ShippingAddress.LastName = reader["ShippingLastName"].ToString();
                    item.CustomerInfo.ShippingAddress.Address1 = reader["ShippingAddress1"].ToString();
                    item.CustomerInfo.ShippingAddress.Address2 = reader["ShippingAddress2"].ToString();
                    item.CustomerInfo.ShippingAddress.City = reader["ShippingCity"].ToString();
                    item.CustomerInfo.ShippingAddress.ZipPostalCode = reader["ShippingZipPostalCode"].ToString();
                    item.CustomerInfo.ShippingAddress.StateProvinceId = Convert.ToInt32(reader["ShippingStateProvince"].ToString());
                    item.CustomerInfo.ShippingAddress.CountryId = Convert.ToInt32(reader["ShippingCountryId"].ToString());
                    item.CustomerInfo.ShippingAddress.PhoneNumber = reader["ShippingPhoneNumber"].ToString();
                    item.CustomerInfo.ShippingAddress.FaxNumber = reader["ShippingFaxNumber"].ToString();
                    StateProvince itemShippingStateProvince = states.FirstOrDefault(x => x.StateProvinceId == Convert.ToInt32(reader["ShippingStateProvince"].ToString()));
                    if (itemShippingStateProvince != null)
                    {
                        item.CustomerInfo.ShippingAddress.StateProvinceName = itemShippingStateProvince.Name;
                    }
                    else
                    {
                        item.CustomerInfo.ShippingAddress.StateProvinceName = string.Empty;
                    }

                    Country itemShippingCountry = countries.FirstOrDefault(x => x.CountryId == Convert.ToInt32(reader["ShippingCountryId"].ToString()));
                    if (itemShippingCountry != null)
                    {
                        item.CustomerInfo.ShippingAddress.CountryCode = itemShippingCountry.Code;
                    }
                    else
                    {
                        item.CustomerInfo.ShippingAddress.CountryCode = string.Empty;
                    }

                     Security.Encryption.DecryptValues(item.CustomerInfo);

                   PaymentInformation paymentDataInfo = new PaymentInformation();
                    paymentDataInfo.CreditCardNumber = CommonHelper.Decrypt(reader["CreditCardNumber"].ToString());
                    paymentDataInfo.CreditCardType = Convert.ToInt32(reader["CreditCardType"]);
                    paymentDataInfo.CreditCardName = reader["CreditCardName"].ToString(); ;
                    paymentDataInfo.CreditCardExpired = Convert.ToDateTime(reader["CreditCardExpired"]);
                    paymentDataInfo.CreditCardCSC = reader["CreditCardCSC"].ToString();
                    paymentDataInfo.AuthorizationCode = reader["AuthorizationCode"].ToString();
                    paymentDataInfo.TransactionCode = reader["TransactionCode"].ToString();
                    item.CreditInfo = paymentDataInfo;



                }


                reader.NextResult();
                item.SkuItems = new List<Sku>();
                while (reader.Read())
                {
                    Sku skuItem = new Sku();
                    skuItem.OrderId = Convert.ToInt32(reader["OrderId"]);
                    skuItem.SkuId = Convert.ToInt32(reader["SkuId"]);
                    skuItem.LongDescription = reader["LongDescription"].ToString();
                    skuItem.InitialPrice = Convert.ToDecimal(reader["InitialAmount"]);
                    skuItem.FullPrice = Convert.ToDecimal(reader["FullPrice"]);  //sku level info
                    skuItem.Quantity = Convert.ToInt32(reader["Quantity"]);
                    skuItem.TaxableFullAmount = Convert.ToDecimal(reader["TaxAmount"]);
                    skuItem.Title = reader["Title"].ToString();
                    skuItem.SkuCode = reader["SkuCode"].ToString();
                    skuItem.OfferCode = reader["OfferCode"].ToString();
                    skuItem.TotalPrice = Math.Round(Convert.ToInt32(reader["Quantity"]) * Convert.ToDecimal(reader["InitialAmount"]), 2);
                    item.SkuItems.Add(skuItem);
                }

                reader.NextResult();
                item.CustomFiledInfo = new List<OrderCustomField>();
                while (reader.Read())
                {
                    OrderCustomField filedItem = new OrderCustomField();
                    filedItem.FieldId = Convert.ToInt32(reader["FieldId"]);
                    filedItem.FieldName = reader["FieldName"].ToString();
                    filedItem.FieldValue = reader["FieldValue"].ToString();
                    item.CustomFiledInfo.Add(filedItem);

                }
            }

            //Decrypt sensitive data before save
            Encryption.DecryptValues(item); 

            return item;

        }

        public void RemoveOrder(int orderId)
        {
            OrderDAL.RemoveOrder(orderId);
        }


        #region

        public List<ReportFields> GetOrderTransaction(DateTime? startDate, DateTime? endDate, int fieldId, bool includeArchiveData)
        {

            List<ReportFields> OrderList = new List<ReportFields>();
            using (SqlDataReader reader = OrderDAL.GetOrderTransaction(startDate, endDate, fieldId, includeArchiveData))
            {
                while (reader.Read())
                {
                    ReportFields item = new ReportFields();
                    item.orderId = Convert.ToInt32(reader["OrderId"]);
                    item.OrderStatus = reader["OrderStatus"].ToString();
                    item.OrderStatusId = Convert.ToInt32(reader["OrderStatusid"].ToString());
                    item.TotalRevenue = Convert.ToDecimal(reader["Total"]);
                    item.Affiliate = reader["Affiliate"].ToString();
                    item.AuthorizationCode = reader["AuthorizationCode"].ToString();
                    item.TransactionCode = reader["TransactionCode"].ToString();
                    item.TransactionDate = reader["TransactionDate"].ToString();
                    item.BillingName = reader["BillingName"].ToString();
                    OrderList.Add(item);

                }

            }

            return OrderList;
        }

        public SqlDataReader GetOrderCouponDetail(DateTime? startDate, DateTime? endDate, bool includeArchiveData)
        {

            return OrderDAL.GetOrderCouponDetail(startDate, endDate, includeArchiveData);

        }

        public SqlDataReader GetOrderCouponSummary(DateTime? startDate, DateTime? endDate, bool includeArchiveData)
        {

            return OrderDAL.GetOrderCouponSummary(startDate, endDate, includeArchiveData);

        }

        #endregion

    }
}
