using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSBusiness.ShoppingManagement;
using System.Configuration;
using CSCore.DataHelper;
using CSBusiness.Resolver;
using CSBusiness.OrderManagement;
using CSBusiness.Cache;
using System.Web;
using CSBusiness.Preference;
using CSBusiness.Attributes;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Text.RegularExpressions;
using CSBusiness.PostSale;

namespace CSBusiness.PostSale
{
    [Serializable]
    public class Template
    {
        #region Properties

        /// <summary>
        /// Gets or sets the PathId
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the customer Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the customer Title
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the customer Title
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the CreateDate
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the ExpireDate
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        public bool Active { get; set; }

        public int OrderNo { get; set; }

        public List<TemplateSku> Items { get; set; }

        public bool HideRemove { get; set; }

        public string Script { get; set; }

        public string UriLabel { get; set; }

        public List<TemplateControl> ControlItems { get; set; }

        #endregion

        /// <summary>
        /// Use CanUseTemplate(ClientCartContext cartContext) overload instead from outside this method as that includes additional checks.
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        private bool CanUseTemplate(Cart cart)
        {
            bool valueToReturn = false;
            //Then if there is a supressed item then don't show the template
            if (!cart.CartItems.Exists(c => this.Items.Exists(t => t.SkuId == c.SkuId && t.TypeId == TemplateItemTypeEnum.Supress)))
            {
                //Check triggers second
                if (cart.CartItems.Exists(c => this.Items.Exists(t => t.SkuId == c.SkuId && t.TypeId == TemplateItemTypeEnum.Triggers)))
                {
                    valueToReturn = true;
                }
                else
                {
                    valueToReturn = false;
                }
            }

            return valueToReturn;
        }

        public bool CanUseTemplate(ClientCartContext cartContext)
        {
            bool canUse = CanUseTemplate(cartContext.CartInfo);

            if (!canUse)
                return canUse;

            canUse = ControlItems.FirstOrDefault(x =>
            {
                return x.StateId.HasValue && x.DisableTemplate.HasValue && 
                    x.StateId.Value == cartContext.CustomerInfo.ShippingAddress.StateProvinceId && x.DisableTemplate.Value;
            }) == null;

            if (!canUse)
                return canUse;

            return canUse;
        }


        public void Process(int orderId, Cart cartData, 
            Dictionary<int, int> selectedProducts,  // skuId => quantity            
            OrderProcessTypeEnum orderProcessType)
        {
            int totalItems = this.Items.Count;

            //Add Selected PostSell Item to Cart 
            if (selectedProducts.Count > 0)
            {
                SkuManager skuManager = new SkuManager();
                // List<Sku> allSkus = skuManager.GetAllSkus();
                foreach (var item in selectedProducts)
                {   
                    cartData.AddItem(item.Key, item.Value, true, true);
                }
            }

            //Apply business rules to add or remove items
            for (int i = 0; i < totalItems; i++)
            {
                TemplateSku item = this.Items[i];
                switch (item.TypeId)
                {
                    case TemplateItemTypeEnum.Add:
                        {
                            cartData.AddItem(item.SkuId, 1, true, true);
                        }
                        break;
                    case TemplateItemTypeEnum.Remove:
                        {
                            cartData.RemoveSku(item.SkuId);
                        }
                        break;
                    default:
                        break;
                }
            }

            // If this is a Upsell Review Order type, then we do not want to compute with non-save-to-db SKU's since it will mess up the totals when we want to save just save-to-db SKU's.
            // So, we drop non-save-to-db SKU's prior to computing, and then add them afterwards (so in the final review page we see ALL of the SKU's).
            List<Sku> nonSaveToDbSkus = new List<Sku>();
            if (orderProcessType == OrderProcessTypeEnum.EnableUpsellReviewOrder)
            {
                for (int i = 0; i < cartData.CartItems.Count; i++)
                {
                    if (cartData.CartItems[i].IsUpSell &&
                        !cartData.CartItems[i].GetAttributeValue<bool>("SaveToDatabase", false))
                    {
                        nonSaveToDbSkus.Add(cartData.CartItems[i]);

                        cartData.CartItems.RemoveAt(i);
                        i--;
                    }
                }
            }

            //Commit to database based on the config
            if (orderProcessType == OrderProcessTypeEnum.InstantOrderProcess
                || orderProcessType == OrderProcessTypeEnum.EnableUpsellReviewOrder)
            {
                cartData.Compute();
                if (orderProcessType != OrderProcessTypeEnum.EnableUpsellReviewOrder)
                {
                    new OrderManager().UpdateOrderAfterUpSell(orderId, cartData);
                }
                
                foreach (Sku sku in nonSaveToDbSkus)
                {
                    cartData.CartItems.Add(sku);
                }
            }
        }

        public static string InsertData(XElement templateTagsXml, string templateBody)
        {
            #region Sample XML
            /*
<TemplateDetails>
  <GetData>
    <Item what="SkuPrice" targetPlaceHolder="{SKU36_PRICE}">
      <Parameters skuId="36" />
    </Item>
    <Item what="SkuPrice" targetPlaceHolder="{SKU39_PRICE}">
      <Parameters skuId="39" />
    </Item>
  </GetData>
</TemplateDetails>
             * */
            #endregion

            // check data pull nodes
            foreach (var s in templateTagsXml.XPathSelectElements("//getdata/item"))
            {
                switch (s.Attribute("what").Value.ToLower())
                {
                    case "skuprice":
                        int skuId = Convert.ToInt32(s.XPathSelectElement("parameters").Attribute("skuid").Value);

                        Sku sku = CSResolve.Resolve<ISkuService>().GetSkuByID(skuId);

                        templateBody = templateBody.Replace(s.Attribute("targetplaceholder").Value, sku.InitialPrice.ToString());

                        break;
                }
            }

            return templateBody;
        }

