<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCartControl.ascx.cs"
    Inherits="CSWeb.Root.UserControls.ShoppingCartControl" %>
<asp:LinkButton ID="refresh" runat="server" CausesValidation="false"></asp:LinkButton>
<asp:Repeater runat="server" ID="rptShoppingCart" OnItemDataBound="rptShoppingCart_OnItemDataBound"
    OnItemCommand="rptShoppingCart_OnItemCommand">
    <HeaderTemplate>
        <div class="cart_table clearfix" style="margin-bottom: 0;">
            <div class="cart_image">&nbsp;</div>
            <div class="cart_text">
                <h4 class="pad0">Item</h4>
            </div>
            <div class="product_price" class="pad0" style="padding-left: 8px;">
                <h4>Price</h4>
            </div>
        </div>
        <div class="horizontal_dots"></div>
    </HeaderTemplate>
    <ItemTemplate>
        <div class="cart_table clearfix">
            <div class="cart_image">
                <asp:Image runat="server" ID="imgProduct" />
            </div>
            <div class="cart_text">
                <div class="basket_title">
                    <asp:Label runat="server" ID="lblSkuCode"></asp:Label></div>
                <div class="basket_description">
                    <asp:Label runat="server" ID='lblSkuDescription'></asp:Label></div>
            </div>
            <div class="product_price">
                <asp:Label runat="server" ID="lblSkuInitialPrice"></asp:Label>
                <td runat="server" width="1%" id='holderRemove' visible="false">
                    <asp:ImageButton ID="btnRemoveItem" runat="server" CommandName="delete" CausesValidation="false"
                        CssClass="ucRemoveButtonOverlay" ImageUrl="//d1kg9stb0ddjcv.cloudfront.net/images/delete.gif" />
                </td>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<asp:Panel ID="pnlTotal" runat="server">
    <asp:PlaceHolder runat="server" ID="holderTaxAndShipping">
        <div class="horizontal_dots">
        </div>

        <div class="multipay_txt">*You will be charged today for your first of 5 monthly payments of $19.99. Applicable taxes will be included in your first payment (this should not display if the consumer selects a 1-pay option). Shipping is FREE. If you are not satisfied with your purchase for any reason, simply return it within 60 days to receive a full refund. Restrictions may apply. Please refer to our <a href="returns.aspx" target="_blank">Return Policy</a>.</div>

        <div class="cart_totals clearfix">
            <div class="cart_totals_left">
                <strong>Subtotal:</strong><br />
                Shipping:<br />
                Tax:<br />
                </div>
            <div class="cart_totals_right">
                <asp:Literal runat="server" ID='lblSubtotal'></asp:Literal><br />
                <div><asp:Literal runat="server" ID="lblShipping" Visible="false"></asp:Literal>$9.95<span class="crossout"></span></div>
                <asp:Literal runat="server" ID="lblTax"></asp:Literal><br />
                
                <asp:Literal runat="server" ID="lblRushShipping" Visible="false"></asp:Literal>
                <table>
                    <tr id='holderRushShippingTotal' runat="server">
                        <td class='cartSubtotalTitle' align="right" colspan="3">
                            Rush Shipping:
                        </td>
                        <td class='cartSubtotalValue' align="center">
                        </td>
                    </tr>
                    <tr id='holderRushShipping' runat="server" visible="false">
                        <td colspan="4" class="rushShipping">
                            <asp:CheckBox runat="server" ID="chkIncludeRushShipping" OnCheckedChanged="chkIncludeRushShipping_OnCheckedChanged"
                                AutoPostBack="true" Text="Rush Shipping" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="clear"></div>
            <div class="horizontal_dots" style="width: 240px; left: -8px;"></div>
            <div class="cart_totals_left f16"><strong>Today's Payment:</strong></div>
            <div class="cart_totals_right f16"><asp:Literal runat="server" ID="lblOrderTotal"></asp:Literal></div>
        </div>
    </asp:PlaceHolder>
</asp:Panel>
   <div class="cart_offer" style="display: none;">
            <strong>*Offer Details:</strong> offer details </div>