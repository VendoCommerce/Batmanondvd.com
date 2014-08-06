<%@Control Language="C#" Inherits="CSWeb.Mobile.UserControls.CheckoutThankYouModule" %>
<%@ Register Src="Header.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %>

<uc:Header ID="Header_Cart" runat="server" />

<div id="stretch_container">
<div id="receipt_content"  style="height: auto; width: 875px; position:relative;padding: 30px 60px 0 60px;color: #5c5c5c;">

<p style="text-align:right"><a href="receipt_friendly.aspx" style="color: #000">Print order for your records <img src="/Content/Images/print_icon.jpg" width="32" height="24" style="vertical-align:top" /></a></p>
<p style="line-height: normal; padding-bottom: 10px; font-size: 30px; color: #000; margin: 0; color: #000;">Thank you <asp:Label ID="lblPurchaseName" runat="server" />!</p>

<p style="text-align:right"><img src="/Content/Images/receipt_logo.jpg" /></p>





    <table width="819" border="0" cellspacing="0" cellpadding="0" id="receipt_table1">
<tr><td class="horizontal_dots2" colspan="3"></td></tr>
<tr>

                <td width="76%" valign="top" style="padding-bottom: 20px">
                    <strong>Description</strong>
                </td>
                <td width="12%" valign="top" align="center">
                    <strong>Quantity</strong>
                </td>
                <td width="12%" valign="top">
                    <strong>Total</strong>
                </td>
            </tr>
              <asp:DataList runat="server" ID="dlordersList" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <ItemTemplate>
                        <tr>
                             <td width="76%" valign="top" style="padding-bottom: 20px">
                                <%# DataBinder.Eval(Container.DataItem, "LongDescription")%>
                            </td>
                            <td width="12%" valign="top" align="center">
                                <%# DataBinder.Eval(Container.DataItem, "Quantity")%>
                            </td>
                        
                             <td width="12%" valign="top">
                                $<%# Math.Round(Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "TotalPrice")), 2).ToString()%>
                            </td>
                           
                        </tr>
                    </ItemTemplate>
                </asp:DataList>

           
            <asp:Literal ID="LiteralTableRows" runat="server"></asp:Literal>
            <tr><td class="horizontal_dots2" colspan="3"></td></tr>
            <tr>
                <td valign="top">&nbsp;
                    
                </td>
                <td valign="top">
                    Subtotal:<br />
                    S &amp; H:
                    <br />
                     <asp:Panel ID="pnlRushLabel" runat="server" Visible="false">
                        Rush S &amp; H:<br />
                    </asp:Panel>
                    Tax:
                    <br />
            <asp:Panel ID="pnlPromotionLabel" runat="server" Visible="false">
                Discount:<br />
            </asp:Panel>
                    Total:
                </td>
                <td valign="top">
                    $<asp:Literal ID="LiteralSubTotal" runat="server"></asp:Literal><br />
                    $<asp:Literal ID="LiteralShipping" runat="server"></asp:Literal><br />
                    <asp:Panel ID="pnlRush" runat="server" Visible="false">
                    $<asp:Literal ID="LiteralRushShipping" runat="server"></asp:Literal><br />
                    </asp:Panel>
                    $<asp:Literal ID="LiteralTax" runat="server"></asp:Literal><br />
            <asp:Panel ID="pnlPromotionalAmount" runat="server" Visible="false">
                <asp:Label runat="server" ID="lblPromotionPrice"></asp:Label><br />
            </asp:Panel>
                    $<asp:Literal ID="LiteralTotal" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr><td class="horizontal_dots2" colspan="3"></td></tr>
        </table>
        <table width="819" border="0" cellspacing="0" cellpadding="0" id="receipt_table2">
            <tr>
                <td colspan="2" valign="top">
                    <strong>Shipping Information:</strong>
                </td>
                <td colspan="2" valign="top">
                    <strong>Billing Information:</strong>
                </td>
            </tr>
            <tr>
                <td width="158" valign="top">
                    Name:
                    <br />
                    Address:
                    <br />
                    Address 2:
                    <br />
                    City:
                    <br />
                    State:
                    <br />
                    Zip Code:
                    <br />
                    Country:
                    <br />
                    Email Address:
                </td>
                <td width="206" valign="top">
                    <asp:Literal ID="LiteralName" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralAddress" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralAddress2" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralCity" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralState" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralZip" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralCountry" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralEmail" runat="server"></asp:Literal><br />
                </td>
                <td width="189" valign="top">
                    Name:
                    <br />
                    Address:
                    <br />
                    Address 2:
                    <br />
                    City:
                    <br />
                    State:
                    <br />
                    Zip Code:<br />
                    Country:
                </td>
                <td width="266" valign="top">
                    <asp:Literal ID="LiteralName_b" runat="server">
                    </asp:Literal><br />
                    <asp:Literal ID="LiteralAddress_b" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralAddress2_b" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralCity_b" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralState_b" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralZip_b" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralCountry_b" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr><td class="horizontal_dots2" colspan="4"></td></tr>
        </table>
        
        <p style="padding-top: 20px">You have a 30-day Money-Back guarantee for OPENED and UNOPENED bottles. If you are not completely satisfied you can return your order within 30 days from the ship date and you will receive a full refund of the purchase price, less shipping and handling.</p>
        <p> To return a product for an exchange or refund, please obtain a Return Merchandise Authorization (RMA) number by contacting Customer Service at: Navarie Healthcare using the contact information below. You are responsible for return postage fees. </p>
<p><strong>Customer Service</strong><br />
          Phone: 1-877-355-6637 (Monday through Friday 5AM - 5PM EST)<br />
      Email: <a href="mailto:cyclevitamins@webcsr.info">cyclevitamins@webcsr.info</a></p>
<p><strong>Please Note: </strong><br />
      We cannot process or refund packages marked &quot;Return to Sender.&quot; Refunds will be issued to the same credit card that was charged when ordering the product. To ensure a refund is processed, please send returns to the address provided along with your RMA number. The RMA number must be clearly written on the package that you are sending back. Our shipping department can not accept any packages without an RMA number.</p>
<p> <strong>Please send returned products to the following address: </strong><br />
  Navarie Healthcare<br />
  7850 Ruffner Avenue<br />
  Dept. 4000<br />
  Van Nuys, CA 91406</p>
<p> We recommend all returned items are sent using a delivery confirmation system to ensure proper delivery. After the shipping department receives your return, it generally takes approximately two business days to process your refund. Once a return is processed, it usually takes 3-5 business days for the return to be posted to your account, depending on your financial institution.</p>
</div>
</div>

<div id="footer">
<div id="footertext">
<p>Copyright &copy; 2012. All rights reserved.</p>
</div>
</div>
<!-- end footer -->

<uc:TrackingPixels ID="TrackingPixels" runat="server" />
