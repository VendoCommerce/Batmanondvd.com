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
<div id="receipt_content"  style="margin: 0 auto; height: auto; width: 850px; position:relative;padding: 30px 60px;color: #000;">

<p class="lh f34 black pad20 bold webfont1">Thank you for your purchase!</p>


  <div class="printfriendly fright">
            <a href="javascript:Clickheretoprint()" class="gray">
                <i class="icon-print"></i> <span class="scored">Printer Friendly Version</span></a></div>
                <div class="clear"></div>

 <table width="100%" border="0" cellspacing="0" cellpadding="0" id="receipt_table1">
<tr class="horzline1">
<td valign="top" class="pad0 black">
                    <strong>Item</strong>
                </td>
                <td valign="top" class="pad0 black">
                   
                </td>
                <td valign="top" class="pad0 text-center black">
                    <strong>Quantity</strong>
                </td>
                <td valign="top" class="pad0 black">
                    <strong>Total</strong>
                </td>
            </tr>
              <asp:DataList runat="server" ID="dlordersList" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <ItemTemplate>
                        <tr> <td valign="top">
                                <img src="<%# DataBinder.Eval(Container.DataItem, "ImagePath")%>" />
                            </td>
                             <td valign="top">
                            

                                <%# DataBinder.Eval(Container.DataItem, "LongDescription")%>
                            </td>
                            <td valign="top" class="black bold text-center">
                                <%# DataBinder.Eval(Container.DataItem, "Quantity")%>
                            </td>
                        
                             <td valign="top" class="black bold">
                                $<%# Math.Round(Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "TotalPrice")), 2).ToString()%>
                            </td>
                           
                        </tr>
                    </ItemTemplate>
                </asp:DataList>

           
            <asp:Literal ID="LiteralTableRows" runat="server"></asp:Literal>
          <tr class="horzline2">
                <td valign="top">&nbsp;
                    
                </td> <td valign="top">&nbsp;
                    
                </td>
                <td valign="top" class="black bold">                    
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
                <td valign="top" class="black bold">
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
        </table>
        <table border="0" cellspacing="0" cellpadding="0" id="receipt_table2">
            <tr>
                <td colspan="2" valign="top" class="caps gray f16 pad0">
                    <strong>Shipping Information:</strong>
                </td>
                <td colspan="2" valign="top" class="caps gray f16 pad0">
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
                    <strong><asp:Literal ID="LiteralName" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralAddress" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralAddress2" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralCity" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralState" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralZip" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralCountry" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralEmail" runat="server"></asp:Literal></strong>
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
                    <strong><asp:Literal ID="LiteralName_b" runat="server">
                    </asp:Literal><br />
                    <asp:Literal ID="LiteralAddress_b" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralAddress2_b" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralCity_b" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralState_b" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralZip_b" runat="server"></asp:Literal><br />
                    <asp:Literal ID="LiteralCountry_b" runat="server"></asp:Literal></strong>
                </td>
            </tr>
        </table>
      
</div>