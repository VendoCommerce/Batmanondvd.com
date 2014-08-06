using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSCore.Utils;
using System.Collections;
using System.Data;
using System.Text;
using AjaxControlToolkit;
using CSBusiness.Attributes;
using System.Collections.Generic;
using System.Web;


namespace CSWeb.Admin
{
    public partial class SKUItem : System.Web.UI.Page, IAttributesPage
    {
        public int SkuId = 0;

        private Sku skuItem;

        public string AttributeFieldsInfoStateKey
        {
            get
            {
                return "AttributeFieldsInfoStateKey";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["skuid"] != null)
            {
                SkuId = Convert.ToInt32(Request["skuid"].ToString());
                
            }
            
            if (!Page.IsPostBack)
            {
                lblSuccess.Text = ResourceHelper.GetResoureValue("LabelSuccess");
                lblCancel.Text = ResourceHelper.GetResoureValue("LabelCancel");
                ddlCategory.DataSource = CSFactory.GetAllCategories();
                ddlCategory.DataTextField = "Title";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select", "0"));

                if (SkuId > 0)
                {
                    skuItem = new SkuManager().GetSkuByID(SkuId);

                    PopulateFields();
                }
                else
                {
                    ucAttributes.NewItem(new Sku());
                }
            }
        }

        protected void PopulateFields()
        {         
            txtTitle.Text = skuItem.Title;
            txtSkuCode.Text = skuItem.SkuCode;
            txtOfferCode.Text = skuItem.OfferCode;
            txtfullprice.Text = String.Format("{0:0.##}", skuItem.FullPrice);
            txtinitialprice.Text = String.Format("{0:0.##}", skuItem.InitialPrice); 
            txtWeight.Text =  skuItem.Weight.ToString();
            txtStock.Text = skuItem.StockQty.ToString();
            cbAvailable.Checked = Convert.ToBoolean(skuItem.IsAvailable);
            txtImagePath.Text = skuItem.ImagePath;
            ftbShortDesc.Content = skuItem.ShortDescription;
            ftbLongDesc.Content = skuItem.LongDescription;
            ftbEmailDesc.Content = skuItem.EmailDescription;
            ddlCategory.Items.FindByValue(skuItem.CategoryId.ToString()).Selected = true;

            if (skuItem.IsAvailable)
            {
                cbAvailable.Checked = true;
            }
         
            if (skuItem.IsTaxable)
            {
                rbListTaxable.Items.FindByValue("Yes").Selected = true;
                pnlTaxableAmount.Visible = true;
                txtTaxAmount.Text = String.Format("{0:0.##}", skuItem.TaxableFullAmount); 
                                     
            }
            else
            {
                rbListTaxable.Items.FindByValue("No").Selected = true;
                pnlTaxableAmount.Visible = false;                
            }

            ucAttributes.Populate(skuItem);
        }

        protected void btnAction_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                if (Page.IsValid)
                {
                    Sku sku = new SkuManager().GetSkuItem();
                    sku.SkuId = SkuId;
                    sku.Title = txtTitle.Text;
                    sku.SkuCode = txtSkuCode.Text;
                    sku.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue); 
                    sku.OfferCode = txtOfferCode.Text;
                    sku.FullPrice = Convert.ToDecimal(txtfullprice.Text);
                    sku.InitialPrice = Convert.ToDecimal(txtinitialprice.Text);
                    sku.ImagePath = txtImagePath.Text;
                    if (txtStock.Text.Length > 0)
                        sku.StockQty = Convert.ToInt32(txtStock.Text);
                    else
                        sku.StockQty = 0;
                    sku.IsAvailable = cbAvailable.Checked;
                    if (rbListTaxable.SelectedIndex == 0)
                    {
                        sku.IsTaxable = true;
                    }
                    else
                    {
                        sku.IsTaxable = false;
                    }

                    if (pnlTaxableAmount.Visible)
                        sku.TaxableFullAmount = Convert.ToDecimal(txtTaxAmount.Text);
                    else
                        sku.TaxableFullAmount = 0;
                    if (txtWeight.Text.Length > 0)
                        sku.Weight = Convert.ToDecimal(txtWeight.Text);
                    else
                        sku.Weight = 0;
                    sku.ShortDescription = ftbShortDesc.Content;
                    sku.LongDescription = ftbLongDesc.Content;
                    sku.EmailDescription = ftbEmailDesc.Content;
                    sku.CreateDate = DateTime.Now;
                    sku.ModifyDate = DateTime.Now;

                    List<string> deleteAttributes;

                    sku.AttributeValues = ucAttributes.GetEnteredAttributeValues(out deleteAttributes);

                    new SkuManager().InsertSku(sku);

                    ucAttributes.SaveDeletedAttributes(sku.ObjectName, sku.ItemId);
                }
                lblSuccess.Visible = true;
                lblCancel.Visible = false;
            }
            else
            {
                Response.Redirect("skulist.aspx");
            }

            //Response.Redirect("skulist.aspx");
        }

        protected void rbListTaxable_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rbListTaxable.SelectedIndex == 0)
            {
                pnlTaxableAmount.Visible = true;;
            }
            else
            {
                pnlTaxableAmount.Visible = false;
            }    
        }
    }
}