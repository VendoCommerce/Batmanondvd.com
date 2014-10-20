using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using CSData;
using CSBusiness.Attributes;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Text;

namespace CSWeb.Admin
{
    public partial class Attributes : BasePage
    {
        public Dictionary<string, List<string>> objectAttributeAssociations = null;
        public DataSet objectAttributeDetails = null;

        public Dictionary<int, string> Objects
        {
            get
            {
                return (Dictionary<int, string>)(ViewState["Objects"] ?? new Dictionary<int, string>());
            }
            set
            {
                ViewState["Objects"] = value;
            }
        }

        public List<KeyValuePair<string, string>> ValueTypes
        {
            get
            {
                List<KeyValuePair<string, string>> items = ViewState["ValueTypes"] as List<KeyValuePair<string, string>>;

                if (items == null)
                {
                    ViewState["ValueTypes"] = items = GetValueTypes();
                }

                return items;
            }
        }

        public List<KeyValuePair<string, string>> ObjectAttributeTypes
        {
            get
            {
                List<KeyValuePair<string, string>> items = ViewState["ObjectAttributeTypes"] as List<KeyValuePair<string, string>>;

                if (items == null)
                {
                    ViewState["ObjectAttributeTypes"] = items = GetObjectAttributeTypes();
                }

                return items;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BaseLoad();
                DataTable objects = ObjectAttribute.GetObjects();
                PopulateObjects(objects);
                LoadObjects(objects);
                PopulateAttributes();
            }
        }

        private void LoadObjects(DataTable objects)
        {
            Dictionary<int, string> objectsInfo = new Dictionary<int, string>();

            foreach (DataRow row in objects.Rows)
                objectsInfo.Add(Convert.ToInt32(row["ObjectID"]), Convert.ToString(row["Name"]));

            Objects = objectsInfo;
        }

        private void PopulateObjects(DataTable objects)
        {            
            dgObjects.DataSource = objects;
            dgObjects.DataBind();
        }

        private void PopulateAttributes()
        {
            dlAttributes.DataKeyField = "AttributeId";
            dlAttributes.DataSource = ObjectAttribute.GetAllAttributes();
            dlAttributes.DataBind();
        }

        protected void dlItem_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            int attributeId = e.Item.ItemIndex > -1 ? (int)dlAttributes.DataKeys[e.Item.ItemIndex] : -1;
            TextBox txtAttributeName = e.Item.FindControl("txtAttributeName") as TextBox;
            TextBox txtDescription = e.Item.FindControl("txtDescription") as TextBox;
            DropDownList ddlAttributeValueType = e.Item.FindControl("ddlAttributeValueType") as DropDownList;
            Literal litAttributeName = e.Item.FindControl("litAttributeName") as Literal;
            bool success = false;
            ShowMessage(string.Empty);

            switch (e.CommandName)
            {
                case "Add":
                    dlAttributes.EditItemIndex = -1;                    
                    HtmlTableRow addNewContainer = e.Item.FindControl("addNewContainer") as HtmlTableRow;
                    addNewContainer.Visible = true;
                    txtAttributeName.Text = "";
                    txtDescription.Text = "";
                    ddlAttributeValueType.SelectedIndex = 0;
                    ShowError(string.Empty);
                    break;
                case "Insert":
                    if (!ValidateAttributeEntry(e))
                    {
                        txtAttributeName.Focus();
                        return;
                    }

                    ShowError(string.Empty);

                    ObjectAttribute.SaveAttribute(txtAttributeName.Text.Trim(), txtDescription.Text.Trim());

                    ShowMessage(string.Format("Attribubute '{0}' successfully created.", txtAttributeName.Text.Trim()));

                    dlAttributes.EditItemIndex = -1;
                    PopulateAttributes();
                    break;
                case "Edit":
                    dlAttributes.EditItemIndex = e.Item.ItemIndex;                    
                    PopulateAttributes();
                    break;
                case "Update":                    
                    if (!ValidateAttributeEntry(e))
                    {
                        txtAttributeName.Focus();
                        return;
                    }
                    
                    ShowError(string.Empty);
              
                    // save attribute
                    ObjectAttribute.SaveAttributeById(attributeId, txtAttributeName.Text.Trim(), txtDescription.Text.Trim(), ddlAttributeValueType.SelectedItem.Text);

                    ShowMessage(string.Format("Attribubute '{0}' successfully updated.", txtAttributeName.Text.Trim()));

                    dlAttributes.EditItemIndex = -1;
                    PopulateAttributes();
                    break;
                case "Delete":

                    success = true;

                    try                    
                    {
                        ObjectAttribute.DeleteAttribute(litAttributeName.Text);
                    }
                    catch (Exception ex)
                    {
                        success = false;
                        ShowError(string.Format("Errors encountered while deleting attribute '{0}' (possibly due associated values): <br/>" + ex.Message, litAttributeName.Text));
                    }

                    if (success)
                    {
                        ShowMessage(string.Format("Attribute '{0}' successfully deleted.", litAttributeName.Text));
                    }

                    PopulateAttributes();

                    break;
                case "Cancel":                    
                    dlAttributes.EditItemIndex = -1;
                    ShowError(string.Empty);
                    PopulateAttributes();
                    mpePopup.Hide();
                    break;
            }
        }

