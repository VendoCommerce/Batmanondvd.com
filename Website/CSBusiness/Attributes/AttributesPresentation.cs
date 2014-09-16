using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using AjaxControlToolkit;
using System.Web.UI.WebControls;
using System.Web;

namespace CSBusiness.Attributes
{
    public class AttributesPresentation
    {
        public static string GetPageFieldId(string attributeName)
        {
            return string.Concat("attr_", Attribute.CaseFixAttributeName(attributeName));
        }

        public static bool MatchesAttributeId(string id)
        {
            return !string.IsNullOrEmpty(id) && id.ToLower().StartsWith("attr_");
        }

        public static void Populate(object control, AttributeValue attributeValue)
        {
            Type controlType = control.GetType();

            if (controlType.Equals(typeof(AjaxControlToolkit.HTMLEditor.Editor)))
            {
                ((AjaxControlToolkit.HTMLEditor.Editor)control).Content = attributeValue.Value;
            }
            else if (controlType.Equals(typeof(TextBox)))
            {
                ((TextBox)control).Text = attributeValue.Value;
            }
        }

        public static AttributeValue GetAttributeValue(object control)
        {
            Type controlType = control.GetType();

            if (controlType.Equals(typeof(AjaxControlToolkit.HTMLEditor.Editor)))
            {
                return new AttributeValue(((AjaxControlToolkit.HTMLEditor.Editor)control).Content);
            }
            else if (controlType.Equals(typeof(TextBox)))
            {
                return new AttributeValue(((TextBox)control).Text);
            }

            return null;
        }

        public static AttributePageControlInfo GetAttributePageControlInfo(object control)
        {
            Type controlType = control.GetType();

            if (controlType.Equals(typeof(AjaxControlToolkit.HTMLEditor.Editor)))
            {
                return new AttributePageControlInfo()
                {
                    UniqueId = string.Concat("_content_", ((AjaxControlToolkit.HTMLEditor.Editor)control).ClientID),
                    RequestFindMethod = RequestFindMethod.BeginsWith
                };
            }
            else 
            {                
                return new AttributePageControlInfo()
                {
                    UniqueId = ((WebControl)control).UniqueID,
                    RequestFindMethod = RequestFindMethod.ExactMatch
                };
            }
        }

        public static void SetAttributePageInfo(IStateManager stateManager, string key, string attributeName, AttributePageControlInfo attributePageControlInfo)
        {
            Dictionary<string, AttributePageControlInfo> list = ((StateBag)stateManager)[key] as Dictionary<string, AttributePageControlInfo>;

            if (list == null)
                list = new Dictionary<string, AttributePageControlInfo>();

            if (list.ContainsKey(attributeName))
                list[attributeName] = attributePageControlInfo;
            else
                list.Add(attributeName, attributePageControlInfo);

            ((StateBag)stateManager)[key] = list;
        }

        public static IDictionary<string, AttributeValue> GetAttributeValues(IStateManager stateManager, string key, HttpContext httpContext)
        {
            Dictionary<string, AttributePageControlInfo> list = ((StateBag)stateManager)[key] as Dictionary<string, AttributePageControlInfo>;
            Dictionary<string, AttributeValue> attributeValues = new Dictionary<string,AttributeValue>();

            if (list != null)
            {
                foreach (string attributeName in list.Keys)
                {
                    attributeValues.Add(attributeName, GetAttributeValue(list[attributeName], httpContext));
                }
            }

            return attributeValues;
        }

        private static AttributeValue GetAttributeValue(AttributePageControlInfo attributePageControlInfo, HttpContext httpContext)
        {
            switch (attributePageControlInfo.RequestFindMethod)
            {
                case RequestFindMethod.ExactMatch:
                    return new AttributeValue(httpContext.Request.Form[attributePageControlInfo.UniqueId]);
                    //break;
                case RequestFindMethod.BeginsWith:
                    foreach (string key in httpContext.Request.Form.AllKeys)
                        if (key.StartsWith(attributePageControlInfo.UniqueId))
                            return new AttributeValue(
                                HttpUtility.HtmlDecode(httpContext.Request.Form[key]) // Requesting directly gives us value in html encoded form, so we want to decode to preserve html styling.
                                );
                    break;
            }

            return null;
        }
    }
}