        public static bool MatchesCondition(XElement conditions, string condKey)
        {
            #region Sample XML
            /*
    <Conditions>
      <Condition key="cond1">
        <FieldMatch name="userSelection" isRegex="false">one</FieldMatch>
      </Condition>
      <Condition key="cond2">
        <FieldMatch name="userSelection" isRegex="false">two</FieldMatch>
      </Condition>
      <Condition key="customer_info">
        <CustomerInfoMatch>
            <State></State>
            <Country></Country>
        </CustomerInfoMatch>
      </Condition>
    </Conditions>             
             */
            #endregion

            var fieldMatches = conditions.XPathSelectElements(string.Format("//condition[@key='{0}']/fieldmatch", condKey));
            if (fieldMatches == null)
                return false;

            string fieldName = null;
            string value = null;
            bool isMatch = true;
            foreach (var f in fieldMatches)
            {
                fieldName = f.Attribute("name").Value;
                value = f.Value;

                if (bool.Parse((f.Attribute("isregex") ?? new XAttribute("false", "false")).Value))
                {
                    isMatch = isMatch && Regex.IsMatch(HttpContext.Current.Request.Form[fieldName], value);
                }
                else
                {
                    isMatch = isMatch && HttpContext.Current.Request.Form[fieldName] == value;
                }

                if (!isMatch)
                    break;
            }

            return isMatch;
        }

        public static void GetTemplateSelections(int orderId, int templateId, List<Sku> cartItems, ref Dictionary<int, int> skusAndQuantities)
        {
            #region Sample XML
            /*
<TemplateDetails>
  <SelectionParameters>    
    <FixedSkuEntryFields useCondition="cond1" type="onepay">
      <Sku Id="36">
        <Field what="quantity" name="SKU36QTY" />
      </Sku>
      <Sku Id="39">
        <Field what="quantity" name="SKU39QTY" defaultValue />
      </Sku>
    </FixedSkuEntryFields>
    <FixedSkuEntryFields useCondition="cond2">
      <Sku Id="40">
        <Field what="quantity" name="SKU40QTY" />
      </Sku>      
    </FixedSkuEntryFields>
    <FixedSkuEntryFields type="onepay" />
    <Conditions>
      <Condition key="cond1">
        <FieldMatch name="userSelection" isRegex="false">one</FieldMatch>
      </Condition>
      <Condition key="cond2">
        <FieldMatch name="userSelection" isRegex="false">two</FieldMatch>
      </Condition>
    </Conditions>
  </SelectionParameters>  
</TemplateDetails>
             * */
            #endregion

            XElement templateTags = XElement.Parse(((Template)(new PathManager().GetTemplate(templateId))).Tag);

            // search "sku and select fields" information
            foreach (var r in templateTags.XPathSelectElements("//selectionparameters/fixedskuentryfields"))
            {
                if (r.Attribute("type") != null && r.Attribute("type").Value.Equals("onepay"))
                {
                    Order orderItem = new OrderManager().GetBatchProcessOrders(orderId);
                    foreach (Sku s in orderItem.SkuItems)
                    {
                        s.LoadAttributeValues();
                        try
                        {
                            if (s.AttributeValues["relatedonepaysku"] != null && !s.AttributeValues["relatedonepaysku"].Value.Equals(""))
                            {
                                int skuId = Convert.ToInt32(s.AttributeValues["relatedonepaysku"].Value);
                                skusAndQuantities.Add(skuId, 1);
                            }
                        }
                        catch
                        {
                        }
                    }

                    foreach (Sku s in cartItems)
                    {
                        s.LoadAttributeValues();
                        try
                        {
                            if (s.AttributeValues["relatedonepaysku"] != null && !s.AttributeValues["relatedonepaysku"].Value.Equals(""))
                            {
                                int skuId = Convert.ToInt32(s.AttributeValues["relatedonepaysku"].Value);
                                skusAndQuantities.Add(skuId, 1);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    if (r.Attribute("usecondition") == null
                        || MatchesCondition(templateTags.XPathSelectElement("//selectionparameters/conditions"), r.Attribute("usecondition").Value))
                    {
                        foreach (var s in r.XPathSelectElements("sku"))
                        {
                            int skuId = Convert.ToInt32(s.Attribute("id").Value);
                            string fieldName = null;

                            // read quantity
                            int quantity = 0;
                            XElement quantField = s.XPathSelectElement("field[@what = 'quantity']");
                            if (quantField != null)
                            {
                                if (quantField.Attribute("name") != null)
                                {
                                    fieldName = quantField.Attribute("name").Value;

                                    if (int.TryParse(HttpContext.Current.Request.Form[fieldName], out quantity))
                                        skusAndQuantities.Add(skuId, quantity);
                                }
                                else
                                    skusAndQuantities.Add(skuId, Convert.ToInt32((quantField.Attribute("defaultvalue") ?? new XAttribute("0", "0")).Value));
                            }
                        }
                    }
                }
            }
        }
    }
}
