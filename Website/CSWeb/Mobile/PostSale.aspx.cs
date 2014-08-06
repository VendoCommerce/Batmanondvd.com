using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using CSBusiness.PostSale;
using CSBusiness;
using System.Text.RegularExpressions;
using CSCore.Utils;
using CSBusiness.OrderManagement;
using CSBusiness.Resolver;
using CSBusiness.ShoppingManagement;
using System.Xml.XPath;
using System.Text;

namespace CSWeb.Mobile.Store
{
    public partial class PostSale : ShoppingCartBasePage
    {
        private List<int> AllTemplates
        {
            get { return ViewState["AllTemplates"] as List<int>; }
            set { ViewState["AllTemplates"] = value; }
        }

        private List<string> Skus
        {
            get { return ViewState["Skus"] as List<string>; }
            set { ViewState["Skus"] = value; }
        }

        private int CurrentTemplateIndex
        {
            get { return Convert.ToInt32(Session["CurrentTemplateIndex"]); }
            set { Session["CurrentTemplateIndex"] = value; }
        }

        private ClientCartContext CartContext
        {
            get
            {
                return Session["ClientOrderData"] as ClientCartContext;
            }
        }

        private List<string> StaticContainers
        {
            get
            {
                List<string> staticContainers = ViewState["StaticContainers"] as List<string>;
                if (staticContainers == null)
                {
                    ViewState["StaticContainers"] = staticContainers = new List<string>();
                }
                return staticContainers;

            }
            set
            {
                ViewState["StaticContainers"] = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (OrderHelper.IsCustomerOrderFlowCompleted(CartContext.OrderId))
            {
                Response.Redirect("receipt.aspx");
            }

            if (!IsPostBack)
            {
                AllTemplates = GetTemplates();
                CurrentTemplateIndex = -1;
                GoToNextTemplate();
            }
        }

        private List<int> GetTemplates()
        {
            PathManager pathManager = new PathManager();
            Path upsalePath = null;
            int pathID = 0;
            if (Request["p"] != null)
            {
                //We can also handle cookie check here.
                pathID = Convert.ToInt32(Request["p"]);
                upsalePath = pathManager.GetUpSalePath(pathID, false);
            }
            else
            {
                upsalePath = pathManager.GetPath(HttpContext.Current, CartContext.VersionId);
            }

            //Set Cookie fist time after path calculation
            if (HttpContext.Current.Request.Cookies["CS-PathId"] == null && upsalePath != null)
            {
                TimeSpan ts = new TimeSpan(24, 0, 0);
                CommonHelper.SetCookie("CS-PathId", upsalePath.PathId.ToString(), ts);

            }

            if (upsalePath == null)
            {
                return new List<int>();
            }
            else
            {
                //Store Order Path to database
                if (upsalePath.PathId > 0)
                {
                    CSResolve.Resolve<IOrderService>().UpdateOrderPath(CartContext.OrderId, upsalePath.PathId);
                }

                return upsalePath.Templates
                    .OrderBy(t => t.OrderNo)
                    .Select(t => t.TemplateId)
                    .ToList();
            }
        }

        private void GoToNextTemplate()
        {
            CurrentTemplateIndex++;
            if (CurrentTemplateIndex < AllTemplates.Count)
            {
                LoadTemplate(CurrentTemplateIndex);
            }
            else
            {
                //SriComments: Admin may setup path with empty templates: kevin business case
                //If Authorization is needed, use Server.Transfer("AuthorizeOrder.aspx"); instead of Response.Redirect("CheckoutThankYou.aspx");
                if (CSFactory.OrderProcessCheck() == (int)OrderProcessTypeEnum.InstantOrderProcess)
                    Server.Transfer("AuthorizeOrder.aspx");//Response.Redirect("CheckoutThankYou.aspx");
                else
                    Response.Redirect("ReviewOrder.aspx");

            }
        }

        private void LoadTemplate(int templateIndex)
        {
            if (templateIndex < AllTemplates.Count)
            {
                //We're making a separate call to database just to get 
                //full template object, ideally we should merge these two calls
                int currentTemplateId = AllTemplates[templateIndex];
                PathManager pathManager = new PathManager();
                Template currentTemplate = pathManager.GetTemplate(currentTemplateId);

                if (currentTemplate.UriLabel != null)
                {
                    Session["PostSaleLabelName"] = currentTemplate.UriLabel;
                }
                else
                {
                    Session["PostSaleLabelName"] = "";
                }
                if (currentTemplate.CanUseTemplate(CartContext))
                {
                    //decimal orderTotal = 0;
                    //decimal neworderTotal = 0;
                    //decimal shippingandhandling = 0;
                    //Order orderItem = new OrderManager().GetBatchProcessOrders(CartContext.OrderId);
                    //shippingandhandling = orderItem.ShippingCost;
                    //foreach (Sku s in orderItem.SkuItems)
                    //{
                    //    orderTotal += s.InitialPrice;
                    //    s.LoadAttributeValues();
                    //    try
                    //    {
                    //        if (s.AttributeValues["relatedonepaysku"] != null && !s.AttributeValues["relatedonepaysku"].Value.Equals(""))
                    //        {
                    //            int skuId = Convert.ToInt32(s.AttributeValues["relatedonepaysku"].Value);
                    //            Sku st = new SkuManager().GetSkuByID(skuId);
                    //            //st.SkuId = skuId;

                    //            st.LoadAttributeValues();
                    //            neworderTotal += st.InitialPrice;
                    //        }
                    //    }
                    //    catch
                    //    {


                    //    }
                    //}
                    decimal orderTotal = 0;
                    decimal neworderTotal = 0;
                    decimal shippingandhandling = 0;
                    string templateBody = currentTemplate.Body;

                    if (templateBody.Contains("<input name=\"onepay\" value=\"onepay\" id=\"onepay\" type=\"hidden\" />"))
                    {

                        Order orderItem = new OrderManager().GetBatchProcessOrders(CartContext.OrderId);
                        shippingandhandling = orderItem.ShippingCost;
                        foreach (Sku s in orderItem.SkuItems)
                        {
                            orderTotal += s.InitialPrice;
                            s.LoadAttributeValues();
                            try
                            {
                                if (s.AttributeValues["relatedonepaysku"] != null && !s.AttributeValues["relatedonepaysku"].Value.Equals(""))
                                {
                                    int skuId = Convert.ToInt32(s.AttributeValues["relatedonepaysku"].Value);
                                    Sku st = new SkuManager().GetSkuByID(skuId);
                                    //st.SkuId = skuId;

                                    st.LoadAttributeValues();
                                    neworderTotal += st.InitialPrice;
                                }
                            }
                            catch
                            {


                            }
                        }
                    }
                    StringBuilder sb = new StringBuilder();
                    sb.Append(templateBody);

                    //templateBody.Replace('{upsellTotal}', Math.Round(neworderTotal, 2).ToString());
                    sb.Replace("{upsellTotal}", Math.Round(neworderTotal, 2).ToString());
                    sb.Replace("{upsellshipping}", Math.Round(shippingandhandling, 2).ToString());
                    sb.Replace("{upsellsave}", Math.Round(shippingandhandling + 14.95m, 2).ToString());
                    templateBody = sb.ToString();
                    templateBody = BindLinks(templateBody);
                    templateBody = BindValidators(templateBody);
                    BindContainers(templateBody);

                    string script = string.Format("<script type=\"text/javascript\">\r\n{0}\r\n</script>", currentTemplate.Script);

                    //Tags contain some template related configuration information
                    var templateTagsXml = XElement.Parse("<root>" + currentTemplate.Tag + "</root>");

                    templateBody = InsertData(templateTagsXml, templateBody);

                    // write template html to page
                    mainContainer.InnerHtml = string.Format("{0}\r\n{1}", script, templateBody);
                }
                else
                {
                    GoToNextTemplate();
                }
            }
        }

        private string BindValidators(string templateBody)
        {
            //Commented as everything will be handled in the JS
            /*
            MatchCollection links = Regex.Matches(templateBody, "(<select.*?</select>|<input.*?>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            int totalMatches = links.Count;
            for (int i = 0; i < totalMatches; i++)
            {
                XElement controlXml = XElement.Parse(links[i].Value);
                if (controlXml.Attribute("required") != null && controlXml.Attribute("required").Value == "true")
                {
                    string classAttribute = "validate[required]";
                    if (controlXml.Attribute("class") != null)
                    {
                        classAttribute += " " + controlXml.Attribute("class").Value;
                    }

                    controlXml.SetAttributeValue("class", classAttribute);
                    string controlID = string.Empty;
                    if (controlXml.Attribute("id") != null)
                    {
                        controlID = controlXml.Attribute("id").Value;
                    }
                    else
                    {
                        controlID = Guid.NewGuid().ToString().Substring(0, 6);
                    }
                    controlXml.SetAttributeValue("id", controlID);
                    templateBody = templateBody.Replace(links[i].Value, controlXml.ToString());
                }
            }
             */
            return templateBody;
        }

        private string BindLinks(string templateBody)
        {
            //"<sku id='5' />Do you want to take advantage of this offer <a href='javascript:void(0)' bind='yes'>Yes</a> no <a href='javascript:void(0)' bind='no'>No</a>";
            MatchCollection links = Regex.Matches(templateBody, "<a.*?</a>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            int totalMatches = links.Count;
            for (int i = 0; i < totalMatches; i++)
            {
                XElement linkXml = XElement.Parse(links[i].Value);
                if (linkXml.Attribute("bind") != null)
                {
                    string attributeName = linkXml.Attribute("bind").Value;
                    Button btn = Page.FindControl("btn" + attributeName) as Button;
                    if (btn != null)
                    {
                        string clientScript = ClientScript.GetPostBackEventReference(btn, "", btn.CausesValidation);

                        if (attributeName == "yes")
                            clientScript = "if(validateForm()) " + clientScript;

                        if (linkXml.Attribute("onclick") != null)
                            clientScript = string.Concat(linkXml.Attribute("onclick").Value, clientScript);

                        linkXml.SetAttributeValue("onclick", clientScript + ";return false;");
                        templateBody = templateBody.Replace(links[i].Value, linkXml.ToString());
                    }
                }
            }
            return templateBody;
        }

        //this will be used on 
        private void BindContainers(string templateBody)
        {
            StaticContainers.ForEach(c =>
            {
                Page.FindControl(c).Visible = false;
            });
            StaticContainers.Clear();

            MatchCollection containers = Regex.Matches(templateBody, "<container.*?/>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            int totalMatches = containers.Count;
            for (int i = 0; i < totalMatches; i++)
            {
                XElement containerXml = XElement.Parse(containers[i].Value);
                string containerID = containerXml.Attribute("id").Value;
                Control control = Page.FindControl(containerID);
                if (control != null)
                {
                    control.Visible = true;
                    StaticContainers.Add(containerID);
                }
            }
        }

        private string InsertData(XElement templateTagsXml, string templateBody)
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

        private bool MatchesCondition(XElement conditions, string condKey)
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
                    isMatch = isMatch && Regex.IsMatch(Request.Form[fieldName], value);
                }
                else
                {
                    isMatch = isMatch && Request.Form[fieldName] == value;
                }

                if (!isMatch)
                    break;
            }

            return isMatch;
        }

