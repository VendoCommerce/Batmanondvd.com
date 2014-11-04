using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSData;
using CSBusiness.ShoppingManagement;
using CSBusiness.Preference;
using System.Xml;

namespace CSBusiness.Shipping
{
    public class ShippingManager : IShippingCalculator
    {
        static Dictionary<ShippingOptionType, IShippingCalculator> _allShippingCalculators;
		static Dictionary<ShippingOptionType, IShippingCalculator> _allRushShippingCalculators;
        static Dictionary<string, decimal> additionalRushShippingCosts;

        static ShippingManager()
        {
            _allShippingCalculators = new Dictionary<ShippingOptionType, IShippingCalculator>();
            _allShippingCalculators.Add(ShippingOptionType.TotalAmount, new OrderValueShippingCalculator(false));
            _allShippingCalculators.Add(ShippingOptionType.Weight, new OrderWeightShippingCalculator(false));
            _allShippingCalculators.Add(ShippingOptionType.SkuBased, new SkuBasedShippingCalculator(false));
            _allShippingCalculators.Add(ShippingOptionType.Flat, new FlatShippingCalculator(false));

			_allRushShippingCalculators = new Dictionary<ShippingOptionType, IShippingCalculator>();
			_allRushShippingCalculators.Add(ShippingOptionType.TotalAmount, new OrderValueShippingCalculator(true));
			_allRushShippingCalculators.Add(ShippingOptionType.Weight, new OrderWeightShippingCalculator(true));
			_allRushShippingCalculators.Add(ShippingOptionType.SkuBased, new SkuBasedShippingCalculator(true));
			_allRushShippingCalculators.Add(ShippingOptionType.Flat, new FlatShippingCalculator(true));

            additionalRushShippingCosts = new Dictionary<string, decimal>();

        }

