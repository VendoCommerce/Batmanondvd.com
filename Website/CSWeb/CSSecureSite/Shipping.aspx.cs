using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSCore.DataHelper;
using CSData;

namespace CSWeb.Admin
{
    public partial class Shipping : BasePage
    {
        public const int DefaultSitePrefereceId = 1;

        public int CurrentSitePreferenceId
        {
            get
            {
                return DefaultSitePrefereceId;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BaseLoad();
                lblSuccess.Text = ResourceHelper.GetResoureValue("LabelSuccess");
                lblCancel.Text = ResourceHelper.GetResoureValue("LabelCancel");
                PopulateControls();
            }
        }

        public void PopulateControls()
        {
            ShippingPref val = ShippingDAL.GetShippingPref().First();
            txtFlat.Text = String.Format("{0:0.##}", val.flatShipping);
            txtRushFlat.Text = String.Format("{0:0.##}", val.RushShippingCost);

            cbRushShippingOption.Checked = (bool)val.InCludeRushShipping;
            rushShippingSettings.Visible = cbRushShippingOption.Checked;

            BindAll();
            switch (val.OptionId)
            {
                case (int)ShippingOptionType.TotalAmount:
                    cbOrderSubTotal.Checked = true;
                    pnlOrderVal.Visible = true;
                    break;
                case (int)ShippingOptionType.Weight:
                    cbOrderWeight.Checked = true;
                    pnlWeight.Visible = true;
                    break;
                case (int)ShippingOptionType.SkuBased:
                    cbSkuItem.Checked = true;
                    pnlSkuItem.Visible = true;
                    break;
                case (int)ShippingOptionType.Flat:
                    cbFlat.Checked = true;
                    pnlFlat.Visible = true;
                    break;
            }

            switch (val.RushOptionId)
            {
                case (int)ShippingOptionType.TotalAmount:
                    cbRushOrderTotal.Checked = true;
                    pnlRushOrderTotal.Visible = true;
                    break;
                case (int)ShippingOptionType.Weight:
                    cbRushOrderweight.Checked = true;
                    pnlRushOrderweight.Visible = true;
                    break;
                case (int)ShippingOptionType.SkuBased:
                    cbRushSkuItem.Checked = true;
                    pnlRushSkuItem.Visible = true;
                    break;
                case (int)ShippingOptionType.Flat:
                    cbRushFlat.Checked = true;
                    pnlRushFlat.Visible = true;
                    break;
            }
        }

        public void BindAll()
        {
			SkuManager skuMgr = new SkuManager();
			rptSkuItem.DataSource = skuMgr.GetAllSkus();
			rptSkuItem.DataBind();

            rptRushSkuItem.DataSource = skuMgr.GetAllSkus();
            rptRushSkuItem.DataBind();

            rptItems.DataSource = ShippingDAL.GetShippingOrderValue(ShippingOptionType.TotalAmount, false, DefaultSitePrefereceId);
            rptItems.DataBind();

            rptRushOrderTotal.DataSource = ShippingDAL.GetShippingOrderValue(ShippingOptionType.TotalAmount, true, DefaultSitePrefereceId);
            rptItems.DataBind();
            rptRushOrderTotal.DataBind();

            rptOrderWeight.DataSource = ShippingDAL.GetShippingOrderValue(ShippingOptionType.Weight, false, DefaultSitePrefereceId);
            rptItems.DataBind();
            rptOrderWeight.DataBind();

            rptRushOrderWeight.DataSource = ShippingDAL.GetShippingOrderValue(ShippingOptionType.Weight, true, DefaultSitePrefereceId);
            rptItems.DataBind();
            rptRushOrderWeight.DataBind();

            rptShippingCharges.DataSource = ShippingDAL.GetShippingChargesByPref(DefaultSitePrefereceId);
            rptShippingCharges.DataBind();
        }

        public void BindAll(ShippingOptionType type, bool includeRushShipping)
        {
            List<ShippingOrderValue> list = ShippingDAL.GetShippingOrderValue(type, includeRushShipping, DefaultSitePrefereceId);
            if (!includeRushShipping)
            {
                switch (type)
                {
                    case ShippingOptionType.TotalAmount:
                        rptItems.DataSource = list;
                        rptItems.DataBind();
                        break;
                    case ShippingOptionType.Weight:
                        rptOrderWeight.DataSource = list;
                        rptOrderWeight.DataBind();
                        break;
             
                }
            }
            else
            {
                switch (type)
                {
                    case ShippingOptionType.TotalAmount:
                        rptRushOrderTotal.DataSource = list;
                        rptRushOrderTotal.DataBind();
                        break;
                    case ShippingOptionType.Weight:
                        rptRushOrderWeight.DataSource = list;
                        rptRushOrderWeight.DataBind();
                        break;
              
                }
            }
        }