        private bool ValidateAttributeEntry(DataListCommandEventArgs e)
        {
            TextBox txtAttributeName = e.Item.FindControl("txtAttributeName") as TextBox;
            //TextBox txtDescription = e.Item.FindControl("txtDescription") as TextBox;
            //DropDownList ddlAttributeValueType = e.Item.FindControl("ddlAttributeValueType") as DropDownList;

            string attributeName = txtAttributeName.Text.Trim();
            //string attributeDesc = txtDescription.Text.Trim();

            // check uniqueness
            for (int i = 0; i < dlAttributes.Items.Count; i++)
            {
                if (i != e.Item.ItemIndex)
                {
                    if (((Literal)dlAttributes.Items[i].FindControl("litAttributeName")).Text.Trim().ToLower() == attributeName.ToLower())
                    {
                        ShowError("Attribute name already exists.");
                        return false;
                    }
                }
            }

            // check characters 
            if (!ObjectAttribute.IsValidAttributeName(attributeName))
            {
                ShowError("Attribute name is invalid.");
                return false;
            }

            return true;
        }


        private bool ValidateAttributeEntry1()
        {
           

            string attributeName = txtAttributeName1.Text.Trim();
           // string attributeDesc = txtDescription1.Text.Trim();

            //// check uniqueness
            //for (int i = 0; i < dlAttributes.Items.Count; i++)
            //{
                foreach ( DataListItem li in dlAttributes.Items )
                    {

                        if (((Literal)li.FindControl("litAttributeName")).Text.Trim().ToLower() == attributeName.ToLower())
                    {
                        ShowError("Attribute name already exists.");
                        return false;
                    }
               
                    }
                
            

            // check characters 
            if (!ObjectAttribute.IsValidAttributeName(attributeName))
            {
                ShowError("Attribute name is invalid.");
                return false;
            }

            return true;
        }

        protected void dlItem_DataBinding(object sender, EventArgs e)
        {
            
        }

        protected void dlItem_ItemDataBound(object sender, DataListItemEventArgs  e)
        {
            string attributeName = null;

            if (objectAttributeAssociations == null)
            {
                objectAttributeAssociations = GetObjectAttributeAssociations(Objects);
            }
            
            Literal litAttributeName = e.Item.FindControl("litAttributeName") as Literal;        
            Literal litAssociations = e.Item.FindControl("litAssociations") as Literal;
            TextBox txtAttributeName = e.Item.FindControl("txtAttributeName") as TextBox;
            
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                attributeName = litAttributeName.Text;
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                attributeName = txtAttributeName.Text;
            }

            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {                
                DropDownList ddlAttributeValueType = e.Item.FindControl("ddlAttributeValueType") as DropDownList;
                //CheckBoxList cblObjectAssociations = e.Item.FindControl("cblObjectAssociations") as CheckBoxList;

                litFocusCtrlId.Text = dlAttributes.EditItemIndex != -1
                    ? txtAttributeName.ClientID + "_" + dlAttributes.EditItemIndex.ToString() // this works in getting the actual client Id
                    : txtAttributeName.ClientID;
                
                // populate and set type drop down
                ddlAttributeValueType.DataSource = ValueTypes;
                ddlAttributeValueType.DataTextField = "Key";
                ddlAttributeValueType.DataValueField = "Value";
                ddlAttributeValueType.DataBind();
                ddlAttributeValueType.SelectedIndex = ddlAttributeValueType.Items.IndexOf(ddlAttributeValueType.Items.FindByValue(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ValueTypeId"))));
            }
            
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.EditItem)
            {
                List<string> assocications = new List<string>();

                foreach (KeyValuePair<string, List<string>> item in objectAttributeAssociations.Where(x => { return x.Value.Contains(attributeName); }))
                {
                    assocications.Add(item.Key);
                }

                litAssociations.Text = string.Join(", ", assocications.ToArray());
            }
        }

