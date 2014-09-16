using System;
using System.Collections.Generic;
using System.Xml.Linq;
using CSData;
using System.Data.SqlClient;
using System.Data;
using CSCore.Utils;
using System.Collections;

namespace CSBusiness
{
    public class SkuManager : ISkuService
    {
        public Sku skuItem;

        public List<Sku> GetAllSkus()
        {
            List<Sku> SkuList = new List<Sku>();
            using (SqlDataReader reader = SKUDAL.GetAllSkus(0))
            {
                while (reader.Read())
                {
                    Sku item = new Sku();
                    item.SkuId = Convert.ToInt32(reader["SkuId"]);
                    item.Title = reader["Title"].ToString();
                    item.SkuCode = reader["SkuCode"].ToString();
                    item.ShortDescription = reader["ShortDescription"].ToString();
                    item.LongDescription = reader["LongDescription"].ToString();
                    item.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                    item.FullPrice = Convert.ToDecimal(reader["FullPrice"]);
                    item.InitialPrice = Convert.ToDecimal(reader["InitialPrice"]);
                    item.Weight = Convert.ToDecimal(reader["Weight"]);
                    item.OfferCode = reader["OfferCode"].ToString();
                    item.IsTaxable = Convert.ToBoolean(reader["IsTaxable"]);
                    item.TaxableFullAmount = Convert.ToDecimal(reader["TaxableFullAmount"]);
                    item.IsAvailable = Convert.ToBoolean(reader["IsAvailable"]);
                    item.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
                    item.ModifyDate = Convert.ToDateTime(reader["ModifyDate"]);
                    item.SkuTitleCode = String.Format("{0} ({1})", CommonHelper.Left(reader["Title"].ToString(), 50), reader["SkuId"].ToString());
                    item.ImagePath = Convert.IsDBNull(reader["ImagePath"]) ? null : Convert.ToString(reader["ImagePath"]);
                    
                    SkuList.Add(item);

                }

            }
            return SkuList;
        }

        public List<Sku> GetAllSkus(int startRec, int endRec, out int totalCount)
        {
            List<Sku> SkuList = new List<Sku>();
            using (DataTable DtTable =  SKUDAL.GetAllSkus(startRec, endRec, out totalCount))
            {
                 foreach(DataRow row in DtTable.Rows)
                {
                    Sku item = new Sku();
                    item.SkuId = Convert.ToInt32(row["SkuId"]);
                    item.Title = row["Title"].ToString();
                    item.SkuCode = row["SkuCode"].ToString();
                    item.ShortDescription = row["ShortDescription"].ToString();
                    item.LongDescription = row["LongDescription"].ToString();
                    item.CategoryId = Convert.ToInt32(row["CategoryId"]);
                    item.FullPrice = Convert.ToDecimal(row["FullPrice"]);
                    item.InitialPrice = Convert.ToDecimal(row["InitialPrice"]);
                    item.Weight = Convert.ToDecimal(row["Weight"]);
                    item.OfferCode = row["OfferCode"].ToString();
                    item.IsTaxable = Convert.ToBoolean(row["IsTaxable"]);
                    item.TaxableFullAmount = Convert.ToDecimal(row["TaxableFullAmount"]);
                    item.IsAvailable = Convert.ToBoolean(row["IsAvailable"]);
                    item.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    item.ModifyDate = Convert.ToDateTime(row["ModifyDate"]);
                    SkuList.Add(item);

                }

            }
            return SkuList;
        }

        public Sku GetSkuItem()
        {
            skuItem = new Sku();
           return skuItem;
        }

        public Sku GetSkuByID(int skuID)
        {
            skuItem = new Sku();
            using (SqlDataReader reader = SKUDAL.GetAllSkus(skuID))
            {
                while (reader.Read())
                {
                     Sku item = new Sku();
                    item.SkuId = Convert.ToInt32(reader["SkuId"]);
                    item.Title = reader["Title"].ToString();
                    item.SkuCode = reader["SkuCode"].ToString();
                    item.ShortDescription = reader["ShortDescription"].ToString();
                    item.LongDescription = reader["LongDescription"].ToString();
                    item.EmailDescription = reader["EmailDescription"].ToString();
                    item.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                    item.FullPrice = Convert.ToDecimal(reader["FullPrice"]);
                    item.InitialPrice = Convert.ToDecimal(reader["InitialPrice"]);
                    item.Weight = Convert.ToDecimal(reader["Weight"]);
                    item.StockQty = (reader["StockQty"] == DBNull.Value) ? 0 : Convert.ToInt32(reader["StockQty"]);
                    item.OfferCode = reader["OfferCode"].ToString();
                    item.IsAvailable = Convert.ToBoolean(reader["IsAvailable"]);
                    item.IsTaxable = Convert.ToBoolean(reader["IsTaxable"]);
                    item.TaxableFullAmount = Convert.ToDecimal(reader["TaxableFullAmount"]);
                    item.ImagePath = reader["ImagePath"].ToString();
                    item.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
                    item.CreateDate = Convert.ToDateTime(reader["ModifyDate"]);

                    item.LoadAttributeValues();

                    skuItem = item;                  
                }                
            }
            return skuItem;
        }

        public void InsertSku(Sku skuItem)
        {   
            SKUDAL.InsertSku(skuItem.SkuId, Serialize(skuItem));
        }

        public void CopySku(int skuId)
        {
            SKUDAL.CopySku(skuId, Sku.objectName);
        }

        public string Serialize(Sku skuItem)
        {
            XElement rootNode = new XElement("skus");

            XElement attributeValuesElem = new XElement("AttributeValues");
            
            foreach (string attributeName in skuItem.AttributeValues.Keys)
                attributeValuesElem.Add(new XElement(Attributes.Attribute.CaseFixAttributeName(attributeName), skuItem.AttributeValues[attributeName].Value));

            XElement xElem = new XElement("sku",
                    new XAttribute("ObjectName", Sku.objectName),
                    new XAttribute("Title", skuItem.Title.ToString()),
                    new XAttribute("SkuCode", skuItem.SkuCode), 
                    new XAttribute("ShortDescription", skuItem.ShortDescription), 
                    new XAttribute("LongDescription", skuItem.LongDescription),
                    new XAttribute("EmailDescription", skuItem.EmailDescription),
                    new XAttribute("CategoryId", (skuItem.CategoryId>0) ? skuItem.CategoryId.ToString(): String.Empty), 
                    new XAttribute("FullPrice", skuItem.FullPrice), 
                    new XAttribute("InitialPrice", skuItem.InitialPrice),
                    new XAttribute("StockQty", skuItem.StockQty),                     
                    new XAttribute("Weight", skuItem.Weight), 
                    new XAttribute("OfferCode", skuItem.OfferCode), 
                    new XAttribute("IsAvailable", skuItem.IsAvailable), 
                    new XAttribute("IsTaxable", skuItem.IsTaxable), 
                    new XAttribute("TaxableFullAmount", skuItem.TaxableFullAmount),
                     new XAttribute("ImagePath", skuItem.ImagePath), 
                    new XAttribute("CreateDate", skuItem.CreateDate), 
                    new XAttribute("ModifyDate", skuItem.ModifyDate),
                    attributeValuesElem);

            rootNode.Add(xElem);

            return rootNode.ToString();
        }
    }
}
