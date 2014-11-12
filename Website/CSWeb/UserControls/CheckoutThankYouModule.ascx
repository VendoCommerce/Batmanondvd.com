<%@ Control Language="C#" Inherits="CSWeb.Root.UserControls.CheckoutThankYouModule" %>

<script language="javascript">
function Clickheretoprint()
{ 
  var disp_setting="toolbar=yes,location=no,directories=yes,menubar=yes,"; 
      disp_setting+="scrollbars=yes,width=920, height=600, left=100, top=25"; 
  var content_vlue = document.getElementById("receipt_content").innerHTML; 
  
  var docprint=window.open("","",disp_setting); 
   docprint.document.open(); 
   docprint.document.write('<html><head><title>Receipt</title>');
   docprint.document.write('<link href="styles/global.css" rel="stylesheet" type="text/css" media="all" /><link href="/styles/global_print.css" rel="stylesheet" type="text/css" media="all" />'); 
   docprint.document.write('</head><body onLoad="self.print()">');     
   docprint.document.write('<h1>Receipt</h1>');       
   docprint.document.write(content_vlue);          
   docprint.document.write('</body></html>'); 
   docprint.document.close(); 
   docprint.focus(); 
}
</script>
<div id="receipt_content">

    <div class="printfriendly fright">
            <a href="javascript:Clickheretoprint()" class="black">
            <i class="icon-print"></i> <span class="scored">Printer Friendly Version</span></a>
  </div>

<p class="lh f38 black webfont1bold">Your BAT-TASTIC order has been placed!</p>
<p class="webfont1 f22" style="padding-bottom: 30px;"><strong>Thank You and Enjoy Your Purchase!</strong> Your order number is <asp:Literal ID="ltOrderId" runat="server"></asp:Literal>.  <br />
    You will receive a shipping confirmation once your order has shipped.</p>

                <div class="clear"></div>

 <table width="100%" border="0" cellspacing="0" cellpadding="0" id="receipt_table1">
<tr class="horzline1">
<td valign="top" class="pad0 black" style="width: 270px;">
                    <strong>&nbsp;</strong>
                </td>
                <td valign="top" class="pad0 black" style="width: 380px;">
                   &nbsp;
                </td>
                <td valign="top" class="pad0 text-center black">
                    Quantity
                </td>
                <td valign="top" class="pad0 black" style="width: 268px;">
                    <div style="padding-left: 15px;">Price</div>
                </td>
            </tr>
              <asp:DataList runat="server" ID="dlordersList" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <ItemTemplate>
                        <tr>
                             <td valign="top" align="right">
                                <div style="margin-right: 10px;"><img src="<%# ImageURL(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SkuId"))) %>" /></div>
                            </td>
                            <td valign="top" style="font-size: 12px; line-height: 16px;">
                                <%# DataBinder.Eval(Container.DataItem, "LongDescription")%>
                            </td>
                             <td valign="top" class="black text-center">
                                <%# DataBinder.Eval(Container.DataItem, "Quantity")%>
                            </td>
                           <td valign="top" class="black" style="padding-right: 190px; text-align: right;">
                                $<%# Math.Round(Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "TotalPrice")), 2).ToString()%></td>
                           
                        </tr>
                    </ItemTemplate>
                </asp:DataList>

            <asp:Literal ID="LiteralTableRows" runat="server"></asp:Literal>
          <tr class="horzline2">
                <td valign="top" colspan="4" class="black">

             <div class="cart_totals clearfix" style="margin-left: 506px;">
                    
                <div class="cart_totals_left">
                  <strong>Subtotal:</strong><br />
                  Shipping:<br />
                     <asp:Panel ID="pnlRushLabel" runat="server" Visible="false">
                        Rush S &amp; H:<br />
                    </asp:Panel>
                  Tax:<br />
                  <asp:Panel ID="pnlPromotionLabel" runat="server" Visible="false">
                    Discount:<br />
                  </asp:Panel>
                </div>
                    
                <div class="cart_totals_right">
                        $<asp:Literal ID="LiteralSubTotal" runat="server"></asp:Literal><br />
                    $<asp:Literal ID="LiteralShipping" runat="server"></asp:Literal><br />
                    <asp:Panel ID="pnlRush" runat="server" Visible="false">
                    $<asp:Literal ID="LiteralRushShipping" runat="server"></asp:Literal><br />
                    </asp:Panel>
                    $<asp:Literal ID="LiteralTax" runat="server"></asp:Literal><br />
            <asp:Panel ID="pnlPromotionalAmount" runat="server" Visible="false">
                <asp:Label runat="server" ID="lblPromotionPrice"></asp:Label><br />
            </asp:Panel>
                    </div>


                 <div class="clear"></div>
            <div class="horizontal_dots" style="width: 240px; left: -8px;"></div>
            <div class="cart_totals_left f16"><strong>Today's Payment:</strong></div>
            <div class="cart_totals_right f16">$<asp:Literal ID="LiteralTotal" runat="server"></asp:Literal></div>
            
                    
                </td>
            </tr>
        </table>



        <table border="0" cellspacing="0" cellpadding="0" id="receipt_table2">
            <tr>
                <td colspan="2" valign="top" class="caps f12 pad0" style="padding-left: 46px;">
                    <strong>Shipping Information</strong>
                </td>
                <td colspan="2" valign="top" class="caps  f12 pad0" style="padding-left: 0;">
                    <strong>Billing Information</strong>
                </td>
            </tr>
            <tr>
                <td width="190" valign="top" class="f12" style="padding-top: 10px; padding-right: 10px; padding-left: 46px;">
                    Name:
                    <br />
                    Address:
                    <br />
                    Address 2:<br />
                    City:
                    <br />
                    State:
                    <br />
                    Zip Code:
                    <br />
                    Email Address:
                </td>
                <td width="210" valign="top" class="f12" style="padding-top: 10px; padding-left: 0;">
                    <strong><asp:Literal ID="LiteralName" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralAddress" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralAddress2" runat="server"></asp:Literal>
                    <br />
                    <asp:Literal ID="LiteralCity" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralState" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralZip" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralEmail" runat="server"></asp:Literal></strong>
                </td>
                <td width="98" valign="top" class="f12" style="padding-top: 10px; padding-left: 0;">
                    Name:
                    <br />
                    Address:
                    <br />
                    Address 2:<br />
                    City:
                    <br />
                    State:
                    <br />
                    Zip Code:</td>
                <td width="196" valign="top" class="f12" style="padding-top: 10px; padding-left: 0;">
                    <strong><asp:Literal ID="LiteralName_b" runat="server">
                    </asp:Literal><br />
                    <asp:Literal ID="LiteralAddress_b" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralAddress2_b" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralCity_b" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralState_b" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralZip_b" runat="server"></asp:Literal></strong>
                </td>
            </tr>
        </table>
                    
    <div class="receipt_offerterms">
        <asp:Literal runat="server" ID="ltOfferTerms" Visible="False"></asp:Literal>
    </div>
      
</div>