        private void GetTemplateSelections(ref Dictionary<int, int> skusAndQuantities)
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

            XElement templateTags = XElement.Parse(((Template)(new PathManager().GetTemplate(AllTemplates[CurrentTemplateIndex]))).Tag);

            // search "sku and select fields" information
            foreach (var r in templateTags.XPathSelectElements("//selectionparameters/fixedskuentryfields"))
            {
                if (r.Attribute("type") != null && r.Attribute("type").Value.Equals("onepay"))
                {
                    Order orderItem = new OrderManager().GetBatchProcessOrders(CartContext.OrderId);
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

                    foreach (Sku s in CartContext.CartInfo.CartItems)
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

                                    if (int.TryParse(Request.Form[fieldName], out quantity))
                                        skusAndQuantities.Add(skuId, quantity);
                                }
                                else if (quantField.Attribute("skuid") != null)
                                {
                                    Sku matchSku = CartContext.CartInfo.CartItems.FirstOrDefault(x => { return x.SkuId == int.Parse(quantField.Attribute("skuid").Value); });

                                    if (matchSku != null)
                                    {
                                        skusAndQuantities.Add(skuId, matchSku.Quantity);
                                    }
                                    else
                                    {
                                        skusAndQuantities.Add(skuId, Convert.ToInt32((quantField.Attribute("defaultvalue") ?? new XAttribute("0", "0")).Value));
                                    }
                                }
                                else
                                    skusAndQuantities.Add(skuId, Convert.ToInt32((quantField.Attribute("defaultvalue") ?? new XAttribute("0", "0")).Value));
                            }
                        }
                    }
                }

            }
        }

        protected void btnYes_OnClick(object sender, EventArgs e)
        {
            //In case if use clicks back button and clicks yes again we need to redirect to a special page with some session expired text
            if (CurrentTemplateIndex < AllTemplates.Count)
            {
                Dictionary<int, int> skuAndQuantity = new Dictionary<int, int>();

                GetTemplateSelections(ref skuAndQuantity);

                PathManager pathManager = new PathManager();
                Template currentTemplate = pathManager.GetTemplate(AllTemplates[CurrentTemplateIndex]);

                currentTemplate.Process(CartContext.OrderId, CartContext.CartInfo, skuAndQuantity,
                    (OrderProcessTypeEnum)CSFactory.OrderProcessCheck());

                GoToNextTemplate();
            }
            else
            {
                Response.Redirect("CheckoutExpired.aspx");
            }
        }

        protected void btnNo_OnClick(object sender, EventArgs e)
        {
            GoToNextTemplate();
        }
    }
}