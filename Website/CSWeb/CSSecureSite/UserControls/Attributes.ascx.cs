using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using CSData;
using CSBusiness.Attributes;
using System.Data;
using AjaxControlToolkit.HTMLEditor;

namespace CSWeb.Admin.UserControls
{
    public partial class Attributes : System.Web.UI.UserControl
    {        
        public int WidthTotal
        {
            get
            {
                return Convert.ToInt32(ViewState["WidthTotal"] ?? "500");
            }
            set
            {
                ViewState["WidthTotal"] = value;
            }
        }

        public string ObjectName
        {
            get
            {
                return Convert.ToString(ViewState["ObjectName"] ?? string.Empty);
            }
            set
            {
                ViewState["ObjectName"] = value;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void NewItem(ObjectAttribute objectAttribute)
        {
            Populate(objectAttribute, true);
        }

        public void Populate(ObjectAttribute objectAttribute)
        {
            Populate(objectAttribute, false);
        }

        private void Populate(ObjectAttribute objectAttribute, bool newItem)
        {
            List<Dictionary<string, string>> items = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();

            if (!newItem)
            {
                // get all attributes associated to item
                using (SqlDataReader reader = AttributesDAL.GetAllAttributeValuesExtended(objectAttribute.ObjectName, objectAttribute.ItemId))
                {
                    while (reader.Read())
                    {
                        item = new Dictionary<string, string>();
                        item.Add("_Associated", true.ToString()); // indicates if attribute value is associated to item

                        item.Add("AttributeName", Convert.ToString(reader["AttributeName"]));
                        item.Add("Value", Convert.ToString(reader["Value"] ?? string.Empty));
                        item.Add("SqlDbType", Convert.ToString(reader["SqlDbType"]));
                        item.Add("ObjectAttributeTypeName", Convert.ToString(reader["ObjectAttributeTypeName"]));
                        items.Add(item);
                    }
                }
            }

            // get all attributes associated to object and add to list if not already in it
            using (SqlDataReader reader = AttributesDAL.GetObjectAttributes(objectAttribute.ObjectName))
            {
                while (reader.Read())
                {
                    if ((!newItem && items.FirstOrDefault(x =>
                    {
                        return x["AttributeName"] == Convert.ToString(reader["AttributeName"]);
                    }) == null)
                        ||
                        (newItem)) // newItem = "true" will add all items
                    {
                        item = new Dictionary<string, string>();
                        item.Add("_Associated", false.ToString()); // indicates if attribute value is associated to item
                        item.Add("AttributeName", Convert.ToString(reader["AttributeName"]));
                        item.Add("SqlDbType", Convert.ToString(reader["SqlDbType"]));
                        item.Add("ObjectAttributeTypeName", Convert.ToString(reader["ObjectAttributeTypeName"]));
                        items.Add(item);
                    }
                }
            }

            rptAttributes.DataSource = items;
            rptAttributes.DataBind();
        }

        protected void rptAttributes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Dictionary<string, string> item = e.Item.DataItem as Dictionary<string, string>;

                Label lblAttributeName = e.Item.FindControl("lblAttributeName") as Label;
                HiddenField hidSqlDbType = e.Item.FindControl("hidSqlDbType") as HiddenField;                
                TextBox txtAttributeValue = e.Item.FindControl("txtAttributeValue") as TextBox;
                DropDownList ddlYesNo = e.Item.FindControl("ddlYesNo") as DropDownList;
                CheckBox chkDelete = e.Item.FindControl("chkDelete") as CheckBox;
                CheckBox chkAdd = e.Item.FindControl("chkAdd") as CheckBox;

                bool associated = bool.Parse(item["_Associated"]);

                lblAttributeName.Text = item["AttributeName"];
                hidSqlDbType.Value = item["SqlDbType"];

                SqlDbType type = (SqlDbType)Enum.Parse(typeof(SqlDbType), item["SqlDbType"]);

                if (type == SqlDbType.Bit)
                {
                    ddlYesNo.Visible = true;

                    if (associated)
                    {
                        if (!string.IsNullOrEmpty(item["Value"]))
                        {
                            ddlYesNo.SelectedItem.Selected = false; // deselect currently selected one

                            bool value = AttributeValue.GetBooleanFromValue(item["Value"]);
                            ddlYesNo.Items.FindByValue(value.ToString().ToLower()).Selected = true;
                        }
                    }
                    else
                    {
                        ddlYesNo.Attributes["onchange"] = "return attrFieldChanged('" + chkAdd.ClientID + "');";
                    }
                }
                else // text
                {
                    txtAttributeValue.Visible = true;

                    if (item["ObjectAttributeTypeName"] == "rich-control")
                    {
                        txtAttributeValue.TextMode = TextBoxMode.MultiLine;
                    }

                    if (associated)
                    {
                        txtAttributeValue.Text = item["Value"];
                    }
                    else
                    {
                        txtAttributeValue.Attributes["onchange"] = 
                            txtAttributeValue.Attributes["onkeydown"] = "return attrFieldChanged('" + chkAdd.ClientID + "');";
                    }
                }

                if (associated)
                {
                    chkDelete.Visible = true;
                    chkDelete.Attributes["onchange"] = 
                        chkDelete.Attributes["onkeydown"] = "return deleteCheckChanged('" + chkDelete.ClientID + "', '" + txtAttributeValue.ClientID + "', '" + ddlYesNo.ClientID + "');";
                }
                else
                {
                    chkAdd.Visible = true;
                }
            }
        }

