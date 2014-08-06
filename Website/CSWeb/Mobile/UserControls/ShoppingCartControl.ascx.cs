using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using System.Web.UI.HtmlControls;
using CSBusiness.Preference;
using CSBusiness.Shipping;

namespace CSWeb.Mobile.UserControls
{
    public partial class ShoppingCartControl : System.Web.UI.UserControl
    {

		public event EventHandler UpdateCart;

        private ClientCartContext CartContext
        {
            get
            {
                return Session["ClientOrderData"] as ClientCartContext;
            }
        }
        
        public ShoppingCartDisplayTotalMode TotalMode
        {
            get 
            {
                return ViewState["TotalMode"] == null ? ShoppingCartDisplayTotalMode.Full : (ShoppingCartDisplayTotalMode)ViewState["TotalMode"];
            }
            set 
            {
                ViewState["TotalMode"] = value;
            }
        }
        
		public ShoppingCartQuanityMode QuantityMode
		{
			get
			{
				return ViewState["QuantityMode"] == null ? ShoppingCartQuanityMode.Readonly : (ShoppingCartQuanityMode)ViewState["QuantityMode"];
			}
			set
			{
				ViewState["QuantityMode"] = value;
			}
		}

		public bool HideRemoveButton
		{
			get
			{
				return (bool) ( ViewState["HideRemoveButton"] ?? false);
			}
			set
			{
				ViewState["HideRemoveButton"] = value;
			}
		}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
                
                BindControls();   
            }
        }

        public void BindControls()
        {
            if (CartContext.CartInfo.CartItems.Count > 0)
            {
                rptShoppingCart.DataSource = CartContext.CartInfo.CartItems.FindAll(x => x.Visible==true);
                rptShoppingCart.DataBind();

                pnlTotal.Visible = true;
                
                lblSubtotal.Text = String.Format("${0:0.00}", CartContext.CartInfo.SubTotal);
                lblTax.Text = String.Format("${0:0.00}", CartContext.CartInfo.TaxCost);
                lblShipping.Text = String.Format("${0:0.00}", CartContext.CartInfo.ShippingCost);
                lblRushShipping.Text = String.Format("${0:0.00}", CartContext.CartInfo.RushShippingCost);
                lblOrderTotal.Text = String.Format("${0:0.00}", CartContext.CartInfo.Total);

                //Sri Comments on 11/15: Need to Plug-in to Custom Shipping option Model
                SitePreference shippingGetShippingPref = CSFactory.GetCacheSitePref();
                holderRushShipping.Visible = shippingGetShippingPref.IncludeRushShipping ?? false;
                holderRushShippingTotal.Visible = chkIncludeRushShipping.Checked = (CartContext.CartInfo.ShippingMethod == UserShippingMethodType.Rush);
            }
            else
            {
                pnlTotal.Visible = false;
                rptShoppingCart.Visible = false;
            }

        }

        protected void rptShoppingCart_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
				Label lblSkuCode = e.Item.FindControl("lblSkuCode") as Label;
                Label lblSkuDescription = e.Item.FindControl("lblSkuDescription") as Label;
				TextBox txtQuantity = e.Item.FindControl("txtQuantity") as TextBox;
                Label lblQuantity = e.Item.FindControl("lblQuantity") as Label;
                Label lblSkuInitialPrice = e.Item.FindControl("lblSkuInitialPrice") as Label;
				ImageButton btnRemoveItem = e.Item.FindControl("btnRemoveItem") as ImageButton;
				HtmlContainerControl holderQuantity = e.Item.FindControl("holderQuantity") as HtmlContainerControl;
                HtmlContainerControl holderRemove = e.Item.FindControl("holderRemove") as HtmlContainerControl;
                Image imgProduct = e.Item.FindControl("imgProduct") as Image;

                Sku cartItem = e.Item.DataItem as Sku;
				
                lblSkuDescription.Text = cartItem.ShortDescription;
				lblQuantity.Text = txtQuantity.Text = cartItem.Quantity.ToString();
                lblSkuInitialPrice.Text = String.Format("${0:0.##}", cartItem.InitialPrice);
                if (cartItem.ImagePath.Length > 0)
                {
                    imgProduct.ImageUrl = cartItem.ImagePath;
                    lblSkuCode.Visible = false;
                }
                else
                {
                     imgProduct.Visible = false;
                     lblSkuCode.Text = cartItem.SkuCode.ToString();
                }


                    btnRemoveItem.CommandArgument = cartItem.SkuId.ToString();

				txtQuantity.Attributes["onchange"] = Page.ClientScript.GetPostBackEventReference(refresh, "");

				switch (QuantityMode)
				{
					case ShoppingCartQuanityMode.Hidden:
						holderQuantity.Visible = false;
						break;
					case ShoppingCartQuanityMode.Editable:
						lblQuantity.Visible = false;
						break;
					case ShoppingCartQuanityMode.Readonly:
						txtQuantity.Visible = false;
						break;
					default:
						break;
				}

				if (HideRemoveButton)
				{
                    holderRemove.Visible = false;
				}
			}
			else if (e.Item.ItemType == ListItemType.Header)
			{
				HtmlContainerControl holderHeaderQuantity = e.Item.FindControl("holderHeaderQuantity") as HtmlContainerControl;
                HtmlContainerControl holderHeaderRemove = e.Item.FindControl("holderHeaderRemove") as HtmlContainerControl;
				if (QuantityMode == ShoppingCartQuanityMode.Hidden)
				{
					holderHeaderQuantity.Visible = false;
				}

                if (HideRemoveButton)
                {
                    holderHeaderRemove.Visible = false;
                }
            }
        }

        protected void rptShoppingCart_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "delete":
                    {
                        int skuToRemove = Convert.ToInt32(e.CommandArgument);
                        CartContext.CartInfo.UpdateSku(skuToRemove);
                        BindControls();
						if (UpdateCart != null)
							UpdateCart(sender, e);
                    }
                    break;
                default:
                    break;
            }
        }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			holderTaxAndShipping.Visible = TotalMode == ShoppingCartDisplayTotalMode.Full;
		}

		protected void chkIncludeRushShipping_OnCheckedChanged(object sender, EventArgs e)
		{
			CartContext.CartInfo.ShippingMethod = chkIncludeRushShipping.Checked ? UserShippingMethodType.Rush : UserShippingMethodType.Standard;
			CartContext.CartInfo.Compute();
			BindControls();
		}

        protected void OnTextChanged_Changed(object sender, EventArgs e)
		{
            TextBox txtQuantity = (TextBox)sender;
            RepeaterItem item = (RepeaterItem)txtQuantity.NamingContainer;
            ImageButton btnRemoveItem = item.FindControl("btnRemoveItem") as ImageButton;

            int skuID = Convert.ToInt32(btnRemoveItem.CommandArgument);
            Sku cartItem = CartContext.CartInfo.CartItems.FirstOrDefault(c => c.SkuId == skuID);
            int newQuantity = 0;
            if (int.TryParse(txtQuantity.Text, out newQuantity))
                cartItem.Quantity = newQuantity;
			CartContext.CartInfo.Compute();
            BindControls();
			if(UpdateCart != null)
				UpdateCart(sender, e);
		}
    }

    public enum ShoppingCartDisplayTotalMode
    { 
        SubtotalOnly,
        Full
    }

	public enum ShoppingCartQuanityMode
	{
		Editable,
		Readonly,
		Hidden
    }
}