        #region Shipping Options

        protected void cbRushShippingOption_OnCheckedChanged(object sender, EventArgs e)
        {
            rushShippingSettings.Visible = cbRushShippingOption.Checked;
        }	

        public void OnCheckChanged_OrderVal(Object s, EventArgs e)
        {
            pnlOrderVal.Visible = true;
            pnlSkuItem.Visible = false;
            pnlWeight.Visible = false;
            pnlFlat.Visible = false;
          
        }

        public void OnCheckChanged_Weight(Object s, EventArgs e)
        {
            pnlWeight.Visible = true;
            pnlOrderVal.Visible = false;
            pnlSkuItem.Visible = false;     
            pnlFlat.Visible = false;
        }

        public void OnCheckChanged_Sku(Object s, EventArgs e)
        {
            pnlSkuItem.Visible = true;
            pnlWeight.Visible = false;
            pnlOrderVal.Visible = false;
            pnlFlat.Visible = false;
            
        }

        public void OnCheckChanged_Flat(Object s, EventArgs e)
        {
            pnlSkuItem.Visible = false;
            pnlWeight.Visible = false;
            pnlOrderVal.Visible = false;
            pnlFlat.Visible = true;

        }

        protected void btnAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            if (Page.IsValid)
            {
                ShippingOptionType shippingType = ShippingOptionType.TotalAmount;
                if (e.CommandName == "OrderWeight")
                {
                    shippingType = ShippingOptionType.Weight;
                }
                using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
                {


                    CSData.ShippingOrderValue cat = new CSData.ShippingOrderValue
                    {

                        OrderTotal = 0,
                        Cost = 0,
                        TypeId = (int)shippingType,
                        IncludeRushShipping = false,
                        PrefId = DefaultSitePrefereceId
                    };


                    context.ShippingOrderValues.InsertOnSubmit(cat);
                    context.SubmitChanges();

                }

                BindAll(shippingType, false);


            }
        }