        public void Calculate(Cart cart, int prefID)
        {
            SitePreference shippingPreferences = CSFactory.GetCartPrefrence(cart);
            if (shippingPreferences != null)
            {
                ShippingOptionType option = shippingPreferences.ShippingOptionId;
                IShippingCalculator calculator = _allShippingCalculators[option];
				calculator.Calculate(cart, shippingPreferences.ShippingPrefID);

                //CodeReview: Instead of Cart pref and compute based on the admin pref
				if (cart.ShippingMethod == UserShippingMethodType.Rush)
				{
					ShippingOptionType rushOption = shippingPreferences.RushShippingOptionID;
					IShippingCalculator rushCalculator = _allRushShippingCalculators[rushOption];
					rushCalculator.Calculate(cart, shippingPreferences.RushShippingPrefID);
				}
				else
				{
					cart.RushShippingCost = 0;
				}

                // code Review 10/09/2013
                bool additionalShippingScenario = false;
                bool additionalShippingScenarioWithSingleSKUinCart = false; 
                bool overRideShippingCostforAdditionalShippingScenario =false;
                bool additionalShippingScenarioWithOtherItems = false;

                Dictionary<string, string> dicSKUandPrice = new Dictionary<string, string>();
                Dictionary<string, string> dicSKUShippingCostWithQtyRange = new Dictionary<string, string>();
                Dictionary<string, XmlNode> dicSKUShippingCostWithQtyRangeList = new Dictionary<string, XmlNode>();

                SitePreference sitePreference = CSFactory.GetCacheSitePref();
                if(!sitePreference.AttributeValuesLoaded)
                    sitePreference.LoadAttributeValues();

                if (sitePreference.AttributeValues.ContainsKey("additionalshippingscenario"))
                {
                    if (sitePreference.AttributeValues["additionalshippingscenario"].Value != null)
                    {
                        additionalShippingScenario = sitePreference.AttributeValues["additionalshippingscenario"].BooleanValue;
                    }
                }

                if (sitePreference.AttributeValues.ContainsKey("additionalshippingscenariowithsingleskuincart"))
                {
                    if (sitePreference.AttributeValues["additionalshippingscenariowithsingleskuincart"].Value != null)
                    {
                        additionalShippingScenarioWithSingleSKUinCart = sitePreference.AttributeValues["additionalshippingscenariowithsingleskuincart"].BooleanValue;
                    }
                }

                
                if (sitePreference.AttributeValues.ContainsKey("additionalshippingscenariowithotheritems"))
                {
                    if (sitePreference.AttributeValues["additionalshippingscenariowithotheritems"].Value != null)
                    {
                        additionalShippingScenarioWithOtherItems = sitePreference.AttributeValues["additionalshippingscenariowithotheritems"].BooleanValue;
                    }
                }
                                
                if (sitePreference.AttributeValues.ContainsKey("overrideshippingcostforadditionalshippingscenario"))
                {
                    if (sitePreference.AttributeValues["overrideshippingcostforadditionalshippingscenario"].Value != null)
                    {
                        overRideShippingCostforAdditionalShippingScenario = sitePreference.AttributeValues["overrideshippingcostforadditionalshippingscenario"].BooleanValue;
                    }
                }
                
                string additionalShippingScenarioSKUandPrice = "";                
                if (sitePreference.AttributeValues.ContainsKey("additionalshippingscenarioskuandprice"))
                {
                    if (sitePreference.AttributeValues["additionalshippingscenarioskuandprice"].Value != null)
                    {
                        additionalShippingScenarioSKUandPrice = sitePreference.AttributeValues["additionalshippingscenarioskuandprice"].Value.Trim();
                    }
                }

                if (additionalShippingScenario)
                {
                    try
                    {
                        string dicKey = "";
                        string shippingcostwithqtyrange = "no";                        
                        if (!additionalShippingScenarioSKUandPrice.Equals(""))
                        {
                            XmlDocument xml = new XmlDocument();
                            xml.LoadXml(additionalShippingScenarioSKUandPrice);
                            XmlNodeList resources = xml.SelectNodes("skulist/sku");                            
                            foreach (XmlNode node in resources)
                            {
                                dicKey = node.Attributes["skuid"].Value + "_" + node.Attributes["shippingprefid"].Value;
                                if (dicSKUandPrice.ContainsKey(dicKey) == false)
                                {
                                    dicSKUandPrice.Add(dicKey, node.Attributes["shippingcost"].Value); // node.Attributes["skuid"].Value
                                }

                                shippingcostwithqtyrange = "no";

                                if (node.Attributes["shippingcostwithqtyrange"] != null)
                                {                                
                                    if (node.Attributes["shippingcostwithqtyrange"].Value != null)
                                    {
                                        shippingcostwithqtyrange = node.Attributes["shippingcostwithqtyrange"].Value.ToLower();
                                        XmlNode nodeQtyRangeList = node.SelectSingleNode("qtyrangelist");
                                        if (nodeQtyRangeList != null)
                                        {
                                            if (dicSKUShippingCostWithQtyRangeList.ContainsKey(dicKey) == false)
                                            {
                                                dicSKUShippingCostWithQtyRangeList.Add(dicKey, nodeQtyRangeList);
                                            }
                                        }
                                    }
                                }

                                if (dicSKUShippingCostWithQtyRange.ContainsKey(dicKey) == false)
                                {
                                    dicSKUShippingCostWithQtyRange.Add(dicKey, shippingcostwithqtyrange);
                                }
                            }
                        }
                        else
                        {
                            dicSKUandPrice.Clear();
                            dicSKUShippingCostWithQtyRange.Clear();
                            dicSKUShippingCostWithQtyRangeList.Clear();
                        }

                        dicKey = "";
                        int lowerQty=0;
                        int upperQty=0;
                        string nodeName = "";
                        string nodeXml = "";
                        bool cartHasXMlItem = false;
                        foreach (Sku st in cart.CartItems)
                        {
                            string key = st.SkuId.ToString() + "_" + shippingPreferences.ShippingPrefID;
                            if (dicSKUandPrice.ContainsKey(key))
                            {
                                cartHasXMlItem = true;
                                break;
                            }
                        }

                        if (dicSKUandPrice.Count > 0 && cartHasXMlItem == true)
                        {
                            bool loopSingleSKUinCart = false;
                            if (additionalShippingScenarioWithSingleSKUinCart && cart.ItemCount == 1)
                            {
                                loopSingleSKUinCart = true;
                                decimal shippingCost = 0;
                                decimal skuShippingCost = 0;
                                bool isSKUPresent = false;
                                
                                foreach (Sku st in cart.CartItems)
                                {
                                    // if (st.SkuId == currentSetting.SkuId)                                    
                                    string key = st.SkuId.ToString() + "_" + shippingPreferences.ShippingPrefID;                            
                                    if (dicSKUandPrice.ContainsKey(key))
                                    {
                                        isSKUPresent = true;
                                        skuShippingCost = Convert.ToDecimal(dicSKUandPrice[key].ToString());
                                        
                                        if (dicSKUShippingCostWithQtyRange[key].ToString().ToLower().Equals("yes") && dicSKUShippingCostWithQtyRangeList.ContainsKey(key) == true && dicSKUShippingCostWithQtyRange.ContainsKey(key) == true)
                                        {
                                            nodeName = (dicSKUShippingCostWithQtyRangeList[key]).Name;
                                            nodeXml = "<" + nodeName + ">" + dicSKUShippingCostWithQtyRangeList[key].InnerXml.ToString() + "</" + nodeName + ">";
                                            XmlDocument doc = new XmlDocument();
                                            doc.LoadXml(nodeXml);
                                            XmlNodeList nodeList = doc.SelectNodes("/"+nodeName+"/range");
                                            foreach (XmlNode node in nodeList)
                                            {
                                                if (node.Attributes["lowerqty"] != null && node.Attributes["upperqty"] != null)
                                                {
                                                    if (node.Attributes["lowerqty"].Value != null && node.Attributes["upperqty"].Value != null)
                                                    {
                                                        lowerQty = Convert.ToInt32(node.Attributes["lowerqty"].Value);
                                                        upperQty = Convert.ToInt32(node.Attributes["upperqty"].Value);
                                                        if (st.Quantity >= lowerQty && st.Quantity <= upperQty)
                                                        {
                                                            if (node.Attributes["shippingcostrange"] != null)
                                                            {
                                                                if (node.Attributes["shippingcostrange"].Value != null)
                                                                {
                                                                    skuShippingCost = Convert.ToDecimal(node.Attributes["shippingcostrange"].Value);
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        
                                        st.LoadAttributeValues();
                                        if (st.AttributeValues.ContainsKey("shippingwithquantity"))
                                        {
                                            if (st.AttributeValues["shippingwithquantity"].Value == null)
                                            {
                                                shippingCost += skuShippingCost;
                                            }
                                            else if (st.AttributeValues["shippingwithquantity"].BooleanValue)
                                            {
                                                shippingCost += skuShippingCost * st.Quantity;
                                            }
                                            else
                                            {
                                                shippingCost += skuShippingCost;
                                            }
                                        }
                                        else
                                        {
                                            shippingCost += skuShippingCost;
                                        }
                                    }
                                }
                                if (isSKUPresent)
                                {
                                    if (overRideShippingCostforAdditionalShippingScenario)
                                    {
                                        cart.ShippingCost = shippingCost;
                                    }
                                    else
                                    {
                                        cart.ShippingCost += shippingCost;
                                    }
                                    cart.RushShippingCost = 0;
                                }
                            }

                            if (loopSingleSKUinCart == false)
                            {
                                if (additionalShippingScenarioWithOtherItems)
                                {
                                    decimal shippingCost = 0;
                                    decimal skuShippingCost = 0;
                                    bool isSKUPresent = false;
                                    foreach (Sku st in cart.CartItems)
                                    {
                                        // if (st.SkuId == currentSetting.SkuId)
                                        string key = st.SkuId.ToString() + "_" + shippingPreferences.ShippingPrefID;
                                        if (dicSKUandPrice.ContainsKey(key))
                                        {
                                            isSKUPresent = true;
                                            skuShippingCost = Convert.ToDecimal(dicSKUandPrice[key].ToString());

                                            if (dicSKUShippingCostWithQtyRange[key].ToString().ToLower().Equals("yes") && dicSKUShippingCostWithQtyRangeList.ContainsKey(key) == true && dicSKUShippingCostWithQtyRange.ContainsKey(key) == true)
                                            {
                                                nodeName = (dicSKUShippingCostWithQtyRangeList[key]).Name;
                                                nodeXml = "<" + nodeName + ">" + dicSKUShippingCostWithQtyRangeList[key].InnerXml.ToString() + "</" + nodeName + ">";
                                                XmlDocument doc = new XmlDocument();
                                                doc.LoadXml(nodeXml);
                                                XmlNodeList nodeList = doc.SelectNodes("/" + nodeName + "/range");
                                                foreach (XmlNode node in nodeList)
                                                {
                                                    if (node.Attributes["lowerqty"] != null && node.Attributes["upperqty"] != null)
                                                    {
                                                        if (node.Attributes["lowerqty"].Value != null && node.Attributes["upperqty"].Value != null)
                                                        {
                                                            lowerQty = Convert.ToInt32(node.Attributes["lowerqty"].Value);
                                                            upperQty = Convert.ToInt32(node.Attributes["upperqty"].Value);
                                                            if (st.Quantity >= lowerQty && st.Quantity <= upperQty)
                                                            {
                                                                if (node.Attributes["shippingcostrange"] != null)
                                                                {
                                                                    if (node.Attributes["shippingcostrange"].Value != null)
                                                                    {
                                                                        skuShippingCost = Convert.ToDecimal(node.Attributes["shippingcostrange"].Value);
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            
                                            st.LoadAttributeValues();
                                            if (st.AttributeValues.ContainsKey("shippingwithquantity"))
                                            {
                                                if (st.AttributeValues["shippingwithquantity"].BooleanValue)
                                                {
                                                    shippingCost += skuShippingCost * st.Quantity;
                                                }
                                                else
                                                {
                                                    shippingCost += skuShippingCost;
                                                }
                                            }
                                            else
                                            {
                                                shippingCost += skuShippingCost;
                                            }
                                        }
                                    }
                                    if (isSKUPresent)
                                    {
                                        if (overRideShippingCostforAdditionalShippingScenario)
                                        {
                                            cart.ShippingCost = shippingCost;
                                        }
                                        else
                                        {
                                            cart.ShippingCost += shippingCost;
                                        }
                                        cart.RushShippingCost = 0;
                                    }
                                }
                                else
                                {
                                    // Check here if cart has only items which in XML SKU List.
                                    bool cartHasOnlyXMLSku = false;
                                    foreach (Sku st in cart.CartItems)
                                    {
                                        string key = st.SkuId.ToString() + "_" + shippingPreferences.ShippingPrefID;
                                        if (dicSKUandPrice.ContainsKey(key))
                                        {
                                            cartHasOnlyXMLSku = true;
                                        }
                                        else
                                        {
                                            cartHasOnlyXMLSku = false;
                                            break;
                                        }
                                    }

                                    if (cartHasOnlyXMLSku)
                                    {
                                        decimal shippingCost = 0;
                                        decimal skuShippingCost = 0;
                                        bool isSKUPresent = false;
                                        foreach (Sku st in cart.CartItems)
                                        {
                                            // if (st.SkuId == currentSetting.SkuId)
                                            string key = st.SkuId.ToString() + "_" + shippingPreferences.ShippingPrefID;
                                            if (dicSKUandPrice.ContainsKey(key))
                                            {
                                                isSKUPresent = true;
                                                skuShippingCost = Convert.ToDecimal(dicSKUandPrice[key].ToString());

                                                if (dicSKUShippingCostWithQtyRange[key].ToString().ToLower().Equals("yes") && dicSKUShippingCostWithQtyRangeList.ContainsKey(key) == true && dicSKUShippingCostWithQtyRange.ContainsKey(key) == true)
                                                {
                                                    nodeName = (dicSKUShippingCostWithQtyRangeList[key]).Name;
                                                    nodeXml = "<" + nodeName + ">" + dicSKUShippingCostWithQtyRangeList[key].InnerXml.ToString() + "</" + nodeName + ">";
                                                    XmlDocument doc = new XmlDocument();
                                                    doc.LoadXml(nodeXml);
                                                    XmlNodeList nodeList = doc.SelectNodes("/" + nodeName + "/range");
                                                    foreach (XmlNode node in nodeList)
                                                    {
                                                        if (node.Attributes["lowerqty"] != null && node.Attributes["upperqty"] != null)
                                                        {
                                                            if (node.Attributes["lowerqty"].Value != null && node.Attributes["upperqty"].Value != null)
                                                            {
                                                                lowerQty = Convert.ToInt32(node.Attributes["lowerqty"].Value);
                                                                upperQty = Convert.ToInt32(node.Attributes["upperqty"].Value);
                                                                if (st.Quantity >= lowerQty && st.Quantity <= upperQty)
                                                                {
                                                                    if (node.Attributes["shippingcostrange"] != null)
                                                                    {
                                                                        if (node.Attributes["shippingcostrange"].Value != null)
                                                                        {
                                                                            skuShippingCost = Convert.ToDecimal(node.Attributes["shippingcostrange"].Value);
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                                st.LoadAttributeValues();
                                                if (st.AttributeValues.ContainsKey("shippingwithquantity"))
                                                {
                                                    if (st.AttributeValues["shippingwithquantity"].BooleanValue)
                                                    {
                                                        shippingCost += skuShippingCost * st.Quantity;
                                                    }
                                                    else
                                                    {
                                                        shippingCost += skuShippingCost;
                                                    }
                                                }
                                                else
                                                {
                                                    shippingCost += skuShippingCost;
                                                }
                                            }
                                        }
                                        if (isSKUPresent)
                                        {
                                            if (overRideShippingCostforAdditionalShippingScenario)
                                            {
                                                cart.ShippingCost = shippingCost;
                                            }
                                            else
                                            {
                                                cart.ShippingCost += shippingCost;
                                            }
                                            cart.RushShippingCost = 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        CSCore.CSLogger.Instance.LogException("Additional Shipping Scenario Calculation Issue ", ex);
                        throw;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Missing shipping preferences");
            }
        }

        public static List<ShippingCharge> GetAllShippingCharges()
        {
            return ShippingDAL.GetAllShippingCharges();
        }

        public static List<ShippingCharge> GetShippingChargesByPref(int prefId)
        {
            return ShippingDAL.GetShippingChargesByPref(prefId);
        }

        public static void RemoveShippingCharge(int shippingChargeId)
        {
            ShippingDAL.RemoveShippingCharge(shippingChargeId);
        }
    }
}
