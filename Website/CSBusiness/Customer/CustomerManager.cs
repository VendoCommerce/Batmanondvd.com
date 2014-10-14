using System;
using System.Collections.Generic;
using System.Linq;
using CSBusiness.CustomerManagement;
using CSData;
using System.Xml.Linq;
using CSCore;
using CSCore.DataHelper;
using System.Data.SqlClient;
using System.Data;
using CSBusiness.Security;

namespace CSBusiness
{
    class CustomerManager : ICustomerService
    {
        public CustomerManager()
        {

        }

        public Customer GetCustomer(int customerId)
        {

            Customer item = new Customer();
            using (SqlDataReader reader = CustomerDAL.GetCustomer(customerId))
            {
                while (reader.Read())
                {
                    item.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                    item.FirstName = reader["FirstName"].ToString();
                    item.LastName = reader["LastName"].ToString();
                    item.Email = reader["Email"].ToString();
                    item.Username = reader["UserName"].ToString();
                    item.PasswordHash = reader["value"].ToString();
                    item.SaltKey = reader["salt"].ToString();
                    item.Active = Convert.ToBoolean((reader["Active"]));
                    item.RegistrationDate = Convert.ToDateTime(reader["CreatedDate"]);
                    item.UserTypeId = Convert.ToInt32(reader["UserTypeId"]);


                }

                // go to next result set to populate scale
                reader.NextResult();

            }

            //Decrypt sensitive data 
            Encryption.DecryptValues(item); 

            return item;
        }


        public Customer GetCustomerDetails(int customerId)
        {

            Customer item = new Customer();
            item.IsEncrpyed = true;
            using (SqlDataReader reader = CustomerDAL.GetCustomerDetails(customerId))
            {
                while (reader.Read())
                {
                    item.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                    item.FirstName = reader["FirstName"].ToString();
                    item.LastName = reader["LastName"].ToString();
                    item.Email = reader["Email"].ToString();
                    item.Username = reader["UserName"].ToString();
                    item.Active = Convert.ToBoolean((reader["Active"]));
                    item.RegistrationDate = Convert.ToDateTime(reader["CreatedDate"]);
                    item.UserTypeId = Convert.ToInt32(reader["UserTypeId"]);


                }

                // go to next result set to populate customer billing Address
                reader.NextResult();
                while (reader.Read())
                {
                    CSBusiness.CustomerManagement.Address billingAddress = new CSBusiness.CustomerManagement.Address();
                    billingAddress.IsEncrpyed = true;
                    billingAddress.AddressId = Convert.ToInt32(reader["BillingAddressId"]);
                    billingAddress.FirstName = reader["FirstName"].ToString();
                    billingAddress.LastName = reader["LastName"].ToString();
                    billingAddress.Address1 = reader["Address1"].ToString();
                    billingAddress.Address2 = reader["Address2"].ToString();
                    billingAddress.City = reader["City"].ToString();
                    billingAddress.PhoneNumber = reader["PhoneNumber"].ToString();
                    billingAddress.ZipPostalCode = reader["ZipPostalCode"].ToString();
                    billingAddress.StateProvinceId = Convert.ToInt32(reader["StateProvince"]);
                    billingAddress.CountryId = Convert.ToInt32(reader["CountryId"]);
                    item.BillingAddress = billingAddress;

                }

                // go to next result set to populate customer shipping Address
                reader.NextResult();
                while (reader.Read())
                {
                    CSBusiness.CustomerManagement.Address shippingAddress = new CSBusiness.CustomerManagement.Address();
                    shippingAddress.IsEncrpyed = true;
                    shippingAddress.AddressId = Convert.ToInt32(reader["ShippingAddressId"]);
                    shippingAddress.FirstName = reader["FirstName"].ToString();
                    shippingAddress.LastName = reader["LastName"].ToString();
                    shippingAddress.Address1 = reader["Address1"].ToString();
                    shippingAddress.Address2 = reader["Address2"].ToString();
                    shippingAddress.City = reader["City"].ToString();
                    shippingAddress.PhoneNumber = reader["PhoneNumber"].ToString();
                    shippingAddress.ZipPostalCode = reader["ZipPostalCode"].ToString();
                    shippingAddress.StateProvinceId = Convert.ToInt32(reader["StateProvince"]);
                    shippingAddress.CountryId = Convert.ToInt32(reader["CountryId"]);
                    item.ShippingAddress = shippingAddress;

                }
            }

            //Decrypy sensitive data before save
            Encryption.DecryptValues(item); 

            return item;
        }



