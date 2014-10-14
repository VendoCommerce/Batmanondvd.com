<%@Control Language="C#" Inherits="CSWeb.Mobile.UserControls.CheckoutThankYouModule2" %>
<%@ Register Src="../UserControls/TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %>

<p class="webfont1bold f38 text-center" style="background: #ebebeb; padding: 24px 0 20px 0;">
          Your BAT-TASTIC order has been placed!</p>
       
         
<p class="webfont1 pad20" style="padding-top: 40px;"><strong>Thank You and Enjoy Your Purchase!</strong> Your order number is
                <%=orderId.ToString()%>. You will receive a shipping confirmation once your order has shipped.
<span style="display: none;"><%=LiteralEmail.Text%>.</span></p>

<p class="webfont1 pad20 ital" style="letter-spacing: -1px;">Your order will arrive on Nov 11th or shortly thereafter.</p>
             <div class="receipt_header">Billing Information</div>
             <table class="table">
             <tr><td>Name:</td>
             <td><asp:Literal ID="LiteralName_b" runat="server"></asp:Literal></td></tr>
             <tr><td>Address:</td>
             <td><asp:Literal ID="LiteralAddress_b" runat="server"></asp:Literal>
                    <asp:Panel ID="pnlBAddress2" runat="server">
                        <br /><asp:Literal ID="LiteralAddress2_b" runat="server"></asp:Literal>
                    </asp:Panel></td></tr>
              <tr><td>City:</td>
             <td><asp:Literal ID="LiteralCity_b" runat="server"></asp:Literal></td></tr>
              <tr><td>State:</td>
             <td><asp:Literal ID="LiteralState_b" runat="server"></asp:Literal></td></tr>
    <tr><td>Zip Code:</td>
             <td><asp:Literal ID="LiteralZip_b" runat="server"></asp:Literal></td></tr>
             <tr><td>Phone:</td>
             <td><asp:Literal ID="LiteralPhone" runat="server"></asp:Literal></td></tr>
             <tr><td>Email:</td>
             <td><asp:Literal ID="LiteralEmail" runat="server"></asp:Literal></td></tr>
       </table>
        
              <div class="receipt_header">Shipping Information</div>
              
<table class="table">
             <tr><td>Name:</td>
             <td><asp:Literal ID="LiteralName" runat="server"></asp:Literal></td></tr>
             <tr><td>Address:</td>
             <td><asp:Literal ID="LiteralAddress" runat="server"></asp:Literal>
                    <asp:Panel ID="pnlSAddress2" runat="server">
                        <br><asp:Literal ID="LiteralAddress2" runat="server"></asp:Literal>
                    </asp:Panel></td></tr>
             <tr><td>City:</td>
             <td><asp:Literal ID="LiteralCity" runat="server"></asp:Literal></td></tr>
             <tr><td>State:</td>
             <td><asp:Literal ID="LiteralState" runat="server"></asp:Literal></td></tr>
     <tr><td>Zip Code:</td>
             <td><asp:Literal ID="LiteralZip" runat="server"></asp:Literal></td></tr>
             </table>
          

<div class="receipt_header">Your Order</div>

              <table class="receipt_table">
                  <tr>
                      <th colspan="2" class="pad0" style="text-align: left; padding-left: 14px;">Item</th>
                      <th class="text-center pad0">Quantity</th>
                      <th class="text-center pad0">Payment</th>
                  </tr>
          <tr>
            <td colspan="4" style="padding: 0;"><div class="horizontal_dots"></div></td>
          </tr>
                <asp:DataList runat="server" ID="dlordersList" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <ItemTemplate>
                        <tr>
                            <td valign="top" class="receipt_img" width="15%">
                                <img src="<%# ImageURL(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SkuId"))) %>" />
                            </td>                        
                        <td class="receipt_item" width="51%">
                            <%# DataBinder.Eval(Container.DataItem, "LongDescription")%>
                        </td>
                        <td class="text-center bold" width="17%">
                            <%# DataBinder.Eval(Container.DataItem, "Quantity")%>
                       </td>
                           <td class="text-center bold" width="17%">
                           $<%# Math.Round(Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "TotalPrice")), 2).ToString()%> 
                       </td>
                       </tr>
                           
                    </ItemTemplate>
                </asp:DataList>
                <asp:Literal ID="LiteralTableRows" runat="server"></asp:Literal>
          <tr>
              <td colspan="4" style="padding: 0;"><div class="horizontal_dots"></div></td>
          </tr>
                  <tr><td colspan="3" class="bold f24" style="line-height: 29px; text-align: right; padding-right: 15px; color: #979797;">
                    Subtotal<br />
                    <asp:Panel ID="pnlPromotionLabel" runat="server" Visible="false">
                        Discount<br />
                    </asp:Panel>
                    Shipping<br />
                    <asp:Panel ID="pnlRushLabel" runat="server" Visible="false">
                        Rush S &amp; H<br />
                    </asp:Panel>
                     Tax<br />
                    <strong class="black">Total</strong></td>
                    
               <td class="bold f24" style="line-height: 29px; text-align: right; color: #979797;">
                    $<asp:Literal ID="LiteralSubTotal" runat="server"></asp:Literal><br />
                    <asp:Panel ID="pnlPromotionalAmount" runat="server" Visible="false">
                        <asp:Label runat="server" ID="lblPromotionPrice"></asp:Label><br />
                    </asp:Panel>
                    $<asp:Literal ID="LiteralShipping" runat="server"></asp:Literal><br />
                    <asp:Panel ID="pnlRush" runat="server" Visible="false">
                    </asp:Panel>
                    $<asp:Literal ID="LiteralTax" runat="server"></asp:Literal><br />
                   <strong class="black"> $<asp:Literal ID="LiteralTotal" runat="server"></asp:Literal></strong>
       </td></tr></table>
         
         
                   


  <uc:TrackingPixels ID="TrackingPixels" runat="server" />
