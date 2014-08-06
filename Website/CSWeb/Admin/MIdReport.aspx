<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MIdReport.aspx.cs" Inherits="CSWeb.Admin.MIdReport" EnableEventValidation="false"
    EnableViewState="true" EnableSessionState="true" %>

<%@ Register TagPrefix="usercontrols" TagName="RangeDateControl" Src="~/usercontrols/RangeDateControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=siteName %> - MID Report</title>
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
                <a href="VersionReport.aspx">Version Report </a>| <a href="Reports.aspx">Standard
                    Report</a> | <span class="current">MID Report</span> 
            </div>
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
                            <asp:Image ID="imgLogo" runat="server"  /></div>
                </div>
                <div id="retrieve_activity">
                    <h2>
                    </h2>
                    <div id="activity_select">
                        <p>
                            Select range:</p>
                        <p>
                            <usercontrols:RangeDateControl ID="rangeDateControlCriteria" LabelStyle="FieldName"
                                runat="server" DisplayDropDown="true" StartDateWidth="115" EndDateWidth="115"
                                LabelStartText="From" LabelEndText="To" PostbackFunction="btnSubmit" />
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
                <div class="table_pad2">
                    <asp:DataList runat="server" ID="dlVersionList" RepeatLayout="Flow" RepeatDirection="Horizontal"
                        OnItemDataBound="dlVersionList_ItemDataBound">
                        <HeaderTemplate>
                            <table class="summary_table2" width="720" cellpadding="0" cellspacing="0">
                                <tr>
                                    <th class="cola">
                                        MID
                                    </th>
                                    <th class="colb">
                                        Unique Visitors
                                    </th>
                                    <th class="colc">
                                        Total Orders
                                    </th>
                                    <th class="cold">
                                        Conversion %
                                    </th>
                                    <th class="colf">
                                        Total Revenue
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="cola">
                                    <asp:Label ID="lblTitle" runat="server" />
                                </td>
                                <td class="colb">
                                    <asp:Label ID="lbHitLinkVisitor" runat="server" />
                                </td>
                                <td class="colc">
                                    <asp:Label ID="lblTotalOrder" runat="server" />
                                </td>
                                <td class="cold">
                                    <asp:Label ID="lblConversion" runat="server" />
                                </td>
                                <td class="colf">
                                    <asp:Label ID="lblTotalRev" runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr class="total_table2">
                                <td class="cola">
                                    <b>Total</b>
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
                                <td class="colf">
                                    <b>
                                        <asp:Label ID="lblSumTotalRev" runat="server" /><b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    &#160;
                                </td>
                            </tr>
                            </table>
                        </FooterTemplate>
                    </asp:DataList>
                    <!-- end summary table -->
                </div>
                <!-- end table_pad -->
                <!-- end right -->
                <div class="clear">
                </div>
            </div>
            <!-- end main -->
            <div class="clear">
            </div>
        </div>
        <!-- end outerwrap -->
    </form>
</body>
</html>