        public List<Customer> GetAllCustomers(string firstName, string LastName, string Email, int userTypeId, int startRec, int endRec, out int totalCount)
        {

            List<Customer> CustomerList = new List<Customer>();
            using (DataTable DtTable = CustomerDAL.GetCustomers(firstName, LastName, Email, userTypeId, startRec, endRec, out totalCount))
            {
                foreach (DataRow row in DtTable.Rows)
                {
                    Customer item = new Customer();
                    item.CustomerId = Convert.ToInt32(row["CustomerId"]);
                    item.FirstName = row["FirstName"].ToString();
                    item.LastName = row["LastName"].ToString();
                    item.Email = row["Email"].ToString();
                    item.RegistrationDate = Convert.ToDateTime(row["CreatedDate"]);
                    item.PasswordHash = row["value"].ToString();
                    item.SaltKey = row["salt"].ToString();
                    if (Convert.ToInt32(row["UserTypeId"]) != 1)
                    {
                        item.Password = "Hidden";// RijndaelSimple.Decrypt(item.PasswordHash, ConfigHelper.ReadAppSetting("PassPhrase", ""), item.SaltKey, "SHA1",2,"@1B2c3D4e5F6g7H8",256);
                    }
                    //Decrypt sensitive data 
                    Encryption.DecryptValues(item); 

                    CustomerList.Add(item);

                }

            }

            return CustomerList;
        }


        public List<Customer> GetAllCustomerOrders(string firstName, string LastName, string Email, int startRec, int endRec, out int totalCount)
        {

            List<Customer> CustomerList = new List<Customer>();
            using (DataTable DtTable = CustomerDAL.GetAllCustomerOrders(firstName, LastName, Email, startRec, endRec, out totalCount))
            {

                foreach (DataRow row in DtTable.Rows)
                {
                    Customer item = new Customer();
                    item.FirstName = row["FirstName"].ToString();
                    item.LastName = row["LastName"].ToString();
                    item.Email = row["Email"].ToString();
                    item.Password = row["TotalCost"].ToString();
                    item.UserTypeId = Convert.ToInt32(row["TotalOrders"]);
                    Encryption.DecryptValues(item);
                    CustomerList.Add(item);

                }

            }

            return CustomerList;
        }

        public SqlDataReader GetAllCustomerOrdersDetail(string firstName, string LastName, string Email)
        {

            return CustomerDAL.GetAllCustomerOrdersDetail(firstName, LastName, Email);
        }


        public void UpdateUser(Customer custData)
        {
            //Encrypt sensitive data before save
            Encryption.EncryptValues(custData); 

            XElement rootNode = new XElement("customers");
            string guid = String.Empty, passVal = string.Empty;
            Pair<string, string> itemval = new Pair<string, string>();

            if (custData.CustomerId == 0)
            {
                guid = Guid.NewGuid().ToString();
                itemval = EncryptDecryptPassword(custData.Password, String.Empty);

            }
            else
                itemval = EncryptDecryptPassword(custData.Password, custData.SaltKey);

            XElement xElem = new XElement("customer",
                                                new XAttribute("FirstName", custData.FirstName.ToString()),
                                                new XAttribute("LastName", custData.LastName),
                                                new XAttribute("Email", custData.Email),
                                                new XAttribute("UserName", custData.Username),
                                                new XAttribute("Active", custData.Active),
                                                new XAttribute("UniqueId", guid),
                                                new XAttribute("UserTypeId", custData.UserTypeId),
                                                new XAttribute("PasswordSalt", itemval.Item1.ToString()),
                                                 new XAttribute("Password", itemval.Item2.ToString()),
                                                new XAttribute("CreateDate", custData.RegistrationDate)
                                        );

            rootNode.Add(xElem);


            CustomerDAL.UpdateUser(custData.CustomerId, rootNode.ToString());
            Encryption.DecryptValues(custData); 

        }

        public int InsertCartAbandonment(Customer custData, ClientCartContext context)
        {
            string requestUrl = string.Empty;
            string refUrl = string.Empty;

            if (context.OrderAttributeValues != null)
            {
                if (context.OrderAttributeValues["landing_url"] != null)
                {
                    requestUrl = context.OrderAttributeValues["landing_url"].Value;
                }
                else
                    requestUrl = context.RequestParam;

                if (context.OrderAttributeValues["ref_url"] != null)
                {
                    refUrl = context.OrderAttributeValues["ref_url"].Value;
                }
                else
                    refUrl = context.ReferalParam;
            }
            XElement rootNode = new XElement("CartAbandonments");
            XElement xElem = new XElement("CartAbandonment",
                                                new XAttribute("FirstName", custData.FirstName),
                                                new XAttribute("LastName", custData.LastName),
                                                new XAttribute("Address1", custData.BillingAddress.Address1),
                                                new XAttribute("Address2", custData.BillingAddress.Address2),
                                                new XAttribute("City", custData.BillingAddress.City),
                                                new XAttribute("State", custData.BillingAddress.StateProvinceId.ToString()),
                                                new XAttribute("Country", custData.BillingAddress.CountryId.ToString()),
                                                new XAttribute("ZipCode", custData.BillingAddress.ZipPostalCode),
                                                new XAttribute("PhoneNumber", custData.PhoneNumber.ToString()),
                                                new XAttribute("Email", custData.Email.ToString()),
                                                new XAttribute("RequestUrl", requestUrl),
                                                new XAttribute("RefererUrl", refUrl)

                                        );
            rootNode.Add(xElem);
            return CustomerDAL.InsertCartAbandonment(rootNode.ToString());
        }
        public void RemoveCartAbandonment(int cartAbandonmentId)
        {
            CustomerDAL.RemoveCartAbandonment(cartAbandonmentId);
        }