        protected void rptSkuItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            List<SkuShipping> skuItems = ShippingDAL.GetSkuShipping(false, DefaultSitePrefereceId);
            CSBusiness.Sku skuItem = e.Item.DataItem as CSBusiness.Sku;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {


                Label lblTitle = e.Item.FindControl("lblSkuTitle") as Label;
                Label lblSkuId = e.Item.FindControl("lblSkuId") as Label;
                TextBox txtPercentage = (TextBox)e.Item.FindControl("txtPercentage");

                SkuShipping itemVal = skuItems.FirstOrDefault(p => p.SkuId == skuItem.SkuId);
                if (itemVal != null)
                    txtPercentage.Text = String.Format("{0:0.##}", itemVal.Cost);
                else
                    txtPercentage.Text = "0";
                lblSkuId.Text = skuItem.SkuId.ToString();
                lblTitle.Text = skuItem.Title + " (" + skuItem.SkuId.ToString() +")";


            }

        }
        #endregion Shipping Options

        #region Rush Shipping Options

        public void OnCheckChanged_RushFlat(Object s, EventArgs e)
            {
                pnlRushOrderTotal.Visible = false;
                pnlRushSkuItem.Visible = false;
                pnlRushOrderweight.Visible = false;
                pnlRushFlat.Visible = true;

            }

            public void OnCheckChanged_RushOrderVal(Object s, EventArgs e)
            {
                pnlRushOrderTotal.Visible = true;
                pnlRushSkuItem.Visible = false;
                pnlRushOrderweight.Visible = false;
                pnlRushFlat.Visible = false;

            }

            public void OnCheckChanged_RushWeight(Object s, EventArgs e)
            {
                pnlRushOrderTotal.Visible = false;
                pnlRushSkuItem.Visible = false;
                pnlRushOrderweight.Visible = true;
                pnlRushFlat.Visible = false;
            }


            public void OnCheckChanged_RushSku(Object s, EventArgs e)
            {
                pnlRushOrderTotal.Visible = false;
                pnlRushSkuItem.Visible = true;
                pnlRushOrderweight.Visible = false;
                pnlRushFlat.Visible = false;

            }

            protected void btnRushAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
            {
                if (Page.IsValid)
                {
                    ShippingOptionType shippingType = ShippingOptionType.TotalAmount;
                    if (e.CommandName == "RushOrderWeight")
                    {
                        shippingType = ShippingOptionType.Weight;
                    }
                    using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
                    {


                        CSData.ShippingOrderValue cat = new CSData.ShippingOrderValue
                        {

                            OrderTotal = 0,
                            Cost = 0,
                            TypeId = (int)shippingType,
                            IncludeRushShipping = true,
                            PrefId = DefaultSitePrefereceId

                        };


                        context.ShippingOrderValues.InsertOnSubmit(cat);
                        context.SubmitChanges();

                    }

                    BindAll(shippingType, true);

                    //CategoryDAL.UpdateCategory(0, txtCategory.Text, true);
                }
            }

            protected void lbAddShippingCharge_Command(object sender, CommandEventArgs e)
            {
                if (Page.IsValid)
                {
                    using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
                    {
                        ShippingCharge shippingCharge = new ShippingCharge()
                        {
                            PrefId = CurrentSitePreferenceId,
                            Key = string.Empty,
                            Cost = 0,
                            CreateDate = DateTime.Now
                        };

                        context.ShippingCharges.InsertOnSubmit(shippingCharge);
                        context.SubmitChanges();

                        BindAll();
                    }
                }
            }

            protected void rptRushSkuItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
            {
                List<SkuShipping> skuItems = ShippingDAL.GetSkuShipping(true, DefaultSitePrefereceId);
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    CSBusiness.Sku skuItem = e.Item.DataItem as CSBusiness.Sku;

                    Label lblTitle = e.Item.FindControl("lblSkuTitle") as Label;
                    Label lblSkuId = e.Item.FindControl("lblSkuId") as Label;
                    TextBox txtPercentage = (TextBox)e.Item.FindControl("txtPercentage");
              

                    SkuShipping itemVal = skuItems.FirstOrDefault(p => p.SkuId == skuItem.SkuId);
                    if (itemVal != null)
                    {
                        txtPercentage.Text = String.Format("{0:0.##}", itemVal.Cost);
                     
                    }
                    lblSkuId.Text = skuItem.SkuId.ToString();
                    lblTitle.Text = skuItem.Title + " (<b>" + skuItem.SkuId.ToString() + "</b>)";


                }

            }
        #endregion Rush Shipping Options        

        protected void btnSave_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                ShippingOptionType option = ShippingOptionType.TotalAmount;
                ShippingOptionType optionRush = ShippingOptionType.TotalAmount;

                if (pnlOrderVal.Visible)
                {
                    option = ShippingOptionType.TotalAmount;
                    if (this.rptItems.Items.Count > 0)
                    {
                        using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection())
                            )
                        {
                            foreach (RepeaterItem lst in rptItems.Items)
                            {

                                if ((lst.ItemType == ListItemType.Item) ||
                                    (lst.ItemType == ListItemType.AlternatingItem))
                                {
                                    int Id = Convert.ToInt32(((Label) lst.FindControl("lblShippingId")).Text);
                                    TextBox txtOrderVal = (TextBox) lst.FindControl("txtOrderItem");
                                    TextBox txtCostVal = (TextBox) lst.FindControl("txtCostItem");
                                    ShippingOrderValue order =
                                        context.ShippingOrderValues.Single(p => p.ShippingId == Id);
                                    order.OrderTotal = Convert.ToDecimal(txtOrderVal.Text);
                                    order.Cost = Convert.ToDecimal(txtCostVal.Text);
                                    context.SubmitChanges();
                                }
                            }
                        }




                    }
                } //end if for Shipping OrderValue

                if (pnlWeight.Visible)
                {
                    option = ShippingOptionType.Weight;
                    if (this.rptOrderWeight.Items.Count > 0)
                    {
                        using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection())
                            )
                        {
                            foreach (RepeaterItem lst in rptOrderWeight.Items)
                            {

                                if ((lst.ItemType == ListItemType.Item) ||
                                    (lst.ItemType == ListItemType.AlternatingItem))
                                {
                                    int Id = Convert.ToInt32(((Label) lst.FindControl("lblShippingId")).Text);
                                    TextBox txtOrderVal = (TextBox) lst.FindControl("txtOrderItem");
                                    TextBox txtCostVal = (TextBox) lst.FindControl("txtCostItem");
                                    ShippingOrderValue order =
                                        context.ShippingOrderValues.Single(p => p.ShippingId == Id);
                                    order.OrderTotal = Convert.ToDecimal(txtOrderVal.Text);
                                    order.Cost = Convert.ToDecimal(txtCostVal.Text);
                                    context.SubmitChanges();
                                }
                            }
                        }

                    }
                } //end if for Shipping OrderWeight

                if (pnlSkuItem.Visible)
                {
                    option = ShippingOptionType.SkuBased;
                    if (this.rptSkuItem.Items.Count > 0)
                    {
                        using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection())
                            )
                        {
                            foreach (RepeaterItem lst in rptSkuItem.Items)
                            {

                                if ((lst.ItemType == ListItemType.Item) ||
                                    (lst.ItemType == ListItemType.AlternatingItem))
                                {
                                    int Id = Convert.ToInt32(((Label) lst.FindControl("lblSkuId")).Text);
                                    TextBox txtCostVal = (TextBox) lst.FindControl("txtPercentage");
                                    SkuShipping order =
                                        context.SkuShippings.FirstOrDefault(
                                            p => (p.SkuId == Id) && (p.IncludeRushShipping == false) && (p.PrefId == 1));
                                    if (order != null)
                                    {
                                        order.Cost = Convert.ToDecimal(txtCostVal.Text);
                                        context.SubmitChanges();
                                    }
                                    else
                                    {
                                        SkuShipping item = new SkuShipping();
                                        item.SkuId = Id;
                                        item.Cost = Convert.ToDecimal(txtCostVal.Text);
                                        item.IncludeRushShipping = false;
                                        item.PrefId = 1;
                                        context.SkuShippings.InsertOnSubmit(item);
                                        context.SubmitChanges();
                                    }

                                }
                            }
                        }

                    }
                } //end if for Shipping SkuItem level

                if (pnlFlat.Visible)
                {
                    option = ShippingOptionType.Flat;

                }

                if (cbRushShippingOption.Checked)
                {

                    if (pnlRushOrderTotal.Visible)
                    {
                        optionRush = ShippingOptionType.TotalAmount;
                        if (this.rptRushOrderTotal.Items.Count > 0)
                        {
                            using (
                                CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection())
                                )
                            {
                                foreach (RepeaterItem lst in rptRushOrderTotal.Items)
                                {

                                    if ((lst.ItemType == ListItemType.Item) ||
                                        (lst.ItemType == ListItemType.AlternatingItem))
                                    {
                                        int Id = Convert.ToInt32(((Label) lst.FindControl("lblShippingId")).Text);
                                        TextBox txtOrderVal = (TextBox) lst.FindControl("txtOrderItem");
                                        TextBox txtCostVal = (TextBox) lst.FindControl("txtCostItem");
                                        ShippingOrderValue order =
                                            context.ShippingOrderValues.Single(p => p.ShippingId == Id);
                                        order.OrderTotal = Convert.ToDecimal(txtOrderVal.Text);
                                        order.Cost = Convert.ToDecimal(txtCostVal.Text);
                                        context.SubmitChanges();
                                    }
                                }
                            }




                        }
                    }

                    if (pnlRushOrderweight.Visible)
                    {
                        optionRush = ShippingOptionType.Weight;
                        if (this.rptRushOrderWeight.Items.Count > 0)
                        {
                            using (
                                CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection())
                                )
                            {
                                foreach (RepeaterItem lst in rptRushOrderWeight.Items)
                                {

                                    if ((lst.ItemType == ListItemType.Item) ||
                                        (lst.ItemType == ListItemType.AlternatingItem))
                                    {
                                        int Id = Convert.ToInt32(((Label) lst.FindControl("lblShippingId")).Text);
                                        TextBox txtOrderVal = (TextBox) lst.FindControl("txtOrderItem");
                                        TextBox txtCostVal = (TextBox) lst.FindControl("txtCostItem");
                                        ShippingOrderValue order =
                                            context.ShippingOrderValues.Single(p => p.ShippingId == Id);
                                        order.OrderTotal = Convert.ToDecimal(txtOrderVal.Text);
                                        order.Cost = Convert.ToDecimal(txtCostVal.Text);
                                        context.SubmitChanges();
                                    }
                                }
                            }




                        }
                    }

                    if (pnlRushSkuItem.Visible)
                    {
                        optionRush = ShippingOptionType.SkuBased;
                        if (this.rptRushSkuItem.Items.Count > 0)
                        {
                            using (
                                CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection())
                                )
                            {
                                foreach (RepeaterItem lst in rptRushSkuItem.Items)
                                {

                                    if ((lst.ItemType == ListItemType.Item) ||
                                        (lst.ItemType == ListItemType.AlternatingItem))
                                    {
                                        int Id = Convert.ToInt32(((Label) lst.FindControl("lblSkuId")).Text);
                                        TextBox txtCostVal = (TextBox) lst.FindControl("txtPercentage");
                                        SkuShipping order =
                                            context.SkuShippings.FirstOrDefault(
                                                p =>
                                                (p.SkuId == Id) && (p.IncludeRushShipping == true) && (p.PrefId == 1));
                                        if (order != null)
                                        {
                                            order.Cost = Convert.ToDecimal(txtCostVal.Text);
                                            context.SubmitChanges();
                                        }
                                        else
                                        {
                                            SkuShipping item = new SkuShipping();
                                            item.SkuId = Id;
                                            item.Cost = Convert.ToDecimal(txtCostVal.Text);
                                            item.IncludeRushShipping = true;
                                            item.PrefId = 1;
                                            context.SkuShippings.InsertOnSubmit(item);
                                            context.SubmitChanges();
                                        }

                                    }
                                }
                            }




                        }
                    }

                }

                if (pnlRushFlat.Visible)
                {
                    optionRush = ShippingOptionType.Flat;

                }


                using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
                {
                    ShippingPref Val = context.ShippingPrefs.FirstOrDefault(x => x.PrefId == DefaultSitePrefereceId);
                    if (Val != null)
                    {
                        if (pnlFlat.Visible)
                            Val.flatShipping = Convert.ToDecimal(txtFlat.Text);
                        if (pnlRushFlat.Visible)
                            Val.RushShippingCost = Convert.ToDecimal(txtRushFlat.Text);
                        if (cbRushShippingOption.Checked)
                            Val.RushOptionId = (int) optionRush;
                        Val.OptionId = (int) option;

                        Val.InCludeRushShipping = cbRushShippingOption.Checked;
                        context.SubmitChanges();
                    }
                }

                // save additional charges
                if (this.rptShippingCharges.Items.Count > 0)
                {
                    List<int> deleteList = new List<int>();

                    using (CSCommerceDataContext context = new CSCommerceDataContext(ConfigHelper.GetDBConnection()))
                    {
                        foreach (RepeaterItem lst in rptShippingCharges.Items)
                        {
                            if ((lst.ItemType == ListItemType.Item) || (lst.ItemType == ListItemType.AlternatingItem))
                            {
                                int id = Convert.ToInt32(((HiddenField) lst.FindControl("hidShippingChargeId")).Value);
                                CheckBox chkDelete = (CheckBox) lst.FindControl("chkDelete");

                                if (chkDelete.Checked)
                                {
                                    deleteList.Add(id);
                                }
                                else
                                {
                                    TextBox txtKey = (TextBox) lst.FindControl("txtKey");
                                    TextBox txtCost = (TextBox) lst.FindControl("txtCost");
                                    TextBox txtLabel = (TextBox) lst.FindControl("txtLabel");

                                    ShippingCharge shippingCharge =
                                        context.ShippingCharges.Single(p => p.ShippingChargeId == id);

                                    shippingCharge.Key = txtKey.Text;
                                    shippingCharge.Cost = Convert.ToDecimal(txtCost.Text);
                                    shippingCharge.FriendlyLabel = txtLabel.Text;
                                    context.SubmitChanges();
                                }
                            }
                        }
                    }

                    if (deleteList.Count > 0)
                    {
                        foreach (int id in deleteList)
                        {
                            CSBusiness.Shipping.ShippingManager.RemoveShippingCharge(id);
                        }
                    }
                }
                lblCancel.Visible = false;
                lblSuccess.Visible = true;

            } //end of Save Command
            else
            {
                PopulateControls();
                lblCancel.Visible = true;
                lblSuccess.Visible = false;
            }

           // Response.Redirect("Main.aspx");

        }
    }
}