        public Dictionary<string, AttributeValue> GetEnteredAttributeValues(out List<string> deleteAttributes)
        {
            Dictionary<string, AttributeValue> attributeValues = new Dictionary<string, AttributeValue>();
            deleteAttributes = new List<string>();

            foreach (RepeaterItem item in rptAttributes.Items)
            {
                Label lblAttributeName = item.FindControl("lblAttributeName") as Label;
                CheckBox chkDelete = item.FindControl("chkDelete") as CheckBox;
                CheckBox chkAdd = item.FindControl("chkAdd") as CheckBox;

                if (!chkDelete.Checked
                    &&
                    ((chkAdd.Visible && chkAdd.Checked) || (!chkAdd.Visible))) // add checkbox is either invisible OR visible and checked... in other words, it needs to be checked if visible.
                {                    
                    HiddenField hidSqlDbType = item.FindControl("hidSqlDbType") as HiddenField;
                    TextBox txtAttributeValue = item.FindControl("txtAttributeValue") as TextBox;
                    DropDownList ddlYesNo = item.FindControl("ddlYesNo") as DropDownList;

                    SqlDbType type = (SqlDbType)Enum.Parse(typeof(SqlDbType), hidSqlDbType.Value);

                    if (type == SqlDbType.Bit)
                    {
                        attributeValues.Add(lblAttributeName.Text, new AttributeValue(AttributeValue.GetBooleanString(ddlYesNo.SelectedItem.Value)));
                    }
                    else
                    {
                        attributeValues.Add(lblAttributeName.Text, new AttributeValue(txtAttributeValue.Text));
                    }
                }
                else
                {
                    if (chkDelete.Checked)
                        deleteAttributes.Add(lblAttributeName.Text);
                    else
                    {
                        // we should never be here.
                    }
                }
            }

            return attributeValues;
        }

        public void SaveAllEnteredAttributeValues(string objectName, int itemId)
        {
            List<string> deleteAttributes;

            Dictionary<string, AttributeValue> attributeValues = GetEnteredAttributeValues(out deleteAttributes);

            ObjectAttribute.SaveAttributeValues(objectName, itemId, attributeValues);

            ObjectAttribute.DeleteAttributes(objectName, itemId, deleteAttributes);
        }

        public void SaveAllEnteredAttributeValues(ObjectAttribute objectAttribute)
        {
            SaveAllEnteredAttributeValues(objectAttribute.ObjectName, objectAttribute.ItemId);
        }

        public void SaveDeletedAttributes(string objectName, int itemId)
        {
            List<string> deleteAttributes;

            Dictionary<string, AttributeValue> attributeValues = GetEnteredAttributeValues(out deleteAttributes);

            ObjectAttribute.DeleteAttributes(objectName, itemId, deleteAttributes);
        }

        public void SaveDeletedAttributes(ObjectAttribute objectAttribute)
        {
            SaveDeletedAttributes(objectAttribute.ObjectName, objectAttribute.ItemId);
        }
    }
}