        public void ShowError(string errorMsg)
        {
            lblErrorMessage.Text = errorMsg;
        }

        public void ShowMessage(string message)
        {
            lblMessage.Text = message;
        }

        public List<KeyValuePair<string, string>> GetValueTypes()
        {
            List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
            using (SqlDataReader reader = ObjectAttribute.GetAllValueTypes())
            {
                while (reader.Read())
                {
                    items.Add(new KeyValuePair<string, string>(Convert.ToString(reader["Name"]), Convert.ToString(reader["ValueTypeID"])));
                }
            }

            return items;
        }
                
        public List<KeyValuePair<string, string>> GetObjectAttributeTypes()
        {
            List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
            using (SqlDataReader reader = ObjectAttribute.GetAllObjectAttributeTypes())
            {
                while (reader.Read())
                {
                    items.Add(new KeyValuePair<string, string>(Convert.ToString(reader["Name"]), Convert.ToString(reader["ObjectAttributeTypeID"])));
                }
            }

            return items;
        }

        public Dictionary<string, List<string>> GetObjectAttributeAssociations(Dictionary<int, string> objects)
        {
            Dictionary<string, List<string>> objectAttributeAssociations = new Dictionary<string, List<string>>();
            foreach (string objectName in objects.Values)
            {
                List<string> attributes = new List<string>();
                using (SqlDataReader reader = ObjectAttribute.GetObjectAttributes(objectName))
                {
                    while (reader.Read())
                    {
                        attributes.Add(Convert.ToString(reader["AttributeName"]));
                    }
                }

                objectAttributeAssociations.Add(objectName, attributes);
            }

            return objectAttributeAssociations;
        }

        // Associations

        protected void lbEditAttributeAssociation_Command(object sender, CommandEventArgs e)
        {
            // CommandArgument format: <AttributeId>,<AttributeName> 
            string commandArgument = Convert.ToString(e.CommandArgument);
            int attributeId = Convert.ToInt32(commandArgument.Substring(0, commandArgument.IndexOf(",")));
            string attributeName = commandArgument.Substring(commandArgument.IndexOf(",") + 1);

            // save to page
            lblAttributeName.Text = attributeName;
            hidAttributeId.Value = Convert.ToString(attributeId);

            // get object attribute associations
            objectAttributeDetails = CSCore.DataHelper.BaseSqlHelper.GetDataSet(ObjectAttribute.GetAttributeObjects(attributeName));

            // populate associations datalist
            dlAssociations.DataKeyField = "ObjectID";
            dlAssociations.DataSource = ObjectAttribute.GetObjects();            
            dlAssociations.DataBind();

            // show modal
            mpePopup.Show();
        }

        protected void dlAssociations_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            CheckBox chkObjectAssociation = e.Item.FindControl("chkObjectAssociation") as CheckBox;
            DropDownList ddlObjectAttributeType = e.Item.FindControl("ddlObjectAttributeType") as DropDownList;
            TextBox txtDescription = e.Item.FindControl("txtDescription") as TextBox;
            TextBox txtDisplayLabel = e.Item.FindControl("txtDisplayLabel") as TextBox;
            HiddenField hidObjectId = e.Item.FindControl("hidObjectId") as HiddenField;
            
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // populate controls
                chkObjectAssociation.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Name"));
                hidObjectId.Value = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ObjectId"));

                ddlObjectAttributeType.DataSource = ObjectAttributeTypes;
                ddlObjectAttributeType.DataTextField = "Key";
                ddlObjectAttributeType.DataValueField = "Value";
                ddlObjectAttributeType.DataBind();

