using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSWeb.Mobile.UserControls
{
	public partial class CheckoutSummary : System.Web.UI.UserControl
	{
		public decimal Subtotal
		{
			set
			{
				lblSubtotal.Text = string.Format("${0:0.00}", value);
			}
		}
		
		public decimal Tax
		{
			set
			{
				lblTax.Text = string.Format("${0:0.00}", value);
			}
		}
		
		public decimal Shipping
		{
			set
			{
				lblShipping.Text = string.Format("${0:0.00}", value);
			}
		}

		public decimal RushShipping
		{
			set
			{
				lblRushShipping.Text = string.Format("${0:0.00}", value);
			}
		}

		public bool ShowRushShipping
		{
			set {
				holderRushShipping.Visible = value;
                holderRushShippingTitle.Visible = value;
			}
		}

		public decimal Total
		{
			set
			{
				lblTotal.Text = string.Format("${0:0.00}", value);
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}