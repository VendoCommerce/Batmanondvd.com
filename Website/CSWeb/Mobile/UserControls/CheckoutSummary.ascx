<%@Control Language="C#" AutoEventWireup="true" CodeBehind="CheckoutSummary.ascx.cs" Inherits="CSWeb.Mobile.UserControls.CheckoutSummary" %>
<table width="350" border="0" cellspacing="0" cellpadding="0" class="address_table"
    style="margin: 0 auto;">
    <tr>
        <td colspan="2" class="address_header">
            Summary
        </td>
    </tr>
    <tr>
        <td>
            Subtotal:
            <br />
            Tax:
            <br />
            Shipping:
            <br />
            <asp:PlaceHolder runat="server" ID="holderRushShippingTitle">Rush Shipping:
                <br />
            </asp:PlaceHolder>
            Total:
        </td>
        <td>
            <asp:Literal runat="server" ID='lblSubtotal'></asp:Literal><br />
            <asp:Literal runat="server" ID='lblTax'></asp:Literal>  <br />
            <asp:Literal runat="server" ID='lblShipping'></asp:Literal><br />
            <asp:PlaceHolder runat="server" ID="holderRushShipping">
                <asp:Literal runat="server" ID='lblRushShipping' />
                <br />
            </asp:PlaceHolder>
            <asp:Literal runat="server" ID='lblTotal' /><br />
        </td>
    </tr>
</table>