                // loop through associations and set values
                foreach (DataRow row in objectAttributeDetails.Tables[0].Rows)
                {
                    if (Convert.ToInt32(row["ObjectId"]) == Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "ObjectId"))) // indicates association
                    {
                        chkObjectAssociation.Checked = true;
                        
                        ddlObjectAttributeType.SelectedIndex
                            = ddlObjectAttributeType.Items.IndexOf(ddlObjectAttributeType.Items.FindByValue(Convert.ToString(row["ObjectAttributeTypeID"])));

                        txtDescription.Text = Convert.ToString(row["Description"]);

                        txtDisplayLabel.Text = Convert.ToString(row["DisplayLabel"]);
                    }
                }
            }
        }

        protected void btnCancelModalPopup_Click(object sender, EventArgs e)
        {
            mpePopup.Hide();
        }

        protected void btnSaveOrder_Click(object sender, EventArgs e)
        {
            int attributeId = Convert.ToInt32(hidAttributeId.Value);
            StringBuilder deleteErrors = new StringBuilder();

            foreach (DataListItem item in dlAssociations.Items)
            {
                CheckBox chkObjectAssociation = item.FindControl("chkObjectAssociation") as CheckBox;
                DropDownList ddlObjectAttributeType = item.FindControl("ddlObjectAttributeType") as DropDownList;
                TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                TextBox txtDisplayLabel = item.FindControl("txtDisplayLabel") as TextBox;
                HiddenField hidObjectId = item.FindControl("hidObjectId") as HiddenField;

                if (chkObjectAssociation.Checked)
                {
                    ObjectAttribute.SaveObjectAttribute(Objects.Single(x => { return x.Value == chkObjectAssociation.Text; }).Key,
                        attributeId,
                        Convert.ToInt32(ddlObjectAttributeType.SelectedItem.Value),
                        txtDescription.Text, txtDisplayLabel.Text);
                }
                else
                {
                    try
                    {
                        ObjectAttribute.DeleteSingleObjectAttribute(Convert.ToInt32(hidObjectId.Value), attributeId);
                    }
                    catch (Exception ex)
                    {
                        deleteErrors.Append(chkObjectAssociation.Text + ":");
                        deleteErrors.Append(ex.Message);
                        deleteErrors.Append("<br/>");
                    }
                }
            }

            if (deleteErrors.Length > 0)
                ShowError(string.Format("Errors encountered while deleting object attribute associations for '{0}' (possibly due to values already existing): <br/>" + deleteErrors.ToString(), 
                    lblAttributeName.Text));
            else
                ShowError(string.Empty);

            mpePopup.Hide();
            PopulateAttributes();
        }

        protected void btnAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "Add":
                    pnlAddCategory.Visible = true;
                    ddlAttributeValueType1.DataSource = ValueTypes;
                ddlAttributeValueType1.DataTextField = "Key";
                ddlAttributeValueType1.DataValueField = "Value";
                ddlAttributeValueType1.DataBind();
                ScriptManager.RegisterStartupScript(dgObjects, this.GetType(), "scrolltobottom", "window.scrollTo(0,document.body.scrollHeight);",true);
                    break;
                case "Cancel":
                    pnlAddCategory.Visible = false;
                    txtAttributeName1.Text = "";
                    txtDescription1.Text = "";
                    break;
                case "Insert":
                    if (Page.IsValid)
                    {
                        if (!ValidateAttributeEntry1())
                        {
                            txtAttributeName1.Focus();
                            return;
                        }

                        ShowError(string.Empty);

                        ObjectAttribute.SaveAttribute(txtAttributeName1.Text.Trim(), txtDescription1.Text.Trim());

                        ShowMessage(string.Format("Attribubute '{0}' successfully created.", txtAttributeName1.Text.Trim()));

                        dlAttributes.EditItemIndex = -1;
                        PopulateAttributes();
                        pnlAddCategory.Visible = false;
                        txtAttributeName1.Text = "";
                        txtDescription1.Text = "";
                        break;
                    }


                    pnlAddCategory.Visible = false;
                    txtAttributeName1.Text = "";
                    txtDescription1.Text = "";
                    
                    break;

            }

        }
    }
}