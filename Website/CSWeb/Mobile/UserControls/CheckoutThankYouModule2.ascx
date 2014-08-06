<%@Control Language="C#" Inherits="CSWeb.Mobile.UserControls.CheckoutThankYouModule2" %>
<%@ Register Src="../UserControls/TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %>

<p class="red bold f32 caps" style="padding-top: 30px">
          Thank you for ordering!</p>
       
         
<p> Your order number is
                <%=orderId.ToString()%>, and an email confirmation will be sent to 
<%=LiteralEmail.Text%>.</p>
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
             <tr><td>Zip/Postal Code:</td>
             <td><asp:Literal ID="LiteralZip_b" runat="server"></asp:Literal></td></tr>
             <tr><td>Country:</td>
             <td><asp:Literal ID="LiteralCountry_b" runat="server"></asp:Literal></td></tr>
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
             <tr><td>Zip/Postal Code:</td>
             <td><asp:Literal ID="LiteralZip" runat="server"></asp:Literal></td></tr>
             <tr><td>Country:</td>
             <td><asp:Literal ID="LiteralCountry" runat="server"></asp:Literal></td></tr>
      </table>
          
              <table class="receipt_table">
                    <tr>
                    <th>Description</th>
                    <th class="text-center">Qty</th>
                  <th class="text-center">1st Payment</th>
                  <th class="text-center">S&amp;H</th>
                    <th class="text-center">Total</th>
     </tr>
          
                <asp:DataList runat="server" ID="dlordersList" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <ItemTemplate>
                        <tr>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "LongDescription")%>
                        </td>
                        <td class="text-center bold">
                            <%# DataBinder.Eval(Container.DataItem, "Quantity")%>
                       </td>
                           <td class="text-center bold">
                           $<%# Math.Round(Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "TotalPrice")), 2).ToString()%> 
                       </td>
                     <td class="text-center bold">
                            $<%# Math.Round(CalculateSkuBaseShipping(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "skuid"))), 2).ToString()%>
                       </td>
                       <td class="text-center bold">
                           $<%# Math.Round(CalculateSkuBaseShipping(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "skuid"))) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "TotalPrice")), 2).ToString()%>
                       </td>
                       </tr>
                           
                    </ItemTemplate>
                </asp:DataList>
                <asp:Literal ID="LiteralTableRows" runat="server"></asp:Literal>
          <tr><td colspan="4" style="text-align:right; padding-right: 5px;">
                    SUBTOTAL:<br />
                    <asp:Panel ID="pnlPromotionLabel" runat="server" Visible="false">
                        Discount:<br />
                    </asp:Panel>
                    SHIPPING:<br />
                    <asp:Panel ID="pnlRushLabel" runat="server" Visible="false">
                        Rush S &amp; H:<br />
                    </asp:Panel>
                    ESTIMATED TAX:<br />
                    <strong>TOTAL:</strong></td>
                    
               <td>
                    <strong>$<asp:Literal ID="LiteralSubTotal" runat="server"></asp:Literal></strong><br />
                    <asp:Panel ID="pnlPromotionalAmount" runat="server" Visible="false">
                        <asp:Label runat="server" ID="lblPromotionPrice"></asp:Label><br />
                    </asp:Panel>
                    $<asp:Literal ID="LiteralShipping" runat="server"></asp:Literal><br />
                    <asp:Panel ID="pnlRush" runat="server" Visible="false">
                    </asp:Panel>
                    $<asp:Literal ID="LiteralTax" runat="server"></asp:Literal><br />
                   <strong> $<asp:Literal ID="LiteralTotal" runat="server"></asp:Literal></strong>
       </td></tr></table>
         
         
                   


  <uc:TrackingPixels ID="TrackingPixels" runat="server" />