        public int UpdateCustomer(int orderId, Customer custData)
        {
            //Encrypt sensitive data before save
            Encryption.EncryptValues(custData); 

            XElement rootNode = new XElement("customers");
            string guid = String.Empty, passVal = string.Empty;
            int result = 0;

            if (orderId == 0)
            {
                guid = Guid.NewGuid().ToString();

            }


            XElement xElem = new XElement("customer",
                                                new XAttribute("FirstName", custData.FirstName),
                                                new XAttribute("LastName", custData.LastName),
                                                new XAttribute("Email", custData.Email),
                                                new XAttribute("UserName", custData.Username),
                                                new XAttribute("Active", custData.Active),
                                                new XAttribute("UniqueId", guid)

                                        );

            rootNode.Add(xElem);
            XElement xAddressElem = new XElement("billingAddress",
                                                new XAttribute("FirstName", custData.BillingAddress.FirstName),
                                                new XAttribute("LastName", custData.BillingAddress.LastName),
                                                new XAttribute("Address1", custData.BillingAddress.Address1),
                                                new XAttribute("Address2",""),// custData.BillingAddress.Address2),
                                                new XAttribute("City", custData.BillingAddress.City),
                                                new XAttribute("State", custData.BillingAddress.StateProvinceId.ToString()),
                                                new XAttribute("Country", custData.BillingAddress.CountryId.ToString()),
                                                new XAttribute("ZipCode", custData.BillingAddress.ZipPostalCode),
                                                new XAttribute("PhoneNumber", custData.PhoneNumber.ToString())

                                        );

            rootNode.Add(xAddressElem);

            XElement xShippingAddressElem = new XElement("shippingAddress",
                                                new XAttribute("FirstName", custData.ShippingAddress.FirstName),
                                                new XAttribute("LastName", custData.ShippingAddress.LastName),
                                                new XAttribute("Address1", custData.ShippingAddress.Address1),
                                                new XAttribute("Address2", custData.ShippingAddress.Address2),
                                                new XAttribute("City", custData.ShippingAddress.City),
                                                new XAttribute("State", custData.ShippingAddress.StateProvinceId.ToString()),
                                                new XAttribute("Country", custData.ShippingAddress.CountryId.ToString()),
                                                new XAttribute("ZipCode", custData.ShippingAddress.ZipPostalCode),
                                                new XAttribute("PhoneNumber", custData.PhoneNumber.ToString())

                                        );

            rootNode.Add(xShippingAddressElem);
            result = CustomerDAL.UpdateCustomer(orderId, rootNode.ToString());
            Security.Encryption.DecryptValues(custData); 

            return result;

        }

        public Pair<string, string> EncryptDecryptPassword(string passwordVal, string saltVal)
        {
            Pair<string, string> item = new Pair<string, string>();
            if (saltVal == String.Empty)
                item.Item1 = CSEncryption.CreateSalt(4);
            else
                item.Item1 = saltVal;
            item.Item2 = RijndaelSimple.Encrypt(passwordVal,
                                                   ConfigHelper.ReadAppSetting("PassPhrase", ""),
                                                   item.Item1,
                                                   "SHA1",
                                                   2,
                                                   "@1B2c3D4e5F6g7H8",
                                                   256);
            //item.Item2 =   CSEncryption.CreatePasswordHash(passwordVal, saltVal, "SHA1");
            return item;
        }


        public CSBusiness.CustomerManagement.Address GetAddressById(int addressId)
        {
            if (addressId == 0)
                return null;

            CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection());

            var query = from c in context.Addresses
                        where c.AddressId == addressId
                        select c;
            var address = query.FirstOrDefault();
           CSBusiness.CustomerManagement.Address newAddress= new CSBusiness.CustomerManagement.Address { AddressId = address.AddressId };
           Security.Encryption.DecryptValues(newAddress);
           return newAddress;

        }

        public int Validate(string username, string password)
        {
            Triplet<string, string, int> pairVal = CustomerDAL.ValidateUser(username);

            if (pairVal.Item1 != String.Empty)
            {

                string saltValue = pairVal.Item2;
                string decryptVal = RijndaelSimple.Decrypt(pairVal.Item1,
                                                   ConfigHelper.ReadAppSetting("PassPhrase", ""),
                                                  saltValue,
                                                 "SHA1",
                                                        2,
                                                        "@1B2c3D4e5F6g7H8",
                                                        256); ;
                if (password == decryptVal)
                    return pairVal.Item3;
                else
                    return 0;
            }
            else
                return 0;

        }
    }
}
