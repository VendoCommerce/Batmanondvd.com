<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VersionReport.aspx.cs" EnableEventValidation="false"
    Inherits="CSWeb.Admin.VersionReport" EnableViewState="true" EnableSessionState="true" %>

<%@ Register TagPrefix="usercontrols" TagName="RangeDateControl" Src="usercontrols/RangeDateControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>BatmanOnDvd.com - Version Report</title>
    <script type="text/javascript">
        function removejscssfile(filename, filetype) {
            var targetelement = (filetype == "js") ? "script" : (filetype == "css") ? "link" : "none" //determine element type to create nodelist from
            var targetattr = (filetype == "js") ? "src" : (filetype == "css") ? "href" : "none" //determine corresponding attribute to test for
            var allsuspects = document.getElementsByTagName(targetelement)
            for (var i = allsuspects.length; i >= 0; i--) { //search backwards within nodelist for matching elements to remove
                if (allsuspects[i] && allsuspects[i].getAttribute(targetattr) != null && allsuspects[i].getAttribute(targetattr).indexOf(filename) != -1)
                    allsuspects[i].parentNode.removeChild(allsuspects[i]) //remove element by calling parentNode.removeChild()
            }
        }

    </script>
    <link href="/Styles/midstyles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="outerwrap2">
        <div id="header2">
            <div id="logo">
                <img src="/Content/images/conversion_logo.jpg" width="238" height="62" />

            </div>
            <!-- end logo -->
           
            <!-- end log_link -->
            <div id="report_nav">
               <span class="current">Version Report</span> | <a href="sReports.aspx">
                    Standard Report</a>
                 | <a href="SIdReport.aspx">
                    SId Report</a>             
            </div>
            <div id="log_link">
                <a id="lnk_logout" href="Logout.aspx">Logout</a></div>
        </div>
        <!-- end header -->
        <div id="main2">
            <div id="date2">
                <p class="day">
                    <asp:Literal ID="liHeader" runat="server" /></p>
                <p class="time">
                    <asp:Literal ID="liSubHeader" runat="server" /></p>
            </div>
            <div id="left">
                <div id="client">
                    <h2>
                    </h2>
                    <div id="client_logo">
                        <asp:Image ID="imgLogo" style="max-width: 120px" runat="server"  /></div>
                </div>
                <div id="retrieve_activity">
                    <h2>
                    </h2>
                    <div id="activity_select">
                        <p>
                            Select range:</p>
                        <p>
                            <usercontrols:RangeDateControl ID="rangeDateControlCriteria" runat="server" DisplayDropDown="true" StartDateWidth="115" EndDateWidth="115"
                                LabelStartText="From:" LabelEndText="To:" PostbackFunction="btnSubmit" />
                        </p>
                        <div class="clear">
                        </div>
                        
                        <p>
                            &nbsp;
                        </p>
                        <p>
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" /></p>
                        <p>
                            &nbsp;
                        </p>
                    </div>
                </div>
            </div>
            <!-- end left -->
            <div id="right2">
                <h2>
                </h2>
                <!-- end summary header -->
                
                    <asp:DataList runat="server" ID="dlVersionCategoryList" RepeatLayout="Flow" RepeatDirection="Horizontal"
                        OnItemDataBound="dlVersionCategoryList_ItemDataBound" DataKeyField="CategoryId">
                        <HeaderTemplate>
                            <table class="summary_table2" width="720" cellpadding="0" cellspacing="0">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr  id="CategoryHeaderRow" runat="server">
                                <td class="CategoryHeader" colspan="7">
                                <table class="category_header_table" width="726" cellpadding="0" cellspacing="0">
                                <tr><td>
                                    <b>
                                        <asp:Label ID="lblCategory" runat="server" /></b>
                                       </td></tr></table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7" style="padding-top: 0">
                                <asp:DataList runat="server" ID="dlVersionItemList" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                    OnItemDataBound="dlVersionList_ItemDataBound">
                                    <HeaderTemplate>
                                    <div class="table_pad2">
                                        <table width="100%" border="0" cellspacing="1" cellpadding="2" class="summary_table2">
                                            <tr>
                                                <th width="71" class="cola">
                                                    Sites
                                                </th>
                                                <th width="100" class="colb">
                                                    Unique Visitors
                                                </th>
                                                <th width="84" class="colc">
                                                    Total Orders
                                                </th>
                                                <th width="98" class="cold">
                                                    Conversion %
                                                </th>
                                                <th width="135" class="cole">
                                                    Avg. Order Value
                                                </th>
                                                <th width="228" class="colf">
                                                    Total Revenue
                                                </th>
                                                <th width="228" class="colg">
                                                    Revenue Per Visitor
                                                </th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="cola border_right">
                                                <asp:Label ID="lblTitle" runat="server" />
                                            </td>
                                            <td class="colb border_right">
                                                <asp:Label ID="lbHitLinkVisitor" runat="server" />
                                            </td>
                                            <td class="colc border_right">
                                                <asp:Label ID="lblTotalOrder" runat="server" />
                                            </td>
                                            <td class="cold border_right">
                                                <asp:Label ID="lblConversion" runat="server" />
                                            </td>
                                            <td class="cole border_right">
                                                <asp:Label ID="lblAvgOrder" runat="server" />
                                            </td>
                                            <td class="colf border_right">
                                                <asp:Label ID="lblTotalRev" runat="server" />
                                            </td>
                                            <td class="colg border_right">
                                                <asp:Label ID="lblRevenuePerVisit" runat="server" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </table>
                                     <table width="720" cellspacing="0" cellpadding="0" class="total_table2 subtotal_version">
                                        <tr id="versionFooter" runat="server">
                                            <td class="cola">
                                                    <b>Total:</b>
                                            </td>
                                            <td class="colb">
                                                <b>
                                                    <asp:Label ID="lblSumHitLinkVisitor" runat="server" /><b>
                                            </td>
                                            <td class="colc">
                                                <b>
                                                    <asp:Label ID="lblSumTotalOrder" runat="server" /><b>
                                            </td>
                                            <td class="cold">
                                                <b>
                                                    <asp:Label ID="lblSumTotalConversion" runat="server" /><b>
                                            </td>
                                            <td class="cole">
                                                <b>
                                                    <asp:Label ID="lblSumAvgOrder" runat="server" /><b>
                                            </td>
                                            <td class="colf">
                                                <b>
                                                    <asp:Label ID="lblSumTotalRev" runat="server" /><b>
                                            </td>
                                            <td class="colg">
                                                <b>
                                                    <asp:Label ID="lblSumRevenuePerClick" runat="server" /><b>
                                            </td>
                                        </tr>
                                
                                            </table>
                                               </div>
                                    </FooterTemplate>
                                </asp:DataList>
                                    <td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                   
                       

                            <tr><td colspan="7">
                            <div class="table_pad2">
                            <table width="720" cellspacing="0" cellpadding="0" class="total_table2 final_total">
                            <tr>
                                <td class="cola">
                                    <b>Total:</b>
                                </td>
                                <td class="colb">
                                    <b>
                                        <asp:Label ID="lblTotalSumHitLinkVisitor" runat="server" /><b>
                                </td>
                                <td class="colc">
                                    <b>
                                        <asp:Label ID="lblTotalSumTotalOrder" runat="server" /><b>
                                </td>
                                <td class="cold">
                                    <b>
                                        <asp:Label ID="lblTotalSumTotalConversion" runat="server" /><b>
                                </td>
                                <td class="cole">
                                    <b>
                                        <asp:Label ID="lblTotalSumAvgOrder" runat="server" /><b>
                                </td>
                                <td class="colf">
                                    <b>
                                        <asp:Label ID="lblTotalSumTotalRev" runat="server" /><b>
                                </td>
                                <td class="colg">
                                    <b>
                                        <asp:Label ID="lblTotalSumRevenuePerClick" runat="server" /><b>
                                </td>
                            </tr>
                            </table></div></td></tr></table>
                        </FooterTemplate>
                    </asp:DataList>
             
             
                   <div class="clear">
            </div>
        </div>
            <div class="clear">
            </div>
    </div>
    <!-- end outerwrap -->
     </form>
</body>
</html>
